using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1030_XML
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        //번역
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "오늘 날씨는 어떻습니까?";
            string json_string  = WbAPIExamTranslate.Translate(textBox1.Text);
            
            //json 파싱
            textBox2.Text = WbAPIExamTranslate.Parse(json_string);
        }
    }


    //클래스
    public static class WbAPIExamTranslate
    {
        public static string Translate(string msg)
        {
            //요청 객체(HttpWebRequest) 초기화 
            string url = "https://openapi.naver.com/v1/papago/n2mt";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "_8N0Vw553omSQFopjZdN");
            request.Headers.Add("X-Naver-Client-Secret", "A6hD_fXeqf");
            request.Method = "POST";
            string query = msg;
            byte[] byteDataParams = Encoding.UTF8.GetBytes("source=ko&target=ja&text=" + query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;
            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();

            //응답결과(HttpWebResponse) <----- 요청(HttpWebRequest)
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();
            stream.Close();
            response.Close();
            reader.Close();
            return text;
        }
    
        public static string Parse(string json_string)
        {
            JObject jobject = JObject.Parse(json_string);
            //return jobject.ToString();
            //return jobject["message"].ToString();
            //return jobject["message"]["result"].ToString();
            return jobject["message"]["result"]["translatedText"].ToString();
        }
    }
}
