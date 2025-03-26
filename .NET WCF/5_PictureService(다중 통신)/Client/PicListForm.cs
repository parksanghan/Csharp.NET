using System;
using System.Windows.Forms;
using Client.ServiceReference1;


namespace Client
{
    public partial class PicListForm : Form
    {
        public string SelectedPic
        {
            get
            {
                return listBox1.SelectedItem.ToString();
            }
        }

        private PictureClient pic = new PictureClient();

        public PicListForm()
        {
            InitializeComponent();

            // 이미지 파일의 목록을 가져오는 메소드를 호출해서 문자열 배열에 저장한다.
            string[] strPicList = pic.GetPictureList();
            listBox1.DataSource = strPicList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();

        }
    }
}
