using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1109_MVVM.Model
{
    class MainModel : INotifyPropertyChanged
    {
        private int num1 = 1;
        public int Num1
        {
            get { return num1; }
            set { num1 = value;  OnPropertyChange("Num1"); }
        }

        private int num2 = 1;
        public int Num2
        {
            get { return num2; }
            set { num2 = value; OnPropertyChange("Num2"); }
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
    
    }
}
