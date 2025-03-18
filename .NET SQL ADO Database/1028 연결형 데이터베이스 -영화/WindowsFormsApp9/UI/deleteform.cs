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
    public partial class deleteform : Form
    {
       
        public deleteform()
        {
            InitializeComponent();
            selectall();
        }

        control con = control.instance;
      private void selectall()
        {
            List<Movie> movies  = con.selectall();
            listBox1.Items.Clear();
            foreach (Movie movie in movies) 
            { 
                listBox1.Items.Add(movie);  
            }
        }


        string deletename = null;

        private void button1_Click(object sender, EventArgs e) // deleteform
        {

            if (MessageBox.Show("해당 영화" + deletename + "을 삭제합니까?", "check",
              MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (con.deletemovie(deletename) == false) MessageBox.Show("fail");
                MessageBox.Show("success");
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        { 
       
            if (listBox1.SelectedItem == null) return;
            string msg = listBox1.SelectedItem.ToString();
            string[] sp = msg.Split('\t');
              deletename = sp[0];

            //  con.deletemovie(title);
        }

    }
}
