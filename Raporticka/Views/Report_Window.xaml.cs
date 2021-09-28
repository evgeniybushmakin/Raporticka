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

namespace Raporticka.Views
{
    /// <summary>
    /// Логика взаимодействия для Report_Window.xaml
    /// </summary>
    public partial class Report_Window : Window
    {
        public Report_Window(Window w)
        {
            this.Owner = w;
            this.Closing += Window_Closing;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            EditGroupWindow edit = new EditGroupWindow(this);
            edit.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            CreateGroupWindow create = new CreateGroupWindow(this);
            create.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Visibility = Visibility.Hidden;
            this.Owner.Visibility = Visibility.Visible;
        }
    }
}