using NetworkGenerator.Attributes;
using NetworkGenerator.MessageStructs;
using System.Collections.Generic;

namespace NetworkGenerator.Packets
{
    [DataPakcetObject(EMessageID.e_data_one)]
    public sealed class CntlCmdUdp
        : DataPacketObject<CntlCmdUdpData>
    {
        private static readonly Dictionary<string, double>
            ResolutionValues =
                new Dictionary<string, double>
                {
                    { nameof(CntlCmdUdpData.UvhfCommand), 1.0 },
                    { nameof(CntlCmdUdpData.PttStatus), 1.0 },
                    { nameof(CntlCmdUdpData.RadioRxVolStatus), 1.0 }
                };

        private static readonly Dictionary<string, double>
            MaximumValues =
                new Dictionary<string, double>
                {
                    { nameof(CntlCmdUdpData.UvhfCommand), 2.0 },
                    { nameof(CntlCmdUdpData.PttStatus), 2.0 },
                    { nameof(CntlCmdUdpData.RadioRxVolStatus), 99.0 }
                };

        private static readonly Dictionary<string, double>
            MinimumValues =
                new Dictionary<string, double>
                {
                    { nameof(CntlCmdUdpData.UvhfCommand), 0.0 },
                    { nameof(CntlCmdUdpData.PttStatus), 0.0 },
                    { nameof(CntlCmdUdpData.RadioRxVolStatus), 0.0 }
                };

        public override EMessageID MessageID
        {
            get { return EMessageID.e_data_one; }
        }

        public override ushort MessageSync
        {
            get { return 65505; }
        }

        public override CntlCmdUdpData m_Data { get; set; }

        protected override Dictionary<string, double> m_Resolutions
        {
            get { return ResolutionValues; }
        }

        protected override Dictionary<string, double> m_MaxValues
        {
            get { return MaximumValues; }
        }

        protected override Dictionary<string, double> m_MinValues
        {
            get { return MinimumValues; }
        }
    }
}
