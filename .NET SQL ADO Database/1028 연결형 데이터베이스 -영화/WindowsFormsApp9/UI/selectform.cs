using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class selectform : Form
    {
        public selectform()
        {
            InitializeComponent();
            selectall();
         
        }
        control con = control.instance;

        private void button1_Click(object sender, EventArgs e)
        {
             
        }
        private void selectall()
        {
            List<Movie> movies = con.selectall();
            listBox1.Items.Clear(); 
            foreach (Movie movie in movies) 
            {
                listBox1.Items.Add(movie);
            
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string msg =listBox1.SelectedItem.ToString();
            string[] sp = msg.Split('\t');
            string title = sp[0];
            Movie mov = con.selectmovies(title);
            setMovie(mov);
            
        }
        private void setMovie(Movie mov)
        {
            textbox_title.Text = mov.title;
            dateTimePicker2.Value = mov.release;
            textBox_runtime.Text = mov.timemin.ToString();
            textBox_image.Text = mov.imagepath;
            textBox_linker.Text= mov.linker;

        }

        private void textBox_image_TextChanged(object sender, EventArgs e)
        {
            if (textBox_image.Text == null) return;
            pictureBox1.Image = new System.Drawing.Bitmap(textBox_image.Text);
 
        }
    }
}
