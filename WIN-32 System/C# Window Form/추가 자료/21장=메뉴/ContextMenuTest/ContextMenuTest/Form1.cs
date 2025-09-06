using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ContextMenuTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 빨간색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = Color.Red;
        }

        private void 파란색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = Color.Blue;
        }

        private void 초록색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = Color.Green;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            빨간색ToolStripMenuItem.Checked = (BackColor == Color.Red);
            파란색ToolStripMenuItem.Checked = (BackColor == Color.Blue);
            초록색ToolStripMenuItem.Checked = (BackColor == Color.Green);
        }
    }
}