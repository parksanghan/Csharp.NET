using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataGenerator.Services
{
    public sealed class ProjectLocator
    {
        private static readonly Regex SolutionProjectPattern =
            new Regex(
                "^Project\\(\"[^\"]+\"\\)\\s*=\\s*\"(?<name>[^\"]+)\",\\s*\"(?<path>[^\"]+\\.csproj)\"",
                RegexOptions.Compiled |
                RegexOptions.CultureInvariant |
                RegexOptions.IgnoreCase);

        public IReadOnlyList<ProjectCandidate> FindProjects(
            string solutionOrProjectPath)
        {
            if (string.IsNullOrWhiteSpace(solutionOrProjectPath))
            {
                throw new ArgumentException(
                    "솔루션 또는 프로젝트 경로를 입력해야 합니다.",
                    nameof(solutionOrProjectPath));
            }

            string fullPath =
                Path.GetFullPath(solutionOrProjectPath);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException(
                    "솔루션 또는 프로젝트 파일을 찾을 수 없습니다.",
                    fullPath);
            }

            string extension =
                Path.GetExtension(fullPath);

            if (extension.Equals(
                    ".csproj",
                    StringComparison.OrdinalIgnoreCase))
            {
                return new[]
                {
                    new ProjectCandidate
                    {
                        Name =
                            Path.GetFileNameWithoutExtension(fullPath),
                        ProjectPath = fullPath
                    }
                };
            }

            if (!extension.Equals(
                    ".sln",
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidDataException(
                    ".sln 또는 .csproj 파일만 선택할 수 있습니다.");
            }

            string solutionDirectory =
                Path.GetDirectoryName(fullPath)
                ?? throw new InvalidDataException(
                    "솔루션 디렉터리를 확인할 수 없습니다.");

            var projects = new List<ProjectCandidate>();

            foreach (string line in File.ReadLines(fullPath))
            {
                Match match = SolutionProjectPattern.Match(line);
                if (!match.Success)
                {
                    continue;
                }

                string relativePath =
                    match.Groups["path"].Value
                        .Replace(
                            '\\',
                            Path.DirectorySeparatorChar)
                        .Replace(
                            '/',
                            Path.DirectorySeparatorChar);

                string projectPath =
                    Path.GetFullPath(
                        Path.Combine(
                            solutionDirectory,
                            relativePath));

                if (!File.Exists(projectPath))
                {
                    continue;
                }

                projects.Add(
                    new ProjectCandidate
                    {
                        Name = match.Groups["name"].Value,
                        ProjectPath = projectPath
                    });
            }

            List<ProjectCandidate> result = projects
                .GroupBy(
                    project => project.ProjectPath,
                    StringComparer.OrdinalIgnoreCase)
                .Select(group => group.First())
                .OrderBy(
                    project => project.Name,
                    StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (result.Count == 0)
            {
                throw new InvalidDataException(
                    "솔루션에서 C# 프로젝트(.csproj)를 찾지 못했습니다.");
            }

            return result;
        }
    }
}
