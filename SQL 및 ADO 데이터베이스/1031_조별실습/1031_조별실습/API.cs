using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _1031_조별실습
{
	internal static class API
	{
		//response
		internal static string Search(string msg)
		{
			string url = "https://openapi.naver.com/v1/search/book?query=" + msg;

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Headers.Add("X-Naver-Client-Id", "YUbsGlJgexx9BP1wOROX"); // 클라이언트 아이디
			request.Headers.Add("X-Naver-Client-Secret", "1mCwWJIR6k");       // 클라이언트 시크릿
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			string status = response.StatusCode.ToString();
			if (status == "OK")
			{
				Stream stream = response.GetResponseStream();
				StreamReader reader = new StreamReader(stream, Encoding.UTF8);

				string result = reader.ReadToEnd();
				return result;
			}
			else
				return "Erorr" + status;
		}

		//parse
		internal static List<Data> Parse(string text)
		{
			List<Data> books = new List<Data>();

			JObject jobject = JObject.Parse(text);

			JToken jToken = jobject["items"];
			
			foreach(JToken data in jToken)
			{
				string title = data["title"].ToString();
				string link = data["link"].ToString();
				string image = data["image"].ToString();
				string author = data["author"].ToString();
				int discount = int.Parse(data["discount"].ToString());
				string publisher = data["publisher"].ToString();
				string pubdate = data["pubdate"].ToString();
				long isbn = long.Parse(data["isbn"].ToString());
				string description = data["description"].ToString();

				books.Add(new Data(title, link, image, author, discount, publisher, pubdate, isbn, description)); ;
			}

			return books;
		}
	}
}
