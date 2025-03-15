namespace ButtonProperty
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.AutoEllipsis = true;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "AutoEllipsis 아주 긴 텍스트입니다.";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 65);
            this.button2.TabIndex = 1;
            this.button2.Text = "Padding 프로퍼티를 0으로 설정한 버튼입니다";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(105, 54);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(3);
            this.button3.Size = new System.Drawing.Size(84, 65);
            this.button3.TabIndex = 2;
            this.button3.Text = "Padding 프로퍼티를 3으로 설정한 버튼입니다";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(196, 54);
            this.button4.Name = "button4";
            this.button4.Padding = new System.Windows.Forms.Padding(6);
            this.button4.Size = new System.Drawing.Size(84, 65);
            this.button4.TabIndex = 3;
            this.button4.Text = "Padding 프로퍼티를 6으로 설정한 버튼입니다";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(12, 135);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(84, 27);
            this.button5.TabIndex = 6;
            this.button5.Text = "Flat";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Location = new System.Drawing.Point(105, 135);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(84, 27);
            this.button6.TabIndex = 7;
            this.button6.Text = "Popup";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(195, 135);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(84, 27);
            this.button7.TabIndex = 8;
            this.button7.Text = "Standard";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button8.Location = new System.Drawing.Point(285, 135);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(84, 27);
            this.button8.TabIndex = 9;
            this.button8.Text = "System";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.button9.FlatAppearance.BorderSize = 3;
            this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Location = new System.Drawing.Point(376, 135);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(84, 27);
            this.button9.TabIndex = 10;
            this.button9.Text = "Flat";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Image = global::ButtonProperty.Properties.Resources.dol;
            this.button10.Location = new System.Drawing.Point(12, 179);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(84, 72);
            this.button10.TabIndex = 11;
            this.button10.Text = "Text";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Image = global::ButtonProperty.Properties.Resources.dol;
            this.button11.Location = new System.Drawing.Point(105, 179);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(84, 72);
            this.button11.TabIndex = 12;
            this.button11.Text = "Text";
            this.button11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Image = global::ButtonProperty.Properties.Resources.dol;
            this.button12.Location = new System.Drawing.Point(195, 179);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(84, 72);
            this.button12.TabIndex = 13;
            this.button12.Text = "Text";
            this.button12.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.button12.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Image = global::ButtonProperty.Properties.Resources.dol;
            this.button13.Location = new System.Drawing.Point(285, 179);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(84, 72);
            this.button13.TabIndex = 14;
            this.button13.Text = "Text";
            this.button13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button13.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            this.button14.Image = global::ButtonProperty.Properties.Resources.dol;
            this.button14.Location = new System.Drawing.Point(375, 179);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(84, 72);
            this.button14.TabIndex = 15;
            this.button14.Text = "Text";
            this.button14.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button14.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(12, 274);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(84, 48);
            this.button15.TabIndex = 16;
            this.button15.Text = "Text";
            this.button15.UseVisualStyleBackColor = true;
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(105, 274);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(84, 48);
            this.button16.TabIndex = 17;
            this.button16.Text = "Text";
            this.button16.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button16.UseVisualStyleBackColor = true;
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(196, 274);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(84, 48);
            this.button17.TabIndex = 18;
            this.button17.Text = "Text";
            this.button17.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button17.UseVisualStyleBackColor = true;
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(286, 274);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(84, 48);
            this.button18.TabIndex = 19;
            this.button18.Text = "Text";
            this.button18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button18.UseVisualStyleBackColor = true;
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(376, 274);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(84, 48);
            this.button19.TabIndex = 20;
            this.button19.Text = "Text";
            this.button19.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.button19.UseVisualStyleBackColor = true;
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(286, 54);
            this.button20.Name = "button20";
            this.button20.Padding = new System.Windows.Forms.Padding(10);
            this.button20.Size = new System.Drawing.Size(84, 65);
            this.button20.TabIndex = 4;
            this.button20.Text = "Padding 프로퍼티를 10으로 설정한 버튼입니다";
            this.button20.UseVisualStyleBackColor = true;
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(376, 54);
            this.button21.Name = "button21";
            this.button21.Padding = new System.Windows.Forms.Padding(16);
            this.button21.Size = new System.Drawing.Size(84, 65);
            this.button21.TabIndex = 5;
            this.button21.Text = "Padding 프로퍼티를 16으로 설정한 버튼입니다";
            this.button21.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 345);
            this.Controls.Add(this.button19);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button21);
            this.Controls.Add(this.button20);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
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
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
    }
}

