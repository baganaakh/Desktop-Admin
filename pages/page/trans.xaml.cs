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
    /// Interaction logic for trans.xaml
    /// </summary>
    public partial class trans : Page
    {
        public trans()
        {
            InitializeComponent();
            FillDataGrid();
            bindcombo();
        }
        string connectionString = Properties.Settings.Default.ConnectionString;
        static string cid,statid;
        long id;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as Transaction;
            if (null == values) return;
            id = values.id;

            sboardid.SelectedValue = values.memberid;
            accId.SelectedValue= values.accountId;
            trtype.SelectedValue= values.type;
            trtype1.SelectedValue= values.type1;
            tramount.Text = values.amount.ToString();
            trcurrent.Text = values.currency;
            trrate.Text = values.rate.ToString();
            trnote.Text = values.note;
            trtdate.SelectedDate = values.tdate;
            trstate.SelectedIndex=Convert.ToInt32(values.state+1);
            truser.Text = values.userId.ToString();
            asstId.SelectedValue = values.assetId;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            int type = trtype.SelectedIndex;
            if (type == 0)
                type = -1;
            using(demoEntities10 conx=new demoEntities10())
            {
                Transaction tra = new Transaction
                {
                    accountId = Convert.ToInt64(accId.SelectedValue),
                    type = Convert.ToInt16(type),
                    type1 = Convert.ToInt16(trtype1.SelectedIndex),
                    amount = Convert.ToInt32(tramount.Text),
                    userId = Convert.ToInt32(App.Current.Properties["User_id"]),
                    assetId = Convert.ToInt32(asstId.SelectedValue),
                    rate = Convert.ToInt32(trrate.Text),
                    note = trnote.Text,
                    tdate = trtdate.SelectedDate,
                    state = Convert.ToInt16(trstate.SelectedIndex - 1),
                    modified = DateTime.Now,
                    memberid = Convert.ToInt32(sboardid.SelectedValue),
                    currency = trcurrent.Text,
            };
                conx.Transactions.Add(tra);
                conx.SaveChanges();
            }
           // if (trtdate.SelectedDate == null)
           // {
           //     MessageBox.Show("Please Set Date !!!!!");
           //     return;
           // }
           // string MemID = cid;
           // string accountId = accId.Text;
           // string type = trtype.Text;
           // string type1 = trtype1.Text;
           // string ammount = tramount.Text;
           // string currecy = trcurrent.Text;
           // string rate= trrate.Text;
           // string note= trnote.Text;
           // string tdate = trtdate.SelectedDate.Value.ToShortDateString();
           // string state = trstate.Text;
           // string userId = truser.Text;

           // System.Data.SqlClient.SqlConnection sqlConnection1 =
           //new System.Data.SqlClient.SqlConnection(connectionString);

           // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
           // cmd.CommandType = System.Data.CommandType.Text;
           // cmd.CommandText = "insert into dbo.trans(accountId, type, type1, amount, currency, rate, note, tdate, state, modified, userId,memberid) values" +
           //     " ('" + accountId+ "',N'" + type + "',N'" + type1+ "',N'" + ammount+ "', '" + currecy+ "', '" + rate+ "', '" + note+ "',N'" +tdate +"', '" + statid+ "', getdate(), '" + userId+"', '" + MemID+"')";

           // cmd.Connection = sqlConnection1;
           // sqlConnection1.Open();
           // cmd.ExecuteNonQuery();
           // sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region numbers

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        #endregion
        #region fill
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.Transactions.ToList();           
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        #endregion
        #region new
        private void newData(object sender, RoutedEventArgs e)
        {
            accId.Text = null;
            trtype.Text = null;
            trtype1.Text = null;
            tramount.Text = null;
            trcurrent.Text = null;
            trrate.Text = null;
            trnote.Text = null;
            trtdate.SelectedDate = null;
            trstate.Text = null;
            truser.Text = null;
            sboardid.SelectedValue = null;
            id = 0;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as Transaction;
            if (value == null) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.Transactions.Where(x => x.id == value.id).First();
                conx.Transactions.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            int type = trtype.SelectedIndex;
            if (type == 0)
                type = -1;
            using (demoEntities10 conx = new demoEntities10())
            {
                Transaction se = conx.Transactions.FirstOrDefault(r => r.id == id);
                se.accountId = Convert.ToInt64(accId.SelectedValue);
                se.type = Convert.ToInt16(type);
                se.type1 = Convert.ToInt16(trtype1.SelectedIndex);
                se.amount = Convert.ToInt32(tramount.Text);
                se.userId = Convert.ToInt32(App.Current.Properties["User_id"]);
                se.assetId = Convert.ToInt32(asstId.SelectedValue);
                se.rate = Convert.ToInt32(trrate.Text);
                se.note = trnote.Text;
                se.tdate = trtdate.SelectedDate;
                se.state = Convert.ToInt16(trstate.SelectedIndex - 1);
                se.modified = DateTime.Now;
                se.memberid = Convert.ToInt32(sboardid.SelectedValue);
                se.currency = trcurrent.Text;
                conx.SaveChanges();
            }


            // string accountId = accId.Text;
            // string MemID = cid;
            // string type = trtype.Text;
            // string type1 = trtype1.Text;
            // string ammount = tramount.Text;
            // string currecy = trcurrent.Text;
            // string rate = trrate.Text;
            // string note = trnote.Text;
            // string tdate = trtdate.SelectedDate.Value.ToShortDateString();
            // string state = trstate.Text;
            // string userId = truser.Text;

            // System.Data.SqlClient.SqlConnection sqlConnection1 =
            //new System.Data.SqlClient.SqlConnection(connectionString);

            // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            // cmd.CommandType = System.Data.CommandType.Text;
            // cmd.CommandText = "UPDATE demo.dbo.trans SET " +
            //     "accountId= '" + accountId+ "', " +
            //     "type= '" + type + "', " +
            //     "type1= '" + type1 + "', " +
            //     "amount= '" + ammount + "', " +
            //     "currency= '" + currecy+ "', " +
            //     "rate= '" + rate+ "', " +
            //     "note= '" + note+ "', " +
            //     "tdate= '" + tdate+ "', " +
            //     "state= '" + statid+ "', " +
            //     "userId= '" + userId+ "', " +
            //     "userId= '" + MemID+ "', " +
            //     "modified = getdate() " +
            //     "WHERE id = '" + id + "'";

            // cmd.Connection = sqlConnection1;
            // sqlConnection1.Open();
            // cmd.ExecuteNonQuery();
            // sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region combos
        private void bindcombo()
        {
            demoEntities10 dc = new demoEntities10();
            sboardid.ItemsSource = dc.Members.ToList();
            accId.ItemsSource = dc.Accounts.ToList();
            asstId.ItemsSource = dc.Assets.ToList();
        }
        #endregion
    }
}
