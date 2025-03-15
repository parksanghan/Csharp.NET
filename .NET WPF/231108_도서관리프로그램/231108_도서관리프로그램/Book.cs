using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace _231108_도서관리프로그램
{
    public class Book : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // 맴버필드
        private string title;
        private int? price;
        private string author;
        private string publisher;

        // 프로퍼티
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Title"));
            }
        }
        public int? Price
        {
            get { return price; }
            set
            {
                price = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Price"));
            }

        }
        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Author"));
            }

        }
        public string Publisher
        {
            get { return publisher; }
            set
            {
                publisher = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Publisher"));
            }

        }

        public override string ToString()
        {
            return Title + "\t" + Price + "원" + "\t" + Author + "\t" + Publisher;
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
}
