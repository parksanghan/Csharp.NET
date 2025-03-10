using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 서버
{
    internal class Database:IDatabase
    {
        public SqlConnection scon = null;
        public const string constr =
            @"Data Source=LAPTOP-70HTDOU4\SQLEXPRESS;Initial Catalog=WB38;User Id=aaa;Password=1234";
        public SqlConnection SCon { get { return scon; } }
        public SqlDataReader reader = null;
        static public Database Singleton { get; private set; }
        static Database()
        {
            Singleton = new Database();
        }
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool insertqury(int id , string name ,string ph , string gender)
        {
            try
            {
                string qury =
                    string.Format(
                        "insert into EXam(id, name, phone, gender) " +
                        "values(@ID,@Name,@Phone, @Gender);");

                SqlCommand cmd = SCon.CreateCommand();
                // sql command 은 sql injection에 대비해 자동으로 sql 쿼리로 변환시켜줌 

                //-- 파라미터 등록
                SqlParameter param_title = new SqlParameter("@ID", id);
                cmd.Parameters.Add(param_title);

                SqlParameter param_link = new SqlParameter("@Name", name);
                cmd.Parameters.Add(param_link);

                SqlParameter param_image = new SqlParameter("@Phone", ph);
                cmd.Parameters.Add(param_image);

                SqlParameter param_author = new SqlParameter("@Gender", gender);
                cmd.Parameters.Add(param_author);
                cmd.CommandText = qury;
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
        public bool deletedata(int id)
        {

            try
            {
                string qury =
                    string.Format("delete Exam where ID = @ID ;");


                SqlCommand cmd = SCon.CreateCommand();
                SqlParameter param_title = new SqlParameter("@ID", id);
                cmd.Parameters.Add(param_title);
                cmd.CommandText = qury;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public string selectall()
        {
            try
            {
                string query = string.Format("select * from Exam ;");
                SqlCommand cmd = new SqlCommand(query, scon);
                string messages = string.Empty;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    messages += reader[0] + "#";
                    messages += reader[1] + "#";
                    messages += reader[2] + "#";
                    messages += reader[3] + "@";



                }
                reader.Close();
                return messages;

            }
            catch { reader.Close(); return null; }
            finally { reader.Close(); }

        }
        public string selectitem(int id)
        {
            try
            {
                string query = string.Format("select * from Exam where id = '{0}' ;", id);
                SqlCommand cmd = new SqlCommand(query, scon);

                reader = cmd.ExecuteReader();
                reader.Read();
                string message = string.Empty;
                message += reader[0] + "#";
                message += reader[1] + "#";
                message += reader[2] + "#";
                message += reader[3];
                reader.Close();
                return message;
            }


            catch { reader.Close(); return null; }
            finally { reader.Close(); }
        }
        public bool updateitem(int id, string ph)
        {
            try
            {
                string qury = string.Format("update Exam set phone =@Phone where id = @ID");
                SqlCommand cmd = SCon.CreateCommand();


                SqlParameter param_link = new SqlParameter("@ID", id);
                cmd.Parameters.Add(param_link);
                SqlParameter param_title = new SqlParameter("@Phone", ph);
                cmd.Parameters.Add(param_title);
                cmd.CommandText = qury;
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
    }
}
