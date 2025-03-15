using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /* 단일 선택
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(openFileDialog1.FileName + "를 선택했습니다.");
            }
        }
        //*/

        //* 복수 선택
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Title = "파일을 잽싸게 고르셔";
            openFileDialog1.Filter = "텍스트 파일|*.txt|모든 파일|*.*";
            openFileDialog1.ShowReadOnly = true;
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openFileDialog1.FileNames)
                {
                    MessageBox.Show(file + "를 선택했습니다.");
                }
            }
        }
        //*/
    }
}