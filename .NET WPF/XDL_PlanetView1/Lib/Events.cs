using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Lib
{
    /// <summary>
    /// 인자 X Void 리턴 Delegate
    /// </summary>
    public class EVT : Singleton<string, CVoidDelegate>
    {

    }
    public class EvenR_I : Singleton<string , CReturnDelegate<int>>
    {
    }
    public class EvenR_S : Singleton<string, CReturnDelegate<string>>
    {
    }
    public class EvenR_O : Singleton<string, CReturnDelegate<object>>
    {
    }
    /// <summary>
    /// <para>• No Return</para>
    /// <para>• Args 1: Object sender</para>
    /// <para>• Args 2: RoutedEventArgs args</para>
    /// </summary>
    public class EvT_OR: Singleton<string, CReturnDelegate<object, RoutedEventArgs>>
    {

    }
}
