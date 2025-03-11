using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1027_메모리DB
{
    public partial class Form3 : Form
    {
        private WbDB db = new WbDB();

        public Form3()
        {
            InitializeComponent();
        }

        //Account Table(Fill)
        private void button1_Click(object sender, EventArgs e)
        {
            db.Fill_AccountTable();

            Table_Schema_Print(db.Account_Table, listBox1);
            dataGridView1.DataSource = db.Account_Table;
        }

        //Table 스키마(Schema) 정보 출력
        private void Table_Schema_Print(DataTable dt, ListBox listbox)
        {
            listbox.Items.Add("-------------------------------------");
            listbox.Items.Add("테이블명 : " + dt.TableName);
            listbox.Items.Add("컬럼 개수: " + dt.Columns.Count);
            listbox.Items.Add("Primary Key : " + dt.PrimaryKey[0]);
            listbox.Items.Add("-------------------------------------");
            listbox.Items.Add("");
            listbox.Items.Add("데이터 컬럼");
            foreach (DataColumn dc in dt.Columns)
            {
                listbox.Items.Add("  " + dc.ColumnName + "\t" + dc.DataType);
            }
        }

        //AccountTable에(Adapter.Update)
        private void button4_Click(object sender, EventArgs e)
        {
            db.Update_AccountTable();
        }

        //insert
        private void button3_Click(object sender, EventArgs e)
        {
            int accid   = int.Parse(textBox1.Text);
            string name = textBox2.Text;
            int balance = int.Parse(textBox3.Text);

            if( db.Account_Insert(accid, name, balance) == true)
            {
                MessageBox.Show("성공");
            }                
        }

        //delete
        private void button2_Click(object sender, EventArgs e)
        {
            int accid = int.Parse(textBox6.Text);

            if (db.Account_Delete(accid) == true)
            {
                MessageBox.Show("성공");
            }
        }

        //update
        private void button5_Click(object sender, EventArgs e)
        {
            int accid = int.Parse(textBox4.Text);
            int money = int.Parse(textBox7.Text);

            if (db.Account_Update(accid, money) == true)
            {
                MessageBox.Show("성공");
            }
        }
    }
}
