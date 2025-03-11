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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public bool st = false ;

        control con = control.instance;
      
        private void btn_menu_Click_1(object sender, EventArgs e)
        {
            if (st == false)
            {
                btn_movieinsert.Visible = true;
                btn_moviedelete.Visible = true;
                btn_movieselect.Visible = true;
                btn_movie_update.Visible = true;
                st = true;

            }
            else if (st == true)
            {
                btn_movieinsert.Visible = false;
                btn_moviedelete.Visible = false;
                btn_movieselect.Visible = false;
                btn_movie_update.Visible = false;
                st = false;
            }

        }

        private void btn_movieinsert_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            insertform form = new insertform();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            tabControl1.TabPages[0].Controls.Add(form);
            form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            form.Show();

        }

        private void btn_movie_update_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            updateform form = new updateform();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            form.Show();
            tabPage4.Controls.Add(form);
            tabControl1.SelectedIndex = 3;
        }

        private void btn_movieselect_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            selectform form = new selectform();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            form.Show();
            tabPage3.Controls.Add(form);
            tabControl1.SelectedIndex = 2;
        }

        private void btn_moviedelete_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            deleteform form = new deleteform();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            form.Show();
            tabPage4.Controls.Add(form);
            tabControl1.SelectedIndex = 3;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (con.connect() == true)
            {
                this.Text = "성공";

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (con.close() == true) this.Text = "실패";

        }
    }
}
