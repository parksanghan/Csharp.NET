using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace DataGenerator.Services
{
    public sealed class PacketXmlParser
    {
        private static readonly Regex IdentifierPattern =
            new Regex(
                "^[A-Za-z_][A-Za-z0-9_]*$",
                RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static readonly HashSet<string> SupportedTypes =
            new HashSet<string>(StringComparer.Ordinal)
            {
                "byte", "sbyte", "short", "ushort",
                "int", "uint", "long", "ulong",
                "float", "double", "decimal", "bool", "char"
            };

        private static readonly HashSet<string> CSharpKeywords =
            new HashSet<string>(StringComparer.Ordinal)
            {
                "abstract", "as", "base", "bool", "break", "byte", "case",
                "catch", "char", "checked", "class", "const", "continue",
                "decimal", "default", "delegate", "do", "double", "else",
                "enum", "event", "explicit", "extern", "false", "finally",
                "fixed", "float", "for", "foreach", "goto", "if", "implicit",
                "in", "int", "interface", "internal", "is", "lock", "long",
                "namespace", "new", "null", "object", "operator", "out",
                "override", "params", "private", "protected", "public",
                "readonly", "ref", "return", "sbyte", "sealed", "short",
                "sizeof", "stackalloc", "static", "string", "struct", "switch",
                "this", "throw", "true", "try", "typeof", "uint", "ulong",
                "unchecked", "unsafe", "ushort", "using", "virtual", "void",
                "volatile", "while"
            };

        public PacketDefinitionDocument Parse(string xmlPath)
        {
            return Parse(
                xmlPath,
                null,
                CancellationToken.None);
        }

        public PacketDefinitionDocument Parse(
            string xmlPath,
            IProgress<PacketParseProgress>? progress,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ReportProgress(
                progress,
                0,
                true,
                "XML 파일 경로를 확인하는 중입니다.",
                0,
                0,
                0,
                0);

            if (string.IsNullOrWhiteSpace(xmlPath))
            {
                throw new ArgumentException(
                    "XML 파일 경로를 입력해야 합니다.",
                    nameof(xmlPath));
            }

            string fullPath = Path.GetFullPath(xmlPath);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException(
                    "패킷 XML 파일을 찾을 수 없습니다.",
                    fullPath);
            }

            cancellationToken.ThrowIfCancellationRequested();
            ReportProgress(
                progress,
                5,
                true,
                "XML 파일을 읽는 중입니다.",
                0,
                0,
                0,
                0);

            XDocument xml;
            try
            {
                xml = XDocument.Load(fullPath, LoadOptions.SetLineInfo);
            }
            catch (XmlException exception)
            {
                throw new InvalidDataException(
                    "XML 문법 오류: " + exception.Message,
                    exception);
            }

            cancellationToken.ThrowIfCancellationRequested();

            XElement root = xml.Root
                ?? throw new InvalidDataException(
                    "XML 루트 요소가 없습니다.");

            if (!string.Equals(
                    root.Name.LocalName,
                    "PacketDefinitions",
                    StringComparison.Ordinal))
            {
                throw CreateError(
                    root,
                    "루트 요소는 <PacketDefinitions>여야 합니다.");
            }

            var document = new PacketDefinitionDocument
            {
                SourcePath = fullPath
            };

            List<XElement> packetElements =
                root.Elements("Packet").ToList();

            if (packetElements.Count == 0)
            {
                throw CreateError(
                    root,
                    "최소 한 개의 <Packet>이 필요합니다.");
            }

            int totalFields = packetElements.Sum(
                packet =>
                    packet.Element("Fields")?
                        .Elements("Field")
                        .Count() ?? 0);

            int totalWork =
                Math.Max(
                    1,
                    packetElements.Count + totalFields);

            int completedWork = 0;
            int processedFields = 0;
            int lastReportedPercentage = 15;

            ReportProgress(
                progress,
                15,
                false,
                "패킷 구조를 분석하는 중입니다.",
                0,
                packetElements.Count,
                0,
                totalFields);

            for (int index = 0; index < packetElements.Count; index++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                XElement packetElement =
                    packetElements[index];

                string packetDisplayName =
                    ((string?)packetElement.Attribute("Name"))
                        ?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(packetDisplayName))
                {
                    packetDisplayName =
                        "Packet #" +
                        (index + 1).ToString(
                            CultureInfo.InvariantCulture);
                }

                PacketDefinition packet =
                    ParsePacket(
                        packetElement,
                        index,
                        cancellationToken,
                        fieldName =>
                        {
                            processedFields++;
                            completedWork++;

                            int percentage =
                                CalculateParsePercentage(
                                    completedWork,
                                    totalWork);

                            if (percentage >
                                lastReportedPercentage)
                            {
                                lastReportedPercentage =
                                    percentage;

                                ReportProgress(
                                    progress,
                                    percentage,
                                    false,
                                    packetDisplayName +
                                    " · " +
                                    fieldName +
                                    " 필드 분석 완료",
                                    index,
                                    packetElements.Count,
                                    processedFields,
                                    totalFields);
                            }
                        });

                document.Packets.Add(packet);
                completedWork++;

                int packetPercentage =
                    CalculateParsePercentage(
                        completedWork,
                        totalWork);

                if (packetPercentage >
                        lastReportedPercentage ||
                    index == packetElements.Count - 1)
                {
                    lastReportedPercentage =
                        packetPercentage;

                    ReportProgress(
                        progress,
                        packetPercentage,
                        false,
                        packet.Name + " 패킷 검증 완료",
                        index + 1,
                        packetElements.Count,
                        processedFields,
                        totalFields);
                }
            }

            cancellationToken.ThrowIfCancellationRequested();
            ReportProgress(
                progress,
                95,
                false,
                "Message ID와 형식의 중복 여부를 검증하는 중입니다.",
                packetElements.Count,
                packetElements.Count,
                processedFields,
                totalFields);

            ValidateDocument(document);

            ReportProgress(
                progress,
                100,
                false,
                "XML 패킷 분석이 완료되었습니다.",
                packetElements.Count,
                packetElements.Count,
                processedFields,
                totalFields);

            return document;
        }

        private static PacketDefinition ParsePacket(
            XElement element,
            int packetIndex,
            CancellationToken cancellationToken,
            Action<string>? fieldParsed)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string name = RequiredAttribute(element, "Name");
            string messageId = RequiredAttribute(element, "MessageId");
            string dataType = RequiredAttribute(element, "DataType");

            ValidateIdentifier(element, "Packet Name", name);
            ValidateIdentifier(element, "MessageId", messageId);
            ValidateIdentifier(element, "DataType", dataType);

            ushort sync = ParseUInt16(
                element,
                "Sync",
                RequiredAttribute(element, "Sync"));

            int messageIdValue = packetIndex;
            XAttribute? valueAttribute =
                element.Attribute("MessageIdValue");

            if (valueAttribute != null &&
                !int.TryParse(
                    valueAttribute.Value,
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out messageIdValue))
            {
                throw CreateError(
                    element,
                    "MessageIdValue는 Int32 숫자여야 합니다.");
            }

            XElement fieldsElement =
                element.Element("Fields")
                ?? throw CreateError(
                    element,
                    "<Fields> 요소가 필요합니다.");

            var packet = new PacketDefinition
            {
                Name = name,
                MessageId = messageId,
                MessageIdValue = messageIdValue,
                Sync = sync,
                DataType = dataType
            };

            List<XElement> fieldElements =
                fieldsElement.Elements("Field").ToList();

            if (fieldElements.Count == 0)
            {
                throw CreateError(
                    fieldsElement,
                    "패킷에는 최소 한 개의 <Field>가 필요합니다.");
            }

            foreach (XElement fieldElement in fieldElements)
            {
                cancellationToken.ThrowIfCancellationRequested();

                PacketFieldDefinition field =
                    ParseField(fieldElement);

                packet.Fields.Add(field);
                fieldParsed?.Invoke(field.Name);
            }

            cancellationToken.ThrowIfCancellationRequested();
            ValidatePacket(packet, element);
            return packet;
        }

        private static int CalculateParsePercentage(
            int completedWork,
            int totalWork)
        {
            return 15 +
                   (int)Math.Round(
                       completedWork /
                       (double)Math.Max(1, totalWork) *
                       75.0,
                       MidpointRounding.AwayFromZero);
        }

        private static void ReportProgress(
            IProgress<PacketParseProgress>? progress,
            int percentage,
            bool isIndeterminate,
            string message,
            int processedPackets,
            int totalPackets,
            int processedFields,
            int totalFields)
        {
            progress?.Report(
                new PacketParseProgress
                {
                    Percentage = percentage,
                    IsIndeterminate = isIndeterminate,
                    Message = message,
                    ProcessedPackets = processedPackets,
                    TotalPackets = totalPackets,
                    ProcessedFields = processedFields,
                    TotalFields = totalFields
                });
        }

        private static PacketFieldDefinition ParseField(XElement element)
        {
            string name = RequiredAttribute(element, "Name");
            string typeName = RequiredAttribute(element, "Type");

            ValidateIdentifier(element, "Field Name", name);

            if (!SupportedTypes.Contains(typeName))
            {
                throw CreateError(
                    element,
                    "지원하지 않는 필드 Type입니다: " + typeName + ".");
            }

            int order = ParseInt32(
                element,
                "Order",
                RequiredAttribute(element, "Order"));

            if (order <= 0)
            {
                throw CreateError(
                    element,
                    "Field Order는 1 이상이어야 합니다.");
            }

            double resolution = ParseDouble(
                element,
                "Resolution",
                RequiredAttribute(element, "Resolution"));

            double minimum = ParseDouble(
                element,
                "Min",
                RequiredAttribute(element, "Min"));

            double maximum = ParseDouble(
                element,
                "Max",
                RequiredAttribute(element, "Max"));

            if (resolution <= 0)
            {
                throw CreateError(
                    element,
                    "Resolution은 0보다 커야 합니다.");
            }

            if (minimum > maximum)
            {
                throw CreateError(
                    element,
                    "Min은 Max보다 클 수 없습니다.");
            }

            return new PacketFieldDefinition
            {
                Name = name,
                Order = order,
                TypeName = typeName,
                Resolution = resolution,
                Minimum = minimum,
                Maximum = maximum
            };
        }

        private static void ValidateDocument(
            PacketDefinitionDocument document)
        {
            EnsureUnique(
                document.Packets,
                packet => packet.Name,
                "Packet Name");

            EnsureUnique(
                document.Packets,
                packet => packet.MessageId,
                "MessageId");

            EnsureUnique(
                document.Packets,
                packet => packet.MessageIdValue.ToString(
                    CultureInfo.InvariantCulture),
                "MessageIdValue");

            EnsureUnique(
                document.Packets,
                packet => packet.DataType,
                "DataType");
        }

        private static void ValidatePacket(
            PacketDefinition packet,
            XElement source)
        {
            string duplicateName = packet.Fields
                .GroupBy(field => field.Name, StringComparer.Ordinal)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .FirstOrDefault() ?? string.Empty;

            if (duplicateName.Length > 0)
            {
                throw CreateError(
                    source,
                    "중복 Field Name: " + duplicateName + ".");
            }

            int? duplicateOrder = packet.Fields
                .GroupBy(field => field.Order)
                .Where(group => group.Count() > 1)
                .Select(group => (int?)group.Key)
                .FirstOrDefault();

            if (duplicateOrder.HasValue)
            {
                throw CreateError(
                    source,
                    "중복 Field Order: " +
                    duplicateOrder.Value.ToString(
                        CultureInfo.InvariantCulture) +
                    ".");
            }
        }

        private static void EnsureUnique(
            IEnumerable<PacketDefinition> packets,
            Func<PacketDefinition, string> selector,
            string label)
        {
            string duplicate = packets
                .GroupBy(selector, StringComparer.Ordinal)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .FirstOrDefault() ?? string.Empty;

            if (duplicate.Length > 0)
            {
                throw new InvalidDataException(
                    "중복 " + label + ": " + duplicate + ".");
            }
        }

        private static string RequiredAttribute(
            XElement element,
            string attributeName)
        {
            string? value =
                (string?)element.Attribute(attributeName);

            if (string.IsNullOrWhiteSpace(value))
            {
                throw CreateError(
                    element,
                    attributeName + " 속성이 필요합니다.");
            }

            return value.Trim();
        }

        private static int ParseInt32(
            XElement element,
            string name,
            string value)
        {
            if (!int.TryParse(
                    value,
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out int result))
            {
                throw CreateError(
                    element,
                    name + "은 Int32 숫자여야 합니다.");
            }

            return result;
        }

        private static ushort ParseUInt16(
            XElement element,
            string name,
            string value)
        {
            if (!ushort.TryParse(
                    value,
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out ushort result))
            {
                throw CreateError(
                    element,
                    name + "은 0~65535 숫자여야 합니다.");
            }

            return result;
        }

        private static double ParseDouble(
            XElement element,
            string name,
            string value)
        {
            if (!double.TryParse(
                    value,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double result) ||
                double.IsNaN(result) ||
                double.IsInfinity(result))
            {
                throw CreateError(
                    element,
                    name + "은 유한한 숫자여야 합니다.");
            }

            return result;
        }

        private static void ValidateIdentifier(
            XElement source,
            string label,
            string value)
        {
            if (!IdentifierPattern.IsMatch(value) ||
                CSharpKeywords.Contains(value))
            {
                throw CreateError(
                    source,
                    label + "은 올바른 C# 식별자여야 합니다: " +
                    value +
                    ".");
            }
        }

        private static InvalidDataException CreateError(
            XElement source,
            string message)
        {
            IXmlLineInfo lineInfo = source;
            if (lineInfo.HasLineInfo())
            {
                message +=
                    " (줄 " +
                    lineInfo.LineNumber.ToString(
                        CultureInfo.InvariantCulture) +
                    ")";
            }

            return new InvalidDataException(message);
        }
    }
}
