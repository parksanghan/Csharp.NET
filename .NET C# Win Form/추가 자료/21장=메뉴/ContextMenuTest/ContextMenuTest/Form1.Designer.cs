namespace ContextMenuTest
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
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.빨간색ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.파란색ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.초록색ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.빨간색ToolStripMenuItem,
            this.파란색ToolStripMenuItem,
            this.초록색ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(111, 70);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// 빨간색ToolStripMenuItem
			// 
			this.빨간색ToolStripMenuItem.Name = "빨간색ToolStripMenuItem";
			this.빨간색ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.빨간색ToolStripMenuItem.Text = "빨간색";
			this.빨간색ToolStripMenuItem.Click += new System.EventHandler(this.빨간색ToolStripMenuItem_Click);
			// 
			// 파란색ToolStripMenuItem
			// 
			this.파란색ToolStripMenuItem.Name = "파란색ToolStripMenuItem";
			this.파란색ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.파란색ToolStripMenuItem.Text = "파란색";
			this.파란색ToolStripMenuItem.Click += new System.EventHandler(this.파란색ToolStripMenuItem_Click);
			// 
			// 초록색ToolStripMenuItem
			// 
			this.초록색ToolStripMenuItem.Name = "초록색ToolStripMenuItem";
			this.초록색ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.초록색ToolStripMenuItem.Text = "초록색";
			this.초록색ToolStripMenuItem.Click += new System.EventHandler(this.초록색ToolStripMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 빨간색ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 파란색ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 초록색ToolStripMenuItem;
    }
}

