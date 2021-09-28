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

namespace Raporticka
{
    /// <summary>
    /// Логика взаимодействия для CreateRaportWindow.xaml
    /// </summary>
    public partial class CreateRaportWindow : Window
    {
        public CreateRaportWindow(Window w)
        {
            this.Owner = w;
            this.Closing += Window_Closing;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Visibility = Visibility.Hidden;
            this.Owner.Visibility = Visibility.Visible;
        }
    }
}
