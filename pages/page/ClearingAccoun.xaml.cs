using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using Admin.dbBind;

namespace Admin
{
    /// <summary>
    /// Interaction logic for ClearingAccoun.xaml
    /// </summary>
    public partial class ClearingAccoun : Page
    {
        public ClearingAccoun()
        {
            InitializeComponent();
            FillDataGrid();
            bindcombo();
        }
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as ClearingAccount;
            if (null == values) return;

            memid.SelectedValue=values.memberid;
            accid.Text=values.account;
            typee.SelectedIndex=Convert.ToInt32(values.type);
            currency.Text=values.currency.ToString();
            balanc.Text=values.blnc.ToString();
            sbalanc.Text=values.sblnc.ToString();
            linkacc.SelectedValue=values.linkaccount;
            stat.SelectedIndex=Convert.ToInt32(values.state);
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (memid.SelectedItem== null || typee.SelectedItem == null || string.IsNullOrEmpty(accid.Text) 
                    || string.IsNullOrEmpty(currency.Text) || string.IsNullOrEmpty(balanc.Text)
                    || string.IsNullOrEmpty(sbalanc.Text) || stat.SelectedItem == null 
                )
            {
                MessageBox.Show("Талбар дутууу !!!!");
                return;
            }
            using (demoEntities10 contx=new demoEntities10())
            {
                ClearingAccount ca = new ClearingAccount
                {
                    memberid = Convert.ToInt32(memid.SelectedValue),
                    type = Convert.ToInt16(typee.SelectedIndex),
                    account = accid.Text,
                    currency = Convert.ToInt32(currency.Text),
                    blnc = Convert.ToDecimal(balanc.Text),
                    sblnc = Convert.ToDecimal(sbalanc.Text),
                    linkaccount = Convert.ToInt64(linkacc.SelectedValue),
                    state = Convert.ToInt16(stat.SelectedIndex - 1),
                    modified = DateTime.Now,
                };
                contx.ClearingAccounts.Add(ca);
                contx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region FillGrid, Number, refresh and new
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {   
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.ClearingAccounts.ToList();
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            memid.Text = null;
            accid.Text = null;
            typee.Text = null;
            currency.Text = null;
            balanc.Text =null;
            sbalanc.Text = null;
            linkacc.Text = null;
            stat.Text = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as ClearingAccount;
            if (null == value) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.ClearingAccounts.Where(x => x.id == value.id).First();
                conx.ClearingAccounts.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            var ac = DateTable2.SelectedItem as ClearingAccount;
            using (demoEntities10 conx = new demoEntities10())
            {
                ClearingAccount ca = conx.ClearingAccounts.FirstOrDefault(r => r.id == ac.id);
                ca.memberid = Convert.ToInt32(memid.SelectedValue);
                ca.account = accid.Text;
                ca.type =Convert.ToInt16(typee.SelectedIndex);
                ca.currency = Convert.ToInt32(currency.Text);
                ca.blnc = Convert.ToDecimal(balanc.Text);
                ca.sblnc = Convert.ToDecimal(sbalanc.Text);
                ca.linkaccount = Convert.ToInt64(linkacc.SelectedValue);
                ca.state=Convert.ToInt16(stat.SelectedIndex-1);
                ca.modified = DateTime.Now;
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region combos
        private void bindcombo()
        {
            demoEntities10 dc = new demoEntities10();            
            memid.ItemsSource = dc.Members.ToList();
            linkacc.ItemsSource = dc.Accounts.ToList();
        }
        #endregion
    }
}
