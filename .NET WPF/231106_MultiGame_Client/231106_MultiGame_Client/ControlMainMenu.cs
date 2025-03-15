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
    public partial class ControlMainMenu : UserControl
    {
        public ControlMainMenu()
        {
            InitializeComponent();
            Location = new Point(360, 140);


            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
            labelUserName.Text = ClientController.name + "님";
        }

        private void buttonGetIn_Click(object sender, EventArgs e)
        {
            if (listBoxIngames.SelectedItems.Count == 0) 
            {
                MessageBox.Show("먼저 방을 선택해주세요.");
                return;
            }

            string[] sp = listBoxIngames.SelectedItem.ToString().Split('(');

            ClientController.instance.ServerSend(Packet.RoomGetIn(ClientController.id, sp[0]));

        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            DialogCreate dialogCreate = new DialogCreate();
            dialogCreate.ShowDialog();
        }

        public void UpdateRoomList(List<string> rooms) 
        {
            Action<List<string>> del = (ingames) =>
            {
                listBoxIngames.Items.Clear();

                foreach (string room in ingames)
                    listBoxIngames.Items.Add(room);
            };
            listBoxIngames.Invoke(del, new object[] { rooms });
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            ClientController.instance.ServerSend(Packet.UserLogout(ClientController.id));
        }
    }
}
