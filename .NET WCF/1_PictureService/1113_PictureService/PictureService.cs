using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1113_PictureService
{
    internal class PictureService : IPicture
    {
        //In  : 파일명
        //Out : 파일 정보를 byte[]로 반환
        public byte[] GetPicture(string strFileName)
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

        //Out : 이미지 저장 폴더에 있는 파일의 이름들을 반환
        public string[] GetPictureList()
        {
            //full경로
            string[] strPicList = Directory.GetFiles(@"C:\Picture\");

            //파일명만 추출 저장
            for(int i =0; i< strPicList.Length; i++)
            {
                FileInfo fi = new FileInfo(strPicList[i]);
                strPicList[i] = fi.Name;
            }
           
            return strPicList;
        }

        //In : 파일명, 이미지데이터
        //클라이언트 -> 서버 
        public bool UploadPicture(string strFileName, byte[] bytePic)
        {
            try
            {
                FileStream writeFileStream = new FileStream(@"C:\Picture\" + strFileName, 
                    FileMode.Create, FileAccess.Write);

                //보조객체 
                BinaryWriter picWriter = new BinaryWriter(writeFileStream);
                picWriter.Write(bytePic);
                writeFileStream.Close();
               
                return true;
            }
            catch (Exception)
            {
                // 업로드 실패
                return false;
            }
        }
    }
}
