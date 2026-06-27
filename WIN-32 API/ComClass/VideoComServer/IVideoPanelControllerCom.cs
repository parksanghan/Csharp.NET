using System.Runtime.InteropServices;

namespace VideoComServer;

[ComVisible(true)]
[Guid("08AFC8E2-C035-4C5A-8600-D92092758000")]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
public interface IVideoPanelControllerCom
{
    [DispId(1)]
    VideoCommandResultCom Load(string filePath);

    [DispId(2)]
    VideoCommandResultCom Play();

    [DispId(3)]
    VideoCommandResultCom Pause();

    [DispId(4)]
    VideoCommandResultCom Stop();

    [DispId(5)]
    VideoCommandResultCom SeekSeconds(double seconds);

    [DispId(6)]
    VideoCommandResultCom SetVolume(double volume);

    [DispId(7)]
    string GetStatus();
}
