using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _231026_비트고급반_극장판_폭풍을_부르는_이미지랑_동영상_DB
{
    internal class WbDb
    {
        public SqlConnection scon;
        const string constr = @"Data Source=KAMOS_VICTUS\SQLEXPRESS; Initial Catalog = WB38; User ID=Kamo; Password=1234";

        #region [DB BASIC CMD]
        public bool Connect()
        {
            try
            {
                scon = new SqlConnection(constr);
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

        public bool Insert(Movie mov)
        {
            try
            {
                string qury = string.Format("insert into movie values('{0}', '{1}', {2}, '{3}', '{4}', '{5}', '{6}')",
                    mov.name, mov.genre, mov.runtime, mov.releaseTime.Date.ToString("yyyy-MM-dd HH:mm:ss"), mov.description, mov.poster, mov.videoLink);

                if (ExecCommand(qury))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool Delete(string name)
        {
            try
            {
                string qury = string.Format("delete from movie where name like '{0}'", name);
                if (ExecCommand(qury))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public List<Movie> SelectAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = scon;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = string.Format("select * from Movie");

                List<Movie> movies = new List<Movie>();
                
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["name"].ToString();
                    string genre = reader["genre"].ToString();
                    int runtime = int.Parse(reader["runtime"].ToString());
                    DateTime releaseTime = DateTime.Parse(reader["releaseTime"].ToString());
                    string descript = reader["descript"].ToString();
                    string poster = reader["poster"].ToString();
                    string videoLink = reader["videoLink"].ToString();

                    Movie movie = new Movie(name, genre, runtime, releaseTime, descript, poster, videoLink);
                    movies.Add(movie);
                }
                reader.Close();

                return movies;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public Movie Select(string nameForSel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = scon;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = string.Format("select * from movie where name like '{0}'", nameForSel);

                Movie movie = null;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["name"].ToString();
                    string genre = reader["genre"].ToString();
                    int runtime = int.Parse(reader["runtime"].ToString());
                    DateTime releaseTime = DateTime.Parse(reader["releaseTime"].ToString());
                    string descript = reader["descript"].ToString();
                    string poster = reader["poster"].ToString();
                    string videoLink = reader["videoLink"].ToString();

                    movie = new Movie(name, genre, runtime, releaseTime, descript, poster, videoLink);
                }
                reader.Close();

                return movie;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public bool Update(string nameBefore, Movie movie)
        {
            try
            {
                string qury = string.Format("update movie set " +
                                            "name = '{1}', " +
                                            "genre = '{2}', " +
                                            "runtime = {3}, " +
                                            "releaseTime = '{4}', " +
                                            "descript = '{5}', " +
                                            "poster = '{6}', " +
                                            "videoLink = '{7}' " +
                                            "where name = '{0}' ",
                                            nameBefore, movie.name, movie.genre, movie.runtime, movie.releaseTime.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                                            movie.description, movie.poster, movie.videoLink);
                
                if (ExecCommand(qury) == false)
                    throw new Exception("Update 쿼리 실행 오류");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        bool ExecCommand(string qury)
        {
            try
            {
                SqlCommand cmd = scon.CreateCommand();
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

    }
}
