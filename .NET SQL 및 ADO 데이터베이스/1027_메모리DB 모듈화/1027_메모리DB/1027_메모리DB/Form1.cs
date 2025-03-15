using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1027_메모리DB
{
    public partial class Form1 : Form
    {
        private DataTable acount_Table = null;
        private DataView acount_View = null;

        public Form1()
        {
            InitializeComponent();
        }

        #region Table 
        // Account Table생성
        private void button1_Click(object sender, EventArgs e)
        {
            acount_Table = new DataTable("Account");

            DataColumn dc_accid = new DataColumn("AccID", typeof(int));
            dc_accid.AutoIncrement      = true;
            dc_accid.AutoIncrementSeed  = 1000;
            dc_accid.AutoIncrementStep  = 10;
            acount_Table.Columns.Add(dc_accid);

            DataColumn dc_name = new DataColumn("Name", typeof(string));
            dc_name.AllowDBNull = false;
            acount_Table.Columns.Add(dc_name);

            DataColumn dc_balance = new DataColumn("Balance", typeof(int));
            dc_balance.AllowDBNull  = false;
            dc_balance.DefaultValue = 0;
            acount_Table.Columns.Add(dc_balance);

            DataColumn dc_date = new DataColumn("NewDate", typeof(DateTime));
            dc_date.AllowDBNull     = false;
            dc_date.DefaultValue    = DateTime.Now;
            acount_Table.Columns.Add(dc_date);

            DataColumn[] dc = new DataColumn[1];
            dc[0] = dc_accid;
            acount_Table.PrimaryKey = dc;

            Table_Schema_Print(acount_Table, listBox1);

            dataGridView1.DataSource = acount_Table; //****
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
                listbox.Items.Add("  " + dc.ColumnName + "\t" +  dc.DataType);
            }
        }

        //Account 계좌 생성
        private void button2_Click(object sender, EventArgs e)
        {   
            try
            {
                string name = textBox1.Text;
                int balance = int.Parse(textBox2.Text);
                DateTime dt = monthCalendar1.SelectionStart;
               DataRow ddd= acount_Table.NewRow();
                ddd["Name"] = name;
                ddd["Balance"]= balance;
                ddd["NewData"] = dt;
                acount_Table.Rows.Add(ddd); 

                DataRow dr = acount_Table.NewRow();
                dr["Name"] = name;
                dr["Balance"] = balance;
                dr["NewDate"] = dt;

                acount_Table.Rows.Add(dr);

                DataRow row = acount_Table.NewRow();
                row["Name"] = name;
                row["balance"] = balance;
                row["newdate"] = dt;
                acount_Table.Rows.Add(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //XmlWrite
        private void button3_Click(object sender, EventArgs e)
        {   
            acount_Table.WriteXmlSchema("account.xsd", true);
            acount_Table.WriteXml("accounts.xml", true);
            MessageBox.Show("XML저장");
           
        }

        //XmlRead
        private void button4_Click(object sender, EventArgs e)
        {
            acount_Table = new DataTable("Account");
            acount_Table.ReadXmlSchema("account.xsd");
            acount_Table.ReadXml("accounts.xml");

            Table_Schema_Print(acount_Table, listBox1);
            dataGridView1.DataSource = acount_Table;

        }

        //검색
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int accid = int.Parse(textBox3.Text);
                DataRow dr = acount_Table.Rows.Find(accid); //********

                textBox4.Text = dr["Name"].ToString();
                textBox5.Text = dr["Balance"].ToString();
                monthCalendar2.SelectionStart = DateTime.Parse(dr["NewDate"].ToString());
                monthCalendar2.SelectionEnd  = DateTime.Parse(dr["NewDate"].ToString());

                //----DataGridView에 출력----------------------------
                
                DataTable temp_Table = new DataTable("Account");
                temp_Table.ReadXmlSchema("account.xsd"); //정의만 담음 
                DataRow temp_row = temp_Table.NewRow();
                for (int i = 0; i < temp_Table.Columns.Count; i++)
                    temp_row[i] = dr[i];
                temp_Table.Rows.Add(temp_row);
                dataGridView2.DataSource = temp_Table;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //수정
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int accid = int.Parse(textBox3.Text);
                DataRow dr = acount_Table.Rows.Find(accid); //********

                dr["Name"]     = textBox4.Text;
                dr["Balance"]  = int.Parse(textBox5.Text);
                dr["NewDate"]  = monthCalendar2.SelectionStart;

                MessageBox.Show("수정완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //삭제
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int accid = int.Parse(textBox3.Text);
                DataRow dr = acount_Table.Rows.Find(accid); //********

                acount_Table.Rows.Remove(dr);

                MessageBox.Show("삭제완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        //View생성[원본이 변경되면 View도 변경됨]
        private void button9_Click(object sender, EventArgs e)
        {
            acount_View =
                //new DataView(acount_Table);
                new DataView(acount_Table, "Name='홍길동'", "AccID", DataViewRowState.CurrentRows);
               //new DataView(acount_Table, "AccID=1000", "AccID", DataViewRowState.CurrentRows);
               
            dataGridView3.DataSource = acount_View;
        }

        //View Insert -> 원본?
        private void button8_Click(object sender, EventArgs e)
        {   
            try
            {
                string name = textBox6.Text;
                int balance = int.Parse(textBox7.Text);
                DateTime dt = monthCalendar3.SelectionStart;


                DataRowView dr = acount_View.AddNew();
                dr["Name"] = name;
                dr["Balance"] = balance;
                dr["NewDate"] = dt;
                dr.EndEdit();

                //acount_Table.Rows.Add(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
