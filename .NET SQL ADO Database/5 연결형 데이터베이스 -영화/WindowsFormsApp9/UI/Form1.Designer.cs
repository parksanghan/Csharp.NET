namespace WindowsFormsApp9
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_menu = new System.Windows.Forms.Button();
            this.btn_movieinsert = new System.Windows.Forms.Button();
            this.btn_movie_update = new System.Windows.Forms.Button();
            this.btn_movieselect = new System.Windows.Forms.Button();
            this.btn_moviedelete = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_menu);
            this.splitContainer1.Panel1.Controls.Add(this.btn_movieinsert);
            this.splitContainer1.Panel1.Controls.Add(this.btn_movie_update);
            this.splitContainer1.Panel1.Controls.Add(this.btn_movieselect);
            this.splitContainer1.Panel1.Controls.Add(this.btn_moviedelete);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.button5);
            this.splitContainer1.Size = new System.Drawing.Size(951, 551);
            this.splitContainer1.SplitterDistance = 145;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_menu
            // 
            this.btn_menu.Location = new System.Drawing.Point(3, 21);
            this.btn_menu.Name = "btn_menu";
            this.btn_menu.Size = new System.Drawing.Size(101, 23);
            this.btn_menu.TabIndex = 17;
            this.btn_menu.Text = "메뉴";
            this.btn_menu.UseVisualStyleBackColor = true;
            this.btn_menu.Click += new System.EventHandler(this.btn_menu_Click_1);
            // 
            // btn_movieinsert
            // 
            this.btn_movieinsert.Location = new System.Drawing.Point(3, 50);
            this.btn_movieinsert.Name = "btn_movieinsert";
            this.btn_movieinsert.Size = new System.Drawing.Size(101, 23);
            this.btn_movieinsert.TabIndex = 9;
            this.btn_movieinsert.Text = "영화저장";
            this.btn_movieinsert.UseVisualStyleBackColor = true;
            this.btn_movieinsert.Visible = false;
            this.btn_movieinsert.Click += new System.EventHandler(this.btn_movieinsert_Click_1);
            // 
            // btn_movie_update
            // 
            this.btn_movie_update.Location = new System.Drawing.Point(3, 90);
            this.btn_movie_update.Name = "btn_movie_update";
            this.btn_movie_update.Size = new System.Drawing.Size(101, 23);
            this.btn_movie_update.TabIndex = 10;
            this.btn_movie_update.Text = "영화정보수정";
            this.btn_movie_update.UseVisualStyleBackColor = true;
            this.btn_movie_update.Visible = false;
            this.btn_movie_update.Click += new System.EventHandler(this.btn_movie_update_Click_1);
            // 
            // btn_movieselect
            // 
            this.btn_movieselect.Location = new System.Drawing.Point(3, 133);
            this.btn_movieselect.Name = "btn_movieselect";
            this.btn_movieselect.Size = new System.Drawing.Size(101, 23);
            this.btn_movieselect.TabIndex = 11;
            this.btn_movieselect.Text = "영화검색";
            this.btn_movieselect.UseVisualStyleBackColor = true;
            this.btn_movieselect.Visible = false;
            this.btn_movieselect.Click += new System.EventHandler(this.btn_movieselect_Click_1);
            // 
            // btn_moviedelete
            // 
            this.btn_moviedelete.Location = new System.Drawing.Point(3, 179);
            this.btn_moviedelete.Name = "btn_moviedelete";
            this.btn_moviedelete.Size = new System.Drawing.Size(101, 23);
            this.btn_moviedelete.TabIndex = 12;
            this.btn_moviedelete.Text = "영화삭제";
            this.btn_moviedelete.UseVisualStyleBackColor = true;
            this.btn_moviedelete.Visible = false;
            this.btn_moviedelete.Click += new System.EventHandler(this.btn_moviedelete_Click_1);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(644, 406);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(636, 380);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(636, 380);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(636, 380);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(636, 380);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(-33, 100);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(8, 8);
            this.button5.TabIndex = 13;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 551);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_menu;
        private System.Windows.Forms.Button btn_movieinsert;
        private System.Windows.Forms.Button btn_movie_update;
        private System.Windows.Forms.Button btn_movieselect;
        private System.Windows.Forms.Button btn_moviedelete;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button5;
    }
}

