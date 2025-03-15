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
    public partial class DialogCreate : Form
    {
        public DialogCreate()
        {
            InitializeComponent();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length == 0)
            {
                MessageBox.Show("방 이름을 입력해주세요.");
                return;
            }

            ClientController.instance.ServerSend(Packet.RoomMake(ClientController.id, textBoxName.Text) );
            Close();
        }
    }
}
