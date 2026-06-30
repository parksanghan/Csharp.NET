using System.Runtime.InteropServices;

namespace GraphComServer;

[ComVisible(true)]
[Guid("92C5EF45-B1E4-4C8F-8DC4-3910BA2B2879")]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
public interface IGraphSeriesProviderCom
{
    [DispId(1)]
    GraphPointCom[] CreateSeries(
        int function,
        double amplitude,
        double frequency,
        double minX,
        double maxX,
        int sampleCount);

    [DispId(2)]
    double CalculateY(int function, double x, double amplitude, double frequency);
}
