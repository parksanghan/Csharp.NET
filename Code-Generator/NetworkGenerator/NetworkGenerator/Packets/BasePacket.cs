using NetworkGenerator.MessageStructs;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace NetworkGenerator.Packets
{
    public abstract class BasePacket : IDisposable
    {
        private bool disposedValue;
        //전송될 메세지 ID 불필요 할수도
        public EMessageID messageID {  get; private set; }

        bool m_IsSendDataUpdated= false;
        bool m_IsSendDate = false;

        public double m_SendPeriod = 20;
        public double m_RecvPeriod = 20;

        Timer m_TSendTime = new Timer();    
        Timer m_TRecvTimer = new Timer();

        protected List<Socket> Sockets = new List<Socket>();    
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        // ~BasePacket()
        // {
        //     // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
