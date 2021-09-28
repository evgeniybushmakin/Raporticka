using Raporticka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClosedXML;

namespace Raporticka.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateRaportWindow.xaml
    /// </summary>
    public partial class CreateRaportWindow : Window
    {
        public CreateRaportWindow(Window w)
        {
            DataContext = new CreateRaportWindowVM();
            this.Owner = w;
            this.Closing += Window_Closing;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Visibility = Visibility.Hidden;
            this.Owner.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
