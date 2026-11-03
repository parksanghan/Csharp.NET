namespace DataGenerator.Models
{
    public sealed class PacketParseProgress
    {
        public int Percentage { get; set; }

        public bool IsIndeterminate { get; set; }

        public string Message { get; set; } = string.Empty;

        public int ProcessedPackets { get; set; }

        public int TotalPackets { get; set; }

        public int ProcessedFields { get; set; }

        public int TotalFields { get; set; }
    }
}
