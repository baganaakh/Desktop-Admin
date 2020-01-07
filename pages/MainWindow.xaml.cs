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
using System.Globalization;
using System.Threading;

namespace pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            CultureInfo ci = new CultureInfo("us-en");
            Thread.CurrentThread.CurrentCulture = ci;
            InitializeComponent();
            Main.Content = new boards();

        }

        private void BtnClickP1(object sender, RoutedEventArgs e)
        {
            Main.Content = new boards();
        }
        private void BtnClickP2(object sender, RoutedEventArgs e)
        {
            Main.Content = new session();
        }
        private void BtnClickP3(object sender, RoutedEventArgs e)
        {
            Main.Content = new participants();
        }
        private void BtnClickP4(object sender, RoutedEventArgs e)
        {
            Main.Content = new assets();
        }
        private void BtnClickP5(object sender, RoutedEventArgs e)
        {
            Main.Content = new securities();
        }
        private void BtnClickP6(object sender, RoutedEventArgs e)
        {
            Main.Content = new contracts();
        }
        private void BtnClickP7(object sender, RoutedEventArgs e)
        {
            Main.Content = new trans();
        }
        private void BtnClickP8(object sender, RoutedEventArgs e)
        {
            Main.Content = new margins();
        }
        private void BtnClickP9(object sender, RoutedEventArgs e)
        {
            Main.Content = new MarketMakers();
        }
        private void BtnClickP10(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClearingAccount();
        }
        private void BtnClickP11(object sender, RoutedEventArgs e)
        {
            Main.Content = new TickSizeTable();
        }
        private void BtnClickP12(object sender, RoutedEventArgs e)
        {
            Main.Content = new members();
        }
        private void BtnClickP13(object sender, RoutedEventArgs e)
        {
            Main.Content = new dealerAccount();
        }
        private void BtnClickP14(object sender, RoutedEventArgs e)
        {
            Main.Content = new RefPrice();
        }
        private void BtnClickP15(object sender, RoutedEventArgs e)
        {
            Main.Content = new Spread();
        }
        
        private void BtnClickP17(object sender, RoutedEventArgs e)
        {
            Main.Content = new account();
        }
        private void BtnClickP18(object sender, RoutedEventArgs e)
        {
            Main.Content = new Table();
        }
        private void BtnClickP19(object sender, RoutedEventArgs e)
        {
            Main.Content = new Order();
        }
        private void BtnClickP20(object sender, RoutedEventArgs e)
        {
            Main.Content = new deal2();
        }
        private void BtnClickP21(object sender, RoutedEventArgs e)
        {
            Main.Content = new orderOk();
        }
        private void BtnClickP22(object sender, RoutedEventArgs e)
        {
            Main.Content = new Fee();
        }
        private void BtnClickP23(object sender, RoutedEventArgs e)
        {
            Main.Content = new demo1();
        }
        private void BtnClickP24(object sender, RoutedEventArgs e)
        {
            Main.Content = new invoices();
        }
    }
}