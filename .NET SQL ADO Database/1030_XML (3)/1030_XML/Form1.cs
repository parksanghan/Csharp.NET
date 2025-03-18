//Form1.cs
using System;
using System.Windows.Forms;

namespace _1030_XML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WbXMLSample.Sample1("data1.xml");
            textBox1.Text = WbXMLSample.ReadXml("data1.xml");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WbXMLSample.Sample2("data2.xml");
            textBox1.Text = WbXMLSample.ReadXml("data2.xml");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WbXMLSample.Sample3("data3.xml");
            textBox1.Text = WbXMLSample.ReadXml("data3.xml");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = WbXMLSample.Sample4("https://www.kma.go.kr/weather/forecast/mid-term-rss3.jsp?stnId=108");
        }
    }
}
