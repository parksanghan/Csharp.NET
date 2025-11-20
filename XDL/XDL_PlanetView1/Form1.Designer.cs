namespace XDL_PlanetView1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxShowStatusInfo = new System.Windows.Forms.CheckBox();
            this.checkBoxShowStar = new System.Windows.Forms.CheckBox();
            this.checkBoxShowPBP = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonScaleApply = new System.Windows.Forms.Button();
            this.comboBoxScale = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxGrid = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxRotatable = new System.Windows.Forms.CheckBox();
            this.checkBoxInverseMouseWheel = new System.Windows.Forms.CheckBox();
            this.checkBoxInverseMouseButton = new System.Windows.Forms.CheckBox();
            this.nxPlanetView1 = new Pixoneer.NXDL.NXPlanet.NXPlanetView();
            this.nxPlanetLayer1 = new Pixoneer.NXDL.NXPlanet.NXPlanetLayer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.nxPlanetView1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.nxPlanetView1);
            this.splitContainer1.Size = new System.Drawing.Size(1139, 662);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxShowStatusInfo);
            this.groupBox4.Controls.Add(this.checkBoxShowStar);
            this.groupBox4.Controls.Add(this.checkBoxShowPBP);
            this.groupBox4.Location = new System.Drawing.Point(10, 287);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(183, 92);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // checkBoxShowStatusInfo
            // 
            this.checkBoxShowStatusInfo.AutoSize = true;
            this.checkBoxShowStatusInfo.Location = new System.Drawing.Point(17, 66);
            this.checkBoxShowStatusInfo.Name = "checkBoxShowStatusInfo";
            this.checkBoxShowStatusInfo.Size = new System.Drawing.Size(115, 16);
            this.checkBoxShowStatusInfo.TabIndex = 2;
            this.checkBoxShowStatusInfo.Text = "Show StatusInfo";
            this.checkBoxShowStatusInfo.UseVisualStyleBackColor = true;
            this.checkBoxShowStatusInfo.CheckedChanged += new System.EventHandler(this.checkBoxShowStatusInfo_CheckedChanged);
            // 
            // checkBoxShowStar
            // 
            this.checkBoxShowStar.AutoSize = true;
            this.checkBoxShowStar.Location = new System.Drawing.Point(17, 42);
            this.checkBoxShowStar.Name = "checkBoxShowStar";
            this.checkBoxShowStar.Size = new System.Drawing.Size(82, 16);
            this.checkBoxShowStar.TabIndex = 1;
            this.checkBoxShowStar.Text = "Show Star";
            this.checkBoxShowStar.UseVisualStyleBackColor = true;
            this.checkBoxShowStar.CheckedChanged += new System.EventHandler(this.checkBoxShowStar_CheckedChanged);
            // 
            // checkBoxShowPBP
            // 
            this.checkBoxShowPBP.AutoSize = true;
            this.checkBoxShowPBP.Location = new System.Drawing.Point(17, 18);
            this.checkBoxShowPBP.Name = "checkBoxShowPBP";
            this.checkBoxShowPBP.Size = new System.Drawing.Size(84, 16);
            this.checkBoxShowPBP.TabIndex = 0;
            this.checkBoxShowPBP.Text = "Show PBP";
            this.checkBoxShowPBP.UseVisualStyleBackColor = true;
            this.checkBoxShowPBP.CheckedChanged += new System.EventHandler(this.checkBoxShowPBP_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonScaleApply);
            this.groupBox3.Controls.Add(this.comboBoxScale);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(10, 200);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(183, 88);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scale";
            // 
            // buttonScaleApply
            // 
            this.buttonScaleApply.Location = new System.Drawing.Point(86, 52);
            this.buttonScaleApply.Name = "buttonScaleApply";
            this.buttonScaleApply.Size = new System.Drawing.Size(75, 23);
            this.buttonScaleApply.TabIndex = 2;
            this.buttonScaleApply.Text = "Apply";
            this.buttonScaleApply.UseVisualStyleBackColor = true;
            this.buttonScaleApply.Click += new System.EventHandler(this.buttonScaleApply_Click);
            // 
            // comboBoxScale
            // 
            this.comboBoxScale.FormattingEnabled = true;
            this.comboBoxScale.Items.AddRange(new object[] {
            "1000000",
            "500000",
            "100000",
            "50000",
            "10000"});
            this.comboBoxScale.Location = new System.Drawing.Point(40, 25);
            this.comboBoxScale.Name = "comboBoxScale";
            this.comboBoxScale.Size = new System.Drawing.Size(121, 20);
            this.comboBoxScale.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "1 :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxGrid);
            this.groupBox2.Location = new System.Drawing.Point(10, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(183, 56);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grid";
            // 
            // comboBoxGrid
            // 
            this.comboBoxGrid.FormattingEnabled = true;
            this.comboBoxGrid.Items.AddRange(new object[] {
            "None",
            "Degrees",
            "GARS"});
            this.comboBoxGrid.Location = new System.Drawing.Point(17, 20);
            this.comboBoxGrid.Name = "comboBoxGrid";
            this.comboBoxGrid.Size = new System.Drawing.Size(149, 20);
            this.comboBoxGrid.TabIndex = 0;
            this.comboBoxGrid.SelectedIndexChanged += new System.EventHandler(this.comboBoxGrid_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxRotatable);
            this.groupBox1.Controls.Add(this.checkBoxInverseMouseWheel);
            this.groupBox1.Controls.Add(this.checkBoxInverseMouseButton);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mouse";
            // 
            // checkBoxRotatable
            // 
            this.checkBoxRotatable.AutoSize = true;
            this.checkBoxRotatable.Location = new System.Drawing.Point(17, 84);
            this.checkBoxRotatable.Name = "checkBoxRotatable";
            this.checkBoxRotatable.Size = new System.Drawing.Size(76, 16);
            this.checkBoxRotatable.TabIndex = 2;
            this.checkBoxRotatable.Text = "Rotatable";
            this.checkBoxRotatable.UseVisualStyleBackColor = true;
            this.checkBoxRotatable.CheckedChanged += new System.EventHandler(this.checkBoxRotatable_CheckedChanged);
            // 
            // checkBoxInverseMouseWheel
            // 
            this.checkBoxInverseMouseWheel.AutoSize = true;
            this.checkBoxInverseMouseWheel.Location = new System.Drawing.Point(17, 42);
            this.checkBoxInverseMouseWheel.Name = "checkBoxInverseMouseWheel";
            this.checkBoxInverseMouseWheel.Size = new System.Drawing.Size(138, 16);
            this.checkBoxInverseMouseWheel.TabIndex = 1;
            this.checkBoxInverseMouseWheel.Text = "InverseMouseWheel";
            this.checkBoxInverseMouseWheel.UseVisualStyleBackColor = true;
            this.checkBoxInverseMouseWheel.CheckedChanged += new System.EventHandler(this.checkBoxInverseMouseWheel_CheckedChanged);
            // 
            // checkBoxInverseMouseButton
            // 
            this.checkBoxInverseMouseButton.AutoSize = true;
            this.checkBoxInverseMouseButton.Location = new System.Drawing.Point(17, 20);
            this.checkBoxInverseMouseButton.Name = "checkBoxInverseMouseButton";
            this.checkBoxInverseMouseButton.Size = new System.Drawing.Size(139, 16);
            this.checkBoxInverseMouseButton.TabIndex = 0;
            this.checkBoxInverseMouseButton.Text = "InverseMouseButton";
            this.checkBoxInverseMouseButton.UseVisualStyleBackColor = true;
            this.checkBoxInverseMouseButton.CheckedChanged += new System.EventHandler(this.checkBoxInverseMouseButton_CheckedChanged);
            // 
            // nxPlanetView1
            // 
            this.nxPlanetView1.AutoFocus = false;
            this.nxPlanetView1.Brightness = 1F;
            this.nxPlanetView1.Contrast = 1F;
            this.nxPlanetView1.Controls.Add(this.nxPlanetLayer1);
            this.nxPlanetView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nxPlanetView1.EarthMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eEarthMode.Planet2D;
            this.nxPlanetView1.FrameCapture = null;
            this.nxPlanetView1.GridType = Pixoneer.NXDL.NXPlanet.NXPlanetView.eGridType.GridDegrees;
            this.nxPlanetView1.InverseMouseButton = false;
            this.nxPlanetView1.InverseMouseWheel = false;
            this.nxPlanetView1.LayoutMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eLayoutMode.Windows;
            this.nxPlanetView1.Location = new System.Drawing.Point(0, 0);
            this.nxPlanetView1.Name = "nxPlanetView1";
            this.nxPlanetView1.RelativeHeight = 1D;
            this.nxPlanetView1.RelativeLeft = 0D;
            this.nxPlanetView1.RelativeTop = 0D;
            this.nxPlanetView1.RelativeWidth = 1D;
            this.nxPlanetView1.RestrictRenerArea = false;
            this.nxPlanetView1.Rotatable = true;
            this.nxPlanetView1.Saturation = 1F;
            this.nxPlanetView1.ShowGrid = true;
            this.nxPlanetView1.ShowPBP = true;
            this.nxPlanetView1.ShowStatusInfo = false;
            this.nxPlanetView1.Size = new System.Drawing.Size(934, 662);
            this.nxPlanetView1.TabIndex = 0;
            this.nxPlanetView1.ToolboxAreaUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxAreaUnit.SquareMeter;
            this.nxPlanetView1.ToolboxDistUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxDistUnit.Meter;
            this.nxPlanetView1.ToolboxMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxMode.None;
            xAngle1.deg = 45D;
            this.nxPlanetView1.ViewAreaFOV = xAngle1;
            this.nxPlanetView1.ViewAreaID = -1;
            this.nxPlanetView1.ZoomCenterMode = Pixoneer.NXDL.eViewZoomCenterMode.CenterByCursor;
            // 
            // nxPlanetLayer1
            // 
            this.nxPlanetLayer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nxPlanetLayer1.LayerCapture = true;
            this.nxPlanetLayer1.LayerVisible = true;
            this.nxPlanetLayer1.Location = new System.Drawing.Point(74, 102);
            this.nxPlanetLayer1.Name = "nxPlanetLayer1";
            this.nxPlanetLayer1.Size = new System.Drawing.Size(145, 30);
            this.nxPlanetLayer1.TabIndex = 0;
            this.nxPlanetLayer1.Visible = false;
            this.nxPlanetLayer1.OnWndProc += new Pixoneer.NXDL.NXPlanet.NXPlanetLayerWndProcEvent(this.nxPlanetLayer1_OnWndProc);
            this.nxPlanetLayer1.OnOrthoRender += new Pixoneer.NXDL.NXPlanet.NXPlanetLayerRenderEvent(this.nxPlanetLayer1_OnOrthoRender);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 662);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.nxPlanetView1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Pixoneer.NXDL.NXPlanet.NXPlanetView nxPlanetView1;
        private Pixoneer.NXDL.NXPlanet.NXPlanetLayer nxPlanetLayer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxRotatable;
        private System.Windows.Forms.CheckBox checkBoxInverseMouseWheel;
        private System.Windows.Forms.CheckBox checkBoxInverseMouseButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxScale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxGrid;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxShowStatusInfo;
        private System.Windows.Forms.CheckBox checkBoxShowStar;
        private System.Windows.Forms.CheckBox checkBoxShowPBP;
        private System.Windows.Forms.Button buttonScaleApply;
    }
}

