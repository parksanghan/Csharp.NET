namespace _231106_MultiGame_Client
{
    partial class ControlIngame
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
            inputTimer.Stop();
            inputTimer.Dispose();
            drawEngine.Dispose();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGetOut = new System.Windows.Forms.Button();
            this.labelUpper = new System.Windows.Forms.Label();
            this.labelMiddle = new System.Windows.Forms.Label();
            this.labelLower = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonGetOut
            // 
            this.buttonGetOut.Location = new System.Drawing.Point(3, 3);
            this.buttonGetOut.Name = "buttonGetOut";
            this.buttonGetOut.Size = new System.Drawing.Size(94, 77);
            this.buttonGetOut.TabIndex = 1;
            this.buttonGetOut.Text = "방 나가기";
            this.buttonGetOut.UseVisualStyleBackColor = true;
            this.buttonGetOut.Click += new System.EventHandler(this.buttonGetOut_Click);
            // 
            // labelUpper
            // 
            this.labelUpper.AutoSize = true;
            this.labelUpper.Location = new System.Drawing.Point(103, 34);
            this.labelUpper.Name = "labelUpper";
            this.labelUpper.Size = new System.Drawing.Size(13, 15);
            this.labelUpper.TabIndex = 2;
            this.labelUpper.Text = "/";
            // 
            // labelMiddle
            // 
            this.labelMiddle.AutoSize = true;
            this.labelMiddle.Location = new System.Drawing.Point(103, 49);
            this.labelMiddle.Name = "labelMiddle";
            this.labelMiddle.Size = new System.Drawing.Size(13, 15);
            this.labelMiddle.TabIndex = 3;
            this.labelMiddle.Text = "/";
            // 
            // labelLower
            // 
            this.labelLower.AutoSize = true;
            this.labelLower.Location = new System.Drawing.Point(103, 65);
            this.labelLower.Name = "labelLower";
            this.labelLower.Size = new System.Drawing.Size(13, 15);
            this.labelLower.TabIndex = 4;
            this.labelLower.Text = "/";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(103, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(467, 25);
            this.textBox1.TabIndex = 5;
            // 
            // ControlIngame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelLower);
            this.Controls.Add(this.labelMiddle);
            this.Controls.Add(this.labelUpper);
            this.Controls.Add(this.buttonGetOut);
            this.Name = "ControlIngame";
            this.Size = new System.Drawing.Size(577, 88);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonGetOut;
        public System.Windows.Forms.Label labelUpper;
        public System.Windows.Forms.Label labelMiddle;
        public System.Windows.Forms.Label labelLower;
        private System.Windows.Forms.TextBox textBox1;
    }
}
