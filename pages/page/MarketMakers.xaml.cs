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
    /// Interaction logic for MarketMakers.xaml
    /// </summary>
    public partial class MarketMakers : Page
    {
        public MarketMakers()
        {
            InitializeComponent();
            FillDataGrid();
            bindcombo();
        }
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as MarketMaker;
            if (null == values) return;
         
            markcontact.SelectedValue=values.contactid;
            markmember.SelectedValue=values.memberid;
            markaccount.SelectedValue=values.accountid;
            sdat.SelectedDate = values.startdate;
            edat.SelectedDate=values.enddate;
            markticks.Text = values.ticks.ToString();
            markdesc.Text=values.description;
            markorderl.Text=values.orderlimit.ToString();
            markstat.SelectedIndex=values.state+1;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (sdat.SelectedDate == null || edat.SelectedDate == null)
            {
                MessageBox.Show("Please Set Date !!!!!");
                return;
            }
            using (demoEntities10 contx = new demoEntities10())
            {
                MarketMaker mam = new MarketMaker
                {
                    contactid=Convert.ToInt32(markcontact.SelectedValue),
                    memberid=Convert.ToInt32(markmember.SelectedValue),
                    accountid=Convert.ToInt64(markaccount.SelectedValue),
                    startdate=Convert.ToDateTime(sdat.SelectedDate),
                    enddate=Convert.ToDateTime( edat.SelectedDate),
                    ticks=Convert.ToInt32(markticks.Text),
                    description=markdesc.Text,
                    orderlimit=Convert.ToInt32(markorderl.Text),
                    state=Convert.ToInt16(markstat.SelectedIndex-1),
                    modified=DateTime.Now,
                };
                contx.MarketMakers.Add(mam);
                contx.SaveChanges();
            }               
                FillDataGrid();
        }
        #endregion
        #region fill Number
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.MarketMakers.ToList();
        }
        #endregion
        #region ref new
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            markcontact.Text = null;
            markmember.Text = null;
            markaccount.Text = null;
            sdat.SelectedDate = null;
            edat.SelectedDate = null;
            markticks.Text = null;
            markdesc.Text = null;
            markorderl.Text = null;
            markstat.Text = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as MarketMaker;
            if (value == null) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.MarketMakers.Where(x => x.id == value.id).First();
                conx.MarketMakers.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            var ac = DateTable2.SelectedItem as MarketMaker;
            using (demoEntities10 conx = new demoEntities10())
            {
                MarketMaker mm = conx.MarketMakers.FirstOrDefault(r=>r.id==ac.id);
                mm.contactid =Convert.ToInt32(markcontact.SelectedValue);
                mm.memberid = Convert.ToInt32(markmember.SelectedValue);
                mm.modified = DateTime.Now;
                mm.accountid = Convert.ToInt64(markaccount.SelectedValue);
                mm.startdate =Convert.ToDateTime( sdat.SelectedDate);
                mm.enddate = Convert.ToDateTime(edat.SelectedDate);
                mm.ticks = Convert.ToInt32(markticks.Text);
                mm.description = markdesc.Text;
                mm.orderlimit = Convert.ToInt32(markorderl.Text);
                mm.state=Convert.ToInt16(markstat.SelectedIndex-1);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region combos
        private void bindcombo()
        {
            demoEntities10 dc = new demoEntities10();
            markmember.ItemsSource= dc.Members.ToList();
            markcontact.ItemsSource = dc.Contracts.ToList();
            markaccount.ItemsSource = dc.Accounts.ToList();
        }
        #endregion
    }
}
