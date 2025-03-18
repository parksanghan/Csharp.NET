//wbdb.cs
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1025_계좌실습
{
    internal class WbDB
    {
        private SqlConnection scon = null;
        private const string constr =
            @"Data Source=DESKTOP-0I86BTV\SQLEXPRESS;Initial Catalog=WB38;User ID =aaa;Password=1234;";

        #region 서버 연결, 해제
        public bool Connect()
        {
            try
            {
                scon = new SqlConnection();
                scon.ConnectionString = constr;
                scon.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                scon.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion

        #region 기능

        //@AccID int,        @Name varchar(20),     @Balance int
        public bool Account_Insert(int accid, string name, int balance)
        {
            string query = "Account_Insert";

            SqlCommand cmd = new SqlCommand(query, scon);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //--
            SqlParameter param_accid = new SqlParameter("@AccID", accid);
            param_accid.SqlDbType = System.Data.SqlDbType.Int;
            cmd.Parameters.Add(param_accid);

            SqlParameter param_name = new SqlParameter("@Name", name);
            cmd.Parameters.Add(param_name);

            SqlParameter param_balance = new SqlParameter("@Balance", balance);
            param_balance.SqlDbType = System.Data.SqlDbType.Int;
            cmd.Parameters.Add(param_balance);
            //--
            return ExecCommand(cmd);
            
        }

        //@AccID int
        public bool Account_Delete(int accid)
        {
            string query = "Account_Delete";

            SqlCommand cmd = new SqlCommand(query, scon);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //--
            SqlParameter param_accid = new SqlParameter("@AccID", accid);
            param_accid.SqlDbType = System.Data.SqlDbType.Int;
            cmd.Parameters.Add(param_accid);
             //--

            return ExecCommand(cmd);
        }

        //@AccID int,        @Money int
        public bool Account_Input(int accid, int money)
        {
            string query = "Account_Input";

            SqlCommand cmd = new SqlCommand(query, scon);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //--
            SqlParameter param_accid = new SqlParameter("@AccID", accid);
            param_accid.SqlDbType = System.Data.SqlDbType.Int;
            cmd.Parameters.Add(param_accid);

            SqlParameter param_money = new SqlParameter("@Money", money);
            param_money.SqlDbType = System.Data.SqlDbType.Int;
            cmd.Parameters.Add(param_money);
            //--
            return ExecCommand(cmd);
        }

        public bool Account_Output(int accid, int money)
        {
            string query = "Account_Output";

            SqlCommand cmd = new SqlCommand(query, scon);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //--
            SqlParameter param_accid = new SqlParameter("@AccID", accid);
            param_accid.SqlDbType = System.Data.SqlDbType.Int;
            cmd.Parameters.Add(param_accid);

            SqlParameter param_money = new SqlParameter("@Money", money);
            param_money.SqlDbType = System.Data.SqlDbType.Int;
            cmd.Parameters.Add(param_money);
            //--
            return ExecCommand(cmd);
        }

        private bool ExecCommand(SqlCommand comm)
        {
            try
            {
                if (comm.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public List<string> Account_SelectAll()
        {
            try
            {
                string qury = "Account_SelectAll";

                SqlCommand cmd = new SqlCommand(qury, scon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                List<string> accounts = new List<string>();
                while (reader.Read())   //0...N RowData를 순차적 접근
                {
                    string temp = string.Empty;
                    temp += reader[0] + "#";
                    temp += reader[1] + "#";
                    temp += reader[2];
                    accounts.Add(temp);
                }
                reader.Close(); //*************************************
                return accounts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //@AccID int
        public string Account_Select(int accid)
        {
            SqlDataReader reader = null;
            try
            {
                string qury = "Account_Select";

                SqlCommand cmd = new SqlCommand(qury, scon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //--
                SqlParameter param_accid = new SqlParameter("@AccID", accid);
                param_accid.SqlDbType = System.Data.SqlDbType.Int;
                cmd.Parameters.Add(param_accid);

                reader = cmd.ExecuteReader();

                reader.Read();
                string temp = string.Empty;
                temp += reader[0] + "#";
                temp += reader[1] + "#";
                temp += reader[2];               
                return temp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally //성공 -> 실행, 실패 -> 실행
            {
                reader.Close(); //*************************************
            }
        }

        //@AccID int
        public List<string> AccountIO_SelectAll(int accid)
        {
            try
            {
                string qury = "AccountIO_SelectAll";

                SqlCommand cmd = new SqlCommand(qury, scon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //--
                SqlParameter param_accid = new SqlParameter("@AccID", accid);
                param_accid.SqlDbType = System.Data.SqlDbType.Int;
                cmd.Parameters.Add(param_accid);
                //--

                SqlDataReader reader = cmd.ExecuteReader();

                List<string> accounts = new List<string>();
                while (reader.Read())   //0...N RowData를 순차적 접근
                {
                    string temp = string.Empty;
                    temp += reader[0] + "#";
                    temp += reader[1] + "#";
                    temp += reader[2] + "#";
                    temp += reader[3] + "#";
                    temp += reader[4] + "#";
                    temp += reader[5];
                    accounts.Add(temp);
                }
                reader.Close(); //*************************************
                return accounts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        #endregion
    }
}
