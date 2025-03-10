using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileService
{
    internal class FileServices : IFile
    {  //서비스 > 클라이언트
        private FileCallback callback = null;
        private static List<FileCallback> Filecallbacks = new List<FileCallback>();
        public void FileServiceLoad()
        {
            Console.WriteLine("FileServiceLoad");


            lock (this) { 
            callback = OperationContext.Current.GetCallbackChannel<FileCallback>();

                Filecallbacks.Add(callback);
                 }
            Broadcast("FileServiceLoad", null, null);
        }

        public void FileServiceClose()
        {


            Console.WriteLine("FileServiceClose");
            lock (Filecallbacks)
            {
                Filecallbacks.Remove(callback);
            }
            Broadcast("FileServiceClose", null, null);
        }
         
        public void Leave()
        {
            Console.WriteLine("[Leave] ");
            Broadcast("UserLeave",null, null);

            lock (Filecallbacks)
               
            callback = null;
        }
        #region 인터페이스 메서드
        public void GetPicture(string strFileName)
        {
           
            Console.WriteLine("[이미지가져오기] : " + strFileName);
           
            byte[] buffer = GetPictureex(strFileName);
            Broadcast("GetPicture", buffer, null);

        }
        public byte[] GetPictureex(string strFileName)
        {
            byte[] bytePic = { 0 }; // 바이트 배열을 하나 만든다.
            try
            {
                FileStream picFileStream = new FileStream(@"C:\Picture\" + strFileName, FileMode.Open,
                    FileAccess.Read, FileShare.Read);

                //보조객체
                BinaryReader picReader = new BinaryReader(picFileStream);
                bytePic = picReader.ReadBytes(Convert.ToInt32(picFileStream.Length));
                picFileStream.Close();

                return bytePic;
            }
            catch
            {
                return bytePic;
            }
        }
        public void GetPictureList()
        {
            
            Console.WriteLine("[GetPictureList] : ");
            string[] strings = GetPictureListex();
            Broadcast("GetPictureList", null, strings);
         
     
        }
        private string[] GetPictureListex()
        {
            //full경로
            string[] strPicList = Directory.GetFiles(@"C:\Picture");

            //파일명만 추출 저장
            for (int i = 0; i < strPicList.Length; i++)
            {
                FileInfo fi = new FileInfo(strPicList[i]);
                strPicList[i] = fi.Name;
            }

            return strPicList;
        }

        #endregion

        public void UploadPicture(string strFileName, byte[] bytePic)
        {
            Console.WriteLine("[UploadPicture] : " + strFileName);
            UploadPictureex(strFileName, bytePic);
        
            Broadcast("UploadPicture", null, null);
        }
        public void UploadPictureex(string strFileName, byte[] bytePic)
        {
            try
            {
                FileStream writeFileStream = new FileStream(@"C:\Picture\" + strFileName,
                    FileMode.Create, FileAccess.Write);

                //보조객체 
                BinaryWriter picWriter = new BinaryWriter(writeFileStream);
                picWriter.Write(bytePic);
                writeFileStream.Close();

                 
            }
            catch (Exception)
            {
                // 업로드 실패
               
            }
        }
        //클라이언트 메시지 호출

        public void Broadcast(string msgType, byte[] buffers, string[] msg)
            {
                foreach (var callback in Filecallbacks)
                {
                    UserHandler(callback, msgType, buffers, msg);
                }
            }
        public void UserHandler(FileCallback ex, string msgType, byte[] buffers, string[] msg)
            {
                try
                {
                    //클라이언트에게 보내기
                    switch (msgType)
                    {
                        case "GetPicture":
                            ex.GetPictureACK(buffers);
                            break;
                        case "GetPictureList":
                            ex.GetPictureListACK(msg);
                            break;
                        case "UploadPicture":
                            ex.UploadPictureACK();
                            break;
                         case "FileServiceClose":
                            ex.ServiceClose();
                            break;
                        case "FileServiceLoad":
                            ex.ServiceLoad();
                           break;  


                    }
                }
                catch//에러가 발생했을 경우
                {
                    Console.WriteLine("UserHandler : {0}", msgType);
                }
            }

       
    }
    }
