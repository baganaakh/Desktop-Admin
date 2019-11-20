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
using System.Data.SqlClient;

namespace pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void btnClickP1(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page1();
        }
        private void btnClickP2(object sender, RoutedEventArgs e)
        {
            Main.Content = new session();
        }
        private void btnClickP3(object sender, RoutedEventArgs e)
        {
            Main.Content = new participants();
        }
        private void btnClickP4(object sender, RoutedEventArgs e)
        {
            Main.Content = new assets();
        }
        private void btnClickP5(object sender, RoutedEventArgs e)
        {
            Main.Content = new securities();
        }
    }
}
