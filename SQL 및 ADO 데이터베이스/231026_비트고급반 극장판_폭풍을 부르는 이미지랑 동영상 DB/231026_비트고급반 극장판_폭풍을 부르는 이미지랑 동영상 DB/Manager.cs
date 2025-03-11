using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Windows.Forms;

namespace _231026_비트고급반_극장판_폭풍을_부르는_이미지랑_동영상_DB
{
    internal class Manager
    {
        public static Manager instance;
        static Manager()
        {
            instance = new Manager();
        }
        private Manager() 
        {
            db = new WbDb();
        }
        WbDb db;

        public void Init() 
        {
            db.Connect();
        }
        public void End() 
        {
            db.Close();
        }





        public string GetImageFromUser() 
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return ofd.FileName;
        }


        public bool InsertMovie(Movie movie)
        {
            return db.Insert(movie);
        }

        public bool DeleteMovie(string movieName) 
        {
            return db.Delete(movieName);
        }

        public bool UpdateMovie(string nameBefore, Movie movie) 
        {
            return db.Update(nameBefore, movie);
        }

        public Movie SelectMovie(string movieName) 
        {
            return db.Select(movieName);
        }

        public List<Movie> SelectAllMovie()
        {
            return db.SelectAll();
        }
    }
}
