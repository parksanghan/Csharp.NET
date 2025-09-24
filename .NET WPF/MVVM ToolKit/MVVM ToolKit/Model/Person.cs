using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_ToolKit.Model
{
    public partial class Person: ObservableObject
    {
        [ObservableProperty] private string name = string.Empty;
        [ObservableProperty] private int age;
    }
}
