using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DataGenerator.Services
{
    public sealed class PacketCodeGenerator
    {
        public IReadOnlyList<GeneratedSource> Generate(
            PacketDefinitionDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            string xmlFileName =
                Path.GetFileName(document.SourcePath);

            var sources = new List<GeneratedSource>
            {
                new GeneratedSource(
                    Path.Combine(
                        "MessageStructs",
                        "MessageModels.cs"),
                    GenerateMessageModels(document, xmlFileName))
            };

            foreach (PacketDefinition packet in document.Packets)
            {
                sources.Add(
                    new GeneratedSource(
                        Path.Combine(
                            "Packets",
                            packet.Name + ".cs"),
                        GeneratePacketClass(packet, xmlFileName)));
            }

            return sources;
        }

        private static string GenerateMessageModels(
            PacketDefinitionDocument document,
            string xmlFileName)
        {
            var code = new StringBuilder();

            AppendHeader(code, xmlFileName);
            code.AppendLine("using NetworkGenerator.Attributes;");
            code.AppendLine("using System.Runtime.InteropServices;");
            code.AppendLine();
            code.AppendLine("namespace NetworkGenerator.MessageStructs");
            code.AppendLine("{");
            code.AppendLine("    public enum EMessageID");
            code.AppendLine("    {");

            for (int index = 0;
                 index < document.Packets.Count;
                 index++)
            {
                PacketDefinition packet =
                    document.Packets[index];

                string comma =
                    index == document.Packets.Count - 1
                        ? string.Empty
                        : ",";

                code.Append("        ")
                    .Append(packet.MessageId)
                    .Append(" = ")
                    .Append(
                        packet.MessageIdValue.ToString(
                            CultureInfo.InvariantCulture))
                    .Append(comma)
                    .AppendLine();
            }

            code.AppendLine("    }");

            foreach (PacketDefinition packet in document.Packets)
            {
                code.AppendLine();
                code.AppendLine(
                    "    [StructLayout(LayoutKind.Sequential, Pack = 1)]");
                code.Append("    public struct ")
                    .Append(packet.DataType)
                    .AppendLine();
                code.AppendLine("    {");

                List<PacketFieldDefinition> fields =
                    packet.Fields
                        .OrderBy(field => field.Order)
                        .ToList();

                for (int fieldIndex = 0;
                     fieldIndex < fields.Count;
                     fieldIndex++)
                {
                    PacketFieldDefinition field =
                        fields[fieldIndex];

                    code.Append("        [PacketField(")
                        .Append(
                            field.Order.ToString(
                                CultureInfo.InvariantCulture))
                        .AppendLine(")]");

                    code.Append("        public ")
                        .Append(field.TypeName)
                        .Append(' ')
                        .Append(field.Name)
                        .AppendLine(" { get; set; }");

                    if (fieldIndex < fields.Count - 1)
                    {
                        code.AppendLine();
                    }
                }

                code.AppendLine("    }");
            }

            code.AppendLine("}");
            return code.ToString();
        }

        private static string GeneratePacketClass(
            PacketDefinition packet,
            string xmlFileName)
        {
            var code = new StringBuilder();

            AppendHeader(code, xmlFileName);
            code.AppendLine("using NetworkGenerator.Attributes;");
            code.AppendLine("using NetworkGenerator.MessageStructs;");
            code.AppendLine("using System.Collections.Generic;");
            code.AppendLine();
            code.AppendLine("namespace NetworkGenerator.Packets");
            code.AppendLine("{");
            code.Append("    [DataPakcetObject((int)EMessageID.")
                .Append(packet.MessageId)
                .AppendLine(")]");
            code.Append("    public sealed class ")
                .Append(packet.Name)
                .AppendLine();
            code.Append("        : DataPacketObject<")
                .Append(packet.DataType)
                .AppendLine(">");
            code.AppendLine("    {");

            AppendDictionary(
                code,
                packet,
                "ResolutionValues",
                field => field.Resolution);

            code.AppendLine();

            AppendDictionary(
                code,
                packet,
                "MaximumValues",
                field => field.Maximum);

            code.AppendLine();

            AppendDictionary(
                code,
                packet,
                "MinimumValues",
                field => field.Minimum);

            code.AppendLine();
            code.AppendLine("        public override int MessageID");
            code.AppendLine("        {");
            code.Append("            get { return (int)EMessageID.")
                .Append(packet.MessageId)
                .AppendLine("; }");
            code.AppendLine("        }");
            code.AppendLine();
            code.AppendLine("        public override ushort MessageSync");
            code.AppendLine("        {");
            code.Append("            get { return ")
                .Append(
                    packet.Sync.ToString(
                        CultureInfo.InvariantCulture))
                .AppendLine("; }");
            code.AppendLine("        }");
            code.AppendLine();
            code.Append("        public override ")
                .Append(packet.DataType)
                .AppendLine(" m_Data { get; set; }");
            code.AppendLine();
            code.AppendLine(
                "        protected override Dictionary<string, double> m_Resolutions");
            code.AppendLine("        {");
            code.AppendLine(
                "            get { return ResolutionValues; }");
            code.AppendLine("        }");
            code.AppendLine();
            code.AppendLine(
                "        protected override Dictionary<string, double> m_MaxValues");
            code.AppendLine("        {");
            code.AppendLine(
                "            get { return MaximumValues; }");
            code.AppendLine("        }");
            code.AppendLine();
            code.AppendLine(
                "        protected override Dictionary<string, double> m_MinValues");
            code.AppendLine("        {");
            code.AppendLine(
                "            get { return MinimumValues; }");
            code.AppendLine("        }");
            code.AppendLine("    }");
            code.AppendLine("}");

            return code.ToString();
        }

        private static void AppendDictionary(
            StringBuilder code,
            PacketDefinition packet,
            string dictionaryName,
            Func<PacketFieldDefinition, double> valueSelector)
        {
            code.AppendLine(
                "        private static readonly Dictionary<string, double>");
            code.Append("            ")
                .Append(dictionaryName)
                .AppendLine(" =");
            code.AppendLine(
                "                new Dictionary<string, double>");
            code.AppendLine("                {");

            List<PacketFieldDefinition> fields =
                packet.Fields
                    .OrderBy(field => field.Order)
                    .ToList();

            for (int index = 0; index < fields.Count; index++)
            {
                PacketFieldDefinition field = fields[index];
                string comma =
                    index == fields.Count - 1
                        ? string.Empty
                        : ",";

                code.Append("                    { nameof(")
                    .Append(packet.DataType)
                    .Append('.')
                    .Append(field.Name)
                    .Append("), ")
                    .Append(FormatDouble(valueSelector(field)))
                    .Append(" }")
                    .Append(comma)
                    .AppendLine();
            }

            code.AppendLine("                };");
        }

        private static string FormatDouble(double value)
        {
            string text = value.ToString(
                "R",
                CultureInfo.InvariantCulture);

            if (text.IndexOf('.') < 0 &&
                text.IndexOf('E') < 0 &&
                text.IndexOf('e') < 0)
            {
                text += ".0";
            }

            return text;
        }

        private static void AppendHeader(
            StringBuilder code,
            string xmlFileName)
        {
            code.AppendLine("// <auto-generated />");
            code.Append("// Source: ")
                .AppendLine(xmlFileName);
            code.AppendLine(
                "// DataGenerator에서 생성했습니다. 직접 수정하지 마십시오.");
            code.AppendLine();
        }
    }
}
