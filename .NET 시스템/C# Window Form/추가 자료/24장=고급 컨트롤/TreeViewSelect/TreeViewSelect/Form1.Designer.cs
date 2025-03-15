namespace TreeViewSelect
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("은평구");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("서울특별시", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("수원시");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("죽전동");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("용인시", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("경기도", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("당진군");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("충청도", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("대한민국", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode6,
            treeNode8});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "노드4";
            treeNode1.Text = "은평구";
            treeNode2.Name = "노드1";
            treeNode2.Text = "서울특별시";
            treeNode3.Name = "노드5";
            treeNode3.Text = "수원시";
            treeNode4.Name = "노드8";
            treeNode4.Text = "죽전동";
            treeNode5.Name = "노드6";
            treeNode5.Text = "용인시";
            treeNode6.Name = "노드2";
            treeNode6.Text = "경기도";
            treeNode7.Name = "노드7";
            treeNode7.Text = "당진군";
            treeNode8.Name = "노드3";
            treeNode8.Text = "충청도";
            treeNode9.Name = "노드0";
            treeNode9.Text = "대한민국";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9});
            this.treeView1.Size = new System.Drawing.Size(268, 198);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "행선지를 선택하십시오.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;

    }
}

