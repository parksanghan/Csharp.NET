using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MDIForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 새파일NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = new Form2();
            Child.MdiParent = this;
            Child.Show();
        }

        private void 닫기CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Child = ActiveMdiChild;
            if (Child != null)
            {
                Child.Close();
            }
        }

        private void 계단식정렬CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 수평바둑판정렬HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void 수직바둑판정렬VToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
    }
}