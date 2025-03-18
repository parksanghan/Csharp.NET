//WbXMLSample.cs
using System;
using System.IO;
using System.Xml;

namespace _1030_XML
{
    internal static class WbXMLSample
    {
        //page17
        public static void Sample1(string filename)
        {
            XmlWriterSettings xsettings = new XmlWriterSettings();
            xsettings.Indent = true;

            XmlWriter xwriter = XmlWriter.Create(filename, xsettings);

            xwriter.WriteComment("XmlWriter 개체 만들기 실습 예제");
            xwriter.WriteStartElement("books"); //<books>
            xwriter.WriteStartElement("book");  //      <book>ADO.NET</book>
            xwriter.WriteValue("ADO.NET");
            xwriter.WriteEndElement();
            xwriter.WriteStartElement("book");  //      <book>XML.NET</book>
            xwriter.WriteValue("XML.NET");
            xwriter.WriteEndElement();
            xwriter.WriteEndElement();          //</book>

            xwriter.Close();
        }
    
        //p19
        public static void Sample2(string filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(filename, settings);
            writer.WriteComment("XmlWriter 개체로 요소 쓰기");
            writer.WriteStartElement("books");      //<books>
            
            writer.WriteStartElement("book");       //  <book>
            writer.WriteStartElement("title");      //      <title>XML.NET</title>
            writer.WriteValue("XML.NET");            //??
            writer.WriteEndElement();
            writer.WriteStartElement("가격");       //      <가격>12000</가격>
            writer.WriteValue(12000);
            writer.WriteEndElement();
            writer.WriteEndElement();               //  </book>

            writer.WriteStartElement("book");                   //<book>
            writer.WriteElementString("title", "ADO.NET");      //  <title>ADO.NET</title>
            writer.WriteStartElement("가격");                   //  <가격>15000</가격>
            writer.WriteValue(15000);
            writer.WriteEndElement();
            writer.WriteEndElement();                           //</book>
            writer.WriteEndElement();               //</books>
            writer.Close();
           
            
            //XmlReader xreader = XmlReader.Create("data1.xml"); //XmlReader 개체 생성
            //XmlWriter xwriter = XmlWriter.Create(Console.Out); //콘솔 출력 스트림으로 XmlWriter 개체 생성
            //xwriter.WriteNode(xreader, false); //xreader 개체로 읽어온 데이터를 xwriter 개체에 복사
            //xwriter.Close();
            //xreader.Close();
        }

        //p21(특성)
        public static void Sample3(string filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(filename, settings);
            writer.WriteComment("XmlWriter 개체로 특성 쓰기");
            writer.WriteStartElement("books"); //루트 요소 쓰기
            
            //-------------------<book> ---------------------------
            //<book title=XML.NET 가격=12000/>
            writer.WriteStartElement("book");//book 요소 쓰기
            
            writer.WriteStartAttribute("title"); //title 특성 쓰기******
            writer.WriteString("XML.NET"); //title 특성 값 쓰기
            writer.WriteEndAttribute(); //title 특성 닫기
            
            writer.WriteStartAttribute("가격");//가격 특성 쓰기
            writer.WriteValue(12000); //가격 특성 값 쓰기
            writer.WriteEndAttribute(); //가격 특성 닫기

            writer.WriteEndElement(); //book 요소 닫기
            //-----------------</book>-------------------------------


            writer.WriteStartElement("book");//book 요소 쓰기
            writer.WriteAttributeString("title", "ADO.NET");//title 특성과 값 쓰기
            writer.WriteStartAttribute("가격");//가격 특성 쓰기
            writer.WriteValue(15000);//가격 특성 값 쓰기
            writer.WriteEndAttribute();//가격 특성 닫기
            writer.WriteEndElement();//book 요소 닫기
            writer.WriteEndElement();//루트 요소 닫기
            writer.Close();


            //XmlReader xreader = XmlReader.Create("data.xml"); //XmlReader 개체 생성
            //XmlWriter xwriter = XmlWriter.Create(Console.Out, settings); //XmlWriter 개체 생성
            //while (xreader.Read())  //정방향 읽기!
            //{
            //    if (xreader.NodeType == XmlNodeType.Element)
            //    {
            //        xwriter.WriteStartElement(xreader.Name);
            //        xwriter.WriteAttributes(xreader, false); //xreader의 현재 특성을 쓰기
            //        if (xreader.IsEmptyElement)
            //        {
            //            xwriter.WriteEndElement();
            //        }
            //    }
            //    else if (xreader.NodeType == XmlNodeType.EndElement)
            //    {
            //        xwriter.WriteEndElement();
            //    }
            //}
            //xwriter.Close();
            //xreader.Close();
            //Console.WriteLine();
        }
 
        //p29(외부자원)
        public static string Sample4(string url)
        {
            XmlUrlResolver resolver = new XmlUrlResolver();
            resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = resolver;
            XmlReader reader = XmlReader.Create(url, settings);

            //쓰기---------------------------------------------
            StringWriter sw = new StringWriter();  
            XmlWriter xwriter = XmlWriter.Create(sw);
            xwriter.WriteNode(reader, false);       //저장!!!!!
            xwriter.Close();
            reader.Close();
            return sw.ToString();
        }

        public static string ReadXml(string filename)
        {
            //읽기
            XmlReaderSettings r_settings = new XmlReaderSettings();
            r_settings.IgnoreComments = true;   //주석 제거
            XmlReader xreader = XmlReader.Create(filename, r_settings);

            //쓰기
            StringWriter sw = new StringWriter();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;   //XML선언    
            XmlWriter xwriter = XmlWriter.Create(sw, settings);

            xwriter.WriteNode(xreader, false);
            
            xwriter.Close();
            xreader.Close();

            return sw.ToString();
        }
    
    
        //Form2
        //노드 형식 확인(p32) - 특성 읽기 불가!
        public static string Sample5(string filename)
        {
            XmlReader reader = XmlReader.Create(filename);
            reader.MoveToContent();

            string string_xml = string.Empty;
            while(reader.Read())
            {
                string_xml += reader.NodeType + "\t" +
                    reader.Name + "\t" + reader.Value + "\r\n";
            }
            reader.Close();

            return string_xml;
        }
    }
}
