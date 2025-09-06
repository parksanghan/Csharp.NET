namespace ImageViewer
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
            this.btn_Minus = new System.Windows.Forms.Button();
            this.btn_Plus = new System.Windows.Forms.Button();
            this.m_SmallPanel = new System.Windows.Forms.Panel();
            this.menuItem_Exit = new System.Windows.Forms.MenuItem();
            this.m_OriginalPanel = new System.Windows.Forms.Panel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem_FileOpen = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // btn_Minus
            // 
            this.btn_Minus.Location = new System.Drawing.Point(488, 257);
            this.btn_Minus.Name = "btn_Minus";
            this.btn_Minus.Size = new System.Drawing.Size(48, 24);
            this.btn_Minus.TabIndex = 7;
            this.btn_Minus.Text = "-";
            this.btn_Minus.Click += new System.EventHandler(this.btn_Minus_Click);
            // 
            // btn_Plus
            // 
            this.btn_Plus.Location = new System.Drawing.Point(408, 257);
            this.btn_Plus.Name = "btn_Plus";
            this.btn_Plus.Size = new System.Drawing.Size(48, 24);
            this.btn_Plus.TabIndex = 6;
            this.btn_Plus.Text = "+";
            this.btn_Plus.Click += new System.EventHandler(this.btn_Plus_Click);
            // 
            // m_SmallPanel
            // 
            this.m_SmallPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_SmallPanel.Location = new System.Drawing.Point(342, 27);
            this.m_SmallPanel.Name = "m_SmallPanel";
            this.m_SmallPanel.Size = new System.Drawing.Size(256, 224);
            this.m_SmallPanel.TabIndex = 5;
            this.m_SmallPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.m_SmallPanel_Paint);
            // 
            // menuItem_Exit
            // 
            this.menuItem_Exit.Index = 1;
            this.menuItem_Exit.Text = "종료";
            this.menuItem_Exit.Click += new System.EventHandler(this.menuItem_Exit_Click);
            // 
            // m_OriginalPanel
            // 
            this.m_OriginalPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_OriginalPanel.Location = new System.Drawing.Point(8, 17);
            this.m_OriginalPanel.Name = "m_OriginalPanel";
            this.m_OriginalPanel.Size = new System.Drawing.Size(328, 272);
            this.m_OriginalPanel.TabIndex = 4;
            this.m_OriginalPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_OriginalPanel_MouseDown);
            this.m_OriginalPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_OriginalPanel_MouseMove);
            this.m_OriginalPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.m_OriginalPanel_Paint);
            this.m_OriginalPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_OriginalPanel_MouseUp);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_FileOpen,
            this.menuItem_Exit});
            // 
            // menuItem_FileOpen
            // 
            this.menuItem_FileOpen.Index = 0;
            this.menuItem_FileOpen.Text = "파일 열기";
            this.menuItem_FileOpen.Click += new System.EventHandler(this.menuItem_FileOpen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 307);
            this.Controls.Add(this.btn_Minus);
            this.Controls.Add(this.btn_Plus);
            this.Controls.Add(this.m_SmallPanel);
            this.Controls.Add(this.m_OriginalPanel);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "이미지 뷰어 1.0";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Minus;
        private System.Windows.Forms.Button btn_Plus;
        private System.Windows.Forms.Panel m_SmallPanel;
        private System.Windows.Forms.MenuItem menuItem_Exit;
        private System.Windows.Forms.Panel m_OriginalPanel;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem_FileOpen;
    }
}

