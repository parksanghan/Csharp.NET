using _231026_비트고급반_극장판_폭풍을_부르는_이미지랑_동영상_DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1026_진짜실습
{
    public partial class Delete : Form
    {
        public Delete()
        {
            InitializeComponent();

            ListUpdate();
        }





        private void ListUpdate()
        {
            List<Movie> movies = Manager.instance.SelectAllMovie();
            if (movies == null) MessageBox.Show("SelectAllMovie 실패");

            listBoxMovies.Items.Clear();
            foreach (Movie movie in     movies)
            {
                listBoxMovies.Items.Add(movie);
            }

        }


        private void ListSelectedChanged(object sender, EventArgs e)
        {
            if (listBoxMovies.SelectedItem == null) return;
            string rawString = listBoxMovies.SelectedItem.ToString();
            string movName = rawString.Split('\t')[0];

            if (MessageBox.Show(movName + "를(을) 삭제하시겠습니까?", "확인", MessageBoxButtons.OKCancel) == DialogResult.OK)
                Manager.instance.DeleteMovie(movName);
        }



    }




}
