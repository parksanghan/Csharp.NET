using System.Runtime.InteropServices;

namespace VideoComServer;

[ComVisible(true)]
[Guid("5CF6B56B-99FE-470E-9835-E359B7E46F0E")]
[ClassInterface(ClassInterfaceType.None)]
public sealed class VideoCommandResultCom
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; } = string.Empty;

    public int State { get; set; }
}
