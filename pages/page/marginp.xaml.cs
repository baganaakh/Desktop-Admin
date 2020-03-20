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
using System.Text.RegularExpressions;
using System.Data;
using Admin.dbBind;

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

            mbuy.Text=values.buy.ToString();
            msell.Text=values.sell.ToString();
            mbuyType.SelectedIndex=values.buytype;
            mselltype.SelectedIndex = values.selltype;
            mbuy_Copy.Text=values.mbuy.ToString();
            mmsell.Text=values.msell.ToString();
            contractid.SelectedValue = values.coid;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {

            using (demoEntities10 contx = new demoEntities10())
            {
                Margin ma = new Margin
                {
                    buy = Convert.ToDecimal(mbuy.Text),
                    buytype = Convert.ToInt16(mbuyType.SelectedIndex),
                    sell = Convert.ToDecimal(msell.Text),
                    selltype = Convert.ToInt16(mselltype.SelectedIndex),
                    modified = DateTime.Now,
                    msell = Convert.ToDecimal(mmsell.Text),
                    mbuy = Convert.ToDecimal(mbuy_Copy.Text),
                    coid =Convert.ToInt64(contractid.SelectedValue),
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
            mbuy.Text=null;
            msell.Text=null;
            mbuyType.Text=null;
            mselltype.Text=null;
            mbuy_Copy.Text=null;
            mmsell.Text=null;
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
                var del = conx.Margins.Where(x => x.contractId== values.contractId).First();
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
                ma.buy = Convert.ToDecimal(mbuy.Text);
                ma.buytype = Convert.ToInt16(mbuyType.SelectedIndex);
                ma.sell = Convert.ToDecimal(msell.Text);
                ma.selltype = Convert.ToInt16(mselltype.SelectedIndex);
                ma.modified = DateTime.Now;
                ma.msell = Convert.ToDecimal(mmsell.Text);
                ma.mbuy = Convert.ToDecimal(mbuy_Copy.Text);
                ma.coid = Convert.ToInt64(contractid.SelectedValue);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
    }
}
