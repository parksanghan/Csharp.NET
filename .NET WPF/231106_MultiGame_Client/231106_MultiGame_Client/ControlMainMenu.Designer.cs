namespace _231106_MultiGame_Client
{
    partial class ControlMainMenu
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxIngames = new System.Windows.Forms.ListBox();
            this.labelIngamesCount = new System.Windows.Forms.Label();
            this.buttonGetIn = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.labelUserName = new System.Windows.Forms.Label();
            this.buttonLogOut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxIngames
            // 
            this.listBoxIngames.FormattingEnabled = true;
            this.listBoxIngames.ItemHeight = 15;
            this.listBoxIngames.Location = new System.Drawing.Point(31, 111);
            this.listBoxIngames.Name = "listBoxIngames";
            this.listBoxIngames.Size = new System.Drawing.Size(372, 304);
            this.listBoxIngames.TabIndex = 0;
            // 
            // labelIngamesCount
            // 
            this.labelIngamesCount.AutoSize = true;
            this.labelIngamesCount.Location = new System.Drawing.Point(28, 89);
            this.labelIngamesCount.Name = "labelIngamesCount";
            this.labelIngamesCount.Size = new System.Drawing.Size(130, 15);
            this.labelIngamesCount.TabIndex = 1;
            this.labelIngamesCount.Text = "진행중인 게임 0개";
            // 
            // buttonGetIn
            // 
            this.buttonGetIn.Location = new System.Drawing.Point(267, 421);
            this.buttonGetIn.Name = "buttonGetIn";
            this.buttonGetIn.Size = new System.Drawing.Size(136, 42);
            this.buttonGetIn.TabIndex = 2;
            this.buttonGetIn.Text = "입장";
            this.buttonGetIn.UseVisualStyleBackColor = true;
            this.buttonGetIn.Click += new System.EventHandler(this.buttonGetIn_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(122, 421);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(139, 42);
            this.buttonCreate.TabIndex = 3;
            this.buttonCreate.Text = "방 생성";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Font = new System.Drawing.Font("굴림", 20F);
            this.labelUserName.Location = new System.Drawing.Point(25, 22);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(103, 34);
            this.labelUserName.TabIndex = 4;
            this.labelUserName.Text = "???님";
            // 
            // buttonLogOut
            // 
            this.buttonLogOut.Location = new System.Drawing.Point(267, 14);
            this.buttonLogOut.Name = "buttonLogOut";
            this.buttonLogOut.Size = new System.Drawing.Size(139, 42);
            this.buttonLogOut.TabIndex = 5;
            this.buttonLogOut.Text = "로그아웃";
            this.buttonLogOut.UseVisualStyleBackColor = true;
            this.buttonLogOut.Click += new System.EventHandler(this.buttonLogOut_Click);
            // 
            // ControlMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonLogOut);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.buttonGetIn);
            this.Controls.Add(this.labelIngamesCount);
            this.Controls.Add(this.listBoxIngames);
            this.Name = "ControlMainMenu";
            this.Size = new System.Drawing.Size(434, 507);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxIngames;
        private System.Windows.Forms.Label labelIngamesCount;
        private System.Windows.Forms.Button buttonGetIn;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Button buttonLogOut;
    }
}
