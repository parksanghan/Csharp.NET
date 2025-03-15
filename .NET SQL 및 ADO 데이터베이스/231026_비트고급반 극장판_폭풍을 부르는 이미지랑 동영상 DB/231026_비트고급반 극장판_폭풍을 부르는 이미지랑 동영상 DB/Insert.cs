using _231026_비트고급반_극장판_폭풍을_부르는_이미지랑_동영상_DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1026_진짜실습
{
    public partial class Insert : Form
    {
        public Insert()
        {
            InitializeComponent();
        }

        private void buttonInsertPoster_Click(object sender, EventArgs e)
        {
            string root = Manager.instance.GetImageFromUser();

            textBoxPosterPath.Text = root;
        }

        private void buttonMovieInsert_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string genre = textBoxGenre.Text;
            int runtTime = int.Parse(textBoxRuntime.Text);
            DateTime realeaseTime = dateTimePickerRelease.Value;
            string descript = textBoxDescription.Text;
            string poster = textBoxPosterPath.Text;
            string videoLink = textBoxVideoLink.Text;

            Movie movie = new Movie(name, genre, runtTime, realeaseTime, descript, poster, videoLink);

            MessageBox.Show( Manager.instance.InsertMovie(movie)? "저장 성공":"저장 실패");
        }

        private void textBoxPosterPath_TextChanged(object sender, EventArgs e)
        {
            UpdatePoster(textBoxPosterPath.Text);
        }

        private void UpdatePoster(string root)
        {
            pictureBoxPoster.ImageLocation = root;
        }

    }
}
