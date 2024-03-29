﻿using System.Windows;
using System.Globalization;
using System.Threading;
using Admin.page;

namespace Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            InitializeComponent();

            Main.Content = new participants();
        }
        private void BtnClickP1(object sender, RoutedEventArgs e)
        {
            Main.Content = new boards();
            this.Title = "Board";
        }
        private void BtnClickP2(object sender, RoutedEventArgs e)
        {
            Main.Content = new session();
            this.Title = "Session";
        }
        private void BtnClickP3(object sender, RoutedEventArgs e)
        {
            Main.Content = new participants();
            this.Title = "Participants";
        }
        private void BtnClickP4(object sender, RoutedEventArgs e)
        {
            Main.Content = new assets();
            this.Title = "Asset";
        }
        private void BtnClickP5(object sender, RoutedEventArgs e)
        {
            Main.Content = new securities();
            this.Title = "Securities";
        }
        private void BtnClickP6(object sender, RoutedEventArgs e)
        {
            Main.Content = new contracts();
            this.Title = "Contract";
        }
        private void BtnClickP7(object sender, RoutedEventArgs e)
        {
            Main.Content = new trans();
            this.Title = "Trans";
        }
        private void BtnClickP8(object sender, RoutedEventArgs e)
        {
            Main.Content = new marginp();
            this.Title = "Marginp";
        }
        private void BtnClickP9(object sender, RoutedEventArgs e)
        {
            Main.Content = new MarketMakers();
            this.Title = "Market Maker";
        }
        private void BtnClickP10(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClearingAccoun();
            this.Title = "Clearing Account";
        }
        private void BtnClickP11(object sender, RoutedEventArgs e)
        {
            Main.Content = new TickSizeTabl();
            this.Title = "Tick Size Table";
        }
        private void BtnClickP12(object sender, RoutedEventArgs e)
        {
            Main.Content = new members();
            this.Title = "Member";
        }
        private void BtnClickP13(object sender, RoutedEventArgs e)
        {
            Main.Content = new dealerAccount();
            this.Title = "Dealer Account";
        }
        private void BtnClickP14(object sender, RoutedEventArgs e)
        {
            Main.Content = new RefPric();
            this.Title = "Ref Price";
        }
        private void BtnClickP15(object sender, RoutedEventArgs e)
        {
            Main.Content = new Spreads();
            this.Title = "Spread";
        }
        private void BtnClickP17(object sender, RoutedEventArgs e)
        {
            Main.Content = new account();
            this.Title = "Account";
        }
        private void BtnClickP18(object sender, RoutedEventArgs e)
        {
            Main.Content = new Table();
            this.Title = "Table";
        }
        private void BtnClickP19(object sender, RoutedEventArgs e)
        {
            Main.Content = new OrderP();
            this.Title = "OrderP";
        }
        private void BtnClickP20(object sender, RoutedEventArgs e)
        {
            Main.Content = new deal2();
            this.Title = "Deal 2";
        }
        private void BtnClickP21(object sender, RoutedEventArgs e)
        {
            Main.Content = new orderOk();
            this.Title = "Order Ok";
        }
        private void BtnClickP22(object sender, RoutedEventArgs e)
        {
            Main.Content = new Fee();
            this.Title = "Fee";
        }
        private void BtnClickP23(object sender, RoutedEventArgs e)
        {
            Main.Content = new demo1();
            this.Title = "Demo 1";
        }
        private void BtnClickP24(object sender, RoutedEventArgs e)
        {
            Main.Content = new invoices();
            this.Title = "Invoices";
        }
        private void BtnClickP25(object sender, RoutedEventArgs e)
        {
            Main.Content = new userPage();
            this.Title = "User";
        }
        private void BtnClickP26(object sender, RoutedEventArgs e)
        {
            Main.Content = new CollApprove();
            this.Title = "Collateral Approve";
        }
    }
}