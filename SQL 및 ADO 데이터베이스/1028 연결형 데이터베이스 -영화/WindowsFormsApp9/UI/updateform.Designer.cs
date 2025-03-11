namespace WindowsFormsApp9
{
    partial class updateform
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.textBox_title = new System.Windows.Forms.TextBox();
            this.textBox_runtime = new System.Windows.Forms.TextBox();
            this.textBox_contents = new System.Windows.Forms.TextBox();
            this.textBox_linker = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_update = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button_image = new System.Windows.Forms.Button();
            this.textBox_image = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(343, 436);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(540, 273);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(8, 4);
            this.listBox2.TabIndex = 1;
            // 
            // textBox_title
            // 
            this.textBox_title.Location = new System.Drawing.Point(385, 25);
            this.textBox_title.Name = "textBox_title";
            this.textBox_title.Size = new System.Drawing.Size(100, 21);
            this.textBox_title.TabIndex = 2;
            this.textBox_title.Text = "제목";
            // 
            // textBox_runtime
            // 
            this.textBox_runtime.Location = new System.Drawing.Point(385, 105);
            this.textBox_runtime.Name = "textBox_runtime";
            this.textBox_runtime.Size = new System.Drawing.Size(100, 21);
            this.textBox_runtime.TabIndex = 3;
            this.textBox_runtime.Text = "시간";
            // 
            // textBox_contents
            // 
            this.textBox_contents.Location = new System.Drawing.Point(385, 132);
            this.textBox_contents.Multiline = true;
            this.textBox_contents.Name = "textBox_contents";
            this.textBox_contents.Size = new System.Drawing.Size(200, 189);
            this.textBox_contents.TabIndex = 4;
            this.textBox_contents.Text = "설명";
            // 
            // textBox_linker
            // 
            this.textBox_linker.Location = new System.Drawing.Point(430, 369);
            this.textBox_linker.Name = "textBox_linker";
            this.textBox_linker.Size = new System.Drawing.Size(100, 21);
            this.textBox_linker.TabIndex = 6;
            this.textBox_linker.Text = "링크";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(618, 105);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(145, 201);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // button_update
            // 
            this.button_update.Location = new System.Drawing.Point(554, 369);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(75, 25);
            this.button_update.TabIndex = 18;
            this.button_update.Text = "업데이트";
            this.button_update.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(385, 68);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 19;
            // 
            // button_image
            // 
            this.button_image.Location = new System.Drawing.Point(554, 327);
            this.button_image.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_image.Name = "button_image";
            this.button_image.Size = new System.Drawing.Size(94, 26);
            this.button_image.TabIndex = 21;
            this.button_image.Text = "파일선택";
            this.button_image.UseVisualStyleBackColor = true;
            this.button_image.Click += new System.EventHandler(this.button_image_Click);
            // 
            // textBox_image
            // 
            this.textBox_image.Location = new System.Drawing.Point(430, 327);
            this.textBox_image.Name = "textBox_image";
            this.textBox_image.Size = new System.Drawing.Size(100, 21);
            this.textBox_image.TabIndex = 22;
            this.textBox_image.TextChanged += new System.EventHandler(this.textBox_image_TextChanged);
            // 
            // updateform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox_image);
            this.Controls.Add(this.button_image);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox_linker);
            this.Controls.Add(this.textBox_contents);
            this.Controls.Add(this.textBox_runtime);
            this.Controls.Add(this.textBox_title);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Name = "updateform";
            this.Text = "updateform";
            this.Load += new System.EventHandler(this.updateform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox textBox_title;
        private System.Windows.Forms.TextBox textBox_runtime;
        private System.Windows.Forms.TextBox textBox_contents;
        private System.Windows.Forms.TextBox textBox_linker;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button_image;
        private System.Windows.Forms.TextBox textBox_image;
    }
}