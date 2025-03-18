using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureService
{
    internal class Database
    {
        private SqlConnection con = null;

        private const string constr 
            = @"Data Source=DESKTOP-FPJBV6K\SQLEXPRESS;Initial Catalog=WB38;User ID = aaa;Password=1234;";

        #region 싱글톤
        public static Database Singleton { get; private set; } = null;
        static Database()
        {
            Singleton = new Database();

        }

        private Database()
        {

        }
        #endregion

        #region 연결처리
        public bool Connect()
        {
            try
            {
                con = new SqlConnection();
                con.ConnectionString = constr;
                con.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool Close()
        {
            try
            {
                con.Close(); //연결 닫기

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
        #endregion

        private bool ExecCommand(SqlCommand comm)
        {
            try
            {
                if (comm.ExecuteNonQuery() != -1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #region 쿼리문(프로시저)
        public bool Picture_Insert(string name, string msg, string tags)
        {
            string qury = "PICTURE_INSERT";

            SqlCommand cmd = new SqlCommand(qury, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //--
            SqlParameter param_name = new SqlParameter("@NAME", name);
            cmd.Parameters.Add(param_name);

            SqlParameter param_msg = new SqlParameter("@MSG", msg);
            cmd.Parameters.Add(param_msg);

            SqlParameter param_tags = new SqlParameter("@TAGS", tags);
            cmd.Parameters.Add(param_tags);

            return ExecCommand(cmd);
        }

        public string Picture_SelectAll()
        {
            try
            {
                string qury = "PICTURE_SELECTALL";

                SqlCommand cmd = new SqlCommand(qury, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();
                string temp = string.Empty;

                while (reader.Read())   //0...N RowData를 순차적 접근
                {
                    temp += reader[0] + "#";
                    temp += reader[1] + "#";
                    temp += reader[2] + "@";
                }
                reader.Close(); //*************************************
                return temp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public string Picture_Select(string name)
        {
            SqlDataReader reader = null;
            try
            {
                string qury = "PICTURE_SELECT";

                SqlCommand cmd = new SqlCommand(qury, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //--
                SqlParameter param_name = new SqlParameter("@NAME", name);
                cmd.Parameters.Add(param_name);

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
                Console.WriteLine(ex.Message);
                return null;
            }
            finally //성공 -> 실행, 실패 -> 실행
            {
                reader.Close(); //*************************************
            }
        }
        #endregion
    }
}
