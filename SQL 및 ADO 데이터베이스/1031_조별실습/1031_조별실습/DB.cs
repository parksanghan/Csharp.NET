using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _1031_조별실습
{
	internal class DB
	{
		private SqlConnection scon = null;
		const string constr =
            @"Data Source=LAPTOP-70HTDOU4\SQLEXPRESS;Initial Catalog=WB38;User Id=aaa;Password=1234";

		public SqlConnection SCon { get { return scon; } }
		public void Data_Insert(List<Data> book)
		{
			foreach(Data d  in book)
			{
				Data_Insert(d);
			}
		}
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
		private SqlDataReader reader = null;
        #endregion
		public bool Data_Update(string extitle ,Data book)
		{
			try
			{
				string qury = string.Format
					("update book set title = @Title , link = @Link," +
					"image = @Image , discount = @Discount,"
					+ "publisher = @Publisher , pudate = @Pudate , " +
					"isbn = @Isbn, description = @Description "
					+ "where title like'{0}'",extitle);
                SqlCommand cmd = SCon.CreateCommand();
                // sql c	ommand 은 sql injection에 대비해 자동으로 sql 쿼리로 변환시켜줌 
              
                //-- 파라미터 등록
                SqlParameter param_title = new SqlParameter("@Title", book.Title);
                cmd.Parameters.Add(param_title);

                SqlParameter param_link = new SqlParameter("@Link", book.Link);
                cmd.Parameters.Add(param_link);

                SqlParameter param_image = new SqlParameter("@Image", book.Image);
                cmd.Parameters.Add(param_image);

                SqlParameter param_author = new SqlParameter("@Author", book.Author);
                cmd.Parameters.Add(param_author);

                SqlParameter param_discount = new SqlParameter("@Discount", book.Discount);
                cmd.Parameters.Add(param_discount);

                SqlParameter param_publisher = new SqlParameter("@Publisher", book.Publisher);
                cmd.Parameters.Add(param_publisher);

                SqlParameter param_pubdate = new SqlParameter("@Pudate", book.Pubdate);
                cmd.Parameters.Add(param_pubdate);

                SqlParameter param_isbn = new SqlParameter("@Isbn", book.Isbn);
                cmd.Parameters.Add(param_isbn);

                SqlParameter param_description = new SqlParameter("@Description", book.Description);
                cmd.Parameters.Add(param_description);
                //

                cmd.CommandText = qury;
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
		}
        public Data Data_Select (string title)
		{
			try
			{
				string query = string.Format("select * from book where title like '{0}';",title);

                
                 
                SqlCommand cmd = new SqlCommand(query, scon);
              
                List<Data> datas = new List<Data>();
				Data data = null;

                  reader  = cmd.ExecuteReader();
				reader.Read();
                    string temp = string.Empty;
					 data =
					new Data(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
					reader[3].ToString(),int.Parse(reader[4].ToString()), reader[5].ToString(),
					reader[6].ToString(), long.Parse(reader[7].ToString()), reader[8].ToString());
                    datas.Add(data);
 
				reader.Close();

				// return datas
				return data;
            }

			catch(Exception ex) { MessageBox.Show(ex.Message); reader.Close();
				
				return null; }
			

		}

        public List<Data> Data_SelectAll()
        {
            try
            {
                string query = string.Format("select * from book ;");

                

                SqlCommand cmd = new SqlCommand(query, scon);
     
                List<Data> datas = new List<Data>();
                Data data = null;

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string temp = string.Empty;
                    data =
                   new Data(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                   reader[3].ToString(), int.Parse(reader[4].ToString()), reader[5].ToString(),
                   reader[6].ToString(), long.Parse(reader[7].ToString()), reader[8].ToString());
                    datas.Add(data);
                }
                reader.Close();

                // return datas
                return datas;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); reader.Close();

                return null;
            }


        }
        public bool Data_Delete(string title)
		{
			string qury = string.Format("delete book where title = @title ;");
            SqlCommand cmd = SCon.CreateCommand();
            // sql command 은 sql injection에 대비해 자동으로 sql 쿼리로 변환시켜줌 

            //-- 파라미터 등록
            SqlParameter param_title = new SqlParameter("@Title", title);
            cmd.Parameters.Add(param_title);
			cmd.CommandText = qury;
			if (cmd.ExecuteNonQuery() == 1)
			{
				return true;
			}
			return false;
        }
	 
		#region 메서드
		public bool Data_Insert(Data book)
		{
			try
			{
				string qury =
					string.Format(
						"insert into book(title, link, image, author, discount, publisher, pudate, isbn, description) " +
						"values(@Title, @Link, @Image, @Author, @Discount, @Publisher, @Pubdate, @Isbn, @Description);");

				SqlCommand cmd = SCon.CreateCommand(); 
				// sql command 은 sql injection에 대비해 자동으로 sql 쿼리로 변환시켜줌 

				//-- 파라미터 등록
				SqlParameter param_title = new SqlParameter("@Title", book.Title);
				cmd.Parameters.Add(param_title);

				SqlParameter param_link = new SqlParameter("@Link", book.Link);
				cmd.Parameters.Add(param_link);

                SqlParameter param_image = new SqlParameter("@Image", book.Image);
                cmd.Parameters.Add(param_image);

                SqlParameter param_author = new SqlParameter("@Author", book.Author);
                cmd.Parameters.Add(param_author);

                SqlParameter param_discount = new SqlParameter("@Discount", book.Discount);
                cmd.Parameters.Add(param_discount);

                SqlParameter param_publisher = new SqlParameter("@Publisher", book.Publisher);
                cmd.Parameters.Add(param_publisher);

                SqlParameter param_pubdate = new SqlParameter("@Pubdate", book.Pubdate);
                cmd.Parameters.Add(param_pubdate);

                SqlParameter param_isbn = new SqlParameter("@Isbn", book.Isbn);
                cmd.Parameters.Add(param_isbn);

                SqlParameter param_description = new SqlParameter("@Description", book.Description);
                cmd.Parameters.Add(param_description);
				//
	 
				 
                cmd.CommandText = qury;
				if (cmd.ExecuteNonQuery() == 1)
					return true;
				else
					return false;
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
        }
		#endregion
	}
}
