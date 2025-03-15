using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ColorFont
{
    public partial class Form1 : Form
    {
        private Font NowFont;
        private Color NowColor;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush S = new SolidBrush(NowColor);
            e.Graphics.DrawString("대화상자", NowFont, S, 10, 10);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NowFont = this.Font;
            NowColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = NowFont;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                NowFont = fontDialog1.Font;
                Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = NowColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                NowColor = colorDialog1.Color;
                Invalidate();
            }
        }
    }
}