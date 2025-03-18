using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FileServer
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드, svc 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
    // 참고: 이 서비스를 테스트하기 위해 WCF 테스트 클라이언트를 시작하려면 솔루션 탐색기에서Service1.svc나 Service1.svc.cs를 선택하고 디버깅을 시작하십시오.
    public class CFileService : IFileService
    {
        private IChatCallback callback = null;

        private static List<IChatCallback> chatCallbacks = new List<IChatCallback>();

        #region 인터페이스 메서드
        public void Join(string name)
        {
            callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();
            Console.WriteLine("[Join] : " + name);
            chatCallbacks.Add(callback);
            Broadcast("UserEnter", name, "연결");

        }

        public void Leave(string name)
        {

            Console.WriteLine("[Leave] : " + name);
            chatCallbacks.Remove(callback);
            Broadcast("Leave", name, "해제");

            callback = null;
        }

        public void Say(string name, string msg)
        {
            Console.WriteLine("[Say] : " + name + " : " + msg);
            Broadcast("Receive", name, msg);
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
                    case "Leave":
                        callback.UserLeave(name);
                        break;
                }
            }
            catch//에러가 발생했을 경우
            {
                Console.WriteLine("UserHandler : {0}", name);
            }
        }
        private void Broadcast(string megtype, string name, string msg)
        {
            foreach (var callback in chatCallbacks)
            {
                UserHandler(callback, megtype, name, msg);
            }
        }
        private void UserHandler(IChatCallback ex, string msgType, string name, string msg)
        {
            try
            {
                //클라이언트에게 보내기
                switch (msgType)
                {
                    case "Receive":
                        ex.Receive(name, msg);
                        break;
                    case "UserEnter":
                        ex.UserEnter(name);
                        break;
                    case "Leave":
                        ex.UserLeave(name);
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
