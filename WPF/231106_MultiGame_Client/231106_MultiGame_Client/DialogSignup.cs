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
    public partial class DialogSignup : Form
    {
        public DialogSignup()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            //회원가입 패킷 전송
            ClientController.instance.ServerSend(Packet.UserSignup(textBoxId.Text, textBoxPw.Text, textBoxName.Text));

            DialogResult = DialogResult = DialogResult.OK;
            Close();
        }
    }
}
