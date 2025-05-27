    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace WpfApp4
    {
          public class Command
        {
            Action<object> ExecuteMethod;
            Func<object, bool> CanexecuteMethod;

            public Command(Action<object> e, Func<object, bool> c)
            {
                this.ExecuteMethod = e;
                this.CanexecuteMethod = c;
            }
            public Command(Action<object> e )
            {
                this.ExecuteMethod = e;
      
            }

            public event EventHandler CanExecuteChanged = null;
            public bool CanExecute(object parameter)
            {
                return CanexecuteMethod(parameter);
            }

            public void Execute(object parameter)
            {
                ExecuteMethod(parameter);
            }
        }
    }
