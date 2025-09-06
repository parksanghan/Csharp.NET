using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MenuTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void 항목1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("항목1을 선택했습니다.");
		}

		private void 항목2ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("항목2를 선택했습니다.");
		}

		private void 종료XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
