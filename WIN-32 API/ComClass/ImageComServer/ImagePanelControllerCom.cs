using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ImageComServer;

[ComVisible(true)]
[Guid("8C70894E-27EA-49B3-90F5-19748836A59F")]
[ClassInterface(ClassInterfaceType.None)]
[ProgId("WpfGraphSample.ImagePanelController")]
public sealed class ImagePanelControllerCom : IImagePanelControllerCom
{
    private readonly ImagePanelStateCom _state = new();

    public ImageCommandResultCom Load(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return Fail("Choose an image file.");
        }

        if (!File.Exists(filePath))
        {
            return Fail($"File not found: {filePath}");
        }

        _state.FilePath = filePath;
        _state.Zoom = 1;
        _state.RotationDegrees = 0;
        _state.Brightness = 0;
        _state.Contrast = 1;

        return Ok($"Loaded: {Path.GetFileName(filePath)}");
    }

    public ImageCommandResultCom ZoomIn()
    {
        _state.Zoom = Math.Min(8, _state.Zoom + 0.25);
        return Ok($"Zoom: {_state.Zoom:P0}");
    }

    public ImageCommandResultCom ZoomOut()
    {
        _state.Zoom = Math.Max(0.1, _state.Zoom - 0.25);
        return Ok($"Zoom: {_state.Zoom:P0}");
    }

    public ImageCommandResultCom RotateLeft()
    {
        _state.RotationDegrees = NormalizeRotation(_state.RotationDegrees - 90);
        return Ok($"Rotation: {_state.RotationDegrees:0} deg");
    }

    public ImageCommandResultCom RotateRight()
    {
        _state.RotationDegrees = NormalizeRotation(_state.RotationDegrees + 90);
        return Ok($"Rotation: {_state.RotationDegrees:0} deg");
    }

    public ImageCommandResultCom SetBrightness(double brightness)
    {
        _state.Brightness = Math.Clamp(brightness, -1, 1);
        return Ok($"Brightness: {_state.Brightness:0.00}");
    }

    public ImageCommandResultCom SetContrast(double contrast)
    {
        _state.Contrast = Math.Clamp(contrast, 0.2, 3);
        return Ok($"Contrast: {_state.Contrast:0.00}");
    }

    public ImageCommandResultCom Reset()
    {
        _state.Zoom = 1;
        _state.RotationDegrees = 0;
        _state.Brightness = 0;
        _state.Contrast = 1;
        return Ok("Image view reset.");
    }

    public ImagePanelStateCom GetState()
    {
        return new ImagePanelStateCom
        {
            FilePath = _state.FilePath,
            Zoom = _state.Zoom,
            RotationDegrees = _state.RotationDegrees,
            Brightness = _state.Brightness,
            Contrast = _state.Contrast
        };
    }

    public string GetStatus()
    {
        var fileName = string.IsNullOrWhiteSpace(_state.FilePath) ? "No image" : Path.GetFileName(_state.FilePath);
        return $"{fileName} | Zoom {_state.Zoom:P0} | Rotation {_state.RotationDegrees:0} deg";
    }

    private static double NormalizeRotation(double degrees)
    {
        var normalized = degrees % 360;
        return normalized < 0 ? normalized + 360 : normalized;
    }

    private static ImageCommandResultCom Ok(string message)
    {
        return new ImageCommandResultCom { IsSuccess = true, Message = message };
    }

    private static ImageCommandResultCom Fail(string message)
    {
        return new ImageCommandResultCom { IsSuccess = false, Message = message };
    }
}
