
using Example.Window1;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace _1107_WPF
{
    /// <summary>
    /// Window2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window3 : Window
    {
        //People people= null;
        //People people = new People();
        public Window3()
        {
            InitializeComponent();

            //panel.DataContext = people;
            //people = (People)FindResource(people);

            
            Validation.AddErrorHandler(age, age_ValidationError);
            Validation.AddErrorHandler(name, name_ValidationError);
        }
        void age_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            Control control = sender as Control;
            control.ToolTip = (string)e.Error.ErrorContent;
        }
        void name_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            Control control = sender as Control;
            control.ToolTip = (string)e.Error.ErrorContent;
        }


        private void eraseButton_Click_1(object sender, RoutedEventArgs e) // current item 을 현재 거를 가져와서 아이템을 값들을 초기화 
        {
            ICollectionView view =CollectionViewSource.GetDefaultView(FindResource("people"));
            Person person = (Person)view.CurrentItem;
            person.Age = null;
            person.Name = "";
            person.Phone = "";
            person.Male = null;


           // panel.SourceUpdated += Panel_SourceUpdated; FindResource("people");

        }

 
 
        private void prev_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view =CollectionViewSource.GetDefaultView(FindResource("people"));
            view.MoveCurrentToPrevious();
            if (view.IsCurrentBeforeFirst)
            {
                view.MoveCurrentToFirst();  // 0인덱스에서 더 이전으로 갈시 다시 처음으로 즉 0 번째로 이동 
            }

        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("people"));
            view.MoveCurrentToNext();  // 다음으로 이동 
            if (view.IsCurrentAfterLast)
            {
                view.MoveCurrentToLast(); // 마지막 위치에서 다음으로 갈 시 마지막 인덱스로  이동 
            }
        }

        private void deletebutton_Click(object sender, RoutedEventArgs e) // person에대한 삭제 히
        {
            People peo = (People)FindResource("people"); // 위에서는 수정개념이라 person을 가져옴
            if(mylistbox.SelectedIndex>= 0)
            {
                peo.RemoveAt(mylistbox.SelectedIndex); // 이렇게하면 당연히 WPF 는 인지ㅁ모ㅗㅅ함
            }
        }

        private void sortbutton_Click(object sender, RoutedEventArgs e) //정렬 
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("people"));
            if (view.SortDescriptions.Count == 0) // 정렬에 대한 기준이 등록이 되어있나 
            {
                view.SortDescriptions.Add(new SortDescription("Name",
               ListSortDirection.Ascending));
                view.SortDescriptions.Add(new SortDescription("Male", // 이름이 중복되었을때 정렬하는 기준 
               ListSortDirection.Ascending));
            }
            else
            {
                view.SortDescriptions.Clear();
            }
        }

        private void filter_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view =CollectionViewSource.GetDefaultView(FindResource("people"));
            if (view.Filter == null)
            {
                view.Filter = delegate (object obj)
                {
                    return ((Person)obj).Age < 10;
                };
            }
            else
            {
                view.Filter = null;
            }


        }
    }
}
