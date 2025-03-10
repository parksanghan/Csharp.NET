using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RuntimeControl
{
    public partial class Form1 : Form
    {
        Button Btn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("실행중에 만든 버튼을 클릭했습니다.");
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Btn == null)
            {
                Btn = new Button();
                Btn.Location = new Point(10, 10);
                Btn.Size = new Size(150, 25);
                Btn.Name = "RunButton";
                Btn.Text = "실행중에 만든 버튼";
                Btn.Click += new EventHandler(Btn_Click);

                Controls.Add(Btn);
            }
            if (e.Button == MouseButtons.Right && Btn != null)
            {
                Controls.RemoveAt(0);
                Btn = null;
            }
        }
    }
}