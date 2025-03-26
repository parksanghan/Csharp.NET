using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _1027_메모리DB
{
    public partial class Form2 : Form
    {
        const string constr =
              @"Data Source=DESKTOP-0I86BTV\SQLEXPRESS;Initial Catalog=WB38;User ID =aaa;Password=1234;";

        private DataSet ds = new DataSet("WB38");

        public Form2()
        {
            InitializeComponent();
        }

        //Account Table(Fill)
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constr);
            SqlDataAdapter ad = new SqlDataAdapter();
            
            string sql = "select * from Account";
            ad.SelectCommand = new SqlCommand(sql, conn);
            ad.FillSchema(ds, SchemaType.Mapped, "Account");
            ad.Fill(ds, "Account");

            ad.Dispose();
            conn.Dispose();

            Table_Schema_Print(ds.Tables["Account"], listBox1);
            dataGridView1.DataSource = ds.Tables["Account"];
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

        //AccountIO Table(Adapter.Fill)
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constr);
            SqlDataAdapter ad = new SqlDataAdapter();

            string sql = "select * from AccountIO";
            ad.SelectCommand = new SqlCommand(sql, conn);
            ad.FillSchema(ds, SchemaType.Mapped, "AccountIO");
            ad.Fill(ds, "AccountIO");

            ad.Dispose();
            conn.Dispose();

            Table_Schema_Print(ds.Tables[1], listBox2);
            dataGridView2.DataSource = ds.Tables[1];
        }

        //AccountTable에 insert
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                int balance = int.Parse(textBox2.Text);
                DateTime dt = monthCalendar1.SelectionStart;

                DataRow dr = ds.Tables[0].NewRow(); //Account
                dr["AccID"] = 1234;
                dr["Name"] = name;
                dr["Balance"] = balance;
                //dr["NewDate"] = dt;

                ds.Tables[0].Rows.Add(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //AccountTable에(Adapter.Update)
        //https://learn.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqldataadapter.selectcommand?view=netframework-4.7.2
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(constr);
                SqlDataAdapter ad = new SqlDataAdapter();

                string sql = "insert into Account(accid, name, balance) values(@Accid, @Name, @Balance)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Accid", SqlDbType.Int,4, "accid");
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 20, "name");
                cmd.Parameters.Add("@Balance", SqlDbType.Int, 4, "balance");
                ad.InsertCommand = cmd;              

                //ad.FillSchema(ds, SchemaType.Mapped, "AccountIO");
                ad.Update(ds, "Account");

                ad.Dispose();
                conn.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
