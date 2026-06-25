using System;

namespace WpfComObjectSample.Graphing;

public sealed class GraphCalculator
{
    public double CalculateY(double x, GraphOptions options)
    {
        return options.Function switch
        {
            GraphFunction.Sine => options.Amplitude * Math.Sin(options.Frequency * x),
            GraphFunction.Cosine => options.Amplitude * Math.Cos(options.Frequency * x),
            GraphFunction.Parabola => options.Amplitude * 0.08 * x * x,
            GraphFunction.Cubic => options.Amplitude * 0.01 * x * x * x,
            _ => 0
        };
    }
}
