using NetworkGenerator.Attributes;
using NetworkGenerator.MessageStructs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkGenerator.Packets
{
    [DataPakcetObject(EMessageID.e_data_one)]
    partial class DataTest : DataPacketObject<CntlCmdUdpData>
    {
        public override EMessageID MessageID => EMessageID.e_data_one;

        public override CntlCmdUdpData m_Data { get; set; }

        protected override Dictionary<string, double> m_Resolutions => throw new NotImplementedException();

        protected override Dictionary<string, double> m_MaxValues => throw new NotImplementedException();

        protected override Dictionary<string, double> m_MinValues => throw new NotImplementedException();

        public override void Validate()
        {
            base.Validate();
        }
    }
}
