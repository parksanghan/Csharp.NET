namespace _1026_진짜실습
{
    partial class Insert
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Insert));
            this.buttonInsertPoster = new System.Windows.Forms.Button();
            this.pictureBoxPoster = new System.Windows.Forms.PictureBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxGenre = new System.Windows.Forms.TextBox();
            this.dateTimePickerRelease = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRuntime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxPosterPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxVideoLink = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonMovieInsert = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonInsertPoster
            // 
            this.buttonInsertPoster.Location = new System.Drawing.Point(587, 392);
            this.buttonInsertPoster.Name = "buttonInsertPoster";
            this.buttonInsertPoster.Size = new System.Drawing.Size(107, 32);
            this.buttonInsertPoster.TabIndex = 3;
            this.buttonInsertPoster.Text = "사진 선택";
            this.buttonInsertPoster.UseVisualStyleBackColor = true;
            this.buttonInsertPoster.Click += new System.EventHandler(this.buttonInsertPoster_Click);
            // 
            // pictureBoxPoster
            // 
            this.pictureBoxPoster.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPoster.Image")));
            this.pictureBoxPoster.Location = new System.Drawing.Point(408, 12);
            this.pictureBoxPoster.Name = "pictureBoxPoster";
            this.pictureBoxPoster.Size = new System.Drawing.Size(286, 374);
            this.pictureBoxPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPoster.TabIndex = 2;
            this.pictureBoxPoster.TabStop = false;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(89, 30);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(295, 25);
            this.textBoxName.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "제목";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "장르";
            // 
            // textBoxGenre
            // 
            this.textBoxGenre.Location = new System.Drawing.Point(89, 61);
            this.textBoxGenre.Name = "textBoxGenre";
            this.textBoxGenre.Size = new System.Drawing.Size(295, 25);
            this.textBoxGenre.TabIndex = 6;
            // 
            // dateTimePickerRelease
            // 
            this.dateTimePickerRelease.Location = new System.Drawing.Point(89, 92);
            this.dateTimePickerRelease.Name = "dateTimePickerRelease";
            this.dateTimePickerRelease.Size = new System.Drawing.Size(295, 25);
            this.dateTimePickerRelease.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "개봉일";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "상영시간";
            // 
            // textBoxRuntime
            // 
            this.textBoxRuntime.Location = new System.Drawing.Point(89, 123);
            this.textBoxRuntime.Name = "textBoxRuntime";
            this.textBoxRuntime.Size = new System.Drawing.Size(264, 25);
            this.textBoxRuntime.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "줄거리";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(89, 154);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(295, 306);
            this.textBoxDescription.TabIndex = 12;
            // 
            // textBoxPosterPath
            // 
            this.textBoxPosterPath.Location = new System.Drawing.Point(408, 398);
            this.textBoxPosterPath.Name = "textBoxPosterPath";
            this.textBoxPosterPath.Size = new System.Drawing.Size(173, 25);
            this.textBoxPosterPath.TabIndex = 14;
            this.textBoxPosterPath.TextChanged += new System.EventHandler(this.textBoxPosterPath_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 469);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "예고편";
            // 
            // textBoxVideoLink
            // 
            this.textBoxVideoLink.Location = new System.Drawing.Point(89, 466);
            this.textBoxVideoLink.Name = "textBoxVideoLink";
            this.textBoxVideoLink.Size = new System.Drawing.Size(295, 25);
            this.textBoxVideoLink.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(356, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "분";
            // 
            // buttonMovieInsert
            // 
            this.buttonMovieInsert.Location = new System.Drawing.Point(408, 429);
            this.buttonMovieInsert.Name = "buttonMovieInsert";
            this.buttonMovieInsert.Size = new System.Drawing.Size(286, 78);
            this.buttonMovieInsert.TabIndex = 18;
            this.buttonMovieInsert.Text = "영화 등록";
            this.buttonMovieInsert.UseVisualStyleBackColor = true;
            this.buttonMovieInsert.Click += new System.EventHandler(this.buttonMovieInsert_Click);
            // 
            // Insert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(706, 519);
            this.Controls.Add(this.buttonMovieInsert);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxVideoLink);
            this.Controls.Add(this.textBoxPosterPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxRuntime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePickerRelease);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxGenre);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonInsertPoster);
            this.Controls.Add(this.pictureBoxPoster);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Insert";
            this.Text = "Insert";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInsertPoster;
        private System.Windows.Forms.PictureBox pictureBoxPoster;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxGenre;
        private System.Windows.Forms.DateTimePicker dateTimePickerRelease;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRuntime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxPosterPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxVideoLink;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonMovieInsert;
    }
}