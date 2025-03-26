using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _1114_Chating
{
    internal class ChatService : IChat
    {
        //서비스 > 클라이언트
        private IChatCallback callback = null;
        
        #region 인터페이스 메서드
        public void Join(string name)
        {
            callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();
            Console.WriteLine("[Join] : " + name);
            UserHandler("UserEnter", name, "연결");

        }

        public void Leave(string name)
        {
            Console.WriteLine("[Leave] : " + name);
            callback = null;
        }

        public void Say(string name, string msg)
        {
            Console.WriteLine("[Say] : " + name + " : " + msg);
            UserHandler("Receive", name, msg);
        }
        #endregion

        //클라이언트 메시지 호출
        private void UserHandler(string msgType, string name, string msg)
        {
            try
            {
                //클라이언트에게 보내기
                switch (msgType)
                {
                    case "Receive":
                        callback.Receive(name, msg);
                        break;
                    case "UserEnter":
                        callback.UserEnter(name);
                        break;
                }
            }
            catch//에러가 발생했을 경우
            {
                Console.WriteLine("UserHandler : {0}", name);
            }
        }

    }
}
