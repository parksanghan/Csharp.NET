using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1027_메모리DB
{
    /// <summary>
    /// 비연결지향
    /// </summary>
    internal class WbDB
    {
        const string constr =
                 @"Data Source=DESKTOP-0I86BTV\SQLEXPRESS;Initial Catalog=WB38;User ID =aaa;Password=1234;";

        private SqlConnection conn = new SqlConnection(constr);
        private SqlDataAdapter ad = new SqlDataAdapter();

        private DataSet ds = new DataSet("WB38");
        public  DataTable Account_Table { get { return ds.Tables["Account"]; } }

        public WbDB()
        {            
            ad.SelectCommand = MakeSelectCommand();
            ad.InsertCommand = MakeInsertCommand();
            ad.DeleteCommand = MakeDeleteCommand();
            ad.UpdateCommand = MakeUpdateCommand();
        }

        #region 쿼리문 초기화
        private SqlCommand MakeSelectCommand()
        {
            string sql = "select * from Account";
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd;
        }
        private SqlCommand MakeInsertCommand()
        {
            string sql = "insert into Account(accid, name, balance) values(@Accid, @Name, @Balance)";
            
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Accid", SqlDbType.Int, 4, "accid");
            cmd.Parameters.Add("@Name", SqlDbType.VarChar, 20, "name");
            cmd.Parameters.Add("@Balance", SqlDbType.Int, 4, "balance");

            return cmd;
        }

        private SqlCommand MakeDeleteCommand()
        {
            string sql = "delete from Account where accid = @Accid";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Accid", SqlDbType.Int, 4, "accid");
            return cmd;
        }

        private SqlCommand MakeUpdateCommand()
        {
            string sql = "update Account set balance = @Balance where accid = @accid";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Accid", SqlDbType.Int, 4, "accid");
            cmd.Parameters.Add("@Balance", SqlDbType.Int, 4, "balance");

            return cmd;
        }

        #endregion 

        public void Fill_AccountTable()
        {
            try
            {
                ad.FillSchema(ds, SchemaType.Mapped, "Account");
                ad.Fill(ds, "Account");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Update_AccountTable()
        {
            try
            {
                ad.Update(ds, "Account");
                MessageBox.Show("Update성공");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
        public bool Account_Insert(int accid, string name, int balance)
        {
            try
            {
                DataRow dr = ds.Tables["Account"].NewRow(); //Account
                dr["AccID"] = accid;
                dr["Name"] = name;
                dr["Balance"] = balance;

                ds.Tables[0].Rows.Add(dr);
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public bool Account_Delete(int accid)
        {
            try
            {
                DataRow dr = ds.Tables["Account"].Rows.Find(accid); //********
                ds.Tables["Account"].Rows.Remove(dr);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    
        public bool Account_Update(int accid, int money)
        {
            try
            {
                DataRow dr = ds.Tables["Account"].Rows.Find(accid); //********
                dr["Balance"] = money;               
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
