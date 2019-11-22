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
            Main.Content = new boards();

        }

        private void btnClickP1(object sender, RoutedEventArgs e)
        {
            Main.Content = new boards();
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
        private void btnClickP6(object sender, RoutedEventArgs e)
        {
            Main.Content = new contracts();
        }
        private void btnClickP7(object sender, RoutedEventArgs e)
        {
            Main.Content = new trans();
        }
        private void btnClickP8(object sender, RoutedEventArgs e)
        {
            Main.Content = new margins();
        }
        private void btnClickP9(object sender, RoutedEventArgs e)
        {
            Main.Content = new MarketMakers();
        }
        private void btnClickP10(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClearingAccount();
        }
        private void btnClickP11(object sender, RoutedEventArgs e)
        {
            Main.Content = new TickSizeTable();
        }
        private void btnClickP12(object sender, RoutedEventArgs e)
        {
            Main.Content = new members();
        }
        private void btnClickP13(object sender, RoutedEventArgs e)
        {
            Main.Content = new dealerAccount();
        }
        private void btnClickP14(object sender, RoutedEventArgs e)
        {
            Main.Content = new RefPrice();
        }
        private void btnClickP15(object sender, RoutedEventArgs e)
        {
            Main.Content = new Spread();
        }
        private void btnClickP16(object sender, RoutedEventArgs e)
        {
            Main.Content = new showData();
        }
    }
}
