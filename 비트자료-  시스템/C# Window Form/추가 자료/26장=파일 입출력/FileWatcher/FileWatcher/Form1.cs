using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileWatcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            listBox1.Items.Add("파일이 변경되었습니다.");
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            listBox1.Items.Add("파일이 생성되었습니다.");
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            listBox1.Items.Add("파일이 삭제되었습니다.");
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            listBox1.Items.Add("파일의 이름이 바뀌었습니다.");
        }
    }
}