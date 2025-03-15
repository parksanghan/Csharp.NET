using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Management;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace _231106_MultiGame_Client
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            // 창을 전체 화면으로 만들기
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;


            //주사율
            int refreshRate = 144;
            Timer timer = new Timer(1024f/ refreshRate);

            timer.Elapsed += DrawMainMenu;
            timer.Start();


            ShowLoginControl();
        }

        public static Vector2 winSize = new Vector2(1500, 800);

        #region [컨트롤 제어]
        public ControlLogin loginCon = null;
        public ControlMainMenu mainMenu = null;
        public ControlIngame ingameCon = null;

        public void ShowLoginControl() 
        {
            if (loginCon != null) return;

            loginCon = new ControlLogin();
            loginCon.Paint += this.FormMain_Paint;
            this.Controls.Add(loginCon);

            DestroyMenuControl();
            DestroyIngameControl();
        }
        void DestroyLoginControl()
        {
            Controls.Remove(loginCon);
            loginCon?.Dispose();
            loginCon = null;
        }

        public void ShowMenuControl()
        {
            if (mainMenu != null) return;

            mainMenu = new ControlMainMenu();
            mainMenu.Paint += this.FormMain_Paint;
            this.Controls.Add(mainMenu);

            DestroyLoginControl();
            DestroyIngameControl();
        }
        void DestroyMenuControl()
        {
            Controls.Remove(mainMenu);
            mainMenu?.Dispose();
            mainMenu = null;
        }
        
        public void ShowIngameControl()
        {
            if (ingameCon != null) return;

            ingameCon = new ControlIngame();
            ingameCon.Paint += this.FormMain_Paint;
            this.Controls.Add(ingameCon);


            DestroyLoginControl();
            DestroyMenuControl();
        }
        void DestroyIngameControl()
        {
            Controls.Remove(ingameCon);
            ingameCon?.Dispose();
            ingameCon = null;
        }
        #endregion

        private void DrawMainMenu(object sender, ElapsedEventArgs e)
        {
            try
            {
                Action del = () =>
                {
                    Invalidate(true);
                    loginCon?.Invalidate(true);
                    mainMenu?.Invalidate(true);
                    ingameCon?.Invalidate(true);
                };
                Invoke(del);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ClientController.instance.Load(this);
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromHwnd(this.Handle);
            g.Clear(Color.White);
            g.DrawString(e.Location.ToString(), new Font("굴림", 10), new SolidBrush(Color.Black), new PointF(10, 10));
            g.Dispose();
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            //Point screenCoordinates = Cursor.Position;
            //Point formCoordinates = this.PointToClient(screenCoordinates);
            //Func<Point, Point> GetDrawPos = (pt) =>
            //{
            //   return new Point(
            //   pt.X - ((ContainerControl)sender).Location.X,
            //   pt.Y - ((ContainerControl)sender).Location.Y
            //   );
            //};

            // 실제 화면 크기 및 작업 영역 크기 가져오기
            //Rectangle workingArea = Screen.FromControl(this).WorkingArea;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //e.Graphics.Clear(Color.Silver);
            ////e.Graphics.DrawString(formCoordinates.ToString(), new Font("굴림", 10), new SolidBrush(Color.Black), GetDrawPos(formCoordinates));
        }
    }


}
