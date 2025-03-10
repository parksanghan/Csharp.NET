namespace FileFind
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
            this.lbl_Dir = new System.Windows.Forms.Label();
            this.txt_Dir = new System.Windows.Forms.TextBox();
            this.btn_Path = new System.Windows.Forms.Button();
            this.txt_Filename = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.lst_View = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lbl_Dir
            // 
            this.lbl_Dir.AutoSize = true;
            this.lbl_Dir.Location = new System.Drawing.Point(27, 26);
            this.lbl_Dir.Name = "lbl_Dir";
            this.lbl_Dir.Size = new System.Drawing.Size(93, 12);
            this.lbl_Dir.TabIndex = 0;
            this.lbl_Dir.Text = "검색할 디렉토리";
            // 
            // txt_Dir
            // 
            this.txt_Dir.Location = new System.Drawing.Point(132, 20);
            this.txt_Dir.Name = "txt_Dir";
            this.txt_Dir.Size = new System.Drawing.Size(237, 21);
            this.txt_Dir.TabIndex = 1;
            this.txt_Dir.Text = "C:\\";
            // 
            // btn_Path
            // 
            this.btn_Path.Location = new System.Drawing.Point(375, 22);
            this.btn_Path.Name = "btn_Path";
            this.btn_Path.Size = new System.Drawing.Size(53, 21);
            this.btn_Path.TabIndex = 2;
            this.btn_Path.Text = "경로";
            this.btn_Path.UseVisualStyleBackColor = true;
            this.btn_Path.Click += new System.EventHandler(this.btn_Path_Click);
            // 
            // txt_Filename
            // 
            this.txt_Filename.Location = new System.Drawing.Point(29, 245);
            this.txt_Filename.Name = "txt_Filename";
            this.txt_Filename.Size = new System.Drawing.Size(261, 21);
            this.txt_Filename.TabIndex = 4;
            this.txt_Filename.Text = "*.*";
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(296, 245);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(131, 21);
            this.btn_Search.TabIndex = 5;
            this.btn_Search.Text = "검색";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // lst_View
            // 
            this.lst_View.Location = new System.Drawing.Point(31, 54);
            this.lst_View.Name = "lst_View";
            this.lst_View.Size = new System.Drawing.Size(396, 180);
            this.lst_View.TabIndex = 6;
            this.lst_View.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 292);
            this.Controls.Add(this.lst_View);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.txt_Filename);
            this.Controls.Add(this.btn_Path);
            this.Controls.Add(this.txt_Dir);
            this.Controls.Add(this.lbl_Dir);
            this.Name = "Form1";
            this.Text = "파일 검색";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Dir;
        private System.Windows.Forms.TextBox txt_Dir;
        private System.Windows.Forms.Button btn_Path;
        private System.Windows.Forms.TextBox txt_Filename;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.ListView lst_View;
    }
}

