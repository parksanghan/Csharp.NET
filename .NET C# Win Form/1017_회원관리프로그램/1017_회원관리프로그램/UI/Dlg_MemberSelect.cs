using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1017_회원관리프로그램
{
    public partial class Dlg_MemberSelect : Form
    {
        #region 컨트롤과 연결된 속성
        public int MemberId
        {
            get { return Convert.ToInt32(textBox1.Text); }
            set { textBox1.Text = value.ToString(); }
        }

        #endregion

        public Dlg_MemberSelect()
        {
            InitializeComponent();
        }

        //이벤트 생성[부모가 자신의 메서드를 등록]
        public event EventHandler Apply = null;

        //적용
        private void button1_Click(object sender, EventArgs e)
        {
            if (Apply != null)
            {
                //이벤트 호출[부모에게 자신의 정보를 가져갈 수 있는 시점 알림]
                Apply(this, new EventArgs());
            }
        }

        //취소
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();  //this.Close();
        }
    }
}
