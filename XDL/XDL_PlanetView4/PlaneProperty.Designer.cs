namespace XDL_PlanetView4
{
    partial class PlaneProperty
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxScaleY = new System.Windows.Forms.TextBox();
            this.textBoxScaleX = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxScaleZ = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxScalable = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxCameraMode = new System.Windows.Forms.ComboBox();
            this.checkBoxShowBoundingBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(114, 308);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(62, 23);
            this.buttonCancel.TabIndex = 23;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(34, 308);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(62, 23);
            this.buttonOK.TabIndex = 22;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxScaleY
            // 
            this.textBoxScaleY.Location = new System.Drawing.Point(46, 47);
            this.textBoxScaleY.Name = "textBoxScaleY";
            this.textBoxScaleY.Size = new System.Drawing.Size(100, 21);
            this.textBoxScaleY.TabIndex = 19;
            // 
            // textBoxScaleX
            // 
            this.textBoxScaleX.Location = new System.Drawing.Point(46, 20);
            this.textBoxScaleX.Name = "textBoxScaleX";
            this.textBoxScaleX.Size = new System.Drawing.Size(100, 21);
            this.textBoxScaleX.TabIndex = 18;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(76, 41);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 21);
            this.textBoxName.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "Y :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "X :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "Name :";
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(76, 12);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.ReadOnly = true;
            this.textBoxID.Size = new System.Drawing.Size(100, 21);
            this.textBoxID.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "ID :";
            // 
            // textBoxScaleZ
            // 
            this.textBoxScaleZ.Location = new System.Drawing.Point(46, 74);
            this.textBoxScaleZ.Name = "textBoxScaleZ";
            this.textBoxScaleZ.Size = new System.Drawing.Size(100, 21);
            this.textBoxScaleZ.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "Z :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxScalable);
            this.groupBox1.Controls.Add(this.textBoxScaleX);
            this.groupBox1.Controls.Add(this.textBoxScaleZ);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxScaleY);
            this.groupBox1.Location = new System.Drawing.Point(14, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 131);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scale";
            // 
            // checkBoxScalable
            // 
            this.checkBoxScalable.AutoSize = true;
            this.checkBoxScalable.Location = new System.Drawing.Point(46, 101);
            this.checkBoxScalable.Name = "checkBoxScalable";
            this.checkBoxScalable.Size = new System.Drawing.Size(73, 16);
            this.checkBoxScalable.TabIndex = 26;
            this.checkBoxScalable.Text = "Scalable";
            this.checkBoxScalable.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 228);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "Mode :";
            // 
            // comboBoxCameraMode
            // 
            this.comboBoxCameraMode.FormattingEnabled = true;
            this.comboBoxCameraMode.Items.AddRange(new object[] {
            "Back View",
            "Front View",
            "Left View",
            "Right View",
            "God’s eye View",
            "God’s eye View Static",
            "Bottom View",
            "Bottom View Static",
            "Inside View",
            "Inside View free",
            "Far View",
            "Unusable"});
            this.comboBoxCameraMode.Location = new System.Drawing.Point(63, 225);
            this.comboBoxCameraMode.Name = "comboBoxCameraMode";
            this.comboBoxCameraMode.Size = new System.Drawing.Size(121, 20);
            this.comboBoxCameraMode.TabIndex = 28;
            // 
            // checkBoxShowBoundingBox
            // 
            this.checkBoxShowBoundingBox.AutoSize = true;
            this.checkBoxShowBoundingBox.Location = new System.Drawing.Point(14, 261);
            this.checkBoxShowBoundingBox.Name = "checkBoxShowBoundingBox";
            this.checkBoxShowBoundingBox.Size = new System.Drawing.Size(139, 16);
            this.checkBoxShowBoundingBox.TabIndex = 27;
            this.checkBoxShowBoundingBox.Text = "Show Bounding Box";
            this.checkBoxShowBoundingBox.UseVisualStyleBackColor = true;
            // 
            // PlaneProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 348);
            this.Controls.Add(this.checkBoxShowBoundingBox);
            this.Controls.Add(this.comboBoxCameraMode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.label1);
            this.Name = "PlaneProperty";
            this.Text = "PlaneProperty";
            this.Load += new System.EventHandler(this.PlaneProperty_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxScaleY;
        private System.Windows.Forms.TextBox textBoxScaleX;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxScaleZ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxScalable;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxCameraMode;
        private System.Windows.Forms.CheckBox checkBoxShowBoundingBox;
    }
}