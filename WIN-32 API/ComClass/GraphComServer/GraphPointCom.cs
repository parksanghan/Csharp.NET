using System.Runtime.InteropServices;

namespace GraphComServer;

[ComVisible(true)]
[Guid("8250B9D2-30D1-43C9-9619-D1E2D057D5EE")]
[ClassInterface(ClassInterfaceType.None)]
public sealed class GraphPointCom
{
    public GraphPointCom()
    {
    }

    public GraphPointCom(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; set; }

    public double Y { get; set; }
}
