using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace OwnerDrawList
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

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Pen P = new Pen(Color.Black, 2);
            P.DashStyle = (DashStyle)e.Index;
            e.DrawBackground();
            P.Color = e.ForeColor;
            e.Graphics.DrawLine(P, e.Bounds.Left + 10, e.Bounds.Top + 10, 
                e.Bounds.Right - 10, e.Bounds.Top + 10);
            e.DrawFocusRectangle();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox L = (ListBox)sender;
            Dash = (DashStyle)L.SelectedIndex;
            Invalidate();
        }
    }
}