using System.Runtime.InteropServices;

namespace ImageComServer;

[ComVisible(true)]
[Guid("EF24E8AE-2233-4826-83B3-B5544310C50D")]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
public interface IImagePanelControllerCom
{
    [DispId(1)]
    ImageCommandResultCom Load(string filePath);

    [DispId(2)]
    ImageCommandResultCom ZoomIn();

    [DispId(3)]
    ImageCommandResultCom ZoomOut();

    [DispId(4)]
    ImageCommandResultCom RotateLeft();

    [DispId(5)]
    ImageCommandResultCom RotateRight();

    [DispId(6)]
    ImageCommandResultCom SetBrightness(double brightness);

    [DispId(7)]
    ImageCommandResultCom SetContrast(double contrast);

    [DispId(8)]
    ImageCommandResultCom Reset();

    [DispId(9)]
    ImagePanelStateCom GetState();

    [DispId(10)]
    string GetStatus();
}
