namespace _231023_클라이언트_시험
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelAddResult = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxPrintAll = new System.Windows.Forms.ListBox();
            this.buttonPrintAll = new System.Windows.Forms.Button();
            this.labelPrintAllCount = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSelectName = new System.Windows.Forms.TextBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.labelSelectResult = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSelectResultPrice = new System.Windows.Forms.TextBox();
            this.textBoxSelectResultName = new System.Windows.Forms.TextBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.labelUpdateResult = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxPrice);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.labelAddResult);
            this.groupBox1.Controls.Add(this.buttonAdd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxUserName);
            this.groupBox1.Location = new System.Drawing.Point(12, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 427);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "가격";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "이름";
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Location = new System.Drawing.Point(90, 210);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(194, 25);
            this.textBoxPrice.TabIndex = 6;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(90, 164);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(194, 25);
            this.textBoxName.TabIndex = 5;
            // 
            // labelAddResult
            // 
            this.labelAddResult.AutoSize = true;
            this.labelAddResult.Location = new System.Drawing.Point(23, 328);
            this.labelAddResult.Name = "labelAddResult";
            this.labelAddResult.Size = new System.Drawing.Size(107, 15);
            this.labelAddResult.TabIndex = 4;
            this.labelAddResult.Text = "저장 시도 결과";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(154, 285);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(130, 34);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "저장";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "사용자명";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(90, 70);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(194, 25);
            this.textBoxUserName.TabIndex = 0;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(18, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(130, 34);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "서버 연결";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxPrintAll);
            this.groupBox2.Controls.Add(this.buttonPrintAll);
            this.groupBox2.Controls.Add(this.labelPrintAllCount);
            this.groupBox2.Location = new System.Drawing.Point(354, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 427);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // listBoxPrintAll
            // 
            this.listBoxPrintAll.FormattingEnabled = true;
            this.listBoxPrintAll.ItemHeight = 15;
            this.listBoxPrintAll.Location = new System.Drawing.Point(9, 92);
            this.listBoxPrintAll.Name = "listBoxPrintAll";
            this.listBoxPrintAll.Size = new System.Drawing.Size(288, 319);
            this.listBoxPrintAll.TabIndex = 6;
            // 
            // buttonPrintAll
            // 
            this.buttonPrintAll.Location = new System.Drawing.Point(146, 24);
            this.buttonPrintAll.Name = "buttonPrintAll";
            this.buttonPrintAll.Size = new System.Drawing.Size(130, 34);
            this.buttonPrintAll.TabIndex = 3;
            this.buttonPrintAll.Text = "전체 출력";
            this.buttonPrintAll.UseVisualStyleBackColor = true;
            this.buttonPrintAll.Click += new System.EventHandler(this.buttonPrintAll_Click);
            // 
            // labelPrintAllCount
            // 
            this.labelPrintAllCount.AutoSize = true;
            this.labelPrintAllCount.Location = new System.Drawing.Point(6, 70);
            this.labelPrintAllCount.Name = "labelPrintAllCount";
            this.labelPrintAllCount.Size = new System.Drawing.Size(77, 15);
            this.labelPrintAllCount.TabIndex = 5;
            this.labelPrintAllCount.Text = "총 출력 수";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelUpdateResult);
            this.groupBox3.Controls.Add(this.buttonDelete);
            this.groupBox3.Controls.Add(this.buttonUpdate);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.labelSelectResult);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.buttonSelect);
            this.groupBox3.Controls.Add(this.textBoxSelectResultPrice);
            this.groupBox3.Controls.Add(this.textBoxSelectResultName);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.textBoxSelectName);
            this.groupBox3.Location = new System.Drawing.Point(707, 52);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 427);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(166, 12);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(130, 34);
            this.buttonDisconnect.TabIndex = 2;
            this.buttonDisconnect.Text = "서버 연결 종료";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "이름";
            // 
            // textBoxSelectName
            // 
            this.textBoxSelectName.Location = new System.Drawing.Point(103, 24);
            this.textBoxSelectName.Name = "textBoxSelectName";
            this.textBoxSelectName.Size = new System.Drawing.Size(194, 25);
            this.textBoxSelectName.TabIndex = 9;
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(167, 60);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(130, 34);
            this.buttonSelect.TabIndex = 7;
            this.buttonSelect.Text = "검색";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // labelSelectResult
            // 
            this.labelSelectResult.AutoSize = true;
            this.labelSelectResult.Location = new System.Drawing.Point(19, 92);
            this.labelSelectResult.Name = "labelSelectResult";
            this.labelSelectResult.Size = new System.Drawing.Size(72, 15);
            this.labelSelectResult.TabIndex = 7;
            this.labelSelectResult.Text = "검색 결과";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "가격";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "이름";
            // 
            // textBoxSelectResultPrice
            // 
            this.textBoxSelectResultPrice.Location = new System.Drawing.Point(103, 232);
            this.textBoxSelectResultPrice.Name = "textBoxSelectResultPrice";
            this.textBoxSelectResultPrice.Size = new System.Drawing.Size(194, 25);
            this.textBoxSelectResultPrice.TabIndex = 10;
            // 
            // textBoxSelectResultName
            // 
            this.textBoxSelectResultName.Location = new System.Drawing.Point(103, 186);
            this.textBoxSelectResultName.Name = "textBoxSelectResultName";
            this.textBoxSelectResultName.Size = new System.Drawing.Size(194, 25);
            this.textBoxSelectResultName.TabIndex = 9;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(22, 263);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(130, 34);
            this.buttonUpdate.TabIndex = 13;
            this.buttonUpdate.Text = "수정";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(167, 263);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(130, 34);
            this.buttonDelete.TabIndex = 14;
            this.buttonDelete.Text = "삭제";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // labelUpdateResult
            // 
            this.labelUpdateResult.AutoSize = true;
            this.labelUpdateResult.Location = new System.Drawing.Point(19, 304);
            this.labelUpdateResult.Name = "labelUpdateResult";
            this.labelUpdateResult.Size = new System.Drawing.Size(148, 19);
            this.labelUpdateResult.TabIndex = 15;
            this.labelUpdateResult.Text = "수정 / 삭제 결과";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 494);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label labelAddResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ListBox listBoxPrintAll;
        private System.Windows.Forms.Button buttonPrintAll;
        private System.Windows.Forms.Label labelPrintAllCount;
        private System.Windows.Forms.Label labelUpdateResult;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelSelectResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.TextBox textBoxSelectResultPrice;
        private System.Windows.Forms.TextBox textBoxSelectResultName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSelectName;
    }
}

