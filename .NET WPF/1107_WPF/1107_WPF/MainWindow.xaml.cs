using Example.MainWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace _1107_WPF
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private People people = new People();
        private Person per = null;
                
        public MainWindow()
        {
            InitializeComponent();
            per = people[0];
            
            DataToUI();
            UpdateListBox();
        }
        private void DataToUI()
        {
            if (per == null)
            {
                name.Text = "";
                phone.Text = "";
                age.Text = "";

                nameLabel.Content = "";
                phoneLabel.Content = "";
                ageLabel.Content = "";
                return;
            }

            name.Text = per.Name;
            phone.Text = per.Phone;
            age.Text = per.Age.ToString();

            nameLabel.Content = per.Name;
            phoneLabel.Content = per.Phone;
            ageLabel.Content = per.Age;
        }
        public void UpdateListBox()
        {
            listbox.Items.Clear();
            foreach (Person p in people)
            {
                listbox.Items.Add(p.ToString());
            }
        }

        private void name_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //per.Name= name.Text;
            if (per == null)
                return;
            nameLabel.Content = per.Name;
        }

        private void phone_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //per.Phone= phone.Text;
            if (per == null)
                return;
            phoneLabel.Content = phone.Text;
        }

        private void age_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //per.Age = int.Parse(age.Text);
            if (per == null)
                return;
            ageLabel.Content = per.Age;
        }

        private void listbox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (listbox.SelectedIndex >= 0)
            {
                per = people[listbox.SelectedIndex];
                DataToUI();
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || phone.Text == "" || age.Text == "")
                return;
            people.Add(new Person() { Name = name.Text, Phone = phone.Text , Age = int.Parse(age.Text)});
            // 리스트 박스의 아이템을 갱신한다.
            UpdateListBox();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (listbox.SelectedIndex >= 0)
            {
                people.RemoveAt(listbox.SelectedIndex);
                // 컬렉션에 원소가 없다면 리슽의 현재 아이템이 없도록(per =null) 한다.
                if (people.Count == 0)
                    per = null;
                else
                    per = people[0];
                // 모든 UI 컨트롤을 갱신한다.
                DataToUI();
                UpdateListBox();
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || phone.Text == "")
                return;
            per.Name = name.Text;
            per.Phone = phone.Text;
            per.Age = int.Parse(age.Text);
            DataToUI();
            UpdateListBox();
        }
    }
}