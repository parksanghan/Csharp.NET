using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Client.ServiceReference1;

namespace Client
{
    public partial class Form1 : Form
    {
        private PictureClient pic = new PictureClient();

        private Image picImage = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void 그림목록보기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicListForm plf = new PicListForm();
            // 새로운 그림을 선택하면
            if (plf.ShowDialog(this) == DialogResult.OK)
            {
                picImage = Image.FromStream(new MemoryStream(pic.GetPicture(plf.SelectedPic)));
                ClientSize = picImage.Size; // 이미지 크기와 창크기를 맞춘다.
                Text = "<파일명 : " + plf.SelectedPic + "> PictureService에서 제공받은 그림파일을 보여주는 클라이언트";
                Invalidate();   // 화면을 갱신한다.
            }
        }

        private void 그림업로드하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
			// 읽어오는 스트림클래스를 선언
			Stream readStream;

			// 파일열기	대화상자를 생성
			OpenFileDialog dlg = new OpenFileDialog();

			// 확장자를 제한한다.
			dlg.Filter = "그림파일 (*.bmp;*.jpg;*.gif;*.jpeg;*.png;*.tiff)|*.bmp;*.jpg;*.gif;*.jpeg;*.png;*.tiff)";
			dlg.RestoreDirectory = true;    // 현재 디렉토리를 저장해놓는다.

			// OK 버튼을 누르면
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if ((readStream = dlg.OpenFile()) != null)
				{
					byte[] bytePic;
					BinaryReader picReader = new BinaryReader(readStream);
					bytePic = picReader.ReadBytes(Convert.ToInt32(readStream.Length));
					FileInfo fi = new FileInfo(dlg.FileName);

					// 업로드 서비스 요청
					if (pic.UploadPicture(fi.Name, bytePic))
					{
						MessageBox.Show("업로드 성공");
					}
					else
					{
						MessageBox.Show("업로드 실패");
					}
					readStream.Close();
				}
			}
		}

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
			if (picImage == null)
			{
				return;
			}
			Graphics g = e.Graphics;

			// 이미지 파일을 화면에 그린다.
			g.DrawImage(picImage, ClientRectangle);
		}
    }
}
