using System.Runtime.InteropServices;

namespace ImageComServer;

[ComVisible(true)]
[Guid("984E7426-685F-4021-9EF6-11EAB5DB0551")]
[ClassInterface(ClassInterfaceType.None)]
public sealed class ImageCommandResultCom
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; } = string.Empty;
}
