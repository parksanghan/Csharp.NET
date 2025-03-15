using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1018_로그인채팅클라이언트
{
    public partial class Form_Main : Form
    {
        private Control _control = Control.Instance;

        public Form_Main()
        {
            InitializeComponent();
        }

        #region 버튼 핸들러
        
        private void btn_login_Click(object sender, EventArgs e)
        {
            string id = txt_id.Text;
            string pw = txt_pw.Text;

            _control.LogIn(id, pw);
        }

        private void btn_memberjoin_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form_MemberJoin dlg = new Form_MemberJoin();
            //부모->자식
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string id = dlg.Id;
                string pw = dlg.Password;
                string name = dlg.MemberName;

                _control.MemberJoin(id, pw, name);                
            }

            this.Show();
        }

        #endregion

        private void Form_Main_Load(object sender, EventArgs e)
        {
            if( _control.Load() == true)
            {
                this.Text = "서버 연결";
            }
            else
            {
                MessageBox.Show("서버 연결 실패");
            }
        }

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            _control.FormClosed();
        }
    }
}
