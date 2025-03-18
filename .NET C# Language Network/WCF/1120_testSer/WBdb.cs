using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _1120_testSer
{
    internal class WBdb
    {
        private SqlConnection con = null;
        const string constr = @"Data Source=DESKTOP-0I86BTV\SQLEXPRESS;Initial Catalog=WB38; User ID=aaa;Password=1234";

        public SqlConnection Con { get { return con; } }

        ~WBdb() { Close(); }

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
        bool ExecCommand(string qury)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = qury;
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public bool Insert(string name, string phone, string gender)
        {
            try
            {
                string qury =
                        string.Format("insert into Student values('{0}','{1}','{2}')", name,phone,gender );

                ExecCommand(qury);

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Delete(string name)
        {
            string qury =
                    string.Format("delete from Student where Name='{0}';", name);

            return ExecCommand(qury);
        }

        public bool Update(string name,string phone,string gender)
        {
            try
            {
                string query = $"UPDATE Student SET Phone = '{phone}', Gender = '{gender}' WHERE Name = '{name}'";
                ExecCommand(query);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
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
                        Name = reader[0].ToString(),
                        Phone = reader[1].ToString(),
                        Gender = reader[2].ToString()
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

