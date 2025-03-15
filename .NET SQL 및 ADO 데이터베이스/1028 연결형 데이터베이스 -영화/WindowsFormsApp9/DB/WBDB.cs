using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp9
{
    internal class WBDB
    {
        public SqlConnection scon = null;
        const string constr
            =
            @"Data Source=LAPTOP-70HTDOU4\SQLEXPRESS;Initial Catalog=WB38; User ID=aaa;Password=1234";
        public WBDB() { }
        SqlDataReader reader = null;
        public bool coonnect()
        {
            try
            {

                scon = new SqlConnection();
                scon.ConnectionString = constr;
                scon.Open();
                return true;
            }
            catch { return false; }
        }

        public bool disconnect()
        {
            try
            {
                scon.Close();
                return true;
            }
            catch { return false; }
        }
        #region insert
        public bool InsertDBMovie(Movie mov)
        {
            try
            {
                string qury = string.Format
                    ("insert into Movies values ('{0}','{1}',{2},'{3}','{4}','{5}' );",
                    mov.title, mov.release.Date.ToString("yyyy-MM-dd"),
                    mov.timemin, mov.descrition, mov.imagepath, mov.linker);
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
        #endregion

        //  ,[Title]
        //,[ReleaseDate]
        //,[DurationMinutes]
        //,[Description]
        //,[PhotoPath]
        //,[TrailerLink]
        #region update
        public bool updateDBMovie(Movie mov)
        {
            try
            {
                string query = string.Format("update Movies set " +
                    "ReleaseDate = '{0}'," +
                    " DurationMinutes = {1}," +
                    "Description = '{2}'," +
                    "PhotoPath = {3}," +
                    "TrailerLink = {4},");
                if (ExecCommand(query))
                    return true;
                return false;
            }
            catch
            {
                return false;

            }
        }
        //this.title = title;
        //    this.release = appear;
                
        //    this.timemin = timeminoute;
        //    this.descrition = dsec;
        //        this.imagepath = Imageload;
        //    this.linker = linker;
        public Movie selectnameup(string name)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = scon;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText  = string.Format
                    ("select * from Movies where Title like '{0}';",name);
                Movie movie = null;
                  reader = cmd.ExecuteReader(); // 여기서 쿼리문전송
                while (reader.Read())
                {
                    string title = reader[0].ToString();
                    DateTime release = DateTime.Parse(reader[1].ToString());
                    int runtime = int.Parse(reader[2].ToString());
                    string script = reader[3].ToString();
                    string imagepath = reader[4].ToString();
                    string linker = reader[5].ToString();
                    movie = (new Movie(title, release, runtime, script, imagepath,
                        linker));
                }
                reader.Close();
                return movie;
                    
            }
            catch(Exception e) { MessageBox.Show(e.Message);return null; }
        }
        public List<Movie> selectallDB()
        {
            try
            {
                List<Movie> movies = new List<Movie>(); 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = scon;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = string.Format
                    ("select * from Movies ;" );
                Movie movie = null;
                SqlDataReader reader = cmd.ExecuteReader(); // 여기서 쿼리문전송
                while (reader.Read())
                {
                    string title = reader[0].ToString();
                    DateTime release = DateTime.Parse(reader[1].ToString());
                    int runtime = int.Parse(reader[2].ToString());
                    string script = reader[3].ToString();
                    string imagepath = reader[4].ToString();
                    string linker = reader[5].ToString();
                    movie = (new Movie(title, release, runtime, script, imagepath,
                        linker));
                    movies.Add(movie);
                }
                reader.Close();
                return movies;

            }
            catch (Exception e) { MessageBox.Show(e.Message); return null; }
        }
        #endregion
        public bool deletemovieDB(string title)
        {
            try
            {
                string query = string.Format("delete from Movies where Title like '{0}';"
                   , title);
                if (ExecCommand(query) == true)
                {
                    return true;
                }
                return false;

            }
            catch(Exception e) {MessageBox.Show(e.Message); return false; }
        }   
        #region DELETEDB


        #endregion
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
