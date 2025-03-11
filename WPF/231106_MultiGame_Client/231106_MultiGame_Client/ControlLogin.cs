using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _231106_MultiGame_Client
{
    public partial class ControlLogin : UserControl
    {
        public ControlLogin()
        {
            InitializeComponent();

            Location = new Point(360, 140);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            ClientController.id = textBoxId.Text;
            ClientController.instance.ServerSend(Packet.UserLogin(textBoxId.Text, textBoxPw.Text));
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            DialogSignup dialogSignup = new DialogSignup();
            dialogSignup.ShowDialog();
        }

        private void ControlLogin_MouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
