using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

/* 이진 포매터 사용
using System.Runtime.Serialization.Formatters.Binary;
namespace Serialize
{
    public partial class Form1 : Form
    {
        Human Kim = new Human("김상형", 28);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = Kim.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"c:\Kim2.bin", FileMode.Create,
                FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, Kim);
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "파일을 읽는 중";
            label1.Refresh();
            System.Threading.Thread.Sleep(1000);
            FileStream fs = new FileStream(@"c:\Kim2.bin", FileMode.Open,
                FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            Kim = (Human)bf.Deserialize(fs);
            fs.Close();
            label1.Text = Kim.ToString();
        }
    }

    [Serializable]
    class Human
    {
        private string Name;
        private int Age;
        [NonSerialized] private float Temp;
        public Human(string aName, int aAge)
        {
            Name = aName;
            Age = aAge;
            Temp = 1.23f;
        }
        public override string ToString()
        {
            Temp += 1;
            return "이름 : " + Name + ", 나이:" + Age;
        }
    }
}
//*/

//* SOAP 포매터 사용
using System.Runtime.Serialization.Formatters.Soap;
namespace Serialize
{
    public partial class Form1 : Form
    {
        Human Kim = new Human("김상형", 28);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = Kim.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"c:\Kim3.bin", FileMode.Create,
                FileAccess.Write);
            SoapFormatter sf = new SoapFormatter();
            sf.Serialize(fs, Kim);
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "파일을 읽는 중";
            label1.Refresh();
            System.Threading.Thread.Sleep(1000);
            FileStream fs = new FileStream(@"c:\Kim3.bin", FileMode.Open,
                FileAccess.Read);
            SoapFormatter sf = new SoapFormatter();
            Kim = (Human)sf.Deserialize(fs);
            fs.Close();
            label1.Text = Kim.ToString();
        }
    }

    [Serializable]
    class Human
    {
        private string Name;
        private int Age;
        [NonSerialized]
        private float Temp;
        public Human(string aName, int aAge)
        {
            Name = aName;
            Age = aAge;
            Temp = 1.23f;
        }
        public override string ToString()
        {
            Temp += 1;
            return "이름 : " + Name + ", 나이:" + Age;
        }
    }
}
//*/
