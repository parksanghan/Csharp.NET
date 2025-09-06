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
    public partial class _01_Color : Form
    {
        public _01_Color()
        {            
            InitializeComponent();

            //1. 직접 코딩 : InitializeComponent() 다음에
            this.Text = "KnownColor";
            this.IsMdiContainer = true;

            //2. 속성 창을 이용해 변경요청 -> InitializeComponent()내부에 코드자동생성
        }

        //색상열거
        private void button1_Click(object sender, EventArgs e)
        {
            Array arr = System.Enum.GetValues(typeof(KnownColor));

            Color_NewForm[] frm = new Color_NewForm[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                frm[i] = new Color_NewForm(arr.GetValue(i).ToString());
                frm[i].TextLabel = arr.GetValue(i).ToString();
                //색상 문자열을 이용하여 색상 생성
                frm[i].BackColor = Color.FromName(arr.GetValue(i).ToString());
                frm[i].SetBounds(0, 0, 200, 50);
                frm[i].MdiParent = this;
                frm[i].Show();
            }
        }

        //정렬
        private void button2_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }
    }

    class Color_NewForm : Form
    {
        public string TextLabel { get; set; }

        public Color_NewForm(string textlabel)
        {
            this.Text = textlabel;
            //.....
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }


        protected override void OnPaint(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;   //BeginPaint로 획득하는 DC
            SolidBrush br = new SolidBrush(Color.Black);
            grfx.DrawString(TextLabel, this.Font, br, 10, 7);
        }
    }

}
