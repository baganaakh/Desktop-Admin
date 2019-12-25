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
using pages.dbBind;

namespace pages
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
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id,cid,statid;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            id = values.Row[0].ToString();
            string accid = values.Row[1].ToString();
            string Type = values.Row[2].ToString();
            string Type1= values.Row[3].ToString();
            string amount= values.Row[4].ToString();
            string currency= values.Row[5].ToString();
            string rate = values.Row[6].ToString();
            string note = values.Row[7].ToString();
            string tdate = values.Row[8].ToString();
            string state = values.Row[9].ToString();
            string userid = values.Row[11].ToString();
            string memberid= values.Row[12].ToString();

            sboardid.SelectedValue = memberid;
            accId.Text= accid;
            trtype.Text = Type;
            trtype1.Text = Type1;
            tramount.Text = amount;
            trcurrent.Text = currency;
            trrate.Text = rate;
            trnote.Text = note;
            trtdate.SelectedDate = DateTime.Parse(tdate);
            trstate.SelectedValue = state;
            truser.Text = userid;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (trtdate.SelectedDate == null)
            {
                MessageBox.Show("Please Set Date !!!!!");
                return;
            }
            string MemID = cid;
            string accountId = accId.Text;
            string type = trtype.Text;
            string type1 = trtype1.Text;
            string ammount = tramount.Text;
            string currecy = trcurrent.Text;
            string rate= trrate.Text;
            string note= trnote.Text;
            string tdate = trtdate.SelectedDate.Value.ToShortDateString();
            string state = trstate.Text;
            string userId = truser.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.trans(accountId, type, type1, amount, currency, rate, note, tdate, state, modified, userId,memberid) values" +
                " ('" + accountId+ "',N'" + type + "',N'" + type1+ "',N'" + ammount+ "', '" + currecy+ "', '" + rate+ "', '" + note+ "',N'" +tdate +"', '" + statid+ "', getdate(), '" + userId+"', '" + MemID+"')";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region numbers
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        #endregion
        #region fill
        private void FillDataGrid()
        {
            string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
            string CmdString = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                CmdString = "SELECT ALL [id], [accountId], [type], [type1], [amount], [currency], [rate], [note], [tdate], [state], [modified], [userId], [memberid] "+
        "FROM dbo.trans";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Securities");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
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
            id = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.dbo.trans WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            string accountId = accId.Text;
            string MemID = cid;
            string type = trtype.Text;
            string type1 = trtype1.Text;
            string ammount = tramount.Text;
            string currecy = trcurrent.Text;
            string rate = trrate.Text;
            string note = trnote.Text;
            string tdate = trtdate.SelectedDate.Value.ToShortDateString();
            string state = trstate.Text;
            string userId = truser.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.trans SET " +
                "accountId= '" + accountId+ "', " +
                "type= '" + type + "', " +
                "type1= '" + type1 + "', " +
                "amount= '" + ammount + "', " +
                "currency= '" + currecy+ "', " +
                "rate= '" + rate+ "', " +
                "note= '" + note+ "', " +
                "tdate= '" + tdate+ "', " +
                "state= '" + statid+ "', " +
                "userId= '" + userId+ "', " +
                "userId= '" + MemID+ "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region combos
        public List<Member> Emp { get; set; }
        public List<State> statt { get; set; }

        private void bindcombo()
        {
            demoEntities10 dc = new demoEntities10();
            var item = dc.Members.ToList();
            Emp = item;
            sboardid.ItemsSource = Emp;

            demoEntities10 st = new demoEntities10();
            var items = st.States.ToList();
            statt = items;
            trstate.ItemsSource = statt;
        }
        private void partid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = sboardid.SelectedItem as Member;

            try
            {
                cid = item.id.ToString();
            }
            catch
            {
                return;
            }
        }
        private void sstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = trstate.SelectedItem as State;
            try
            {
                statid = items.id.ToString();
            }
            catch
            {
                return;
            }
        }
        #endregion
    }
}
