using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuestionDialog
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
    dlg.ShowDialog();
    if (dlg.DialogResult == DialogResult.Yes)
    {
        Text = "그럼 이제 일해";
    }
    else if (dlg.DialogResult == DialogResult.No)
    {
        Text = "빨리 밥 먹고 와";
    }
    dlg.Dispose();
}
    }
}