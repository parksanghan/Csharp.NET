using Pixoneer.NXDL;
using Pixoneer.NXDL.NXPlanet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Lib;

namespace WPF.ObjectLib.MapObject
{
    public partial class Planet:NXPlanetView,IDisposable
    {
        private NXPlanetLayer _nxPlanetLayer =  new NXPlanetLayer();
        public bool _editLayer = false;
        public bool _displayLayer = false;
        //public bool _
        public Planet()
        {
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Brightness = 1F;
            this.Contrast = 1F;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EarthMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eEarthMode.Planet2D;
            this.LayoutMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eLayoutMode.Windows;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "nxPlanetView2D";
            this.RelativeHeight = 1D;
            this.RelativeLeft = 0D;
            this.RelativeTop = 0D;
            this.RelativeWidth = 1D;
            this.Rotatable = true;
            this.Saturation = 1F;
            this.ShowGrid = true;
            this.ShowStatusInfo = false;
            this.Size = new System.Drawing.Size(406, 577);
            this.TabIndex = 0;
            this.ToolboxAreaUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxAreaUnit.SquareMeter;
            this.ToolboxDistUnit = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxDistUnit.Meter;
            this.ToolboxMode = Pixoneer.NXDL.NXPlanet.NXPlanetView.eToolboxMode.None;
            this.ViewAreaID = 0;
            this.AddRenderLayer(ref _nxPlanetLayer);
        }
        public void InitCamera()
        { 
            this.SetCameraPosition(
                XGeoPoint.FromDegree(127.4, 38.0, 1500000),
                XAngle.FromDegree(0.0)

            );
            this.SetPBIDefaultDataSet("0+101");
            this.SetPBPDefaultDataSet("0");
             
            NXPlanetEngine.SetPBPDefaultDataSet("0");
          
            this.Refresh();


        }
        protected override void Dispose(bool value)
        {
            if(value) 
            {
                 
            }
             
        }
        public void SetMode(bool mode)
        {
            if (mode) base.EarthMode = eEarthMode.Planet2D;
            else base.EarthMode = eEarthMode.Planet3D;

        }
    
    }
    public partial class CPlanet: Singleton<string, Planet>
    {
        public static Planet Instance(string key, bool mode)
        {
            if (key.Equals("2DMap"))
                Instance(key)._editLayer = true;
            Singleton<string, Planet>.Instance(key).SetMode(mode);
            return Singleton<string, Planet>.Instance(key); 
        }
    }
}
