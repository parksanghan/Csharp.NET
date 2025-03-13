using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LabelPos
{
    public partial class Form2 : Form
    {
        public int LabelX
        {
            get { return Convert.ToInt32(textBox1.Text); }
            set { textBox1.Text = value.ToString(); }
        }
        public int LabelY
        {
            get { return Convert.ToInt32(textBox2.Text); }
            set { textBox2.Text = value.ToString(); }
        }
        public string LabelText
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            if (LabelX < 0 || LabelX > 300 || LabelY < 0 || LabelY > 300)
            {
                MessageBox.Show("좌표의 범위는 0 ~ 300까지입니다.");
                DialogResult = DialogResult.None;
                return;
            }
            //*/
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            int x;

            x = Convert.ToInt32(tb.Text);
            if (x < 0 || x > 300)
            {
                MessageBox.Show("좌표의 범위는 0 ~ 300까지입니다.");
                e.Cancel = true;
            }
        }
    }
}