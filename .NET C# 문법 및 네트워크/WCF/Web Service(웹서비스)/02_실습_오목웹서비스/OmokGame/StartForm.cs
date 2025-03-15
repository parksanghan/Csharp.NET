using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OmokGame
{
	/// <summary>
	/// 시작시 누가 먼저 할 것인지를 결정하기 위한 폼
	/// </summary>
	public class StartForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StartForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			label1.Text = @"인공지능 웹 서비스 오목 게임 in C# v1.5 by aquicker

이 프로그램은 웹 서비스를 받아야하는 오목 게임 입니다.
컴퓨터가 두는 최적의 점을 알기 위해 
오목 인공지능 웹 서비스에게 요청하도록 되어 있습니다.
그러므로, 인터넷에 연결되어 있어야 합니다.";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.label1.Location = new System.Drawing.Point(10, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(340, 80);
			this.label1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.button1.Location = new System.Drawing.Point(110, 110);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(255, 40);
			this.button1.TabIndex = 4;
			this.button1.Text = "시작";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// radioButton1
			// 
			this.radioButton1.Checked = true;
			this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.radioButton1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.radioButton1.Location = new System.Drawing.Point(10, 110);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(100, 20);
			this.radioButton1.TabIndex = 2;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "사용자 먼저";
			// 
			// radioButton2
			// 
			this.radioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.radioButton2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.radioButton2.Location = new System.Drawing.Point(10, 130);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(100, 20);
			this.radioButton2.TabIndex = 3;
			this.radioButton2.Text = "컴퓨터 먼저";
			// 
			// groupBox1
			// 
			this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.groupBox1.Location = new System.Drawing.Point(5, 5);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(360, 100);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "잠깐만요!!";
			// 
			// StartForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(380, 163);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1,
																		  this.radioButton2,
																		  this.radioButton1,
																		  this.label1,
																		  this.groupBox1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "StartForm";
			this.Opacity = 0.7;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "누가 먼저 둘까요?";
			this.ResumeLayout(false);

		}
		#endregion
		
		private void button1_Click(object sender, System.EventArgs e)
		{
			if(radioButton1.Checked) DialogResult = DialogResult.Yes;
			else DialogResult = DialogResult.No;
		}

		protected override void OnClosed(System.EventArgs e)
		{
			if(radioButton1.Checked) DialogResult = DialogResult.Yes;
			else DialogResult = DialogResult.No;
		}
	}
}
