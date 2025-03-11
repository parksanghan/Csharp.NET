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
using System.Windows.Forms.VisualStyles;

namespace _1027_메모리DB
{
    public partial class Form2 : Form
    {
       private DataSet ds = new DataSet("WB38"); // 여기에 그럼 Account의 테이블이 있음 
        private const string constr =
            @"Data Source=LAPTOP-70HTDOU4\SQLEXPRESS;Initial Catalog=WB38;User ID =aaa;Password=1234;";
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {    SqlConnection scon = new SqlConnection(constr);
             SqlDataAdapter ad = new SqlDataAdapter(); // 비연결형 데이터 베이스 어뎁터 
            string sq = "select *  from Account";
            ad.SelectCommand = new SqlCommand(sq,scon);
            // select에 대한 커맨드로      sqlcommand를 넣어줌 
            ad.Fill(ds,"Account");   // dataset 에 account 테이블을 채워준다.


            ad.FillSchema(ds, SchemaType.Mapped, "Account"); // 스키마 매핑 

            ad.Dispose(); // 어뎁터 닫아줌
            scon.Dispose(); // sqlconnect 닫아줌
            Table_Schema_Print(ds.Tables["Account"], listBox1) ;

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

        private void button2_Click(object sender, EventArgs e) // account io
        {
            SqlConnection scon = new SqlConnection(constr);
            SqlDataAdapter ad = new SqlDataAdapter();
            string sq = "select *  from Accountio";
            ad.SelectCommand = new SqlCommand(sq, scon);
            ad.Fill(ds, "Accountio");


            ad.FillSchema(ds, SchemaType.Mapped, "Accountio");

            ad.Dispose();
            scon.Dispose();
            Table_Schema_Print(ds.Tables["Accountio"], listBox1);

            dataGridView2.DataSource = ds.Tables["Accountio"];

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.SelectedItem.ToString();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                string name = textBox1.Text;
                int balance = int.Parse(textBox2.Text);
               // DateTime dt = monthCalendar1.SelectionStart;
//

                DataRow dr = ds.Tables["Account"].NewRow();
                dr["Accid"] = 111234;   // 실제 물리적 db에 있는 열이름에 넣어줌?
                dr["Name"] = name;
                dr["Balance"] = balance;
              //  dr["NewDate"] = dt;

                ds.Tables["Account"].Rows.Add(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // account table 의 adapter 
        private void button4_Click(object sender, EventArgs e) // updaate   
        {    // 메모리에서 변경된 것을 서버 sql 로 올려줌 => 서버 꺼져있으면 안댐
            //실제 db 를 변경
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter ad = new SqlDataAdapter();
            string sql = "insert into Account values (@Accid , @name ,@balance);";
            SqlCommand cmd = new SqlCommand(sql, con);
            
            cmd.Parameters.Add("@Accid", SqlDbType.Int,4,"accid");
            cmd.Parameters.Add("@name", SqlDbType.VarChar,20,"name");
            cmd.Parameters.Add("@balance", SqlDbType.Int,4,"balance");
            ad.InsertCommand = cmd; 
            
            

            ad.Update(ds, "Account");
            ad.Dispose();
            con.Dispose();
        }
 
    }
}
