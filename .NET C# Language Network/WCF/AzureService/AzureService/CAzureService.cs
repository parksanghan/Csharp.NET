using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureService
{
    internal class CAzureService : IAzure
    {
        private IAzureCallback callback = null;
        private static List<IAzureCallback> azcallbacks = new List<IAzureCallback>();
        AzureControl con = AzureControl.singleton;

        #region 연결처리
        public void AzureServiceLoad()
        {
            bool reuslt;
            try
            {
                Console.WriteLine("AzureServiceLoad");
                lock (this)
                {
                    callback = OperationContext.Current.GetCallbackChannel<IAzureCallback>();
                    azcallbacks.Add(callback);
                      reuslt =(con.AzureConnect() && con.Database_Connect());
                }
                // 문자열 반환 
                Broadcast("AzureServiceLoad", reuslt, null);
            }
            catch
            (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Broadcast("AzureServiceLoad", false, null);

            }
        }

        public void AzureServiceClose()
        {
            try
            {
                Console.WriteLine("AzureServiceClose");
                bool reusult = (azcallbacks.Remove(callback) && con.Database_Close());
                Broadcast("AzureServiceClose", reusult, null);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Broadcast("AzureServiceClose", false, null);
            }
        }
        #endregion

        #region 파일 저장 및 분석 요청
        public async void Analyze(string filename, byte[] imagebuffer)
        {
            try
            {
                Console.WriteLine("Analyze");

                string basepath = @"C:\Picture\";
                // 이미지 저장 후
                string filepath = basepath + filename;
                bool reuslt =con.SaveImageToFile(filename, imagebuffer);
                string packet = string.Empty;

                // 분석 요청
                packet = await con.AnalyzeImage(filepath);
                Broadcast("Analyze", reuslt, packet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Broadcast("Analyze", false, null);
            }
        }
        #endregion

        #region 파일 저장 및 글자 추출
        public async void ExtractText(string filename, byte[] imagerbuffer)
        {
            try
            {
                Console.WriteLine("ExtractText");

                string basepath = @"C:\Picture\";
                // 이미지 저장 후
                string filepath = basepath + filename;
                bool reuslt= con.SaveImageToFile(filename, imagerbuffer);
                string packet = string.Empty;
                // 분석 요청
                packet = await con.ExtractTextImage(filepath);
                Broadcast("ExtractText", reuslt, packet);
            }

     
             catch (Exception e) { Console.WriteLine(e.Message); Broadcast("ExtractText", false, null); }
 
        }
        #endregion

        #region DB에서 정보 꺼내오기
        // DB에서 전부 가져오기
        public void GetListSql()
        {
            try
            {
                Console.WriteLine("GetListSql");
                
                string pic = con.SelectAll();

                Broadcast("GetListSql", true, pic);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Broadcast("GetListSql", false, null);

            }
        }

        // DB에서 파일명으로 찾아 정보 가져오기
        public void GetSelectItem(string name)
        {
            try
            {
                Console.WriteLine("GetSelectItem");
                string messages = con.Select(name);
                Broadcast("GetSelectItem", true, messages);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Broadcast("GetSelectItem", false, null); 
            }
        }
        #endregion

        #region 콜백 처리
        public void Broadcast(string msgType, bool reuslt, string msg)
        {
            foreach (var callback in azcallbacks)
            {
                UserHandler(callback, msgType, reuslt, msg);
            }
        }

        public void UserHandler(IAzureCallback ex, string msgType, bool reuslt, string msg )
        {
            try
            {
                //클라이언트에게 보내기
                switch (msgType)
                {
                    case "AzureServiceLoad":
                        ex.AzureServiceLoadACK(reuslt);
                        break;
                    case "AzureServiceClose":
                        ex.AzureServiceCloseACK(reuslt);
                        break;
                    case "Analyze":
                        ex.AnalyzeACK (msg);
                        break;
                    case "ExtractText":
                        ex.ExtractTextACK(msg);
                        break;
                    case "GetListSql":
                        ex.GetListSqlACK(msg);
                        break;

                    case "GetSelectItem":
                        ex.GetSelectItemACK(msg);
                        break;


                }
            }
            catch//에러가 발생했을 경우
            {
                Console.WriteLine("UserHandler : {0}", msgType);
            }
        }
        #endregion
    }
}
