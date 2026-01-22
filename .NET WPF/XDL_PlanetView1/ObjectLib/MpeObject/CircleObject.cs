using Pixoneer.NXDL.NSCENE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace WPF.ObjectLib.MpeObject
{
    // Circle Object를 CircleElement 에서 상속하여 사용 
    partial class CircleObject:XscCircle
    {
        public CircleObject(int A,int R,int G, int B)
        {
           base.LineColor = Color.FromArgb (A,R,G,B); // 라인설정
           base.FillColor = Color.FromArgb (A,R,G, B);
        }
        public void SetCenterPosition(double x, double y, double z)
        {
            base.SetPoint(x,y,z);
            base.CalcRange();
   
        }


    }
}
