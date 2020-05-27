using Admin.dbBind;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        string accountType;
        short linkType;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as Account;
            if (null == values) return;

            if (values.accountType.Equals("")) { acctype.SelectedItem = null; }
            else { acctype.SelectedValue = values.accountType; }
            if (linkacc.Equals("")) { linkacc.SelectedItem = null; }
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
            if (string.IsNullOrEmpty(accno.Text) || memid.SelectedItem == null ||
                sdate.SelectedDate == null || edate.SelectedDate == null || pstate.SelectedItem == null)
            {
                MessageBox.Show("Талбарууд дутуу байна");
                return;
            }
            Member me = memid.SelectedItem as Member;
            if (me.nominal == false && linkacc.SelectedItem == null)
            {
                MessageBox.Show("Арилжаа холбох дансгүй бол нээгдэхүй");
                return;
            }
            string accNo = accno.Text;
            //if (acctype.SelectedIndex != 0 && linkacc.SelectedItem == null)
            //{
            //    MessageBox.Show("Нэмэх боломжгүй");
            //    return;
            //}
            accountType = acctype.Text;
            switch (accountType) 
            {
                case "Төлбөр":
                    accountType = "0";
                    break;
                case "Барьцаа":
                    accountType = "1";
                    break;
                case "Клиринг":
                    accountType = "2";
                    break;
                case "Арилжаа":
                    accountType = "3";
                    break;
                default:
                    accountType = "-1";
                    break;
            }
            using (demoEntities10 context = new demoEntities10())
            {
                var exist = context.Accounts.Count(a => a.accNumber == accNo);
                if (exist != 0)
                {
                    MessageBox.Show("Account number exists " + accNo + " !!!");
                    return;
                }
            }
            if (accountType == "0")
            {
                using (var contx = new demoEntities10())
                {
                    var std = new Account()
                    {
                        memberid = Convert.ToInt64(memid.SelectedValue),
                        accNumber = accNo,
                        accountType = Convert.ToInt16(accountType),
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
            using (demoEntities10 conx = new demoEntities10())
            {
                var accoun = new Account
                {
                    memberid = Convert.ToInt64(memid.SelectedValue),
                    accNumber = accno.Text,
                    accountType = Convert.ToInt16(accountType),
                    LinkAccount = Convert.ToInt64(linkacc.SelectedValue),
                    startdate = sdate.SelectedDate,
                    enddate = edate.SelectedDate,
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
            using (demoEntities10 conx = new demoEntities10())
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
            Member me = memid.SelectedItem as Member;
            if (me.nominal == false && linkacc.SelectedItem == null)
            {
                MessageBox.Show("Арилжаа холбох дансгүй бол нээгдэхүй");
                return;
            }
            var ac = DateTable2.SelectedItem as Account;
            if (acctype.SelectedIndex != 0 && linkacc.SelectedItem == null)
            {
                MessageBox.Show("Нэмэх боломжгүй");
                return;
            }
            string accnu = accno.Text;
            using (demoEntities10 context = new demoEntities10())
            {
                var exist = context.Accounts.Count(a => a.accNumber == accnu);
                if (exist != 0)
                {
                    MessageBox.Show("Account number exists " + accnu + " !!!");
                    return;
                }
            }
            using (demoEntities10 conx = new demoEntities10())
            {
                Account acc = conx.Accounts.FirstOrDefault(r => r.id == ac.id);
                acc.memberid = Convert.ToInt64(memid.SelectedValue);
                acc.accNumber = accno.Text;
                acc.accountType = Convert.ToInt16(accountType);
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
        }
        private void acctype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            linkacc.SelectedItem = null;
            linkacc.ItemsSource = null;
            string item = acctype.Text;
            try
            {
                switch (item)
                {
                    case "Төлбөр":
                        linkacc.IsEnabled = false;
                        break;
                    case "Барьцаа":
                        linkType = 0;
                        linkacc.IsEnabled = true;
                        break;
                    case "Клиринг":
                        linkType = 0;
                        linkacc.IsEnabled = true;
                        break;
                    case "Арилжаа":
                        linkType = 2;
                        linkacc.IsEnabled = true;
                        break;
                    case "":
                        MessageBox.Show("Empty String");
                        break;
                    default:
                        MessageBox.Show("Error not expected !!! "+item);
                        break;

                }
                if (item == null)
                    return;
                
                demoEntities10 dc = new demoEntities10();
                List<Account> linkas = linkacc.ItemsSource as List<Account>;
                var lists = dc.Accounts.Where(s => s.accountType == linkType).ToList();
                //var final = linkas.Where
                //linkacc.ItemsSource = linkas.;
                var items = memid.SelectedItem as Member;
                //var lists = dc.Accounts.Where(r => r.memberid == items.id).ToList();
                linkacc.ItemsSource = lists.Where(s => s.accountType == linkType).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void memid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            acctype.ItemsSource = null;
            List<string> actype1 = new List<string>() { "Төлбөр", "Барьцаа", "Клиринг", "Арилжаа" };
            List<string> actype2 = new List<string>() { "Төлбөр", "Барьцаа", "Клиринг" };
            List<string> ariljaa = new List<string>() { "Арилжаа" };

            var item = memid.SelectedItem as Member;
            linkacc.ItemsSource = null;
            try
            {
                string mtypee = item.type.ToString();
                #region ander broker dealer nominal checknull
                string ander, broker, dealer, nominal;
                if (item.ander == null)
                {
                    ander = "False";
                }
                else
                {
                    ander = item.ander.ToString();
                }
                if (item.dealer == null)
                {
                    dealer = "False";
                }
                else
                {
                    dealer = item.dealer.ToString();
                }

                if (item.broker == null)
                {
                    broker = "False";
                }
                else
                {
                    broker = item.broker.ToString();
                }
                if (item.nominal == null)
                {
                    nominal = "False";
                }
                else
                {
                    nominal = item.nominal.ToString();
                }
                #endregion
                #region nominal false
                if (nominal == "False")
                {
                    acctype.Text = "Арилжаа";
                    acctype.ItemsSource = ariljaa;
                    acctype.SelectedIndex = 0;
                    acctype.IsEnabled = false;
                    linkacc.IsEnabled = true;
                    return;
                }

                #endregion
                #region nominal true
                else if
                    (nominal == "True" && item.type == 0 &&
                                            (item.broker == true
                                            || item.dealer == true
                                            || item.ander == true)
                    )
                {
                    acctype.ItemsSource = actype1;
                    acctype.IsEnabled = true;
                    return;
                }
                else if
                    (item.nominal == true && item.type == 1 &&
                                            (item.broker == false
                                            || item.dealer == false
                                            || item.ander == false)
                    )
                {
                    acctype.ItemsSource = actype2;
                    acctype.IsEnabled = true;
                    return;
                }

                #endregion
                
                //if (mtypee == "0") //арилжаа төрөлтэй байвал
                //{
                //    acctype.ItemsSource = actype1;
                //    acctype.SelectedValue = 3;
                //    acctype.IsEnabled = false;
                //    linkacc.IsEnabled = true;
                //}
                //// клиринг өөр юу ч үгүй
                else if (mtypee == "1" && (broker == "False" && dealer == "False" && ander == "False"))
                {
                    acctype.ItemsSource = actype2;
                    linkacc.IsEnabled = true;
                    acctype.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Error un predicted condition match !!!");
                }
                //// клиринг өөр бусад эрхтэй
                //else if (mtypee == "1" &&(broker == "True" || dealer == "True" || ander == "True")
                //        )
                //{
                //    acctype.ItemsSource = actype1;
                //    acctype.SelectedItem = null;
                //    linkacc.IsEnabled = true;
                //    acctype.IsEnabled = true;
                //}
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
                return;
            }
        }
        #endregion
    }
}
