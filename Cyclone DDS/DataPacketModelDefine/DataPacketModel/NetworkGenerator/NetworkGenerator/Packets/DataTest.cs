using NetworkGenerator.MessageStructs;
using System.Collections.Generic;

namespace NetworkGenerator.Packets
{
    // Registration example is CntlCmdUdp. This placeholder is intentionally
    // left without DataPakcetObjectAttribute to avoid a duplicate MessageID.
    internal partial class DataTest
        : DataPacketObject<CntlCmdUdpData>
    {
        private static readonly Dictionary<string, double> EmptyValues =
            new Dictionary<string, double>();

        public override EMessageID MessageID
        {
            get { return EMessageID.e_data_one; }
        }

        public override CntlCmdUdpData m_Data { get; set; }

        protected override Dictionary<string, double> m_Resolutions
        {
            get { return EmptyValues; }
        }

        protected override Dictionary<string, double> m_MaxValues
        {
            get { return EmptyValues; }
        }

        protected override Dictionary<string, double> m_MinValues
        {
            get { return EmptyValues; }
        }
    }
}
