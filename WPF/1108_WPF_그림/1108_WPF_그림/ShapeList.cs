using System.Collections.ObjectModel;
using System.Windows.Media;

namespace _1108_WPF_그림
{
    public class ShapeList : ObservableCollection<Shape>  // : List<Person>
    {
        public ShapeList()
        {
            Add(new Shape() { Typename = "타원", Pointx = 300 ,Pointy = 600, Size = 100, Color = new SolidColorBrush(Color.FromRgb(255,255,255))  });
            Add(new Shape() { Typename = "사각형", Pointx = 1000, Pointy = 1200, Size = 110, Color = new SolidColorBrush(Color.FromRgb(255,255,255)) });
        }
    }
}
