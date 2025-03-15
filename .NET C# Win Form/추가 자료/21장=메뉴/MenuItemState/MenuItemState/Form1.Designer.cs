namespace MenuItemState
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
            this.도형SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.동그라미ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.사각형ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.직선ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.빨간색ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.파란색ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.굵게ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.도형SToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(292, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MenuActivate += new System.EventHandler(this.menuStrip1_MenuActivate);
            // 
            // 도형SToolStripMenuItem
            // 
            this.도형SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.동그라미ToolStripMenuItem,
            this.사각형ToolStripMenuItem,
            this.직선ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.빨간색ToolStripMenuItem,
            this.파란색ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.굵게ToolStripMenuItem});
            this.도형SToolStripMenuItem.Name = "도형SToolStripMenuItem";
            this.도형SToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.도형SToolStripMenuItem.Text = "도형(&S)";
            // 
            // 동그라미ToolStripMenuItem
            // 
            this.동그라미ToolStripMenuItem.Name = "동그라미ToolStripMenuItem";
            this.동그라미ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.동그라미ToolStripMenuItem.Text = "동그라미";
            this.동그라미ToolStripMenuItem.Click += new System.EventHandler(this.동그라미ToolStripMenuItem_Click);
            // 
            // 사각형ToolStripMenuItem
            // 
            this.사각형ToolStripMenuItem.Name = "사각형ToolStripMenuItem";
            this.사각형ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.사각형ToolStripMenuItem.Text = "사각형";
            this.사각형ToolStripMenuItem.Click += new System.EventHandler(this.사각형ToolStripMenuItem_Click);
            // 
            // 직선ToolStripMenuItem
            // 
            this.직선ToolStripMenuItem.Name = "직선ToolStripMenuItem";
            this.직선ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.직선ToolStripMenuItem.Text = "직선";
            this.직선ToolStripMenuItem.Click += new System.EventHandler(this.직선ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // 빨간색ToolStripMenuItem
            // 
            this.빨간색ToolStripMenuItem.Name = "빨간색ToolStripMenuItem";
            this.빨간색ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.빨간색ToolStripMenuItem.Text = "빨간색";
            this.빨간색ToolStripMenuItem.Click += new System.EventHandler(this.빨간색ToolStripMenuItem_Click);
            // 
            // 파란색ToolStripMenuItem
            // 
            this.파란색ToolStripMenuItem.Name = "파란색ToolStripMenuItem";
            this.파란색ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.파란색ToolStripMenuItem.Text = "파란색";
            this.파란색ToolStripMenuItem.Click += new System.EventHandler(this.파란색ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // 굵게ToolStripMenuItem
            // 
            this.굵게ToolStripMenuItem.CheckOnClick = true;
            this.굵게ToolStripMenuItem.Name = "굵게ToolStripMenuItem";
            this.굵게ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.굵게ToolStripMenuItem.Text = "굵게";
            this.굵게ToolStripMenuItem.Click += new System.EventHandler(this.굵게ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 도형SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 동그라미ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 사각형ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 직선ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 빨간색ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 파란색ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 굵게ToolStripMenuItem;
    }
}

