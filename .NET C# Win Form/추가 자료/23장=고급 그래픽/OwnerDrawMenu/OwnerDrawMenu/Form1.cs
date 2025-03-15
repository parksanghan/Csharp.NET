using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OwnerDrawMenu
{
    public partial class Form1 : Form
    {
        private DashStyle Dash = DashStyle.Solid;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen P = new Pen(Color.Black, 2);
            P.DashStyle = Dash;
            e.Graphics.DrawRectangle(P, 200, 20, 140, 140);
        }
    }
}