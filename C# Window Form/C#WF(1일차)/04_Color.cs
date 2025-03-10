using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1016_WindowForm1
{
    public partial class _02_Color : Form
    {
        public _02_Color()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;
            SolidBrush br = new SolidBrush(this.ForeColor);
            Font font = new Font("돋음", 20);
            grfx.DrawString("글자색 변경", font, br, 10, 70);
        }

        //통합
        private void button1_Click(object sender, EventArgs e)
        {
            //색상다이얼로그(미리제공)
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.ShowHelp = true;

            if ((Button)sender == button1)  // 배경색 변경
            {
                colorDlg.Color = this.BackColor;
                if (colorDlg.ShowDialog() == DialogResult.OK)
                    this.BackColor = colorDlg.Color;
            }
            else  // 전경색 변경
            {
                colorDlg.Color = this.ForeColor;
                if (colorDlg.ShowDialog() == DialogResult.OK)
                    this.ForeColor = colorDlg.Color;
            }
        }

        //서로 다르게
        private void button3_Click(object sender, EventArgs e)
        {
            //색상다이얼로그(미리제공)
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.ShowHelp = true;

            colorDlg.Color = this.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
               this.BackColor = colorDlg.Color;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //색상다이얼로그(미리제공)
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.ShowHelp = true;

            colorDlg.Color = this.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
                this.ForeColor = colorDlg.Color;
        }
    }
}
