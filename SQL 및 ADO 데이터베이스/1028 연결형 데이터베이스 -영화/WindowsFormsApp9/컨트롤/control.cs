using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    internal class control
    {
        const string constr =
 
        @"Data Source=LAPTOP-70HTDOU4\SQLEXPRESS;Initial Catalog=WB38; User ID=aaa;Password=1234";
        
        public static control instance;
        static control()
        {
            instance = new control();
        }
        private control() // private 생성자를 통해 외부에서 인스턴스 생성을 막음
        {
        }
        public WBDB db = new WBDB();
        public bool connect()
        {
            try
            {

                if (db.coonnect() == true) return true;
                return false;
            }
            catch { return false; }
        }
        public bool close()
        {
            try
            {
                db.disconnect();
                return true;
            }
            catch { return false; }

        }
        #region  insert 부분
        public string GetImageroot()
        {
            OpenFileDialog file = new OpenFileDialog();

            if (file.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return file.FileName;
        }
        public bool InsertMovie(Movie mov)
        {
            return db.InsertDBMovie(mov);
        }

        #endregion
        #region update 
        public bool updatemovie(Movie mov)
        {
            return db.updateDBMovie(mov);
        }
        public Movie selectmovies(string title)
        {
            return db.selectnameup(title);
        }
        public List<Movie> selectall()
        {
            return db.selectallDB();
        }
        #endregion

        #region  DELETE
        public bool deletemovie(string title)
        {
            return db.deletemovieDB(title);
        }
        #endregion
    }
}
