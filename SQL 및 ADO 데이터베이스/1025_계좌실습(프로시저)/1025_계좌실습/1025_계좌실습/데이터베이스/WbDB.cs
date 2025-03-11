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

        # region 기능
        public bool Account_Insert(int accid, string name, int balance)
        {
            string qury =
                    string.Format("insert into Account  values({0}, '{1}',{2})",
                    accid, name, balance);

            if (ExecCommand(qury) == true)
            {
                if (Account_IOInsert(accid, balance, 0, balance) == true)
                    return true;
                else
                {
                    string qury1 =
                        string.Format("delete from Account where accid={0};", accid);
                    ExecCommand(qury1);
                    return false;
                }
            }
            return false;
        }

        //insert into AccountIO  values(1111, 2000, 0, 2000, CURRENT_TIMESTAMP);
        //insert into AccountIO values(1111, 2000, 0, 2000, GETDATE());
        private bool Account_IOInsert(int accid, int input, int output, int balance)
        {
            string qury =
                    string.Format("insert into AccountIO values({0}, {1}, {2}, {3}, CURRENT_TIMESTAMP);",
                        accid, input, output, balance);

            return ExecCommand(qury);
        }


        //delete from Account where accid=111;
        public bool Account_Delete(int accid)
        {
            string qury =
                    string.Format("delete from Account where accid={0};",accid);

            return ExecCommand(qury);
        }

        //update Account Set balance = balance+1000 where accid = 2222;
        public bool Account_Input(int accid, int money)
        {
            string qury =
                    string.Format("update Account Set balance = balance+{0} where accid = {1};", 
                    money, accid);

            if (ExecCommand(qury) == true)
            {
                //----------------------------------------------
                string qury1 =
                    string.Format("select balance from Account where accid={0};", accid);
                SqlCommand cmd = new SqlCommand(qury1, scon);
                int balance = (int)cmd.ExecuteScalar();
                //------------------------------------------------

                if (Account_IOInsert(accid, money, 0, balance) == true)
                    return true;
                else
                {
                    string qury2=
                        string.Format("delete from Account where accid={0};", accid);
                    ExecCommand(qury2);
                    return false;
                }
            }
            return false;
        }

        public bool Account_Output(int accid, int money)
        {
            string qury =
                    string.Format("update Account Set balance = balance-{0} where accid = {1};",
                    money, accid);

            if (ExecCommand(qury) == true)
            {
                //----------------------------------------------
                string qury1 =
                    string.Format("select balance from Account where accid={0};", accid);
                SqlCommand cmd = new SqlCommand(qury1, scon);
                int balance = (int)cmd.ExecuteScalar();
                //------------------------------------------------

                if (Account_IOInsert(accid, 0, money, balance) == true)
                    return true;
                else
                {
                    string qury2 =
                        string.Format("delete from Account where accid={0};", accid);
                    ExecCommand(qury2);
                    return false;
                }
            }
            return false;
        }

        private bool ExecCommand(string sql)
        {
            try
            {
                SqlCommand cmd3 = new SqlCommand(sql, scon);
                if (cmd3.ExecuteNonQuery() == 1)
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
                string qury = string.Format("select * from Account;");
                SqlCommand cmd3 = new SqlCommand(qury, scon);

                SqlDataReader reader = cmd3.ExecuteReader();

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

        public string Account_Select(int accid)
        {
            SqlDataReader reader = null;
            try
            {
                string qury = string.Format("select * from Account where accid = {0};", 
                    accid);

                SqlCommand cmd3 = new SqlCommand(qury, scon);
                reader = cmd3.ExecuteReader();

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

        public List<string> AccountIO_SelectAll(int accid)
        {
            try
            {
                string qury = 
                    string.Format("select * from AccountIO where accid={0};",accid);
                SqlCommand cmd3 = new SqlCommand(qury, scon);

                SqlDataReader reader = cmd3.ExecuteReader();

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
