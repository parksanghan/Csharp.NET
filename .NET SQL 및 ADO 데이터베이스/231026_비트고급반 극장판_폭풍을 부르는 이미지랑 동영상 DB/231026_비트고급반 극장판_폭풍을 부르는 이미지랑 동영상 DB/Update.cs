using _231026_비트고급반_극장판_폭풍을_부르는_이미지랑_동영상_DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1026_진짜실습
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
            ListUpdate();
        }
   

        private void ListUpdate()
        {
            List<Movie> movies = Manager.instance.SelectAllMovie();
            if (movies == null) MessageBox.Show("SelectAllMovie 실패");

            listBoxMovies.Items.Clear();
            foreach (Movie movie in movies) 
            {
                listBoxMovies.Items.Add(movie);
            }

        }

        private void ListSelectedChanged(object sender, EventArgs e)
        {
            if (listBoxMovies.SelectedItem == null) return;
            string rawString = listBoxMovies.SelectedItem.ToString();
            string movName = rawString.Split('\t')[0];

            Movie movie = Manager.instance.SelectMovie(movName);

            SetByMovie(movie);
        }
         
        private void SetByMovie(Movie movie)
        {
            textBoxName.Text = movie.name;
            textBoxGenre.Text = movie.genre;
            textBoxRuntime.Text = movie.runtime.ToString();
            dateTimePickerRelease.Value = movie.releaseTime;
            textBoxDescription.Text = movie.description;
            textBoxPosterPath.Text = movie.poster;
            textBoxVideoLink.Text = movie.videoLink;
        }

        private void textBoxPosterPath_TextChanged(object sender, EventArgs e)
        {
            UpdatePoster(textBoxPosterPath.Text);
        }

        private void UpdatePoster(string root = null)
        {
            pictureBoxPoster.ImageLocation = root == null? textBoxPosterPath.Text : root;
        }

        private void buttonInsertPoster_Click(object sender, EventArgs e)
        {
            string root = Manager.instance.GetImageFromUser();

            textBoxPosterPath.Text = root;
        }

        private void buttonMovieUpdate_Click(object sender, EventArgs e)
        {

            string rawString = listBoxMovies.SelectedItem.ToString();
            string movName = rawString.Split('\t')[0];


            string name = textBoxName.Text;
            string genre = textBoxGenre.Text;
            int runtTime = int.Parse(textBoxRuntime.Text);
            DateTime realeaseTime = dateTimePickerRelease.Value;
            string descript = textBoxDescription.Text;
            string poster = textBoxPosterPath.Text;
            string videoLink = textBoxVideoLink.Text;

            Movie movie = new Movie(name, genre, runtTime, realeaseTime, descript, poster, videoLink);

            Manager.instance.UpdateMovie(movName, movie);
        }

        private void Update_Load(object sender, EventArgs e)
        {

        }
    }
}
