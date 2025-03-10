using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TreeViewSelect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "대한민국")
            {
                label1.Text = "행선지를 선택하십시오.";
            }
            else
            {
                label1.Text = "부산에서 " + e.Node.Text + 
                    "(으)로 가는 표를 예매합니다.";
            }
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Text == "당진군")
            {
                label1.Text = "교통편이 존재하지 않습니다.";
                e.Cancel = true;
            }
        }
    }
}