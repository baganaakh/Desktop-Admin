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
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id,statid,cid;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            id = values.Row[0].ToString();
            string memId= values.Row[1].ToString();
            string accId= values.Row[2].ToString();
            string State = values.Row[4].ToString();

            memid.SelectedValue=memId;
            accid.Text=accId;
            stat.SelectedValue=State;
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
          //  string memberID = memid.Text;
            string accID = accid.Text;
          //  string state = stat.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.DealerAccounts (memberid, accountid, state, modified) values" +
                " ('" + cid + "',N'" + accID+ "',N'" + statid +"', getdate())";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void FillDataGrid()
        {
            string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
            string CmdString = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                CmdString = "SELECT ALL [id], [memberid], [accountid],[state], [modified] " +
                            "FROM dbo.DealerAccounts";
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
        private void newData(object sender, RoutedEventArgs e)
        {
            memid.Text = null;
            accid.Text = null;
            stat.Text = null;
            id = null;
            statid = null;
            cid = null;
        }
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.dbo.DealerAccounts WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        private void update(object sender, RoutedEventArgs e)
        {
            string memberID = memid.Text;
            string accID = accid.Text;
            string state = stat.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.DealerAccounts SET " +
                "memberid= '" + cid + "', " +
                "accountid= '" + accID+ "', " +
                "state= '" + statid + "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        public List<State> statt { get; set; }
        public List<Member> Emp { get; set; }

        private void bindCombo()
        {
            demoEntities10 dc = new demoEntities10();
            var item = dc.Members.ToList();
            Emp = item;
            memid.ItemsSource = Emp;

            demoEntities10 st = new demoEntities10();
            var items = st.States.ToList();
            statt = items;
            stat.ItemsSource = statt;
        }
        private void sstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = stat.SelectedItem as State;
            try
            {
                statid = items.id.ToString();
            }
            catch
            {
                return;
            }
        }
        private void partid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = memid.SelectedItem as Member;

            try
            {
                cid = item.id.ToString();
            }
            catch
            {
                return;
            }

        }
    }
}
