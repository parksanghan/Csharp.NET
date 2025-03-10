using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace _1120_testservice
{
    public class db
    {
        private SqlConnection con = null;
        const string constr = @"Data Source=DESKTOP-0I86BTV\SQLEXPRESS;Initial Catalog=WB38; User ID=aaa;Password=1234";

        public SqlConnection Con { get { return con; } }

        ~db() { Close(); }

        #region 서버 연결, 해제
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
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion

        public bool ExecCommand(SqlCommand cmd)
        {
            try
            {
                if (cmd.ExecuteNonQuery() == 1)
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

        public bool Insert(string name,string phone,string gender)
        {
            string qury = "INSERT INTO Student VALUES(@name,@phone,@gender)";

            SqlCommand cmd = new SqlCommand(qury, con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@gender", gender);

            return ExecCommand(cmd);
        }

        public bool Delete(string name, string phone)
        {
            string qury = "DELETE FROM Student WHERE NAME=@name AND Phone=@phone";

            SqlCommand cmd = new SqlCommand(qury, con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@phone",phone);

            return ExecCommand(cmd);
        }

        public List<Student> List()
        {
            string qury = "select * from Student";

            SqlCommand cmd = new SqlCommand(qury, con);
            SqlDataReader reader = cmd.ExecuteReader();

            try
            {
                List<Student> students = new List<Student>();
                while (reader.Read())
                {
                    Student s = new Student()
                    {
                        Name = reader[1].ToString(),
                        Phone = reader[2].ToString(),
                        Gender = reader[3].ToString()
                    };
                    students.Add(s);
                }
                return students;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally { reader.Close(); }
        }
    }
}