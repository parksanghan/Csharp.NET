//Dlg_MemberInsert.cs
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace _1017_회원관리프로그램
{
    public partial class Dlg_MemberInsert : Form
    {
        #region 컨트롤과 연결된 속성
        public int MemberId
        {
            get { return Convert.ToInt32(textBox1.Text); }
            set { textBox1.Text = value.ToString(); }
        }

        public string MemberName
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value.ToString(); }
        }

        public string MemberPhone
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }

        public bool MemberGender
        {
            get { return radioButton1.Checked;  }
            set 
            {
                radioButton1.Checked = value;
                radioButton2.Checked = !value;
            }
        }

        public DateTime MemberDate
        {
            get { return dateTimePicker1.Value;     }
            set {  dateTimePicker1.Value = value;   }
        }

        #endregion

        public Dlg_MemberInsert()
        {
            InitializeComponent();
        }        

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            //int value1 = int.Parse(textBox1.Text);
            int value2 = Convert.ToInt32(((TextBox)sender).Text);
            if (value2 == 0)
            {
                MessageBox.Show("잘못입력");
                e.Cancel = true;
            }
         }
    }
}
