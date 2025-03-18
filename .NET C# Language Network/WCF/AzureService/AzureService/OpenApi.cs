using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AzureService
{
    internal class OpenAPI
    {

        //받은 단어 와 언어 합쳐서 번역한 단어     받은 단어 + 받은 단어 언어  + 원하는 단어
        //위에서 찾은 언어 포함해서 언어 번역해주기.
        public static string Translate(string query)
        {
            //요청 객체(request) 초기화
            string url = "https://openapi.naver.com/v1/papago/n2mt";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Headers.Add("X-Naver-Client-Id", "YO7jP55zCnSon8rG_Elp");
            request.Headers.Add("X-Naver-Client-Secret", "75TT8k3qP2");
            request.Method = "POST";
            //string query = "오늘 날씨는 어떻습니까?";

            byte[] byteDataParams = Encoding.UTF8.GetBytes("source=en" + "&target=ko" + "&text=" + query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;

            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();

            //응답결과(httpwebResponse) <----------- 요청(httpWebRequest)
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();
            stream.Close();
            response.Close();
            reader.Close();
            Console.WriteLine(text);

            return text;
        }

        public static string Parse(string json_string)
        {
            JObject jobject = JObject.Parse(json_string);

            return jobject["message"]["result"]["translatedText"].ToString();
        }

        public static string Search(string msg)
        {
            string query = msg; // 검색할 문자열
                                //string url = "https://openapi.naver.com/v1/search/encyc?query=" + query; // JSON 결과
            string url = "https://openapi.naver.com/v1/search/encyc.xml?query=" + query;  // XML 결과
            //url += " display=50";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "FGRk3BzFkhS2O8WxqLm4"); // 클라이언트 아이디
            request.Headers.Add("X-Naver-Client-Secret", "i6J_HHkAxw");       // 클라이언트 시크릿
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();

            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
                return text;
            }
            else
            {
                return "Error 발생 = " + status;
            }
        }
    }
}
