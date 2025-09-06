using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace FormRegion
{
    public partial class Form1 : Form
    {
        GraphicsPath Path;
        public Form1()
        {
            InitializeComponent();
            Path = new GraphicsPath();

            Path.FillMode = FillMode.Winding;
            Path.AddEllipse(50, 30, 100, 100);
            Path.AddEllipse(20, 95, 160, 140);

            Region Rgn = new Region(Path);
            Region = Rgn;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84/*WM_NCHITTEST*/)
            {
                base.WndProc(ref m);
                if (m.Result == (IntPtr)1/*HTCLIENT*/)
                {
                    m.Result = (IntPtr)2/*HTCAPTION*/;
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}