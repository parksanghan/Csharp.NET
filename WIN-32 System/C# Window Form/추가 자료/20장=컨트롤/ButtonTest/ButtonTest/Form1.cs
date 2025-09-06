using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ButtonTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void MyButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("버튼을 클릭했습니다.");
		}
	}
}
