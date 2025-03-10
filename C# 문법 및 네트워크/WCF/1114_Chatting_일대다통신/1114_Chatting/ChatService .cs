using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _1114_Chatting
{
    internal class ChatService : IChat
    {
        //Server -> Client Call
        private IChatCallback callback = null;

        //연결된 IChatCallback을 보관하는 Colletion
        private static List<IChatCallback> chatCallbacks = new List<IChatCallback>();

        #region 인터페이스 메서드
        public void Join(string name)
        {
            callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();
            chatCallbacks.Add(callback);

            Console.WriteLine("[Join] " + name);
            //UserHandler("UserEnter", name, "Connect");        나한테만
            BroadcastMessage("UserEnter", name, "Connect");     //모두에게
        }

        public void Leave(string name)
        {
            Console.WriteLine("[Leave] " + name);
            chatCallbacks.Remove(callback);

            //UserHandler("Leave", name, "DisConnect");        나한테만
            BroadcastMessage("Leave", name, "DisConnect");     //모두에게
            callback = null;
        }

        public void Say(string name, string msg)
        {
            Console.WriteLine("[Say] " + name + ":" + msg);
            //UserHandler("Receive", name, msg); 나한테만
            BroadcastMessage("Receive", name, msg);     //모두에게
        }
        #endregion

        #region Client Message Call
        private void BroadcastMessage(string msgType, string name, string msg)
        {
            foreach (var callback in chatCallbacks)
            {
                UserHandler(callback, msgType, name, msg);
            }
        }

        private void UserHandler(IChatCallback icb, string msgType, string name, string msg)
        {
            try
            {
                //클라이언트에게 보내기
                switch (msgType)
                {
                    case "Receive":
                        icb.Receive(name, msg);
                        break;
                    case "UserEnter":
                        icb.UserEnter(name);
                        break;
                    case "Leave":
                        icb.UserLeave(name);
                        break;
                }
            }
            catch//에러가 발생했을 경우
            {
                Console.WriteLine("UserHandler(ChatService.cs) Error: {0}", name);
            }
        }

        //private void UserHandler(string msgType, string name, string msg)
        //{
        //    try
        //    {
        //        //클라이언트에게 보내기
        //        switch (msgType)
        //        {
        //            case "Receive":
        //                callback.Receive(name, msg);
        //                break;
        //            case "UserEnter":
        //                callback.UserEnter(name);
        //                break;
        //            case "Leave":
        //                callback.UserLeave(name);
        //                break;
        //        }
        //    }
        //    catch//에러가 발생했을 경우
        //    {
        //        Console.WriteLine("UserHandler(ChatService.cs) Error: {0}", name);
        //    }
        //}
        #endregion
    }
}
