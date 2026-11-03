using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace DataGenerator.Services
{
    public sealed class PacketXmlEditor
    {
        private static readonly UTF8Encoding Utf8WithoutBom =
            new UTF8Encoding(false);

        private readonly PacketXmlParser _parser =
            new PacketXmlParser();

        public PacketDefinitionDocument SavePacket(
            string xmlPath,
            int packetIndex,
            string expectedPacketName,
            string expectedMessageId,
            PacketXmlEditDefinition edit)
        {
            if (edit == null)
            {
                throw new ArgumentNullException(nameof(edit));
            }

            string fullPath = Path.GetFullPath(xmlPath);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException(
                    "수정할 패킷 XML 파일을 찾을 수 없습니다.",
                    fullPath);
            }

            XDocument xml = XDocument.Load(fullPath);
            XElement root = xml.Root
                ?? throw new InvalidDataException(
                    "XML 루트 요소가 없습니다.");

            List<XElement> packets =
                root.Elements("Packet").ToList();

            if (packetIndex < 0 ||
                packetIndex >= packets.Count)
            {
                throw new InvalidOperationException(
                    "수정할 패킷의 위치를 찾을 수 없습니다.");
            }

            XElement packetElement =
                packets[packetIndex];

            string currentPacketName =
                ((string?)packetElement.Attribute("Name"))
                    ?.Trim() ?? string.Empty;
            string currentMessageId =
                ((string?)packetElement.Attribute("MessageId"))
                    ?.Trim() ?? string.Empty;

            if (!string.Equals(
                    currentPacketName,
                    expectedPacketName,
                    StringComparison.Ordinal) ||
                !string.Equals(
                    currentMessageId,
                    expectedMessageId,
                    StringComparison.Ordinal))
            {
                throw new InvalidOperationException(
                    "XML 파일이 상세 창을 연 이후 외부에서 변경되었습니다. " +
                    "XML을 다시 불러온 뒤 수정하세요.");
            }

            XElement fieldsElement =
                packetElement.Element("Fields")
                ?? throw new InvalidDataException(
                    "수정할 패킷에 <Fields> 요소가 없습니다.");

            List<XElement> fieldElements =
                fieldsElement.Elements("Field").ToList();

            if (fieldElements.Count != edit.Fields.Count)
            {
                throw new InvalidOperationException(
                    "XML의 필드 개수가 상세 창을 연 이후 변경되었습니다. " +
                    "XML을 다시 불러온 뒤 수정하세요.");
            }

            packetElement.SetAttributeValue(
                "Name",
                edit.Name.Trim());
            packetElement.SetAttributeValue(
                "MessageId",
                edit.MessageId.Trim());
            packetElement.SetAttributeValue(
                "MessageIdValue",
                edit.MessageIdValue.Trim());
            packetElement.SetAttributeValue(
                "Sync",
                edit.Sync.Trim());
            packetElement.SetAttributeValue(
                "DataType",
                edit.DataType.Trim());

            for (int index = 0;
                 index < fieldElements.Count;
                 index++)
            {
                XElement fieldElement =
                    fieldElements[index];
                PacketXmlEditField field =
                    edit.Fields[index];

                fieldElement.SetAttributeValue(
                    "Name",
                    field.Name.Trim());
                fieldElement.SetAttributeValue(
                    "Order",
                    field.Order.Trim());
                fieldElement.SetAttributeValue(
                    "Type",
                    field.TypeName.Trim());
                fieldElement.SetAttributeValue(
                    "Resolution",
                    field.Resolution.Trim());
                fieldElement.SetAttributeValue(
                    "Min",
                    field.Minimum.Trim());
                fieldElement.SetAttributeValue(
                    "Max",
                    field.Maximum.Trim());
            }

            string directory =
                Path.GetDirectoryName(fullPath)
                ?? throw new InvalidOperationException(
                    "XML 파일 디렉터리를 확인할 수 없습니다.");

            string temporaryPath =
                Path.Combine(
                    directory,
                    "." +
                    Path.GetFileName(fullPath) +
                    "." +
                    Guid.NewGuid().ToString("N") +
                    ".tmp");

            try
            {
                WriteXml(temporaryPath, xml);

                PacketDefinitionDocument validated =
                    _parser.Parse(temporaryPath);

                File.Move(
                    temporaryPath,
                    fullPath,
                    true);

                validated.SourcePath = fullPath;
                return validated;
            }
            finally
            {
                if (File.Exists(temporaryPath))
                {
                    File.Delete(temporaryPath);
                }
            }
        }

        private static void WriteXml(
            string path,
            XDocument xml)
        {
            var settings = new XmlWriterSettings
            {
                Encoding = Utf8WithoutBom,
                Indent = true,
                IndentChars = "  ",
                NewLineChars = Environment.NewLine,
                NewLineHandling = NewLineHandling.Replace,
                OmitXmlDeclaration = false
            };

            using (XmlWriter writer =
                   XmlWriter.Create(path, settings))
            {
                xml.Save(writer);
            }
        }
    }
}
