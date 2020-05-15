using Admin.dbBind;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Admin
{
    /// <summary>
    /// Interaction logic for marginp.xaml
    /// </summary>
    public partial class marginp : Page
    {
        public marginp()
        {
            InitializeComponent();
            FillDataGrid();
            bindcombo();
        }
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as Margin;
            if (null == values) return;
            buy.Text = values.buy.ToString();
            sell.Text = values.sell.ToString();
            mbuyType.SelectedIndex = values.buytype;
            mselltype.SelectedIndex = values.selltype;
            mmbuy.Text = values.mbuy.ToString();
            mmsell.Text = values.msell.ToString();
            contractid.SelectedValue = values.coid;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(sell.Text) || string.IsNullOrEmpty(buy.Text) || string.IsNullOrEmpty(mmsell.Text)
                || string.IsNullOrEmpty(mmbuy.Text) || contractid.SelectedItem == null || mbuyType.SelectedItem == null
                || mselltype.SelectedItem == null
                )
            {
                MessageBox.Show("Талбар дутууу !!!!");
                return;
            }
            using (demoEntities10 contx = new demoEntities10())
            {
                Margin ma = new Margin
                {
                    buy = Convert.ToDecimal(buy.Text),
                    buytype = Convert.ToInt16(mbuyType.SelectedIndex),
                    sell = Convert.ToDecimal(sell.Text),
                    selltype = Convert.ToInt16(mselltype.SelectedIndex),
                    modified = DateTime.Now,
                    msell = Convert.ToDecimal(mmsell.Text),
                    mbuy = Convert.ToDecimal(mmbuy.Text),
                    coid = Convert.ToInt64(contractid.SelectedValue),
                };
                contx.Margins.Add(ma);
                contx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region REfresh, FillGrid,number new
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.Margins.ToList();
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            buy.Text = null;
            sell.Text = null;
            mbuyType.Text = null;
            mselltype.Text = null;
            mmbuy.Text = null;
            mmsell.Text = null;
            contractid.SelectedItem = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as Margin;
            if (null == values) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.Margins.Where(x => x.contractId == values.contractId).First();
                conx.Margins.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region combos
        private void bindcombo()
        {
            demoEntities10 ct = new demoEntities10();
            contractid.ItemsSource = ct.Contracts.ToList();
        }
        #endregion
        #region UPDATE
        private void update(object sender, RoutedEventArgs e)
        {
            var ac = DateTable2.SelectedItem as Margin;
            using (demoEntities10 conx = new demoEntities10())
            {
                Margin ma = conx.Margins.FirstOrDefault(r => r.contractId == ac.contractId);
                ma.buy = Convert.ToDecimal(buy.Text);
                ma.buytype = Convert.ToInt16(mbuyType.SelectedIndex);
                ma.sell = Convert.ToDecimal(sell.Text);
                ma.selltype = Convert.ToInt16(mselltype.SelectedIndex);
                ma.modified = DateTime.Now;
                ma.msell = Convert.ToDecimal(mmsell.Text);
                ma.mbuy = Convert.ToDecimal(mmbuy.Text);
                ma.coid = Convert.ToInt64(contractid.SelectedValue);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
    }
}
