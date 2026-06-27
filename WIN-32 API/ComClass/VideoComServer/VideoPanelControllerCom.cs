using System;
using System.IO;
using System.Runtime.InteropServices;

namespace VideoComServer;

[ComVisible(true)]
[Guid("1C3A03DD-A80D-470D-82FA-B614570A6B18")]
[ClassInterface(ClassInterfaceType.None)]
[ProgId("WpfGraphSample.VideoPanelController")]
public sealed class VideoPanelControllerCom : IVideoPanelControllerCom
{
    private string? _filePath;
    private VideoPlaybackState _state = VideoPlaybackState.Empty;
    private double _positionSeconds;
    private double _volume = 0.7;

    public VideoCommandResultCom Load(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return Fail("Choose a video file.");
        }

        if (!File.Exists(filePath))
        {
            return Fail($"File not found: {filePath}");
        }

        _filePath = filePath;
        _positionSeconds = 0;
        _state = VideoPlaybackState.Loaded;
        return Ok($"Loaded: {Path.GetFileName(filePath)}");
    }

    public VideoCommandResultCom Play()
    {
        if (_filePath is null)
        {
            return Fail("Load a video before playing.");
        }

        _state = VideoPlaybackState.Playing;
        return Ok("Play");
    }

    public VideoCommandResultCom Pause()
    {
        if (_filePath is null)
        {
            return Fail("Load a video before pausing.");
        }

        _state = VideoPlaybackState.Paused;
        return Ok("Pause");
    }

    public VideoCommandResultCom Stop()
    {
        if (_filePath is null)
        {
            return Fail("Load a video before stopping.");
        }

        _positionSeconds = 0;
        _state = VideoPlaybackState.Stopped;
        return Ok("Stop");
    }

    public VideoCommandResultCom SeekSeconds(double seconds)
    {
        if (_filePath is null)
        {
            return Fail("Load a video before seeking.");
        }

        _positionSeconds = Math.Max(0, seconds);
        return Ok($"Seek: {_positionSeconds:0.0}s");
    }

    public VideoCommandResultCom SetVolume(double volume)
    {
        _volume = Math.Clamp(volume, 0, 1);
        return Ok($"Volume: {_volume:P0}");
    }

    public string GetStatus()
    {
        var fileName = _filePath is null ? "No file" : Path.GetFileName(_filePath);
        return $"{_state} | {fileName} | {_positionSeconds:0.0}s | Volume {_volume:P0}";
    }

    private VideoCommandResultCom Ok(string message)
    {
        return new VideoCommandResultCom
        {
            IsSuccess = true,
            Message = message,
            State = (int)_state
        };
    }

    private VideoCommandResultCom Fail(string message)
    {
        return new VideoCommandResultCom
        {
            IsSuccess = false,
            Message = message,
            State = (int)_state
        };
    }
}
