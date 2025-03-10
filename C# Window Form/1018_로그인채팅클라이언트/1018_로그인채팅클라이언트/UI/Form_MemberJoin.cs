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
    public partial class Form_MemberJoin : Form
    {
        #region 컨트롤과 연결된 속성
        public string Id
        {
            get { return txt_id.Text; }
            set { txt_id.Text = value; }
        }

        public string Password
        {
            get { return txt_pw.Text; }
            set { txt_pw.Text = value; }
        }

        public string MemberName
        {
            get { return txt_name.Text; }
            set { txt_name.Text = value; }
        }

        #endregion

        public Form_MemberJoin()
        {
            InitializeComponent();
        }
    }
}
