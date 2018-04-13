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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Zad1.xaml
    /// </summary>
    public partial class Zad1 : UserControl
    {
        public Zad1()
        {
            InitializeComponent();
            DataContext = MainWindow.DataContext;
          //  DataContext = new MyViewModel();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
