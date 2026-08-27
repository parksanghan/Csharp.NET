using System.Collections.Generic;
using System.Linq;

namespace DataGenerator.Models
{
    public sealed class PacketDefinitionDocument
    {
        public string SourcePath { get; set; } = string.Empty;

        public List<PacketDefinition> Packets { get; } =
            new List<PacketDefinition>();

        public int TotalFieldCount
        {
            get { return Packets.Sum(packet => packet.Fields.Count); }
        }
    }

    public sealed class PacketDefinition
    {
        public string Name { get; set; } = string.Empty;

        public string MessageId { get; set; } = string.Empty;

        public int MessageIdValue { get; set; }

        public ushort Sync { get; set; }

        public string DataType { get; set; } = string.Empty;

        public List<PacketFieldDefinition> Fields { get; } =
            new List<PacketFieldDefinition>();

        public int FieldCount
        {
            get { return Fields.Count; }
        }
    }

    public sealed class PacketFieldDefinition
    {
        public string Name { get; set; } = string.Empty;

        public int Order { get; set; }

        public string TypeName { get; set; } = string.Empty;

        public double Resolution { get; set; }

        public double Minimum { get; set; }

        public double Maximum { get; set; }
    }

    public sealed class GeneratedSource
    {
        public GeneratedSource(string relativePath, string content)
        {
            RelativePath = relativePath;
            Content = content;
        }

        public string RelativePath { get; }

        public string Content { get; }
    }

    public sealed class ProjectCandidate
    {
        public string Name { get; set; } = string.Empty;

        public string ProjectPath { get; set; } = string.Empty;

        public string DisplayText
        {
            get { return Name + "  —  " + ProjectPath; }
        }
    }

    public sealed class ProjectGenerationResult
    {
        public string ProjectPath { get; set; } = string.Empty;

        public string LibraryPath { get; set; } = string.Empty;

        public bool LibraryCopied { get; set; }

        public bool ProjectFileChanged { get; set; }

        public int CreatedFileCount { get; set; }

        public int UpdatedFileCount { get; set; }

        public int UnchangedFileCount { get; set; }

        public List<string> GeneratedFiles { get; } =
            new List<string>();
    }
}
