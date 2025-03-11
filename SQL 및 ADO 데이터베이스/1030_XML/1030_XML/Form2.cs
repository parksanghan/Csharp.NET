using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1030_XML
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        // 노드 형식의 확인
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text  = WbXMLSample.ReadXml("data.xml");
            textBox2.Text = WbXMLSample.Sample5("data.xml");
        }
    }
}
