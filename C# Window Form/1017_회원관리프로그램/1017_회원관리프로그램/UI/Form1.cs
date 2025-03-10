//Form1.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace _1017_회원관리프로그램
{
    public partial class Form1 : Form
    {
        private MemberControl _member = MemberControl.Singleton;

        //모달리스용 객체 선언
        private Dlg_MemberSelect _selectdlg = null;

        public Form1()
        {
            InitializeComponent();
        }

 
        #region 메뉴 이벤트 핸들러
        private void 파일저장SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _member.Closed();  //내가 원하는 파일명 입력받아서 처리
        }

        private void 파일불러오기LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _member.Load(); //내가 원하는 파일을 불러오기 싶다...
        }

        private void 프로그램종료XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //모달
        private void 회원저장IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dlg_MemberInsert dlg = new Dlg_MemberInsert();
            //부모->자식
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if(_member.Member_Insert(dlg.MemberId, dlg.MemberName,
                    dlg.MemberPhone, dlg.MemberGender, dlg.MemberDate))
                {
                    MemberPrintAll();
                }
                else
                {
                    MessageBox.Show("저장 실패");
                }
            }
        }

        //모달
        private void 회원탈퇴XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dlg_MemberDelete dlg = new Dlg_MemberDelete();
            //부모->자식
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (_member.Member_Delete(dlg.MemberId))
                {
                    MemberPrintAll();
                }
                else
                {
                    MessageBox.Show("삭제 실패");
                }
            }
        }

        //모달리스
        private void 회원검색SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectdlg == null || _selectdlg.IsDisposed)
            {
                _selectdlg = new Dlg_MemberSelect();
                _selectdlg.Owner = this;
                _selectdlg.Apply += _selectdlg_Apply;

                _selectdlg.Show();
            }
            else
            {
                _selectdlg.Focus();
            }
        }

        //모달리스 적용 핸들러
        private void _selectdlg_Apply(object sender, EventArgs e)
        {
            int id = _selectdlg.MemberId;

            int idx = _member.GetIndex(id);
            if (idx != -1)
            {
                listBox1.SelectedIndex = idx;

                Member member = _member.GetMember(idx);
                MemberSelectPrint(member);
            }
            else
            {
                listBox1.SelectedIndex = -1;
                MessageBox.Show("없는 ID");
            }
        }

        //전화번호 변경
        private void button1_Click(object sender, EventArgs e)
        {
            int memberid = int.Parse(textBox1.Text);
            string phone = textBox3.Text;

            if(_member.Member_Update(memberid, phone) == true)
            {
                MemberPrintAll(); //리스트박스 재 출력 요청!
            }
            else
            {

            }
        }

        #endregion

        private void MemberSelectPrint(Member member)
        {
            textBox1.Text = member.Id.ToString();
            textBox2.Text = member.Name;
            textBox3.Text = member.Phone;
            if(member.Gender == true)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }
            dateTimePicker1.Value = member.Date;
        }

        private void MemberPrintAll()
        {
            List<Member> members = _member.Members;

            label1.Text = String.Format("회원 수 : {0}명", members.Count);

            listBox1.Items.Clear();
            foreach(Member member in members)
            {
                listBox1.Items.Add(member);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            _member.Load();
            MemberPrintAll();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _member.Closed();
        }
    }
}
