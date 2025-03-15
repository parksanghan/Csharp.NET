using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp4;

namespace WpfApp4.Model
{

    public class Book : INotifyPropertyChanged
    {
       // public event PropertyChangedEventHandler PropertyChanged;
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                // if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                OnPropertyChange("Title");
            }

        }
        private string publisher;
        public string Publisher
        {
            get { return publisher; }
            set
            {
                publisher = value;
                //if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Publisher"));
                OnPropertyChange("Publisher");
            }

        }
        private string writer;

        public string Writer
        {
            get { return writer; }
            set
            {
                writer = value;
                //if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Writer"));
                OnPropertyChange("Writer");
            }
        }
        private int price;
        public int Price
        {
            get { return price; }
            set
            {
                price = value;
                //if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Price"));
                OnPropertyChange("Price");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public override string ToString()
        {
            return title + "\t" + publisher + "\t" + writer + "\t" + price;
        }

    }

    public class Books : ObservableCollection <Book> , INotifyPropertyChanged
    {
        
        public Books()
        {
            
          
        }
        // INotifyPropertyChanged 인터페이스를 구현한 PropertyChanged 이벤트를 정의
        public event PropertyChangedEventHandler PropertyChangedlist;

        // Books 클래스 내부의 속성이 변경될 때 이 메서드를 호출하여 데이터 바인딩에 알림을 보냅니다.
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedlist?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


    #region 사용자 정의 유효성 검사
    public class IntValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int number;
            if (!int.TryParse((string)value, out number))
            {
                return new ValidationResult(false, "가격을 입력하세요.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }

    public class StringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string name = (string)value;
            if (name == string.Empty)
            {
                return new ValidationResult(false, "문자를 입력하세요.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
    #endregion


    public class ShortNumberValidationRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }


        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int number;
            if (!int.TryParse((string)value, out number))
            {
                return new ValidationResult(false, "정수를 입력하세요.");
            }
            if (Min <= number && number <= 10000)
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
    }
}