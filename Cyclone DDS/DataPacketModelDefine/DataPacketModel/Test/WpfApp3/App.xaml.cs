using NetworkGenerator.Binary;
using NetworkGenerator.MessageStructs;
using NetworkGenerator.Packets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DataObjectRegistry.RegisterAssembly(
    Assembly.GetExecutingAssembly());
        }
        public void GetInstancePacket()
        {
            IDataPacketObject packetObject = DataObjectRegistry.GetInstance((int)EMessageID.e_navigation_status);
            NavigationStatusPacket navigationPacket = (NavigationStatusPacket)packetObject;

        }
        public void Serealize()
        {
            IDataPacketObject packetObject = DataObjectRegistry.GetInstance((int)EMessageID.e_navigation_status);
            NavigationStatusPacket navigationPacket = (NavigationStatusPacket)packetObject;
            byte[] packet = navigationPacket.Serialize();
            // 송신 부분
        }
        public void Deserealize(byte[] receivedBytes,int receivedLength)
        {
          

            IDataPacketObject updatedPacket;
            // Registry에 등록된 IDataPacket 상속받은 패킷 클래스 m_data 구조체 값 업데이트 
            int consumedLength = PacketDispatcher.Process(
                receivedBytes,
                receivedLength,
                out updatedPacket);
             
        }
    }
}
