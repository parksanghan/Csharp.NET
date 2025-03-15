using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileService
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(FileCallback))]
    internal interface IFile
    {
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void FileServiceLoad();
        [OperationContract(IsOneWay = true , IsInitiating = false, IsTerminating = false)]
        void GetPicture(string strFileName);
        [OperationContract(IsOneWay = true , IsInitiating = false, IsTerminating = false)]

        void GetPictureList();
     
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void UploadPicture(string strFileName, byte[] bytePic);
        //void Leave(string name);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void FileServiceClose();
    }
    public interface FileCallback
    {
        [OperationContract(IsOneWay = true)]
        void ServiceLoad();
        [OperationContract(IsOneWay = true)]
        void GetPictureACK(byte[] strFileName);

        [OperationContract(IsOneWay = true)]
        void GetPictureListACK(string[] msg); 

        [OperationContract(IsOneWay = true)]
        void UploadPictureACK();
        [OperationContract(IsOneWay = true)]
        void ServiceClose();
    }
}
