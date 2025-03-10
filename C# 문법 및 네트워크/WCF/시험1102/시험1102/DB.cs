using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 시험1102
{
    internal class DB
    {
        private SqlConnection scon = null;
        const string constr =
            @"Data Source=220.90.179.75\SQLEXPRESS;Initial Catalog=WB38;User Id=aaa;Password=1234";
 
        private SqlDataReader reader = null;

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
        public bool insertqury(Exam data)
        {
            try
            {
                string qury =
                    string.Format(
                        "insert into EXam(id, name, phone, gender) " +
                        "values(@ID,@Name,@Phone, @Gender);");

                SqlCommand cmd = scon.CreateCommand();
                // sql command 은 sql injection에 대비해 자동으로 sql 쿼리로 변환시켜줌 

                //-- 파라미터 등록
                SqlParameter param_title = new SqlParameter("@ID", data.ID);
                cmd.Parameters.Add(param_title);

                SqlParameter param_link = new SqlParameter("@Name", data.NAME);
                cmd.Parameters.Add(param_link);

                SqlParameter param_image = new SqlParameter("@Phone", data.PHONE);
                cmd.Parameters.Add(param_image);

                SqlParameter param_author = new SqlParameter("@Gender", data.Gender);
                cmd.Parameters.Add(param_author);
                cmd.CommandText = qury;
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
        }
        public bool deletedata(int id)
        {

            try
            {
                string qury =
                    string.Format("delete Exam where ID = @ID ;");


                SqlCommand cmd = scon.CreateCommand();
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

        public List<Exam> selectall()
        {
            try
            {
                string query = string.Format("select * from Exam ;");
                SqlCommand cmd = new SqlCommand(query, scon);
                List<Exam> datas = new List<Exam>();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Exam exam =
                    new Exam(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                    datas.Add(exam);
                }
                reader.Close();
                return datas;
                
            }
            catch { reader.Close(); return null; }
            finally { reader.Close(); } 

        }
        public Exam selectitem(int id)
        {
            try
            {
                string query = string.Format("select * from Exam where id = '{0}' ;", id);
                SqlCommand cmd = new SqlCommand(query, scon);

                reader = cmd.ExecuteReader();
                reader.Read();
                Exam exam =
                new Exam(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                reader.Close();
                return exam;
            }


            catch { reader.Close(); return null; }
            finally { reader.Close(); }
        }
        public bool updateitem(int id, string ph)
        {
            try
            {
                string qury = string.Format("update Exam set phone =@Phone where id = @ID");
                SqlCommand cmd = scon.CreateCommand();
             

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
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
        }
    }
}
