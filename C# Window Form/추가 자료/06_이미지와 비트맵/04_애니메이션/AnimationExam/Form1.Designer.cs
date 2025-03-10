namespace AnimationExam
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
            this.btn_AutoRepeat = new System.Windows.Forms.Button();
            this.btn_MakeJPG = new System.Windows.Forms.Button();
            this.btn_Animation = new System.Windows.Forms.Button();
            this.panel_Image = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btn_AutoRepeat
            // 
            this.btn_AutoRepeat.Location = new System.Drawing.Point(294, 120);
            this.btn_AutoRepeat.Name = "btn_AutoRepeat";
            this.btn_AutoRepeat.Size = new System.Drawing.Size(112, 32);
            this.btn_AutoRepeat.TabIndex = 7;
            this.btn_AutoRepeat.Text = "자동반복";
            this.btn_AutoRepeat.Click += new System.EventHandler(this.btn_AutoRepeat_Click);
            // 
            // btn_MakeJPG
            // 
            this.btn_MakeJPG.Location = new System.Drawing.Point(294, 72);
            this.btn_MakeJPG.Name = "btn_MakeJPG";
            this.btn_MakeJPG.Size = new System.Drawing.Size(112, 32);
            this.btn_MakeJPG.TabIndex = 6;
            this.btn_MakeJPG.Text = "JPG 생성";
            this.btn_MakeJPG.Click += new System.EventHandler(this.btn_MakeJPG_Click);
            // 
            // btn_Animation
            // 
            this.btn_Animation.Location = new System.Drawing.Point(294, 24);
            this.btn_Animation.Name = "btn_Animation";
            this.btn_Animation.Size = new System.Drawing.Size(112, 32);
            this.btn_Animation.TabIndex = 5;
            this.btn_Animation.Text = "애니메이션";
            this.btn_Animation.Click += new System.EventHandler(this.btn_Animation_Click);
            // 
            // panel_Image
            // 
            this.panel_Image.Location = new System.Drawing.Point(12, 8);
            this.panel_Image.Name = "panel_Image";
            this.panel_Image.Size = new System.Drawing.Size(250, 246);
            this.panel_Image.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 266);
            this.Controls.Add(this.btn_AutoRepeat);
            this.Controls.Add(this.btn_MakeJPG);
            this.Controls.Add(this.btn_Animation);
            this.Controls.Add(this.panel_Image);
            this.Name = "Form1";
            this.Text = "애니메이션 & JPG 생성";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_AutoRepeat;
        private System.Windows.Forms.Button btn_MakeJPG;
        private System.Windows.Forms.Button btn_Animation;
        private System.Windows.Forms.Panel panel_Image;
    }
}

