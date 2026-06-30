using System;
using System.Runtime.InteropServices;

namespace GraphComServer;

[ComVisible(true)]
[Guid("4F9B77A1-864D-42E7-BC68-823721DE8456")]
[ClassInterface(ClassInterfaceType.None)]
[ProgId("WpfGraphSample.GraphSeriesProvider")]
public sealed class GraphSeriesProviderCom : IGraphSeriesProviderCom
{
    public GraphPointCom[] CreateSeries(
        int function,
        double amplitude,
        double frequency,
        double minX,
        double maxX,
        int sampleCount)
    {
        var count = Math.Clamp(sampleCount, 2, 5000);
        var points = new GraphPointCom[count];
        var step = (maxX - minX) / (count - 1);

        for (var i = 0; i < count; i++)
        {
            var x = minX + step * i;
            var y = CalculateY(function, x, amplitude, frequency);
            points[i] = new GraphPointCom(x, y);
        }

        return points;
    }

    public double CalculateY(int function, double x, double amplitude, double frequency)
    {
        return (GraphFunctionCom)function switch
        {
            GraphFunctionCom.Sine => amplitude * Math.Sin(frequency * x),
            GraphFunctionCom.Cosine => amplitude * Math.Cos(frequency * x),
            GraphFunctionCom.Parabola => amplitude * 0.08 * x * x,
            GraphFunctionCom.Cubic => amplitude * 0.01 * x * x * x,
            _ => 0
        };
    }
}
