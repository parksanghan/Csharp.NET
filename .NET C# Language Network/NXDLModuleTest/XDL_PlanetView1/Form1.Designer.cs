namespace XDL_PlanetView2
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
            Pixoneer.NXDL.XAngle xAngle1 = new Pixoneer.NXDL.XAngle();
            Pixoneer.NXDL.XAngle xAngle2 = new Pixoneer.NXDL.XAngle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dMeasurementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure2DLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure2DPolyLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure2DAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure2DAngleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure2DAngle3PtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure2DCircleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dMeasurementToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure3DLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure3DPolyLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure3DAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure3DAngleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure3DAngle3PtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure3DCircleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.사용자정의ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserDefinedMeasureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.nxPlanetView2D = new Pixoneer.NXDL.NXPlanet.NXPlanetView();
            this.nxPlanetLayer2D = new Pixoneer.NXDL.NXPlanet.NXPlanetLayer();
            this.nxPlanetView3D = new Pixoneer.NXDL.NXPlanet.NXPlanetView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.nxPlanetView2D.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dMeasurementToolStripMenuItem,
            this.dMeasurementToolStripMenuItem1,
            this.사용자정의ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(996, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dMeasurementToolStripMenuItem
            // 
            this.dMeasurementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Measure2DLineToolStripMenuItem,
            this.Measure2DPolyLineToolStripMenuItem,
            this.Measure2DAreaToolStripMenuItem,
            this.Measure2DAngleToolStripMenuItem,
            this.Measure2DAngle3PtToolStripMenuItem,
            this.Measure2DCircleToolStripMenuItem});
            this.dMeasurementToolStripMenuItem.Name = "dMeasurementToolStripMenuItem";
            this.dMeasurementToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.dMeasurementToolStripMenuItem.Text = "2D Measurement";
            // 
            // Measure2DLineToolStripMenuItem
            // 
            this.Measure2DLineToolStripMenuItem.Name = "Measure2DLineToolStripMenuItem";
            this.Measure2DLineToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Measure2DLineToolStripMenuItem.Text = "거리(단선) 측정";
            this.Measure2DLineToolStripMenuItem.Click += new System.EventHandler(this.Measure2DLineToolStripMenuItem_Click);
            // 
            // Measure2DPolyLineToolStripMenuItem
            // 
            this.Measure2DPolyLineToolStripMenuItem.Name = "Measure2DPolyLineToolStripMenuItem";
            this.Measure2DPolyLineToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Measure2DPolyLineToolStripMenuItem.Text = "거리(폴리라인) 측정";
            this.Measure2DPolyLineToolStripMenuItem.Click += new System.EventHandler(this.Measure2DPolyLineToolStripMenuItem_Click);
            // 
            // Measure2DAreaToolStripMenuItem
            // 
            this.Measure2DAreaToolStripMenuItem.Name = "Measure2DAreaToolStripMenuItem";
            this.Measure2DAreaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Measure2DAreaToolStripMenuItem.Text = "면적 측정";
            this.Measure2DAreaToolStripMenuItem.Click += new System.EventHandler(this.Measure2DAreaToolStripMenuItem_Click);
            // 
            // Measure2DAngleToolStripMenuItem
            // 
            this.Measure2DAngleToolStripMenuItem.Name = "Measure2DAngleToolStripMenuItem";
            this.Measure2DAngleToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Measure2DAngleToolStripMenuItem.Text = "각도 측정(진북방향)";
            this.Measure2DAngleToolStripMenuItem.Click += new System.EventHandler(this.Measure2DAngleToolStripMenuItem_Click);
            // 
            // Measure2DAngle3PtToolStripMenuItem
            // 
            this.Measure2DAngle3PtToolStripMenuItem.Name = "Measure2DAngle3PtToolStripMenuItem";
            this.Measure2DAngle3PtToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Measure2DAngle3PtToolStripMenuItem.Text = "각도 측정(3점 이용)";
            this.Measure2DAngle3PtToolStripMenuItem.Click += new System.EventHandler(this.Measure2DAngle3PtToolStripMenuItem_Click);
            // 
            // Measure2DCircleToolStripMenuItem
            // 
            this.Measure2DCircleToolStripMenuItem.Name = "Measure2DCircleToolStripMenuItem";
            this.Measure2DCircleToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Measure2DCircleToolStripMenuItem.Text = "원형 측정";
            this.Measure2DCircleToolStripMenuItem.Click += new System.EventHandler(this.Measure2DCircleToolStripMenuItem_Click);
            // 
            // dMeasurementToolStripMenuItem1
            // 
            this.dMeasurementToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Measure3DLineToolStripMenuItem,
            this.Measure3DPolyLineToolStripMenuItem,
            this.Measure3DAreaToolStripMenuItem,
            this.Measure3DAngleToolStripMenuItem,
            this.Measure3DAngle3PtToolStripMenuItem,
            this.Measure3DCircleToolStripMenuItem});
            this.dMeasurementToolStripMenuItem1.Name = "dMeasurementToolStripMenuItem1";
            this.dMeasurementToolStripMenuItem1.Size = new System.Drawing.Size(112, 20);
            this.dMeasurementToolStripMenuItem1.Text = "3D Measurement";
            // 
            // Measure3DLineToolStripMenuItem
            // 
            this.Measure3DLineToolStripMenuItem.Name = "Measure3DLineToolStripMenuItem";
            this.Measure3DLineToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Measure3DLineToolStripMenuItem.Text = "거리(단선) 측정";
            this.Measure3DLineToolStripMenuItem.Click += new System.EventHandler(this.Measure3DLineToolStripMenuItem_Click);
            // 
            // Measure3DPolyLineToolStripMenuItem
            // 
            this.Measure3DPolyLineToolStripMenuItem.Name = "Measure3DPolyLineToolStripMenuItem";
            this.Measure3DPolyLineToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Measure3DPolyLineToolStripMenuItem.Text = "거리(폴리라인) 측정";
            this.Measure3DPolyLineToolStripMenuItem.Click += new System.EventHandler(this.Measure3DPolyLineToolStripMenuItem_Click);
            // 
            // Measure3DAreaToolStripMenuItem
            // 
            this.Measure3DAreaToolStripMenuItem.Name = "Measure3DAreaToolStripMenuItem";
            this.Measure3DAreaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Measure3DAreaToolStripMenuItem.Text = "면적 측정";
            this.Measure3DAreaToolStripMenuItem.Click += new System.EventHandler(this.Measure3DAreaToolStripMenuItem_Click);
            // 
            // Measure3DAngleToolStripMenuItem
            // 
            this.Measure3DAngleToolStripMenuItem.Name = "Measure3DAngleToolStripMenuItem";
            this.Measure3DAngleToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Measure3DAngleToolStripMenuItem.Text = "각도 측정(진북방향)";
            this.Measure3DAngleToolStripMenuItem.Click += new System.EventHandler(this.Measure3DAngleToolStripMenuItem_Click);
            // 
            // Measure3DAngle3PtToolStripMenuItem
            // 
            this.Measure3DAngle3PtToolStripMenuItem.Name = "Measure3DAngle3PtToolStripMenuItem";
            this.Measure3DAngle3PtToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Measure3DAngle3PtToolStripMenuItem.Text = "각도 측정(3점 이용)";
            this.Measure3DAngle3PtToolStripMenuItem.Click += new System.EventHandler(this.Measure3DAngle3PtToolStripMenuItem_Click);
            // 
            // Measure3DCircleToolStripMenuItem
            // 
            this.Measure3DCircleToolStripMenuItem.Name = "Measure3DCircleToolStripMenuItem";
            this.Measure3DCircleToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Measure3DCircleToolStripMenuItem.Text = "원형 측정";
            this.Measure3DCircleToolStripMenuItem.Click += new System.EventHandler(this.Measure3DCircleToolStripMenuItem_Click);
            // 
            // 사용자정의ToolStripMenuItem
            // 
            this.사용자정의ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserDefinedMeasureToolStripMenuItem});
            this.사용자정의ToolStripMenuItem.Name = "사용자정의ToolStripMenuItem";
            this.사용자정의ToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.사용자정의ToolStripMenuItem.Text = "사용자 정의";
            // 
            // UserDefinedMeasureToolStripMenuItem
            // 
            this.UserDefinedMeasureToolStripMenuItem.Name = "UserDefinedMeasureToolStripMenuItem";
            this.UserDefinedMeasureToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.UserDefinedMeasureToolStripMenuItem.Text = "거리 및 각도 측정";
            this.UserDefinedMeasureToolStripMenuItem.Click += new System.EventHandler(this.UserDefinedMeasureToolStripMenuItem_Click);
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
            this.splitContainer1.Size = new System.Drawing.Size(996, 558);
            this.splitContainer1.SplitterDistance = 476;
            this.splitContainer1.TabIndex = 1;
            // 
            // nxPlanetView2D
            // 
            this.nxPlanetView2D.AutoFocus = false;
            this.nxPlanetView2D.Brightness = 1F;
            this.nxPlanetView2D.Contrast = 1F;
            this.nxPlanetView2D.Controls.Add(this.nxPlanetLayer2D);
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
            this.nxPlanetView2D.Size = new System.Drawing.Size(476, 558);
            this.nxPlanetView2D.TabIndex = 0;
            this.nxPlanetView2D.ToolboxAreaUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxAreaUnit.SquareMeter;
            this.nxPlanetView2D.ToolboxDistUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxDistUnit.Meter;
            this.nxPlanetView2D.ToolboxMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxMode.None;
            xAngle1.deg = 45D;
            this.nxPlanetView2D.ViewAreaFOV = xAngle1;
            this.nxPlanetView2D.ViewAreaID = -1;
            this.nxPlanetView2D.ZoomCenterMode = Pixoneer.NXDL.eViewZoomCenterMode.ByCenter;
            // 
            // nxPlanetLayer2D
            // 
            this.nxPlanetLayer2D.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nxPlanetLayer2D.LayerCapture = true;
            this.nxPlanetLayer2D.LayerVisible = true;
            this.nxPlanetLayer2D.Location = new System.Drawing.Point(107, 90);
            this.nxPlanetLayer2D.Name = "nxPlanetLayer2D";
            this.nxPlanetLayer2D.Size = new System.Drawing.Size(145, 30);
            this.nxPlanetLayer2D.TabIndex = 0;
            this.nxPlanetLayer2D.Visible = false;
            this.nxPlanetLayer2D.OnWndProc += new Pixoneer.NXDL.NXPlanet.NXPlanetLayerWndProcEvent(this.nxPlanetLayer2D_OnWndProc);
            this.nxPlanetLayer2D.OnRender += new Pixoneer.NXDL.NXPlanet.NXPlanetLayerRenderEvent(this.nxPlanetLayer2D_OnRender);
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
            this.nxPlanetView3D.Size = new System.Drawing.Size(516, 558);
            this.nxPlanetView3D.TabIndex = 0;
            this.nxPlanetView3D.ToolboxAreaUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxAreaUnit.SquareMeter;
            this.nxPlanetView3D.ToolboxDistUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxDistUnit.Meter;
            this.nxPlanetView3D.ToolboxMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxMode.None;
            xAngle2.deg = 45D;
            this.nxPlanetView3D.ViewAreaFOV = xAngle2;
            this.nxPlanetView3D.ViewAreaID = -1;
            this.nxPlanetView3D.ZoomCenterMode = Pixoneer.NXDL.eViewZoomCenterMode.ByCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 582);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.nxPlanetView2D.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Pixoneer.NXDL.NXPlanet.NXPlanetView nxPlanetView2D;
        private Pixoneer.NXDL.NXPlanet.NXPlanetLayer nxPlanetLayer2D;
        private Pixoneer.NXDL.NXPlanet.NXPlanetView nxPlanetView3D;
        private System.Windows.Forms.ToolStripMenuItem dMeasurementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Measure2DLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Measure2DPolyLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Measure2DAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Measure2DAngleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Measure2DAngle3PtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Measure2DCircleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dMeasurementToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem Measure3DLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Measure3DPolyLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Measure3DAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Measure3DAngleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Measure3DAngle3PtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Measure3DCircleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 사용자정의ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UserDefinedMeasureToolStripMenuItem;
    }
}

