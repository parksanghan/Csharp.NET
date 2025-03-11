namespace _1026_진짜실습
{
    partial class Update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Update));
            this.labelMovies = new System.Windows.Forms.Label();
            this.listBoxMovies = new System.Windows.Forms.ListBox();
            this.buttonMovieUpdate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxVideoLink = new System.Windows.Forms.TextBox();
            this.textBoxPosterPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRuntime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerRelease = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxGenre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonInsertPoster = new System.Windows.Forms.Button();
            this.pictureBoxPoster = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMovies
            // 
            this.labelMovies.AutoSize = true;
            this.labelMovies.Location = new System.Drawing.Point(10, 34);
            this.labelMovies.Name = "labelMovies";
            this.labelMovies.Size = new System.Drawing.Size(78, 12);
            this.labelMovies.TabIndex = 3;
            this.labelMovies.Text = "영화 수 : N개";
            // 
            // listBoxMovies
            // 
            this.listBoxMovies.FormattingEnabled = true;
            this.listBoxMovies.ItemHeight = 12;
            this.listBoxMovies.Location = new System.Drawing.Point(10, 62);
            this.listBoxMovies.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxMovies.Name = "listBoxMovies";
            this.listBoxMovies.Size = new System.Drawing.Size(133, 340);
            this.listBoxMovies.TabIndex = 2;
            this.listBoxMovies.SelectedIndexChanged += new System.EventHandler(this.ListSelectedChanged);
            // 
            // buttonMovieUpdate
            // 
            this.buttonMovieUpdate.Location = new System.Drawing.Point(403, 334);
            this.buttonMovieUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonMovieUpdate.Name = "buttonMovieUpdate";
            this.buttonMovieUpdate.Size = new System.Drawing.Size(206, 62);
            this.buttonMovieUpdate.TabIndex = 35;
            this.buttonMovieUpdate.Text = "영화 수정";
            this.buttonMovieUpdate.UseVisualStyleBackColor = true;
            this.buttonMovieUpdate.Click += new System.EventHandler(this.buttonMovieUpdate_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(368, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 34;
            this.label7.Text = "분";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(150, 379);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "예고편";
            // 
            // textBoxVideoLink
            // 
            this.textBoxVideoLink.Location = new System.Drawing.Point(201, 377);
            this.textBoxVideoLink.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxVideoLink.Name = "textBoxVideoLink";
            this.textBoxVideoLink.Size = new System.Drawing.Size(191, 21);
            this.textBoxVideoLink.TabIndex = 32;
            // 
            // textBoxPosterPath
            // 
            this.textBoxPosterPath.Location = new System.Drawing.Point(402, 266);
            this.textBoxPosterPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPosterPath.Name = "textBoxPosterPath";
            this.textBoxPosterPath.Size = new System.Drawing.Size(207, 21);
            this.textBoxPosterPath.TabIndex = 31;
            this.textBoxPosterPath.TextChanged += new System.EventHandler(this.textBoxPosterPath_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "줄거리";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(201, 127);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(191, 246);
            this.textBoxDescription.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "상영시간";
            // 
            // textBoxRuntime
            // 
            this.textBoxRuntime.Location = new System.Drawing.Point(201, 102);
            this.textBoxRuntime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxRuntime.Name = "textBoxRuntime";
            this.textBoxRuntime.Size = new System.Drawing.Size(164, 21);
            this.textBoxRuntime.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 26;
            this.label3.Text = "개봉일";
            // 
            // dateTimePickerRelease
            // 
            this.dateTimePickerRelease.Location = new System.Drawing.Point(201, 78);
            this.dateTimePickerRelease.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePickerRelease.Name = "dateTimePickerRelease";
            this.dateTimePickerRelease.Size = new System.Drawing.Size(191, 21);
            this.dateTimePickerRelease.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "장르";
            // 
            // textBoxGenre
            // 
            this.textBoxGenre.Location = new System.Drawing.Point(201, 53);
            this.textBoxGenre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxGenre.Name = "textBoxGenre";
            this.textBoxGenre.Size = new System.Drawing.Size(191, 21);
            this.textBoxGenre.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "제목";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(201, 28);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(191, 21);
            this.textBoxName.TabIndex = 21;
            // 
            // buttonInsertPoster
            // 
            this.buttonInsertPoster.Location = new System.Drawing.Point(514, 291);
            this.buttonInsertPoster.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonInsertPoster.Name = "buttonInsertPoster";
            this.buttonInsertPoster.Size = new System.Drawing.Size(94, 26);
            this.buttonInsertPoster.TabIndex = 20;
            this.buttonInsertPoster.Text = "사진 선택";
            this.buttonInsertPoster.UseVisualStyleBackColor = true;
            this.buttonInsertPoster.Click += new System.EventHandler(this.buttonInsertPoster_Click);
            // 
            // pictureBoxPoster
            // 
            this.pictureBoxPoster.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPoster.Image")));
            this.pictureBoxPoster.Location = new System.Drawing.Point(402, 14);
            this.pictureBoxPoster.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxPoster.Name = "pictureBoxPoster";
            this.pictureBoxPoster.Size = new System.Drawing.Size(206, 243);
            this.pictureBoxPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPoster.TabIndex = 19;
            this.pictureBoxPoster.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(264, 194);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 26);
            this.button1.TabIndex = 36;
            this.button1.Text = "사진 선택";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(208, 176);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(206, 62);
            this.button2.TabIndex = 37;
            this.button2.Text = "영화 수정";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 414);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonMovieUpdate);
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
            this.Controls.Add(this.labelMovies);
            this.Controls.Add(this.listBoxMovies);
            this.Name = "Update";
            this.Text = "Update";
            this.Load += new System.EventHandler(this.Update_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMovies;
        private System.Windows.Forms.ListBox listBoxMovies;
        private System.Windows.Forms.Button buttonMovieUpdate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxVideoLink;
        private System.Windows.Forms.TextBox textBoxPosterPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRuntime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerRelease;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxGenre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonInsertPoster;
        private System.Windows.Forms.PictureBox pictureBoxPoster;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}