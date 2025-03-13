//MemberControl.cs
using System;
using System.Windows.Forms;

namespace _1017_회원관리프로그램
{
    public partial class Dlg_MemberDelete : Form
    {
        #region 컨트롤과 연결된 속성

        public int MemberId
        {
            get { return Convert.ToInt32(textBox1.Text); }
            set { textBox1.Text = value.ToString(); }
        }

        #endregion

        public Dlg_MemberDelete()
        {
            InitializeComponent();
        }
    }
}
