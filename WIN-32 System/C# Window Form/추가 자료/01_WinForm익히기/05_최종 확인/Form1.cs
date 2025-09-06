using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace WinFormTotalExam
{
	/// <summary>
	/// Form1에 대한 요약 설명입니다.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox cmb_BorderStyle;
		private System.Windows.Forms.ComboBox cmb_StartPosition;
		private System.Windows.Forms.TrackBar tb_Opacity;
		private System.Windows.Forms.ComboBox cmb_FormStyle;
		private System.Windows.Forms.Button btn_NewWnd;
		private System.Windows.Forms.Label lbl_OpacityValue;
		private System.Windows.Forms.CheckBox chk_Taskbar;
		private System.Windows.Forms.CheckBox chk_TopMost;
		private System.Windows.Forms.Label lbl_FormBorderStyle;
		private System.Windows.Forms.Label lbl_StartPosition;
		private System.Windows.Forms.Label lbl_Opacity;
		private System.Windows.Forms.Label lbl_FormStyle;
		
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();			
		}

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form 디자이너에서 생성한 코드
		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
			this.lbl_FormBorderStyle = new System.Windows.Forms.Label();
			this.cmb_BorderStyle = new System.Windows.Forms.ComboBox();
			this.cmb_StartPosition = new System.Windows.Forms.ComboBox();
			this.lbl_StartPosition = new System.Windows.Forms.Label();
			this.btn_NewWnd = new System.Windows.Forms.Button();
			this.tb_Opacity = new System.Windows.Forms.TrackBar();
			this.lbl_Opacity = new System.Windows.Forms.Label();
			this.cmb_FormStyle = new System.Windows.Forms.ComboBox();
			this.lbl_FormStyle = new System.Windows.Forms.Label();
			this.lbl_OpacityValue = new System.Windows.Forms.Label();
			this.chk_Taskbar = new System.Windows.Forms.CheckBox();
			this.chk_TopMost = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.tb_Opacity)).BeginInit();
			this.SuspendLayout();
			// 
			// lbl_FormBorderStyle
			// 
			this.lbl_FormBorderStyle.Location = new System.Drawing.Point(16, 24);
			this.lbl_FormBorderStyle.Name = "lbl_FormBorderStyle";
			this.lbl_FormBorderStyle.TabIndex = 0;
			this.lbl_FormBorderStyle.Text = "FormBorderStyle";
			// 
			// cmb_BorderStyle
			// 
			this.cmb_BorderStyle.Location = new System.Drawing.Point(144, 24);
			this.cmb_BorderStyle.Name = "cmb_BorderStyle";
			this.cmb_BorderStyle.Size = new System.Drawing.Size(160, 20);
			this.cmb_BorderStyle.TabIndex = 4;
			// 
			// cmb_StartPosition
			// 
			this.cmb_StartPosition.Location = new System.Drawing.Point(144, 56);
			this.cmb_StartPosition.Name = "cmb_StartPosition";
			this.cmb_StartPosition.Size = new System.Drawing.Size(160, 20);
			this.cmb_StartPosition.TabIndex = 6;
			// 
			// lbl_StartPosition
			// 
			this.lbl_StartPosition.Location = new System.Drawing.Point(16, 56);
			this.lbl_StartPosition.Name = "lbl_StartPosition";
			this.lbl_StartPosition.TabIndex = 5;
			this.lbl_StartPosition.Text = "StartPosition";
			// 
			// btn_NewWnd
			// 
			this.btn_NewWnd.Location = new System.Drawing.Point(32, 256);
			this.btn_NewWnd.Name = "btn_NewWnd";
			this.btn_NewWnd.Size = new System.Drawing.Size(256, 48);
			this.btn_NewWnd.TabIndex = 9;
			this.btn_NewWnd.Text = "윈폼생성";
			this.btn_NewWnd.Click += new System.EventHandler(this.btn_NewWnd_Click);
			// 
			// tb_Opacity
			// 
			this.tb_Opacity.Location = new System.Drawing.Point(8, 200);
			this.tb_Opacity.Maximum = 100;
			this.tb_Opacity.Name = "tb_Opacity";
			this.tb_Opacity.Size = new System.Drawing.Size(304, 45);
			this.tb_Opacity.TabIndex = 10;
			this.tb_Opacity.Scroll += new System.EventHandler(this.tb_Opacity_Scroll);
			// 
			// lbl_Opacity
			// 
			this.lbl_Opacity.Location = new System.Drawing.Point(24, 168);
			this.lbl_Opacity.Name = "lbl_Opacity";
			this.lbl_Opacity.TabIndex = 11;
			this.lbl_Opacity.Text = "Opacity :";
			// 
			// cmb_FormStyle
			// 
			this.cmb_FormStyle.Location = new System.Drawing.Point(144, 88);
			this.cmb_FormStyle.Name = "cmb_FormStyle";
			this.cmb_FormStyle.Size = new System.Drawing.Size(160, 20);
			this.cmb_FormStyle.TabIndex = 13;
			// 
			// lbl_FormStyle
			// 
			this.lbl_FormStyle.Location = new System.Drawing.Point(16, 88);
			this.lbl_FormStyle.Name = "lbl_FormStyle";
			this.lbl_FormStyle.TabIndex = 12;
			this.lbl_FormStyle.Text = "Form Style";
			// 
			// lbl_OpacityValue
			// 
			this.lbl_OpacityValue.Location = new System.Drawing.Point(144, 168);
			this.lbl_OpacityValue.Name = "lbl_OpacityValue";
			this.lbl_OpacityValue.Size = new System.Drawing.Size(104, 24);
			this.lbl_OpacityValue.TabIndex = 14;
			this.lbl_OpacityValue.Text = "0";
			// 
			// chk_Taskbar
			// 
			this.chk_Taskbar.Location = new System.Drawing.Point(16, 120);
			this.chk_Taskbar.Name = "chk_Taskbar";
			this.chk_Taskbar.Size = new System.Drawing.Size(120, 32);
			this.chk_Taskbar.TabIndex = 15;
			this.chk_Taskbar.Text = "ShowInTaskbar";
			// 
			// chk_TopMost
			// 
			this.chk_TopMost.Location = new System.Drawing.Point(152, 128);
			this.chk_TopMost.Name = "chk_TopMost";
			this.chk_TopMost.Size = new System.Drawing.Size(144, 24);
			this.chk_TopMost.TabIndex = 16;
			this.chk_TopMost.Text = "TopMost";
			// 
			// MainWnd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(320, 310);
			this.Controls.Add(this.chk_TopMost);
			this.Controls.Add(this.chk_Taskbar);
			this.Controls.Add(this.lbl_OpacityValue);
			this.Controls.Add(this.cmb_FormStyle);
			this.Controls.Add(this.lbl_FormStyle);
			this.Controls.Add(this.lbl_Opacity);
			this.Controls.Add(this.tb_Opacity);
			this.Controls.Add(this.btn_NewWnd);
			this.Controls.Add(this.cmb_StartPosition);
			this.Controls.Add(this.lbl_StartPosition);
			this.Controls.Add(this.cmb_BorderStyle);
			this.Controls.Add(this.lbl_FormBorderStyle);
			this.Name = "MainWnd";
			this.Text = "윈폼 종합 테스트";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.tb_Opacity)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 해당 응용 프로그램의 주 진입점입니다.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		

		private void Form1_Load(object sender, System.EventArgs e)
		{
			int i;
			Array arr;
			arr = System.Enum.GetValues(typeof(FormBorderStyle));
			for(i = 0; i < arr.Length; i++)
				this.cmb_BorderStyle.Items.Add(arr.GetValue(i).ToString());

			arr = System.Enum.GetValues(typeof(FormStartPosition));
			for(i = 0; i < arr.Length; i++)
				this.cmb_StartPosition.Items.Add(arr.GetValue(i).ToString());

			
			this.cmb_FormStyle.Items.Add("모달");
			this.cmb_FormStyle.Items.Add("모달리스");
			

			this.cmb_BorderStyle.SelectedIndex = 0;
			this.cmb_StartPosition.SelectedIndex = 0;
			this.cmb_FormStyle.SelectedIndex = 0;
			this.tb_Opacity.Minimum = 0;
			this.tb_Opacity.Maximum = 100;
		}

		private void tb_Opacity_Scroll(object sender, System.EventArgs e)
		{
			this.lbl_OpacityValue.Text = this.tb_Opacity.Value.ToString();
		}

		private void btn_NewWnd_Click(object sender, System.EventArgs e)
		{
			string strInfo = null;          // 창 작성 정보 출력
			Form2 new_Form = new Form2();
			new_Form.Text = "윈폼 생성!!!";
			
			switch(this.cmb_BorderStyle.SelectedIndex)
			{
				case 0:
					new_Form.FormBorderStyle = FormBorderStyle.None;
					break;
				case 1:
					new_Form.FormBorderStyle = FormBorderStyle.FixedSingle;
					break;
				case 2:
					new_Form.FormBorderStyle = FormBorderStyle.Fixed3D;
					break;
				case 3:
					new_Form.FormBorderStyle = FormBorderStyle.FixedDialog;
					break;
				case 4:
					new_Form.FormBorderStyle = FormBorderStyle.Sizable;
					break;
				case 5:
					new_Form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
					break;
				case 6:
					new_Form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
					break;
			}

			switch(this.cmb_StartPosition.SelectedIndex)
			{
				case 0:
					new_Form.StartPosition = FormStartPosition.Manual;
					break;
				case 1:
					new_Form.StartPosition = FormStartPosition.CenterScreen;
					break;
				case 2:
					new_Form.StartPosition = FormStartPosition.WindowsDefaultLocation;
					break;
				case 3:
					new_Form.StartPosition = FormStartPosition.WindowsDefaultBounds;
					break;
				case 4:
					new_Form.StartPosition = FormStartPosition.CenterParent;
					break;
			}

			new_Form.Opacity = 1.0 - (this.tb_Opacity.Value/100.0);
			
			if(this.chk_Taskbar.Checked)
				new_Form.ShowInTaskbar = false;

			if(this.chk_TopMost.Checked)
				new_Form.TopMost = true;

			strInfo  = this.lbl_FormBorderStyle.Text + " : ";
			strInfo += this.cmb_BorderStyle.SelectedItem.ToString() + "\r\n";
			strInfo += this.lbl_StartPosition.Text + " : ";
			strInfo += this.cmb_StartPosition.SelectedItem.ToString() + "\r\n";
			strInfo += this.lbl_FormStyle.Text + " : ";
			strInfo += this.cmb_FormStyle.SelectedItem.ToString() + "\r\n";
			strInfo += this.chk_Taskbar.Text + " : ";
			strInfo += ((this.chk_Taskbar.Checked)? "false" : "true") + "\r\n";
			strInfo += this.chk_TopMost.Text + " : ";
			strInfo += ((this.chk_TopMost.Checked)? "true" : "false") + "\r\n";
			strInfo += this.lbl_Opacity.Text + " : ";
			strInfo += this.lbl_OpacityValue.Text + "\r\n";
			
			new_Form.lblInfo = strInfo;

			if(this.cmb_FormStyle.SelectedIndex == 0)
			{				
				new_Form.ShowDialog(this);	
				new_Form.Dispose();
			}			   
			else
			{
				new_Form.Show();			
			}

		}		
	}
}

