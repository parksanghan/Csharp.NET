using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class insertform : Form
    {
       
        public insertform()
        {
            InitializeComponent();
        }
        control con = control.instance;
         
        private void button1_Click(object sender, EventArgs e) // 저장
        {
            Movie mov = new Movie(textBox1.Text, dateTimePicker1.Value, int.Parse(textBox3.Text),
                textBox4.Text, textBox5.Text, textBox6.Text);
            if (con.InsertMovie(mov))
            {
                MessageBox.Show("성공");

            }
            else
            {
                MessageBox.Show("실패");
            }
            

        }

        private void button2_Click(object sender, EventArgs e) // 사진선택
        {
            string imageroot = con.GetImageroot();
            textBox5.Text = imageroot;
            pictureBox1.Image = new System.Drawing.Bitmap(imageroot);
        }
    }
}
