namespace XDL_PlanetView3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.nxPlanetView2D = new Pixoneer.NXDL.NXPlanet.NXPlanetView();
            this.nxPlanetView3D = new Pixoneer.NXDL.NXPlanet.NXPlanetView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.pointToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.polylineToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.polygonToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.circleToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.symbolToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.saveToFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.loadFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.nxPlanetView2D);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.nxPlanetView3D);
            this.splitContainer1.Size = new System.Drawing.Size(1071, 569);
            this.splitContainer1.SplitterDistance = 534;
            this.splitContainer1.TabIndex = 2;
            // 
            // nxPlanetView2D
            // 
            this.nxPlanetView2D.AutoFocus = false;
            this.nxPlanetView2D.Brightness = 1F;
            this.nxPlanetView2D.Contrast = 1F;
            this.nxPlanetView2D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nxPlanetView2D.EarthMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eEarthMode.Planet2D;
            this.nxPlanetView2D.FrameCapture = null;
            this.nxPlanetView2D.GridType = Pixoneer.NXDL.NXPlanet.NXPlanetView.eGridType.GridNone;
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
            this.nxPlanetView2D.ShowGrid = false;
            this.nxPlanetView2D.ShowPBP = true;
            this.nxPlanetView2D.ShowStatusInfo = false;
            this.nxPlanetView2D.Size = new System.Drawing.Size(534, 569);
            this.nxPlanetView2D.TabIndex = 0;
            this.nxPlanetView2D.ToolboxAreaUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxAreaUnit.SquareMeter;
            this.nxPlanetView2D.ToolboxDistUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxDistUnit.Meter;
            this.nxPlanetView2D.ToolboxMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxMode.None;
            xAngle1.deg = 45D;
            this.nxPlanetView2D.ViewAreaFOV = xAngle1;
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
            this.nxPlanetView3D.Size = new System.Drawing.Size(533, 569);
            this.nxPlanetView3D.TabIndex = 0;
            this.nxPlanetView3D.ToolboxAreaUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxAreaUnit.SquareMeter;
            this.nxPlanetView3D.ToolboxDistUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxDistUnit.Meter;
            this.nxPlanetView3D.ToolboxMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxMode.None;
            xAngle2.deg = 45D;
            this.nxPlanetView3D.ViewAreaFOV = xAngle2;
            this.nxPlanetView3D.ViewAreaID = -1;
            this.nxPlanetView3D.ZoomCenterMode = Pixoneer.NXDL.eViewZoomCenterMode.CenterByCursor;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.pointToolStripButton,
            this.polylineToolStripButton,
            this.polygonToolStripButton,
            this.circleToolStripButton,
            this.symbolToolStripButton,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.saveToFileToolStripButton,
            this.loadFileToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1071, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton1.Text = "2D Add :";
            // 
            // pointToolStripButton
            // 
            this.pointToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.pointToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pointToolStripButton.Image")));
            this.pointToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pointToolStripButton.Name = "pointToolStripButton";
            this.pointToolStripButton.Size = new System.Drawing.Size(39, 22);
            this.pointToolStripButton.Text = "Point";
            this.pointToolStripButton.Click += new System.EventHandler(this.pointToolStripButton_Click);
            // 
            // polylineToolStripButton
            // 
            this.polylineToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.polylineToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("polylineToolStripButton.Image")));
            this.polylineToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.polylineToolStripButton.Name = "polylineToolStripButton";
            this.polylineToolStripButton.Size = new System.Drawing.Size(53, 22);
            this.polylineToolStripButton.Text = "Polyline";
            this.polylineToolStripButton.Click += new System.EventHandler(this.polylineToolStripButton_Click);
            // 
            // polygonToolStripButton
            // 
            this.polygonToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.polygonToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("polygonToolStripButton.Image")));
            this.polygonToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.polygonToolStripButton.Name = "polygonToolStripButton";
            this.polygonToolStripButton.Size = new System.Drawing.Size(55, 22);
            this.polygonToolStripButton.Text = "Polygon";
            this.polygonToolStripButton.Click += new System.EventHandler(this.polygonToolStripButton_Click);
            // 
            // circleToolStripButton
            // 
            this.circleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.circleToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("circleToolStripButton.Image")));
            this.circleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.circleToolStripButton.Name = "circleToolStripButton";
            this.circleToolStripButton.Size = new System.Drawing.Size(41, 22);
            this.circleToolStripButton.Text = "Circle";
            this.circleToolStripButton.Click += new System.EventHandler(this.circleToolStripButton_Click);
            // 
            // symbolToolStripButton
            // 
            this.symbolToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.symbolToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("symbolToolStripButton.Image")));
            this.symbolToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.symbolToolStripButton.Name = "symbolToolStripButton";
            this.symbolToolStripButton.Size = new System.Drawing.Size(52, 22);
            this.symbolToolStripButton.Text = "Symbol";
            this.symbolToolStripButton.Click += new System.EventHandler(this.symbolToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton2.Text = "File :";
            // 
            // saveToFileToolStripButton
            // 
            this.saveToFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveToFileToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToFileToolStripButton.Image")));
            this.saveToFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToFileToolStripButton.Name = "saveToFileToolStripButton";
            this.saveToFileToolStripButton.Size = new System.Drawing.Size(73, 22);
            this.saveToFileToolStripButton.Text = "Save to File";
            this.saveToFileToolStripButton.Click += new System.EventHandler(this.saveToFileToolStripButton_Click);
            // 
            // loadFileToolStripButton
            // 
            this.loadFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadFileToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("loadFileToolStripButton.Image")));
            this.loadFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadFileToolStripButton.Name = "loadFileToolStripButton";
            this.loadFileToolStripButton.Size = new System.Drawing.Size(112, 22);
            this.loadFileToolStripButton.Text = "Load into 3D View";
            this.loadFileToolStripButton.Click += new System.EventHandler(this.loadFileToolStripButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 594);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Pixoneer.NXDL.NXPlanet.NXPlanetView nxPlanetView2D;
        private Pixoneer.NXDL.NXPlanet.NXPlanetView nxPlanetView3D;
        private Pixoneer.NXDL.NSCENE.NXPlanetLayerSceneEditor  nxSceneLayerEditor;
        private Pixoneer.NXDL.NSCENE.NXPlanetLayerSceneDisplay nxSceneLayerDisplay;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton pointToolStripButton;
        private System.Windows.Forms.ToolStripButton polylineToolStripButton;
        private System.Windows.Forms.ToolStripButton polygonToolStripButton;
        private System.Windows.Forms.ToolStripButton circleToolStripButton;
        private System.Windows.Forms.ToolStripButton symbolToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton saveToFileToolStripButton;
        private System.Windows.Forms.ToolStripButton loadFileToolStripButton;
    }
}

