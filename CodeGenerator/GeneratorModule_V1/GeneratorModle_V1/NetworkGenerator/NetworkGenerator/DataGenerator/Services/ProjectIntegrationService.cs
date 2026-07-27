using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DataGenerator.Services
{
    public sealed class ProjectIntegrationService
    {
        private static readonly UTF8Encoding Utf8WithoutBom =
            new UTF8Encoding(false);

        public ProjectGenerationResult Apply(
            string projectPath,
            string runtimeLibraryPath,
            IReadOnlyList<GeneratedSource> sources)
        {
            if (string.IsNullOrWhiteSpace(projectPath))
            {
                throw new ArgumentException(
                    "대상 프로젝트 경로가 필요합니다.",
                    nameof(projectPath));
            }

            if (string.IsNullOrWhiteSpace(runtimeLibraryPath))
            {
                throw new ArgumentException(
                    "Runtime DLL 경로가 필요합니다.",
                    nameof(runtimeLibraryPath));
            }

            string fullProjectPath = Path.GetFullPath(projectPath);
            string fullLibraryPath =
                Path.GetFullPath(runtimeLibraryPath);

            if (!File.Exists(fullProjectPath))
            {
                throw new FileNotFoundException(
                    "대상 프로젝트 파일을 찾을 수 없습니다.",
                    fullProjectPath);
            }

            if (!File.Exists(fullLibraryPath))
            {
                throw new FileNotFoundException(
                    "DataGenerator에 포함된 NetworkGenerator.dll을 찾을 수 없습니다.",
                    fullLibraryPath);
            }

            string projectDirectory =
                Path.GetDirectoryName(fullProjectPath)
                ?? throw new InvalidDataException(
                    "대상 프로젝트 디렉터리를 확인할 수 없습니다.");

            var result = new ProjectGenerationResult
            {
                ProjectPath = fullProjectPath
            };

            string targetLibraryDirectory =
                Path.Combine(projectDirectory, "Lib");

            Directory.CreateDirectory(targetLibraryDirectory);

            string targetLibraryPath =
                Path.Combine(
                    targetLibraryDirectory,
                    "NetworkGenerator.dll");

            result.LibraryCopied =
                CopyIfChanged(
                    fullLibraryPath,
                    targetLibraryPath);

            result.LibraryPath = targetLibraryPath;

            foreach (GeneratedSource source in sources)
            {
                string targetPath = GetSafeTargetPath(
                    projectDirectory,
                    source.RelativePath);

                Directory.CreateDirectory(
                    Path.GetDirectoryName(targetPath)!);

                FileWriteState state =
                    WriteTextIfChanged(
                        targetPath,
                        source.Content);

                if (state == FileWriteState.Created)
                {
                    result.CreatedFileCount++;
                }
                else if (state == FileWriteState.Updated)
                {
                    result.UpdatedFileCount++;
                }
                else
                {
                    result.UnchangedFileCount++;
                }

                result.GeneratedFiles.Add(targetPath);
            }

            result.ProjectFileChanged =
                EnsureAssemblyReference(
                    fullProjectPath,
                    sources);

            return result;
        }

        private static bool EnsureAssemblyReference(
            string projectPath,
            IReadOnlyList<GeneratedSource> sources)
        {
            XDocument project = XDocument.Load(projectPath);
            XElement root = project.Root
                ?? throw new InvalidDataException(
                    "대상 .csproj의 루트 요소가 없습니다.");

            XNamespace xmlNamespace = root.Name.Namespace;
            bool changed = false;

            XElement? reference = root
                .Descendants()
                .FirstOrDefault(
                    element =>
                        element.Name.LocalName == "Reference" &&
                        string.Equals(
                            (string?)element.Attribute("Include"),
                            "NetworkGenerator",
                            StringComparison.OrdinalIgnoreCase));

            if (reference == null)
            {
                XElement? itemGroup = root
                    .Elements()
                    .FirstOrDefault(
                        element =>
                            element.Name.LocalName == "ItemGroup" &&
                            element.Elements().Any(
                                child =>
                                    child.Name.LocalName == "Reference"));

                if (itemGroup == null)
                {
                    itemGroup = new XElement(
                        xmlNamespace + "ItemGroup");
                    root.Add(itemGroup);
                }

                reference = new XElement(
                    xmlNamespace + "Reference",
                    new XAttribute(
                        "Include",
                        "NetworkGenerator"));

                itemGroup.Add(reference);
                changed = true;
            }

            changed |= SetChildValue(
                reference,
                xmlNamespace + "HintPath",
                @"Lib\NetworkGenerator.dll");

            changed |= SetChildValue(
                reference,
                xmlNamespace + "Private",
                "true");

            bool isSdkStyle =
                root.Attribute("Sdk") != null ||
                root.Elements()
                    .Any(
                        element =>
                            element.Name.LocalName == "Sdk");

            if (!isSdkStyle)
            {
                changed |= EnsureCompileItems(
                    root,
                    xmlNamespace,
                    sources);
            }

            if (!changed)
            {
                return false;
            }

            string content =
                project.Declaration == null
                    ? project.ToString()
                    : project.Declaration +
                      Environment.NewLine +
                      project.ToString();

            WriteTextAtomically(projectPath, content);
            return true;
        }

        private static bool EnsureCompileItems(
            XElement root,
            XNamespace xmlNamespace,
            IReadOnlyList<GeneratedSource> sources)
        {
            XElement? itemGroup = root
                .Elements()
                .FirstOrDefault(
                    element =>
                        element.Name.LocalName == "ItemGroup" &&
                        element.Elements().Any(
                            child =>
                                child.Name.LocalName == "Compile"));

            if (itemGroup == null)
            {
                itemGroup = new XElement(
                    xmlNamespace + "ItemGroup");
                root.Add(itemGroup);
            }

            bool changed = false;
            foreach (GeneratedSource source in sources)
            {
                string include =
                    source.RelativePath.Replace(
                        Path.DirectorySeparatorChar,
                        '\\');

                bool exists = root
                    .Descendants()
                    .Any(
                        element =>
                            element.Name.LocalName == "Compile" &&
                            string.Equals(
                                (string?)element.Attribute("Include"),
                                include,
                                StringComparison.OrdinalIgnoreCase));

                if (exists)
                {
                    continue;
                }

                itemGroup.Add(
                    new XElement(
                        xmlNamespace + "Compile",
                        new XAttribute("Include", include)));

                changed = true;
            }

            return changed;
        }

        private static bool SetChildValue(
            XElement parent,
            XName childName,
            string value)
        {
            XElement? child = parent.Element(childName);
            if (child == null)
            {
                parent.Add(new XElement(childName, value));
                return true;
            }

            if (string.Equals(
                    child.Value,
                    value,
                    StringComparison.Ordinal))
            {
                return false;
            }

            child.Value = value;
            return true;
        }

        private static string GetSafeTargetPath(
            string projectDirectory,
            string relativePath)
        {
            string fullPath = Path.GetFullPath(
                Path.Combine(
                    projectDirectory,
                    relativePath));

            string root =
                projectDirectory.TrimEnd(
                    Path.DirectorySeparatorChar,
                    Path.AltDirectorySeparatorChar) +
                Path.DirectorySeparatorChar;

            if (!fullPath.StartsWith(
                    root,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "생성 파일 경로가 프로젝트 밖을 가리킵니다: " +
                    relativePath);
            }

            return fullPath;
        }

        private static bool CopyIfChanged(
            string sourcePath,
            string targetPath)
        {
            if (File.Exists(targetPath))
            {
                byte[] sourceBytes =
                    File.ReadAllBytes(sourcePath);
                byte[] targetBytes =
                    File.ReadAllBytes(targetPath);

                if (sourceBytes.SequenceEqual(targetBytes))
                {
                    return false;
                }
            }

            File.Copy(sourcePath, targetPath, true);
            return true;
        }

        private static FileWriteState WriteTextIfChanged(
            string path,
            string content)
        {
            if (!File.Exists(path))
            {
                WriteTextAtomically(path, content);
                return FileWriteState.Created;
            }

            string current =
                File.ReadAllText(path, Encoding.UTF8);

            if (string.Equals(
                    current,
                    content,
                    StringComparison.Ordinal))
            {
                return FileWriteState.Unchanged;
            }

            WriteTextAtomically(path, content);
            return FileWriteState.Updated;
        }

        private static void WriteTextAtomically(
            string path,
            string content)
        {
            string directory =
                Path.GetDirectoryName(path)
                ?? throw new InvalidOperationException(
                    "파일 디렉터리를 확인할 수 없습니다.");

            Directory.CreateDirectory(directory);

            string temporaryPath =
                Path.Combine(
                    directory,
                    "." +
                    Path.GetFileName(path) +
                    "." +
                    Guid.NewGuid().ToString("N") +
                    ".tmp");

            try
            {
                File.WriteAllText(
                    temporaryPath,
                    content,
                    Utf8WithoutBom);

                File.Move(temporaryPath, path, true);
            }
            finally
            {
                if (File.Exists(temporaryPath))
                {
                    File.Delete(temporaryPath);
                }
            }
        }

        private enum FileWriteState
        {
            Created,
            Updated,
            Unchanged
        }
    }
}
