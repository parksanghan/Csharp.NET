using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServerSkleton.MSocket
{
    public enum MSocketProperty
    {
        Init=0,
        Connect=1,
        Disconnect=2,
        Send=3,
        Receive=4,
        Accept=5,
        Stop=6
    }
}
