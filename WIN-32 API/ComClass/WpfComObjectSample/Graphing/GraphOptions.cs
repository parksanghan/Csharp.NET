namespace WpfComObjectSample.Graphing;

public sealed record GraphOptions(
    GraphFunction Function,
    double Amplitude,
    double Frequency,
    double XRange);
