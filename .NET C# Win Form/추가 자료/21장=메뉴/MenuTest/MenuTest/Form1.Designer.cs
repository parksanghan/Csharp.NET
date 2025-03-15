namespace MenuTest
{
	partial class Form1
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.메뉴MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.항목1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.항목2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.종료XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.메뉴MToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(272, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// 메뉴MToolStripMenuItem
			// 
			this.메뉴MToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.항목1ToolStripMenuItem,
            this.항목2ToolStripMenuItem,
            this.종료XToolStripMenuItem});
			this.메뉴MToolStripMenuItem.Name = "메뉴MToolStripMenuItem";
			this.메뉴MToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
			this.메뉴MToolStripMenuItem.Text = "메뉴(&M)";
			// 
			// 항목1ToolStripMenuItem
			// 
			this.항목1ToolStripMenuItem.Name = "항목1ToolStripMenuItem";
			this.항목1ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.항목1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.항목1ToolStripMenuItem.Text = "항목&1";
			this.항목1ToolStripMenuItem.Click += new System.EventHandler(this.항목1ToolStripMenuItem_Click);
			// 
			// 항목2ToolStripMenuItem
			// 
			this.항목2ToolStripMenuItem.Name = "항목2ToolStripMenuItem";
			this.항목2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.항목2ToolStripMenuItem.Text = "항목&2";
			this.항목2ToolStripMenuItem.Click += new System.EventHandler(this.항목2ToolStripMenuItem_Click);
			// 
			// 종료XToolStripMenuItem
			// 
			this.종료XToolStripMenuItem.Name = "종료XToolStripMenuItem";
			this.종료XToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.종료XToolStripMenuItem.Text = "종료(&X)";
			this.종료XToolStripMenuItem.Click += new System.EventHandler(this.종료XToolStripMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(272, 143);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 메뉴MToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 항목1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 항목2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 종료XToolStripMenuItem;
	}
}

