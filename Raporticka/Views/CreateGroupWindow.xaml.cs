using System;
using System.Collections.Generic;
using System.Linq;
using Raporticka.ViewModels;
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

namespace Raporticka.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateGroupWindow.xaml
    /// </summary>
    public partial class CreateGroupWindow : Window
    {
        public CreateGroupWindow(Window w)
        {
            this.Owner = w;
            this.Closing += Window_Closing;
            DataContext = new CreateGroupVM();
            InitializeComponent();
        }

        private void Window_Closing(object sender, EventArgs e)
        {
            Visibility = Visibility.Hidden;
            this.Owner.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string GrName = GroupName.Text;
        }
    }
}
