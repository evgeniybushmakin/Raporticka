using Raporticka.DataBase;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Raporticka.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            Report_Window rep = new Report_Window(this);
            rep.Show();
        }

        private void Button_Report_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            CreateRaportWindow rep = new CreateRaportWindow(this);
            rep.Show();
        }

        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("Рапортичка.chm");
        }
    }
}