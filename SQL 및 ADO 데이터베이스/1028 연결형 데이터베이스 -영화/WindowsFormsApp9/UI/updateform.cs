using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp9
{
    public partial class updateform : Form
    {
        public updateform()
        {
            InitializeComponent();
            ListUpdate();
        }
        control con = control.instance;
        private void ListUpdate()
        {
            List<Movie> movies = con.selectall();
            if (movies == null)
            {
                MessageBox.Show("Nan");

            }
            else
            {
                listBox1.Items.Clear();
                foreach (Movie mov in movies)
                {
                    listBox1.Items.Add(mov);
                }
            }


        }
        private void updateform_Load(object sender, EventArgs e)
        {

        }

        private void buttton_update_Click(object sender, EventArgs e)
        {
            Movie mov = new Movie(textBox_title.Text, dateTimePicker1.Value, int.Parse
                (textBox_runtime.Text), textBox_contents.Text, textBox_image.Text
            , textBox_linker.Text);
            if (con.updatemovie(mov))
            {
                MessageBox.Show("sucess");
            }
            else
            {
                MessageBox.Show("fail");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            string msg = listBox1.SelectedItem.ToString();
            string[] sp = msg.Split('\t');
            string title = sp[0];
            Movie mov = con.selectmovies(title); // 
            setmovie(mov); // form 정의
        }

        private void setmovie(Movie mov)
        {
            textBox_title.Text = mov.title;
            dateTimePicker1.Value = mov.release;
            textBox_runtime.Text = mov.timemin.ToString();
            textBox_contents.Text = mov.descrition;
            textBox_image.Text = mov.imagepath;
            textBox_linker.Text = mov.linker;

        }

        private void textBox_image_TextChanged(object sender, EventArgs e)
        {
            if (textBox_image.Text == null) return;
            pictureBox1.Image =  new System.Drawing.Bitmap(textBox_image.Text);

        }

        private void button_image_Click(object sender, EventArgs e)
        {
            string imageroot = con.GetImageroot();
            textBox_image.Text = imageroot;
            pictureBox1.Image = new System.Drawing.Bitmap(imageroot);
        }
    }
}
