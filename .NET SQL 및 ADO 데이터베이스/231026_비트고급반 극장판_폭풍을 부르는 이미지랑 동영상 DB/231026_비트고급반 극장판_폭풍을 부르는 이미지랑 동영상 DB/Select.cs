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
    public partial class Select : Form
    {
        public Select()
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
            UpdatePoster(); 
            PlayOnlineVideo(movie.videoLink);

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


        private void UpdatePoster(string root = null)
        {
            pictureBoxPoster.ImageLocation = root == null ? textBoxPosterPath.Text : root;
        }

        private void PlayOnlineVideo(string videoUrl)
        {
            // WebBrowser 컨트롤 생성
            WebBrowser webBrowser = new WebBrowser();

            // 부모 컨트롤에 추가
            this.Controls.Add(webBrowser);

            // 컨트롤 크기 설정
            webBrowser.Location = new Point(200, 250);
            webBrowser.Size = new System.Drawing.Size(960, 540);

            // 동영상 URL 로드
            webBrowser.Navigate(videoUrl);
        }

    }
}
