using BookZen.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookZen.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ICommand cmdShowInputBookDialog;
        public ICommand ShowInputBookDialog
        {
            get
            {

                return cmdShowInputBookDialog ??= new RelayCommand(
                    (o) =>
                    {
                        new InputBookDialog().ShowDialog();
                    });
            }
        }
    }
}
