using System.Runtime.InteropServices;

namespace ImageComServer;

[ComVisible(true)]
[Guid("3867109D-B855-49CA-B2E8-FB2B5F21D929")]
[ClassInterface(ClassInterfaceType.None)]
public sealed class ImagePanelStateCom
{
    public string FilePath { get; set; } = string.Empty;

    public double Zoom { get; set; } = 1;

    public double RotationDegrees { get; set; }

    public double Brightness { get; set; }

    public double Contrast { get; set; } = 1;
}
