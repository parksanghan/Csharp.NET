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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace 음료수_관리_프로그램__시험_
{
    public partial class Main : Form
    {
        private DrinkControl drink_control = DrinkControl.Singleton;
        public Main()
        {
            InitializeComponent();
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if(drink_control.Connect()==true)
            {
                this.Text = "서버연결성공";
            }
            else
            {
                this.Text = "서버연결실패";
            }
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            this.Text = "서버연결종료상태";
            drink_control.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string myname = textBox1.Text;
            string d_name = textBox2.Text;
            int price = int.Parse(textBox3.Text);
            drink_control.InsertDrink(myname, d_name, price);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            drink_control.Form_Main = this;
        }

        private void btn_printall_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            drink_control.PrintallDrink(textBox1.Text);
        }


        private void btn_select_Click(object sender, EventArgs e)
        {
            drink_control.SelectDrink(textBox4.Text, textBox5.Text);
        }

        public void SelectPricePrint(string myname, string d_name, string price)
        {
            Action<string> del_v1 = (msg_del) => { textBox8.Text = msg_del; };
            textBox8.Invoke(del_v1, new object[] { myname });

            Action<string> del_v2 = (msg_del) => { textBox7.Text = msg_del; };
            textBox7.Invoke(del_v2, new object[] { d_name });

            Action<string> del_v3 = (msg_del) => { textBox9.Text = msg_del; };
            textBox9.Invoke(del_v3, new object[] { price });
        }


        public void PrintallDrink()
        {
            if (listBox1.InvokeRequired)
            {
                listBox1.Invoke(new MethodInvoker(PrintallDrink)); // 비동기요규할때 동기로 함수실행 
            }
            else
            {
                foreach (Drink drink in drink_control.Drinks)
                {
                    listBox1.Items.Add(drink.ToString());
                }
            }

        }

        #region 라벨출력기능
        public void insertDrink_Label(string msg)
        {
            Action<string> del = (msg_del) => { label5.Text = msg_del; };
            label5.Invoke(del, new object[] { msg });
            Thread th = new Thread(new ThreadStart( ()=>
                {

                    label5.Text = msg;
            }));
        }

        public void PrintallDrinkLabel()
        {
            label6.Text = string.Format("저장된음료 수 : {0}",drink_control.Drinks.Count);
        }

        public void SelectDrink_Label(string msg)
        {
            Action<string> del = (msg_del) => { label7.Text = msg_del; };
            label7.Invoke(del, new object[] { msg });
        }

        public void Update_Delete_Label(string msg)
        {
            Action<string> del = (msg_del) => { label11.Text = msg_del; };
            label11.Invoke(del, new object[] { msg });
        }


        #endregion

        private void btn_update_Click(object sender, EventArgs e)
        {
            drink_control.UpdateDrink(textBox8.Text, textBox7.Text, int.Parse(textBox9.Text));
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            drink_control.DeleteDrink(textBox8.Text, textBox7.Text);
        }
    }
}
