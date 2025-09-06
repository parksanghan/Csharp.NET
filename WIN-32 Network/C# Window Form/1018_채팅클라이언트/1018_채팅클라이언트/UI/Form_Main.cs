using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _1018_채팅클라이언트
{
    public partial class Form_Main : Form
    {
        private Control _control = Control.Instance;

        #region 속성과 컨트롤 연결
        public string NickName
        {
            get { return txt_nickname.Text; }
            set { txt_nickname.Text = value; }
        }

        public string Message
        {
            get { return txt_send.Text; }
            set { txt_send.Text = value; }
        }
        #endregion 

        public Form_Main()
        {
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            _control.FormMain = this;   //자신을 전달!!!!!

            if (_control.Load() == true)
                this.Text = "서버연결";
            else
                MessageBox.Show("서버연결실패");
        }

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            _control.FormClosed();
        }

        #region 메시지 전송(전송버튼) --> 수신
        private void btn_send_Click(object sender, EventArgs e)
        {
            _control.ShortMessage(NickName, Message);
        }

        public void ShortMessageAck(string nickname, string msg)
        {
            string temp = string.Format("[{0}] {1} ({2})",
            nickname, msg, DateTime.Now.ToShortTimeString());
            string temp1 = temp += "\n\n";
            if (txt_view.InvokeRequired)
            {
                Action<string> dell = (msg1) => { txt_view.Text = msg1; };
                txt_view.Invoke(dell, new object[] {temp1});
            }
        }

        }

        #endregion 
    }
 
