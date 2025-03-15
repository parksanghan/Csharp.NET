using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    internal class Database
    {
        static private SqlConnection scon = null;
          const string constr =
          @"Data Source=LAPTOP-70HTDOU4\SQLEXPRESS;Initial Catalog=WB38;User Id=aaa;Password=1234";
        static public SqlConnection SCon { get { return scon; } }
        static private SqlDataReader reader = null;

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
          public bool insertqury(Book data)
        {
            try
            {
                string qury =
                    string.Format(
                        "insert into Books(Title, Publisher, Writer, Price) " +
                        "values(@Title,@Publisher,@Writer, @Price);");

                SqlCommand cmd = SCon.CreateCommand();
                // sql command 은 sql injection에 대비해 자동으로 sql 쿼리로 변환시켜줌 

                //-- 파라미터 등록
                SqlParameter param_title = new SqlParameter("@Title", data.Title);
                cmd.Parameters.Add(param_title);

                SqlParameter param_link = new SqlParameter("@Publisher", data.Pusblisher);
                cmd.Parameters.Add(param_link);

                SqlParameter param_writer = new SqlParameter("@Writer", data.Writer);
                cmd.Parameters.Add(param_writer);

                SqlParameter param_price = new SqlParameter("@Price", data.Price);
                cmd.Parameters.Add(param_price);
                cmd.CommandText = qury;
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
          public bool deletedata(int ID)
        {

            try
            {
                string qury =
                    string.Format("WITH NumberedRows AS " +
                    "(SELECT ROW_NUMBER() OVER (ORDER BY Title DESC) AS row_num, Title FROM Table_1)" +
                    "DELETE FROM NumberedRows WHERE row_num = @ID;");


                SqlCommand cmd = SCon.CreateCommand();
                SqlParameter param_title = new SqlParameter("@ID", ID+1);
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
          public List<Book> selectall()
        {
            try
            {
                string query = string.Format("select title,publisher,writer,price from Books ;");
                SqlCommand cmd = new SqlCommand(query, scon);
                List<Book> datas = new List<Book>();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Book exam =
                    new Book(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), int.Parse(reader[3].ToString()));
                    datas.Add(exam);
                }
                reader.Close();
                return datas;

            }
            catch { reader.Close(); return null; }
            finally { reader.Close(); }

        }
        public bool updateitem(int id, int price)
        {
            try
            {
                string qury =
                    string.Format("WITH NumberedRows AS " +
                    "(SELECT ROW_NUMBER() OVER (ORDER BY Title DESC) AS row_num, Title FROM Table_1)" +
                    "Update Books set Price= @Price NumberedRows WHERE row_num = @ID;");
                SqlCommand cmd = SCon.CreateCommand();

                
                SqlParameter param_link = new SqlParameter("@ID", id+1);
                cmd.Parameters.Add(param_link);
                SqlParameter param_title = new SqlParameter("@Price", price);
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
