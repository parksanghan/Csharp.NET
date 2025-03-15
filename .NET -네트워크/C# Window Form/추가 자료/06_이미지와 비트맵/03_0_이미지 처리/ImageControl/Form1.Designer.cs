namespace ImageControl
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
            System.Windows.Forms.Button button9;
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.m_picImg = new System.Windows.Forms.PictureBox();
            this.button8 = new System.Windows.Forms.Button();
            button9 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_picImg)).BeginInit();
            this.SuspendLayout();
            // 
            // button9
            // 
            button9.Location = new System.Drawing.Point(434, 333);
            button9.Name = "button9";
            button9.Size = new System.Drawing.Size(85, 34);
            button9.TabIndex = 10;
            button9.Text = "원본이미지";
            button9.UseVisualStyleBackColor = true;
            button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(434, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "기울이기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(434, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 34);
            this.button2.TabIndex = 1;
            this.button2.Text = "뒤집기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(434, 93);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 34);
            this.button3.TabIndex = 2;
            this.button3.Text = "회전하기";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(434, 173);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(85, 34);
            this.button4.TabIndex = 3;
            this.button4.Text = "복제하기";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(434, 133);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(85, 34);
            this.button5.TabIndex = 4;
            this.button5.Text = "부분자르기";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(434, 213);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(85, 34);
            this.button6.TabIndex = 5;
            this.button6.Text = "이미지합성";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(434, 253);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(85, 34);
            this.button7.TabIndex = 6;
            this.button7.Text = "해상도 변경";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // m_picImg
            // 
            this.m_picImg.Location = new System.Drawing.Point(9, 16);
            this.m_picImg.Name = "m_picImg";
            this.m_picImg.Size = new System.Drawing.Size(419, 354);
            this.m_picImg.TabIndex = 8;
            this.m_picImg.TabStop = false;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(434, 293);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(85, 34);
            this.button8.TabIndex = 9;
            this.button8.Text = "Thumbnail";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 386);
            this.Controls.Add(button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.m_picImg);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "이미지 조작하기";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_picImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.PictureBox m_picImg;
        private System.Windows.Forms.Button button8;
    }
}

