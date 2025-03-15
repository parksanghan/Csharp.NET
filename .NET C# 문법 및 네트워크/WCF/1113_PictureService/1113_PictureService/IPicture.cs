using System;
using System.ServiceModel;

namespace _1113_PictureService
{
    [ServiceContract]
    internal interface IPicture
    {
        [OperationContract]
        byte[] GetPicture(string strFileName);

        [OperationContract]
        string[] GetPictureList();

        [OperationContract]
        bool UploadPicture(string strFileName, byte[] bytePic);
    }
}
