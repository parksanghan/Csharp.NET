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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }


        private void FormLoad(object sender, EventArgs e)
        {
            Manager.instance.Init();
        }
        private void FormClose(object sender, FormClosedEventArgs e)
        {
            Manager.instance.End();
        }

        #region Menu
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;

            Insert form = new Insert();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(form);
            form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            form.Show();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;

            Update form = new Update();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(form);
            form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;

            Select form = new Select();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(form);
            form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;

            Delete form = new Delete();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(form);
            form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            form.Show();
        }
        #endregion
    }
}
