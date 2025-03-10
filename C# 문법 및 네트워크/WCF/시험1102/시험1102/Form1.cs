using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 시험1102.ServiceReference1;
namespace 시험1102
{
    public partial class Form1 : Form
    {
        ControlServiceClient client = new ControlServiceClient();
        public Form1()
        {
            
            InitializeComponent();
            //con.form = this;
            
        }
       // Control con = Control.Singleton;
        private void Form1_Load(object sender, EventArgs e)
        {
            
            if (client.Load()== true) this.Text = "연결성공";
            else this.Text = "연결실패";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.FormClosed(); MessageBox.Show("DB연결해제");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text.ToString());
            string name = textBox2.Text;
            string ph = textBox3.Text;
            string gen = textBox4.Text;
            
            bool reuslt= client.insecrtdata(id, name, ph, gen);
            string message = reuslt ? "저장성공" : "저장실패";
            MessageBox.Show(message);
            if (reuslt == true)
            {
                StringToList();
            }
            //string packet= Packet.insert(data);

            //if (con.insertdata(data) == true)
            //{
            //    MessageBox.Show("저장성공");
            //    List<Exam> datas = con.selectall();
            //    Action clearList = () => { listBox1.Items.Clear(); };
            //    listBox1.Invoke(clearList);
            //    foreach (Exam ddd in datas)
            //    {
            //        Action<Exam> addItem = (item) => { listBox1.Items.Add(item); };
            //        listBox1.Invoke(addItem, ddd);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("저장실패");
            //}
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text.ToString());

            string packet= Packet.delete(id);

            bool reuslt =client.deletedata(id);
            string message = reuslt ? "삭제 성공" : "삭제 실패";
            MessageBox.Show(message); 
            if (reuslt == true)
            {
                StringToList();
            }
             
            //if (con.deletedata(id) == true)
            //{
            //    MessageBox.Show("삭제성공");
            //    List<Exam> datas = con.selectall();
            //    Action clearList = () => { listBox1.Items.Clear(); };
            //    listBox1.Invoke(clearList);
            //    foreach (Exam ddd in datas)
            //    {
            //        Action<Exam> addItem = (item) => { listBox1.Items.Add(item); };
            //        listBox1.Invoke(addItem, ddd);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("삭제실패");
            //}
        }
        private void StringToList()
        {
            string msg = client.selectall();
            string[] sp = msg.Split('@');
            List<Exam> exams = new List<Exam>();
            foreach (string d in sp)
            {
                if (d == string.Empty) continue;
                string[] spp = d.Split('#');
             
                int id = int.Parse(spp[0].ToString()) ;
                string name = spp[1];
                string ph = spp[2];
                string gen = spp[3];
                Exam exam = new Exam(id, name, ph, gen);
                exams.Add(exam);
            }
            Action clearList = () => { listBox1.Items.Clear(); };
            listBox1.Invoke(clearList);
            foreach (Exam data in exams)
            {
                Action<Exam> addItem = (item) => { listBox1.Items.Add(item); };
                listBox1.Invoke(addItem, data);
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            StringToList();

            //List<Exam> datas = con.selectall();
            //listBox1.Items.Clear();
            //foreach(Exam data in datas)
            //{
            //    listBox1.Items.Add(data);
            //}
        }
        private bool StringToExam(int id)
        {
            try
            {
                string msg = client.selectitem(id);
                string[] spp = msg.Split('#');
                Exam data = null;
                foreach (string d in spp)
                {
                    if (d == string.Empty) continue;

                    int idd = int.Parse(spp[0].ToString()) ;
                    string name = spp[1];
                    string ph = spp[2];
                    string gen = spp[3];
                    data = new Exam(idd, name, ph, gen);

                }
                Action clearList = () => { listBox1.Items.Clear(); };
                listBox1.Invoke(clearList);

                Action<Exam> addItem = (item) => { listBox1.Items.Add(item); };
                listBox1.Invoke(addItem, data);
                return true;
            }
            catch(Exception e) { MessageBox.Show(e.Message); return false; }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            bool reuslt = StringToExam(id);
            string message = reuslt ? "검색성공" : "검색실패";
            MessageBox.Show(message);
            //Exam data = con.selectitem(id);
            //if (con.selectitem(id) != null)
            //{
            //    MessageBox.Show("검색성공");
            //    Action clearList = () => { listBox1.Items.Clear(); };
            //    listBox1.Invoke(clearList);
            //    Action<Exam> addItem = (item) => { listBox1.Items.Add(item); };
            //    listBox1.Invoke(addItem, data);
            //}
            //else
            //{
            //    MessageBox.Show("검색 실패");
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox5.Text);
            string ph = textBox6.Text;
            bool result=client.updatedata(id, ph);
            string message = result ? "수정성공" : "수정실패";
            MessageBox.Show(message);
            StringToList();
            //Exam data = con.selectitem(id);
            //if (con.updatedata(id, ph) == true)
            //{
            //    MessageBox.Show("업데이트 성공");
            //    Action clearList = () => { listBox1.Items.Clear(); };
            //    listBox1.Invoke(clearList);
            //    Action<Exam> addItem = (item) => { listBox1.Items.Add(item); };
            //    listBox1.Invoke(addItem, data);
            //}
            //else
            //{
            //    MessageBox.Show("업데이트 실패");
            //}
        }

        
    }
}
