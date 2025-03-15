using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FileServer
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 인터페이스 이름 "IService1"을 변경할 수 있습니다.
    #region 1. 메세지 관련 Contract InterFace (클라이언트->서버)
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]
    public interface IFileService
    {

        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Join(string name);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Say(string name, string msg);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void Leave(string name);



        // TODO: 여기에 서비스 작업을 추가합니다.
    }
    #endregion

    #region 2. 클라이언트에 콜백할 CallBackContract  (서버->클라이언트)
    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void Receive(string name, string message);

        [OperationContract(IsOneWay = true)]
        void UserEnter(string name);

        [OperationContract(IsOneWay = true)]
        void UserLeave(string name);
    }
    #endregion
}