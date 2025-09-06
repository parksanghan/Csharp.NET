using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1018_로그인채팅클라이언트
{
    public partial class Form_Run : Form
    {
        public Form_Run()
        {
            InitializeComponent();
        }

        //채팅창 실행!!!!
        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("1018_채팅클라이언트.exe"); 
            //Process.Start("Explorer.exe", "https://dotnetkorea.com");
        }
    }
}
