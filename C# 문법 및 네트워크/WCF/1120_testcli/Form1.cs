using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _1120_testcli.ServiceReference1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace _1120_testcli
{
    public partial class Form1 : Form,ItestCallback
    {
        ItestClient tc;
        List<Student> students = new List<Student>();
        public Form1()
        {
            InitializeComponent();
            InstanceContext site = new InstanceContext(this);
            tc = new ItestClient(site);
        }

        #region 콜백
        public void Delete(bool b)
        {
            if (b)
            {
                MessageBox.Show("삭제 성공");
            }
            else
            {
                MessageBox.Show("삭제 실패");
            }
        }

        public void Insert(bool b)
        {
            if (b == true)
            {
                MessageBox.Show("저장성공");
            }
            else
                MessageBox.Show("저장실패");

        }

        public void Insert2(Student student)
        {
            textBox1.Text = student.Name;
            textBox2.Text = student.Phone;
            textBox3.Text = student.Gender;
        }

        public void List(Student[] students)
        {
            listBox1.Items.Clear(); // 현재 리스트 박스를 비웁니다.
            foreach (var student in students)
            {
                listBox1.Items.Add($"이름: {student.Name}, 전화번호: {student.Phone}, 성별: {student.Gender}");
            }
        }
        public void Update(bool result)
        {
            if (result)
            {
                MessageBox.Show("수정 성공");
            }
            else
            {
                MessageBox.Show("수정 실패");
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Student student = new Student()
            {
                Name = textBox1.Text,
                Phone = textBox2.Text,
                Gender = textBox3.Text
            };
            tc.test_Insert(student);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tc.test_List();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string nameToDelete = textBox4.Text;
            tc.test_Delete(nameToDelete);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedStudent = listBox1.SelectedItem.ToString();
                string[] parts = selectedStudent.Split(',');

                if (parts.Length == 3)
                {
                    textBox5.Text = parts[0].Split(':')[1].Trim();
                    textBox6.Text = parts[1].Split(':')[1].Trim();
                    textBox7.Text = parts[2].Split(':')[1].Trim();
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            string originalName = textBox5.Text; 
            string updatedPhone = textBox6.Text; 
            string updatedGender = textBox7.Text;
            tc.test_Update(originalName, updatedPhone, updatedGender);
        }


    }
}
