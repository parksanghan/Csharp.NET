using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 시험1102
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            con.form = this;
        }
        Control con = Control.Singleton;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (con.Load() == true) this.Text = "연결성공";
            else this.Text = "연결실패";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            con.FormClosed(); MessageBox.Show("DB연결해제");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text.ToString());
            string name = textBox2.Text;
            string ph = textBox3.Text;
            string gen = textBox4.Text;
            Exam data = new Exam(id, name, ph, gen);
            if (con.insertdata(data) == true)
            {
                MessageBox.Show("저장성공");
                List<Exam> datas = con.selectall();
                Action clearList = () => { listBox1.Items.Clear(); };
                listBox1.Invoke(clearList);
                foreach (Exam ddd in datas)
                {
                    Action<Exam> addItem = (item) => { listBox1.Items.Add(item); };
                    listBox1.Invoke(addItem, ddd);
                }
            }
            else
            {
                MessageBox.Show("저장실패");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text.ToString());
            if (con.deletedata(id) == true)
            {
                MessageBox.Show("삭제성공");
                List<Exam> datas = con.selectall();
                Action clearList = () => { listBox1.Items.Clear(); };
                listBox1.Invoke(clearList);
                foreach (Exam ddd in datas)
                {
                    Action<Exam> addItem = (item) => { listBox1.Items.Add(item); };
                    listBox1.Invoke(addItem, ddd);
                }
            }
            else
            {
                MessageBox.Show("삭제실패");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Exam> datas = con.selectall();
            listBox1.Items.Clear();
            foreach(Exam data in datas)
            {
                listBox1.Items.Add(data);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            Exam data = con.selectitem(id);
            if (con.selectitem(id) != null)
            {
                MessageBox.Show("검색성공");
                Action clearList = () => { listBox1.Items.Clear(); };
                listBox1.Invoke(clearList);
                Action<Exam> addItem = (item) => { listBox1.Items.Add(item); };
                listBox1.Invoke(addItem, data);
            }
            else
            {
                MessageBox.Show("검색 실패");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox5.Text);
            string ph = textBox6.Text;
            Exam data = con.selectitem(id);
            if (con.updatedata(id, ph) == true)
            {
                MessageBox.Show("업데이트 성공");
                Action clearList = () => { listBox1.Items.Clear(); };
                listBox1.Invoke(clearList);
                Action<Exam> addItem = (item) => { listBox1.Items.Add(item); };
                listBox1.Invoke(addItem, data);
            }
            else
            {
                MessageBox.Show("업데이트 실패");
            }
        }

        
    }
}
