using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NetworkGenerator.MessageStructs
{
    /// <summary>
    /// PPC/U/VHF 명령
    /// </summary>

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CntlCmdUdpData
    {
        public byte UvhfCommand { get; set; }
        public byte PttStatus { get; set; }
        public byte RadioRxVolStatus { get; set; }
    }

}
