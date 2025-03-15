namespace MDIForm
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
            this.파일FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.새파일NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.닫기CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.창WToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.계단식정렬CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.수평바둑판정렬HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.수직바둑판정렬VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일FToolStripMenuItem,
            this.창WToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(576, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일FToolStripMenuItem
            // 
            this.파일FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.새파일NToolStripMenuItem,
            this.닫기CToolStripMenuItem});
            this.파일FToolStripMenuItem.Name = "파일FToolStripMenuItem";
            this.파일FToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.파일FToolStripMenuItem.Text = "파일(&F)";
            // 
            // 새파일NToolStripMenuItem
            // 
            this.새파일NToolStripMenuItem.Name = "새파일NToolStripMenuItem";
            this.새파일NToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.새파일NToolStripMenuItem.Text = "새 파일(&N)";
            this.새파일NToolStripMenuItem.Click += new System.EventHandler(this.새파일NToolStripMenuItem_Click);
            // 
            // 닫기CToolStripMenuItem
            // 
            this.닫기CToolStripMenuItem.Name = "닫기CToolStripMenuItem";
            this.닫기CToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.닫기CToolStripMenuItem.Text = "닫기(&C)";
            this.닫기CToolStripMenuItem.Click += new System.EventHandler(this.닫기CToolStripMenuItem_Click);
            // 
            // 창WToolStripMenuItem
            // 
            this.창WToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.계단식정렬CToolStripMenuItem,
            this.수평바둑판정렬HToolStripMenuItem,
            this.수직바둑판정렬VToolStripMenuItem});
            this.창WToolStripMenuItem.Name = "창WToolStripMenuItem";
            this.창WToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.창WToolStripMenuItem.Text = "창(&W)";
            // 
            // 계단식정렬CToolStripMenuItem
            // 
            this.계단식정렬CToolStripMenuItem.Name = "계단식정렬CToolStripMenuItem";
            this.계단식정렬CToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.계단식정렬CToolStripMenuItem.Text = "계단식 정렬(&C)";
            this.계단식정렬CToolStripMenuItem.Click += new System.EventHandler(this.계단식정렬CToolStripMenuItem_Click);
            // 
            // 수평바둑판정렬HToolStripMenuItem
            // 
            this.수평바둑판정렬HToolStripMenuItem.Name = "수평바둑판정렬HToolStripMenuItem";
            this.수평바둑판정렬HToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.수평바둑판정렬HToolStripMenuItem.Text = "수평 바둑판 정렬(&H)";
            this.수평바둑판정렬HToolStripMenuItem.Click += new System.EventHandler(this.수평바둑판정렬HToolStripMenuItem_Click);
            // 
            // 수직바둑판정렬VToolStripMenuItem
            // 
            this.수직바둑판정렬VToolStripMenuItem.Name = "수직바둑판정렬VToolStripMenuItem";
            this.수직바둑판정렬VToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.수직바둑판정렬VToolStripMenuItem.Text = "수직 바둑판 정렬(&V)";
            this.수직바둑판정렬VToolStripMenuItem.Click += new System.EventHandler(this.수직바둑판정렬VToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 417);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
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
        private System.Windows.Forms.ToolStripMenuItem 파일FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 새파일NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 닫기CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 창WToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 계단식정렬CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 수평바둑판정렬HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 수직바둑판정렬VToolStripMenuItem;
    }
}

