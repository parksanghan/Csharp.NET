using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MenuItemState
{
    public partial class Form1 : Form
    {
        private int PenWidth = 1;
        private Color InColor = Color.Red;
        enum eShape { CIRCLE, RECT, LINE };
        eShape Shape = eShape.CIRCLE;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen P = new Pen(Color.Black, PenWidth);
            SolidBrush B = new SolidBrush(InColor);

            switch (Shape)
            {
                case eShape.CIRCLE:
                    e.Graphics.FillEllipse(B, 100, 50, 100, 100);
                    e.Graphics.DrawEllipse(P, 100, 50, 100, 100);
                    break;
                case eShape.RECT:
                    e.Graphics.FillRectangle(B, 100, 50, 100, 100);
                    e.Graphics.DrawRectangle(P, 100, 50, 100, 100);
                    break;
                case eShape.LINE:
                    e.Graphics.DrawLine(P, 100, 50, 200, 150);
                    break;
            }
        }

        private void 동그라미ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shape = eShape.CIRCLE;
            Invalidate();
        }

        private void 사각형ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shape = eShape.RECT;
            Invalidate();
        }

        private void 직선ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shape = eShape.LINE;
            Invalidate();
        }

        private void 빨간색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InColor = Color.Red;
            Invalidate();
        }

        private void 파란색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InColor = Color.Blue;
            Invalidate();
        }

        private void 굵게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PenWidth == 1)
            {
                PenWidth = 5;
            }
            else
            {
                PenWidth = 1;
            }
            Invalidate();
        }

        private void menuStrip1_MenuActivate(object sender, EventArgs e)
        {
            굵게ToolStripMenuItem.Checked = (PenWidth == 5);
            동그라미ToolStripMenuItem.Checked = (Shape == eShape.CIRCLE);
            사각형ToolStripMenuItem.Checked = (Shape == eShape.RECT);
            직선ToolStripMenuItem.Checked = (Shape == eShape.LINE);

            빨간색ToolStripMenuItem.Checked = (InColor == Color.Red);
            파란색ToolStripMenuItem.Checked = (InColor == Color.Blue);

            빨간색ToolStripMenuItem.Enabled = (Shape != eShape.LINE);
            파란색ToolStripMenuItem.Enabled = (Shape != eShape.LINE);
        }
    }
}