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
    /// Interaction logic for dealerAccount.xaml
    /// </summary>
    public partial class dealerAccount : Page
    {
        public dealerAccount()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as DealerAccount;
            if (null == values) return;

            memid.SelectedValue=values.memberid;
            accid.SelectedValue=values.accountid;
            stat.SelectedIndex=values.state+1;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            using(demoEntities10 contx=new demoEntities10())
            {
                DealerAccount dea = new DealerAccount
                {
                    memberid=Convert.ToInt32(memid.SelectedValue),
                    accountid=Convert.ToInt64(accid.SelectedValue),
                    state=Convert.ToInt16(stat.SelectedIndex -1),
                    modified=DateTime.Now,
                };
                contx.DealerAccounts.Add(dea);
                contx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region refresh, filldatagrid,number new
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.DealerAccounts.ToList();
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            memid.Text = null;
            accid.Text = null;
            stat.Text = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DealerAccount;
            if (null == values) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.DealerAccounts.Where(x => x.id == values.id).First();
                conx.DealerAccounts.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region UPDATE
        private void update(object sender, RoutedEventArgs e)
        {
            var ac = DateTable2.SelectedItem as Board;
            using (demoEntities10 conx = new demoEntities10())
            {
                DealerAccount dea = conx.DealerAccounts.FirstOrDefault(r=>r.id==ac.id);
                dea.memberid = Convert.ToInt64(memid.SelectedValue);
                dea.accountid =Convert.ToInt64(accid.SelectedValue);
                dea.state = Convert.ToInt16(stat.SelectedIndex - 1);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region combos
        private void bindCombo()
        {
            demoEntities10 dc = new demoEntities10();
            memid.ItemsSource = dc.Members.ToList();
            accid.ItemsSource = dc.Accounts.ToList();
        }
        #endregion
    }
}
