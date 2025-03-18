using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AzureService
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IAzureCallback))]
    internal interface IAzure
    {
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void AzureServiceLoad(); // 객체 initatiationg

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Analyze(string filename , byte[] imagebuffer);  // 이미지 버퍼로 받고 경로에 저장한 다음 이미지 분석 요청

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void ExtractText(string filename, byte[] imagerbuffer);// 이미지 버퍼로 받고 경로에 저장한 다음 글자 추출 요청

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)] 
        void GetListSql();  // callback 으로 string 반환

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void GetSelectItem(string shorturl);   // callback 으로 string 반환

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void AzureServiceClose(); // 객체 Terminating 
    }

    public interface IAzureCallback
    {
        [OperationContract(IsOneWay = true)]
        void AzureServiceLoadACK(bool reusult);

        [OperationContract(IsOneWay = true)]
        void AnalyzeACK(string analystrig);

        [OperationContract(IsOneWay = true)]
        void ExtractTextACK(string analystrig);

        [OperationContract(IsOneWay = true)]
        void GetListSqlACK(string packet);

        [OperationContract(IsOneWay = true)]
        void GetSelectItemACK(string packet);

        [OperationContract(IsOneWay = true)]
        void AzureServiceCloseACK(bool reuslt);
    }
}
