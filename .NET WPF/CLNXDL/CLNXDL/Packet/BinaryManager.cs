using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNXDL.Packet
{
    public static class BinaryManager
    {
        /// <summary>
        /// Little Endian 방식 바이너리 모듈  
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        #region Little Endian  
        public static void  WriteUInt16BE(this BinaryWriter writer, ushort value)
        {
            writer.Write(value);
        }
        #endregion
        /// <summary>
        ///  Big Endian 방식 바이너리 모듈 
        /// </summary>
        #region Big Endian  
        public static void test()
        {

        }
        #endregion
    }
}
