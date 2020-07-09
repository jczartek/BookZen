using BookZen.ViewModels;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookZen.Dialogs
{
    /// <summary>
    /// Interaction logic for DetailsBookDialog.xaml
    /// </summary>
    public partial class DetailsBookDialog : Window
    {
        private DetailsBookDialog()
        {
            InitializeComponent();
        }

        public DetailsBookDialog(BookDto bookDto) : this()
        {
            DataContext = new DetailsBookViewModel(bookDto);
        }
    }
}
