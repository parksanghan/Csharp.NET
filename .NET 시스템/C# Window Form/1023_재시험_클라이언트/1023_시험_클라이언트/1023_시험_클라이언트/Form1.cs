using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1023_시험_클라이언트
{
    public partial class Form1 : Form
    {
        private Control _control = Control.Instance;
        public Form1()
        {
            InitializeComponent();
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _control.Form1 = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_control.Load() == true)
                this.Text = "연결상태";
            else
                this.Text = "연결실패";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Text = "서버연결종료상태";            
            _control.FormClosed();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            _control.BoardInsert(textBox1.Text, textbox_boardname.Text, textbox_price.Text, DateTime.Parse( dateTimePicker1.Value.ToShortTimeString()));
        }

        public void BoardInsert_Ack(string msg)
        {
            if (label1.InvokeRequired)
            {
                Action<string> del = (msg_del) =>
                {
                    // msg_del을 부울 값으로 파싱하여 label5.Text 업데이트
                    //label1.Text = bool.Parse(msg_del) ? "저장성공" : "저장실패";
                    if (bool.Parse(msg_del))
                    {
                        label1.Text = "저장성공";
                    }
                    else
                    {
                        label1.Text = "저장실패";
                    }
                };
                label1.Invoke(del, msg);
            }
            else
            {
                if (bool.Parse(msg) == true)
                {
                    label1.Text = "저장성공";
                }
                else
                {
                    label1.Text = "저장실패";
                }
            }           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            _control.BoardPrintAll();
            
        }
        public void BoardPrintAll_Ack()
        {
            if(listBox1.InvokeRequired)
            {
                Action del = () =>
                {
                    foreach (Board d in _control.Boards)
                    {
                        listBox1.Items.Add(d.ToString());
                    }
                    label2.Text = "저장개수 : " + _control.Boards.Count + "개";
                };
                listBox1.Invoke(del);
            }
            else
            {
                foreach(Board d in _control.Boards)
                    {
                    listBox1.Items.Add(d.ToString());
                }
                label2.Text = "저장개수 : " + _control.Boards.Count + "개";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _control.BoardSelect(textBox3.Text);
        }
        public void BoardSelect_Ack(bool b, string name, string boardname, string write, DateTime date) 
        {
            if (label1.InvokeRequired)
            {
                Action<bool> del = (msg_del) =>
                {
                    if (msg_del)
                    {
                        label3.Text = "검색성공";
                        textBox5.Text = name;
                        textBox4.Text = boardname;
                        textBox2.Text = write;
                        dateTimePicker2.Text = date.ToString();                    
                    }
                    else
                    {
                        textBox4.Clear();
                        textBox5.Clear();
                        textBox2.Clear();
                        label3.Text = "검색실패";
                    }
                };
                label1.Invoke(del, b);
            }
            else
            {
                if (b)
                {
                    label3.Text = "검색성공";
                    textBox5.Text = name;
                    textBox4.Text = boardname;
                    textBox2.Text = write;
                    dateTimePicker2.Value = date;
                }
                else
                {
                    textBox4.Clear();
                    label3.Text = "검색실패";
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            _control.BoardUpdate(textBox3.Text, textBox2.Text, DateTime.Parse( DateTime.Now.ToShortTimeString() ));
        }
        public void BoardUpdate_Ack(bool b)
        {
            if (label4.InvokeRequired)
            {
                Action<bool> del = (d) =>
                {
                    if (d == true)
                    {
                        label3.Text = "수정성공";
                    }
                    else
                    {
                        label3.Text = "수정실패";
                    }
                };
                label4.Invoke(del, b);
            }
            else
            {
                if (b == true)
                {
                    label3.Text = "수정성공";
                }
                else
                {
                    label3.Text = "수정실패";
                }
            }
        }
        private void button7_Click_1(object sender, EventArgs e)
        {
            _control.BoardDelete(textBox3.Text);
        }
        public void BoardDelete_Ack(bool b)
        {
            if (label4.InvokeRequired)
            {
                Action<bool> del = (del_b) =>
                {
                    if (del_b == true)
                    {
                        label3.Text = "삭제성공";
                    }
                    else
                    {
                        label3.Text = "삭제실패";
                    }
                };
                label4.Invoke(del, b);
            }
            else
            {
                if (b == true)
                {
                    label3.Text = "삭제성공";
                }
                else
                {
                    label3.Text = "삭제실패";
                }
            }
        }

        
    }
}
