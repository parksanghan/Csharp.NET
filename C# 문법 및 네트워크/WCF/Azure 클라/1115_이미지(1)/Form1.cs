// Form1.cs
using _1115_이미지_1_.ServiceReference1;
using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;

namespace _1115_이미지_1_
{
    public partial class Form1 : Form 
    {
        private Control con = Control.singleton;

        public Form1()
        {
            InitializeComponent();
            con.fm = this;
        }

        #region 연결처리
        private void Form1_Load(object sender, EventArgs e)
        {
            con.Load();
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
        }
        #endregion

        #region 버튼 핸들러
        //이미지 불러오기
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;

            string image_file = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image_file = dialog.FileName;
            }

            pictureBox1.ImageLocation = image_file;

            textBox1.Text = image_file;
        }

        //이미지 분석(업로드)
        private void button2_Click(object sender, EventArgs e)
        {
            string imageFilePath = pictureBox1.ImageLocation;

            if (!string.IsNullOrEmpty(imageFilePath))
            {
                using (FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytePic = new byte[fileStream.Length];
                    fileStream.Read(bytePic, 0, bytePic.Length);

                    con.Analyze(imageFilePath, bytePic);
                }
            }
        }

        //텍스트 추출
        private void button3_Click(object sender, EventArgs e)
        {
            string imageFilePath = pictureBox1.ImageLocation;

            if (!string.IsNullOrEmpty(imageFilePath))
            {
                using (FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytePic = new byte[fileStream.Length];
                    fileStream.Read(bytePic, 0, bytePic.Length);

                    con.ExtractText(imageFilePath, bytePic);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CarmeraForm dlg = new CarmeraForm();

            dlg.ShowDialog();
        }
        #endregion

        #region list selected
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                int selectedIndex = listBox1.SelectedIndices[0]; // 선택한 항목의 인덱스
                if (selectedIndex != -1)
                {
                    string filename = listBox1.SelectedItem.ToString();

                    con.GetSelectItem(filename);
                }
                else
                {
                    MessageBox.Show("선택실패");
                }
            }
        }
        #endregion
    }
}
