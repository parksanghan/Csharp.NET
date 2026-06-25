using System;
using System.Windows;
using WpfComObjectSample.Graphing;

namespace WpfComObjectSample;

public partial class MainWindow : Window
{
    private readonly GraphRenderer _graphRenderer = new();

    public MainWindow()
    {
        InitializeComponent();
        DrawGraph();
    }

    private void GraphInput_Changed(object sender, RoutedEventArgs e)
    {
        DrawGraph();
    }

    private void GraphCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        DrawGraph();
    }

    private void RedrawButton_Click(object sender, RoutedEventArgs e)
    {
        DrawGraph();
    }

    private void DrawGraph()
    {
        if (!IsLoaded || GraphCanvas is null || FunctionComboBox.SelectedItem is not GraphFunction function)
        {
            return;
        }

        var options = new GraphOptions(
            function,
            AmplitudeSlider.Value,
            FrequencySlider.Value,
            RangeSlider.Value);

        AmplitudeTextBlock.Text = options.Amplitude.ToString("0.0");
        FrequencyTextBlock.Text = options.Frequency.ToString("0.0");
        RangeTextBlock.Text = $"-{options.XRange:0} to {options.XRange:0}";

        _graphRenderer.Draw(GraphCanvas, options);
    }
}
