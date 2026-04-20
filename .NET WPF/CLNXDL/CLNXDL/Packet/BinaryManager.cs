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
        #region Little Endian Writer
        public static void  WriteUInt16BE(this BinaryWriter writer, ushort value)
        {
            writer.Write((byte)(value >> 8));
            writer.Write(((byte)value));
        }
        public static void WriteUInt32BE(this BinaryWriter writer, uint value)
        {
            writer.Write((byte)(value) >> 24);
            writer.Write((byte)(value) >> 16);
            writer.Write((byte)(value) >> 8);
            writer.Write(((byte)value));
        }
        #endregion

        #region Little Endian reader
        public static ushort ReadUInt16BE(this BinaryReader reader)
        {
            byte b1 = reader.ReadByte();
            byte b2 = reader.ReadByte();
            return (ushort)((b1 << 8) | b2);
        }
        public static uint ReadUInt32BE(this BinaryReader reeaer)
        {
            byte b1 = reeaer.ReadByte();
            byte b2 = reeaer.ReadByte(); 
            byte b3 = reeaer.ReadByte();
            byte b4 = reeaer.ReadByte();
            return (uint)(
               (b1 << 24) |
               (b2 << 16) |
               (b3 << 8) |
               b4);
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
