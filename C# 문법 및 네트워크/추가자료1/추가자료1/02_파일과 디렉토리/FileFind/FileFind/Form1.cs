using System;
using System.IO;
using System.Windows.Forms;

namespace FileFind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lst_View.Clear();
            lst_View.View = View.Details;
            lst_View.LabelEdit = false;
            lst_View.CheckBoxes = true;
            lst_View.FullRowSelect = true;
            lst_View.GridLines = true;
            lst_View.Sorting = SortOrder.Ascending;
            lst_View.Columns.Add("파일명", 170, HorizontalAlignment.Left);
            lst_View.Columns.Add("파일크기", 80, HorizontalAlignment.Right);
            lst_View.Columns.Add("생성일자", 150, HorizontalAlignment.Left);

        }

        private void btn_Path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dir = new FolderBrowserDialog(); // 디렉토리 선택

            if (dir.ShowDialog() == DialogResult.OK)
            {
                this.txt_Dir.Text = dir.SelectedPath.Trim();  // 디렉토리 가져오기				
            }	
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (txt_Filename.Text != "")
            {
                lst_View.Items.Clear();
                Findfile(txt_Filename.Text);
            }
        }


        /// <summary>
        ///  str형태 파일 검색
        /// </summary>
        /// <param name="str">검색 파일명</param>
        void Findfile(string str)
        {
            string dir = txt_Dir.Text.Trim();  // 검색할 디렉토리 경로 가져오기
            if (dir == "")
            {
                MessageBox.Show("검색 디렉토리를 입력하세요.!");
                return;
            }

            string[] files_list;   // 파일 목록을 저장할 배열 
            try     // 파일 검색시 발생할지 모르는 예외처리
            {
                files_list = Directory.GetFiles(dir, str); // 파일 목록 가져오기 
                for (int i = 0; i < files_list.Length; i++) // 파일 개수만큼
                {
                    ListViewItem item1 = new ListViewItem(files_list[i], 0); //리스트뷰 초기화

                    FileInfo finfo = new FileInfo(files_list[i]);  // 파일 정보 가져오기            
                    item1.SubItems.Add(finfo.Length.ToString() + " Byte"); // 파일크기 크기,
                    item1.SubItems.Add(finfo.CreationTime.ToString());   // 생성일자 출력
                    lst_View.Items.Add(item1);
                }
            }
            catch
            {
                MessageBox.Show("파일 검색중 예외가 발생했습니다.");
            }
        }


    }
}