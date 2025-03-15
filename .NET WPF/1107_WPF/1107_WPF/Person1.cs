using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace Example.Window1
{
    public class Person : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
            }
        }

        private int? age { get; set; }
        public int? Age
        {
            get { return age; }
            set
            {
                age = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Age"));
            }
        }
        private bool? male;
        public bool? Male
        {
            get { return male; }
            set
            {
                male = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Male"));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Name + "\t" + Phone + "\t" + Age + "\t" +  Male;
        }

    }

    #region 형식변환기
    //형식변환기(Male)
    public class MaleToFemaleConverter : IValueConverter
    {
        // 데이터 속성을 UI 속성으로 변경할 때
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool?))
                return null;
            bool? male = (bool?)value;
            if (male == null)
                return null;
            else
                return !(bool?)value;
        }
        // UI 속성을 데이터 속성으로 변경할 때
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool?))
                return null;
            bool? male = (bool?)value;
            if (male == null)
                return null;
            else
                return !(bool?)value;
        }
    }
    //형식변환기(bool < > string)
    public class MaleToStringConverter : IValueConverter
    {
        // 데이터 속성(bool?)을 UI 속성(string)으로 변경할 때
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? male = (bool?)value;
            if (male == null)
                return "";
            else if (male == true)
                return "남";
            else if (male == false)
                return "여";
            else
                return "";
        }
        // UI 속성(string)을 데이터 속성(bool?)으로 변경할 때
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string male = (string)value;
            if (male == "남")
                return true;
            if (male == "여")
                return false;
            else
                return null;
        }
    }
    #endregion

    #region 사용자 정의 유효성 검사
    public class ShortNumberValidationRule : ValidationRule
    {
        public int Min {  get; set; }
        public int Max { get; set; }


        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int number;
            if (!int.TryParse((string)value, out number))
            {
                return new ValidationResult(false, "정수를 입력하세요.");
            }
            if (Min <= number && number <= Max)
            {
                // new ValidationResult(true, null) 같다
                return ValidationResult.ValidResult;
            }
            else
            {
                string msg = string.Format("나이는 {0}에서 {1}까지 입력 가능합니다.", Min, Max);
                return new ValidationResult(false, msg);
            }
        }
        public class People : List<Person>
        {
            public People()
            {
                Add(new Person() { Name = "홍길동", Phone = "010-1111-1234", Age = 10 });
                Add(new Person() { Name = "일지매", Phone = "010-2222-1234", Age = 30 });
                Add(new Person() { Name = "임꺽정", Phone = "010-3333-1234", Age = 50 });
            }
        }
    }

    public class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string name = (string)value;

            if (name == string.Empty)
                return new ValidationResult(false, "이름을 입력하세요");
            else
                return ValidationResult.ValidResult;

        }
       
    }
    #endregion
    // list  삭제나 insert 시 감지하여 적용함 데이터바인딩 
    public class People :  ObservableCollection<Person> //: List<Person>
    {
        public People()
        {
            Add(new Person() { Name = "홍길동", Phone = "010-1111-1234", Age = 10, Male = true });
            Add(new Person() { Name = "일지매", Phone = "010-2222-1234", Age = 30, Male = true });
            Add(new Person() { Name = "임꺽정", Phone = "010-3333-1234", Age = 50, Male = false });
        }
    }
}
