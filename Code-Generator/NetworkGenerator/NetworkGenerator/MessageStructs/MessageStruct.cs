using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NetworkGenerator.MessageStructs
{
    
    public enum EMessageID
    {
        /// 정의될항목들 -> 모든 메세지 이름을 순회하여 해당 enum에 채움
        e_data_one,
    };
    /// <summary>
    /// 네트워크 메세지 헤더 구조체 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MESSAGEHEADER
    {
        public ushort snyc;
        public int messageid;
        public int messagesize;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MESSAGETAIL
    {
        public ushort snyc;
        public bool isresolutioned;
    }
}
