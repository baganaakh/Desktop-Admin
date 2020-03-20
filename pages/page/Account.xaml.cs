using Admin.dbBind;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Admin
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class account : Page
    {
        public account()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        short linkType;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as Account;
            if (null == values) return;

            if (values.accountType.Equals("")) { acctype.SelectedItem = null;}
            else {acctype.SelectedValue = values.accountType; }
            if (linkacc.Equals("")) { linkacc.SelectedItem= null;}
            else { linkacc.SelectedValue = values.LinkAccount; }
            memid.SelectedValue = values.memberid;
            accno.Text = values.accNumber;
            pstate.SelectedValue = values.state;
            sdate.SelectedDate = values.startdate;
            edate.SelectedDate = values.enddate;
        }
        #endregion
        #region insert func
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string accNo = accno.Text;
            string accnu = string.Format(accNo.ToString().PadLeft(8,'0'));
            if (acctype.SelectedIndex != 0 && linkacc.SelectedItem == null)
            {
                MessageBox.Show("Нэмэх боломжгүй");
                return;
            }
            using (demoEntities10 context = new demoEntities10())
            {
                var exist = context.Accounts.Count(a => a.accNumber == accnu);
                if (exist != 0)
                {
                    MessageBox.Show("Account number exists " +accnu+ " !!!");
                    return;
                }
            }
            if (acctype.SelectedIndex == 0)
            {
                using (var contx = new demoEntities10())
                {
                    var std = new Account()
                    {
                        memberid = Convert.ToInt64(memid.SelectedValue),
                        accNumber= accnu,
                        accountType = Convert.ToInt16(acctype.SelectedValue),
                        state = Convert.ToInt16(pstate.SelectedIndex - 1),
                        modified = DateTime.Now
                    };
                    contx.Accounts.Add(std);
                    contx.SaveChanges();
                    //long id = std.id;
                    //var accoundet = new AccountDetail
                    //{
                    //    accountId = id,
                    //};
                    //contx.AccountDetails.Add(accoundet);
                    //contx.SaveChanges();
                }
                FillDataGrid();
                return;
            }
            using(demoEntities10 conx=new demoEntities10())
            {
                var accoun = new Account
                {
                    memberid=Convert.ToInt64(memid.SelectedValue),
                    accNumber=accno.Text,
                    accountType=Convert.ToInt16(acctype.SelectedIndex),
                    LinkAccount=Convert.ToInt64(linkacc.SelectedValue),
                    startdate=sdate.SelectedDate,
                    enddate=edate.SelectedDate,
                };
                conx.Accounts.Add(accoun);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region filldatas, NUMBER, REfresh & NEW
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {
            demoEntities10 dc = new demoEntities10();
            DateTable2.ItemsSource = dc.Accounts.ToList();
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            memid.Text = null;
            pstate.SelectedItem = null;
            linkacc.SelectedItem = null;
            acctype.SelectedItem = null;
            accno.Text = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as Account;
            if (null == value) return;
            using(demoEntities10 conx=new demoEntities10())
            {
                var del = conx.Accounts.Where(x => x.id == value.id).First();
                conx.Accounts.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            var ac = DateTable2.SelectedItem as Account;
            if (acctype.SelectedIndex != 0 && linkacc.SelectedItem == null)
            {
                MessageBox.Show("Нэмэх боломжгүй");
                return;
            }
            string accnu = string.Format(accno.Text.ToString().PadLeft(8, '0'));
            using (demoEntities10 context = new demoEntities10())
            {
                var exist = context.Accounts.Count(a => a.accNumber == accnu);
                if (exist != 0)
                {
                    MessageBox.Show("Account number exists " + accnu + " !!!");
                    return;
                }
            }
            using (demoEntities10 conx=new demoEntities10())
            {
                Account acc = conx.Accounts.FirstOrDefault(r => r.id == ac.id);
                acc.memberid = Convert.ToInt64(memid.SelectedValue);
                acc.accNumber = accno.Text;
                acc.accountType = Convert.ToInt16(acctype.SelectedIndex);
                acc.state = Convert.ToInt16(pstate.SelectedIndex - 1);
                acc.LinkAccount = Convert.ToInt64(linkacc.SelectedValue);
                acc.startdate = sdate.SelectedDate;
                acc.enddate = edate.SelectedDate;
                conx.SaveChanges();
                //List<AccountDetail> accd = conx.AccountDetails.Where(r => r.accountId == ac.id).ToList();
                //accd.ForEach(a => a.accountId = Convert.ToInt64(accno.Text));
                //conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region combos control
        private void bindCombo()
        {
            demoEntities10 dc = new demoEntities10();
            memid.ItemsSource = dc.Members.ToList();
            acctype.ItemsSource = dc.accTypes.ToList();
        }
        private void acctype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            linkacc.SelectedItem = null;
            linkacc.ItemsSource = null;
            int item = acctype.SelectedIndex;
            try
            {
                if (item == null)
                    return;
                if(item == 0)  //Төлбөр
                {
                    linkacc.IsEnabled = false;
                    return;
                }
                else if(item == 1)  //Барьцаа
                {
                    linkType = 0;
                    linkacc.IsEnabled = true;
                }
                else if(item == 2)  //Клиринг
                {
                    linkType = 0;
                    linkacc.IsEnabled = true;
                }
                else if(item == 3)  //Арилжаа
                {
                    linkType = 1;
                    linkacc.IsEnabled = true;
                }
                demoEntities10 de = new demoEntities10();
                var linkas = de.Accounts.Where(s => s.accountType == linkType).ToList();
                linkacc.ItemsSource = linkas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void memid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            demoEntities10 dc = new demoEntities10();
            var accitem = dc.accTypes.ToList();
            var accitem2 = dc.accTypes.ToList();
            accitem2.RemoveAt(3);
            var item = memid.SelectedItem as Member;
            try
            {
                string mtypee = item.type.ToString();
                string mask = item.mask.ToString();
                string ander = item.ander.ToString();
                string dealer = item.dealer.ToString();
                string broker = item.broker.ToString();

                companyName.Content = mask;
                if(mtypee == "0")
                {
                    acctype.ItemsSource = accitem;
                    acctype.SelectedValue = 3;
                    acctype.IsEnabled = false;
                    linkacc.IsEnabled = true;
                }
                else if(mtypee == "1" && (broker == "False" && dealer == "False" && ander == "False"))
                {
                    acctype.ItemsSource = accitem2;
                    linkacc.IsEnabled = true;
                    acctype.IsEnabled = true;
                }
                else if(mtypee == "1" && (broker == "True" || dealer == "True" || ander == "True"))
                {
                    acctype.ItemsSource = accitem;
                    acctype.SelectedItem = null;
                    linkacc.IsEnabled = true;
                    acctype.IsEnabled = true;
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
                return;
            }
        }
        #endregion
    }
}
