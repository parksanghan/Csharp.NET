namespace XDL_PlanetView4
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
            Pixoneer.NXDL.XAngle xAngle9 = new Pixoneer.NXDL.XAngle();
            Pixoneer.NXDL.XAngle xAngle10 = new Pixoneer.NXDL.XAngle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.nxPlanetView2D = new Pixoneer.NXDL.NXPlanet.NXPlanetView();
            this.nxPlanetView3D = new Pixoneer.NXDL.NXPlanet.NXPlanetView();
            this.modelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1017, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.nxPlanetView2D);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.nxPlanetView3D);
            this.splitContainer1.Size = new System.Drawing.Size(1017, 683);
            this.splitContainer1.SplitterDistance = 542;
            this.splitContainer1.TabIndex = 1;
            // 
            // nxPlanetView2D
            // 
            this.nxPlanetView2D.AutoFocus = false;
            this.nxPlanetView2D.Brightness = 1F;
            this.nxPlanetView2D.Contrast = 1F;
            this.nxPlanetView2D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nxPlanetView2D.EarthMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eEarthMode.Planet2D;
            this.nxPlanetView2D.FrameCapture = null;
            this.nxPlanetView2D.GridType = Pixoneer.NXDL.NXPlanet.NXPlanetView.eGridType.GridDegrees;
            this.nxPlanetView2D.InverseMouseButton = false;
            this.nxPlanetView2D.InverseMouseWheel = false;
            this.nxPlanetView2D.LayoutMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eLayoutMode.Windows;
            this.nxPlanetView2D.Location = new System.Drawing.Point(0, 0);
            this.nxPlanetView2D.Name = "nxPlanetView2D";
            this.nxPlanetView2D.RelativeHeight = 1D;
            this.nxPlanetView2D.RelativeLeft = 0D;
            this.nxPlanetView2D.RelativeTop = 0D;
            this.nxPlanetView2D.RelativeWidth = 1D;
            this.nxPlanetView2D.RestrictRenerArea = false;
            this.nxPlanetView2D.Rotatable = true;
            this.nxPlanetView2D.Saturation = 1F;
            this.nxPlanetView2D.ShowGrid = true;
            this.nxPlanetView2D.ShowPBP = true;
            this.nxPlanetView2D.ShowStatusInfo = false;
            this.nxPlanetView2D.Size = new System.Drawing.Size(542, 683);
            this.nxPlanetView2D.TabIndex = 0;
            this.nxPlanetView2D.ToolboxAreaUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxAreaUnit.SquareMeter;
            this.nxPlanetView2D.ToolboxDistUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxDistUnit.Meter;
            this.nxPlanetView2D.ToolboxMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxMode.None;
            xAngle9.deg = 45D;
            this.nxPlanetView2D.ViewAreaFOV = xAngle9;
            this.nxPlanetView2D.ViewAreaID = -1;
            this.nxPlanetView2D.ZoomCenterMode = Pixoneer.NXDL.eViewZoomCenterMode.CenterByCursor;
            // 
            // nxPlanetView3D
            // 
            this.nxPlanetView3D.AutoFocus = false;
            this.nxPlanetView3D.Brightness = 1F;
            this.nxPlanetView3D.Contrast = 1F;
            this.nxPlanetView3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nxPlanetView3D.EarthMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eEarthMode.Planet3D;
            this.nxPlanetView3D.FrameCapture = null;
            this.nxPlanetView3D.GridType = Pixoneer.NXDL.NXPlanet.NXPlanetView.eGridType.GridDegrees;
            this.nxPlanetView3D.InverseMouseButton = false;
            this.nxPlanetView3D.InverseMouseWheel = false;
            this.nxPlanetView3D.LayoutMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eLayoutMode.Windows;
            this.nxPlanetView3D.Location = new System.Drawing.Point(0, 0);
            this.nxPlanetView3D.Name = "nxPlanetView3D";
            this.nxPlanetView3D.RelativeHeight = 1D;
            this.nxPlanetView3D.RelativeLeft = 0D;
            this.nxPlanetView3D.RelativeTop = 0D;
            this.nxPlanetView3D.RelativeWidth = 1D;
            this.nxPlanetView3D.RestrictRenerArea = false;
            this.nxPlanetView3D.Rotatable = true;
            this.nxPlanetView3D.Saturation = 1F;
            this.nxPlanetView3D.ShowGrid = true;
            this.nxPlanetView3D.ShowPBP = true;
            this.nxPlanetView3D.ShowStatusInfo = false;
            this.nxPlanetView3D.Size = new System.Drawing.Size(471, 683);
            this.nxPlanetView3D.TabIndex = 0;
            this.nxPlanetView3D.ToolboxAreaUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxAreaUnit.SquareMeter;
            this.nxPlanetView3D.ToolboxDistUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxDistUnit.Meter;
            this.nxPlanetView3D.ToolboxMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxMode.None;
            xAngle10.deg = 45D;
            this.nxPlanetView3D.ViewAreaFOV = xAngle10;
            this.nxPlanetView3D.ViewAreaID = -1;
            this.nxPlanetView3D.ZoomCenterMode = Pixoneer.NXDL.eViewZoomCenterMode.CenterByCursor;
            // 
            // modelToolStripMenuItem
            // 
            this.modelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addModelToolStripMenuItem});
            this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            this.modelToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.modelToolStripMenuItem.Text = "Model";
            // 
            // addModelToolStripMenuItem
            // 
            this.addModelToolStripMenuItem.Name = "addModelToolStripMenuItem";
            this.addModelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addModelToolStripMenuItem.Text = "Add";
            this.addModelToolStripMenuItem.Click += new System.EventHandler(this.addModelToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 707);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addModelToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Pixoneer.NXDL.NXPlanet.NXPlanetView nxPlanetView2D;
        private Pixoneer.NXDL.NXPlanet.NXPlanetView nxPlanetView3D;
    }
}

