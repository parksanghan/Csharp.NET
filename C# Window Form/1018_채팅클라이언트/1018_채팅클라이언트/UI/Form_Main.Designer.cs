namespace _1018_채팅클라이언트
{
    partial class Form_Main
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_nickname = new System.Windows.Forms.TextBox();
            this.txt_view = new System.Windows.Forms.TextBox();
            this.txt_send = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "대화명";
            // 
            // txt_nickname
            // 
            this.txt_nickname.Location = new System.Drawing.Point(82, 32);
            this.txt_nickname.Name = "txt_nickname";
            this.txt_nickname.Size = new System.Drawing.Size(302, 21);
            this.txt_nickname.TabIndex = 1;
            // 
            // txt_view
            // 
            this.txt_view.Location = new System.Drawing.Point(36, 74);
            this.txt_view.Multiline = true;
            this.txt_view.Name = "txt_view";
            this.txt_view.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_view.Size = new System.Drawing.Size(348, 265);
            this.txt_view.TabIndex = 2;
            // 
            // txt_send
            // 
            this.txt_send.Location = new System.Drawing.Point(36, 369);
            this.txt_send.Name = "txt_send";
            this.txt_send.Size = new System.Drawing.Size(274, 21);
            this.txt_send.TabIndex = 3;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(328, 367);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(56, 23);
            this.btn_send.TabIndex = 4;
            this.btn_send.Text = "전송";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 419);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.txt_send);
            this.Controls.Add(this.txt_view);
            this.Controls.Add(this.txt_nickname);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_Main";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Main_FormClosed);
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_nickname;
        private System.Windows.Forms.TextBox txt_view;
        private System.Windows.Forms.TextBox txt_send;
        private System.Windows.Forms.Button btn_send;
    }
}

