using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LabelPos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();
            dlg.LabelX = label1.Left;
            dlg.LabelY = label1.Top;
            dlg.LabelText = label1.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                label1.Left = dlg.LabelX;
                label1.Top = dlg.LabelY;
                label1.Text = dlg.LabelText;
            }
            dlg.Dispose();
        }
    }
}