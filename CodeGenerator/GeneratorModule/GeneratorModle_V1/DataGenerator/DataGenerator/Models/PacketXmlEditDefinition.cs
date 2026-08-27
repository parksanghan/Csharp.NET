using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DataGenerator.Models
{
    public sealed class PacketXmlEditDefinition
    {
        public string Name { get; set; } = string.Empty;

        public string MessageId { get; set; } = string.Empty;

        public string MessageIdValue { get; set; } = string.Empty;

        public string Sync { get; set; } = string.Empty;

        public string DataType { get; set; } = string.Empty;

        public List<PacketXmlEditField> Fields { get; } =
            new List<PacketXmlEditField>();

        public static PacketXmlEditDefinition FromPacket(
            PacketDefinition packet)
        {
            var edit = new PacketXmlEditDefinition
            {
                Name = packet.Name,
                MessageId = packet.MessageId,
                MessageIdValue =
                    packet.MessageIdValue.ToString(
                        CultureInfo.InvariantCulture),
                Sync =
                    packet.Sync.ToString(
                        CultureInfo.InvariantCulture),
                DataType = packet.DataType
            };

            edit.Fields.AddRange(
                packet.Fields
                    .OrderBy(field => field.Order)
                    .Select(
                        field =>
                            new PacketXmlEditField
                            {
                                Name = field.Name,
                                Order =
                                    field.Order.ToString(
                                        CultureInfo.InvariantCulture),
                                TypeName = field.TypeName,
                                Resolution =
                                    field.Resolution.ToString(
                                        "R",
                                        CultureInfo.InvariantCulture),
                                Minimum =
                                    field.Minimum.ToString(
                                        "R",
                                        CultureInfo.InvariantCulture),
                                Maximum =
                                    field.Maximum.ToString(
                                        "R",
                                        CultureInfo.InvariantCulture)
                            }));

            return edit;
        }
    }

    public sealed class PacketXmlEditField
    {
        public string Name { get; set; } = string.Empty;

        public string Order { get; set; } = string.Empty;

        public string TypeName { get; set; } = string.Empty;

        public string Resolution { get; set; } = string.Empty;

        public string Minimum { get; set; } = string.Empty;

        public string Maximum { get; set; } = string.Empty;
    }
}
