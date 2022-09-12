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

namespace Пробный
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
        }
        Window1 window = new Window1();
        Window2 window1 = new Window2();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.Show();
            window1.Close();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            window1.Close();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            window1.Show();
        }
    }
}
