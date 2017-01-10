using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace First_appl_MVVM.Command
{
    public class RelayComand : ICommand
    {
        private Action execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayComand(Action execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return execute != null ;
        }

        public void Execute(object parameter)
        {
            this.execute();
        }
    }
}
