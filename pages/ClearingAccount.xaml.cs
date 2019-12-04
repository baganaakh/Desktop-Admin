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
    /// Interaction logic for ClearingAccount.xaml
    /// </summary>
    public partial class ClearingAccount : Page
    {
        public ClearingAccount()
        {
            InitializeComponent();
            FillDataGrid();
            bindcombo();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id,cid;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            id = values.Row[0].ToString();
            string MID= values.Row[1].ToString();
            string Acc = values.Row[2].ToString();
            string Type= values.Row[3].ToString();
            string Currency = values.Row[4].ToString();
            string Blnc = values.Row[5].ToString();
            string Sblnc = values.Row[6].ToString();
            string Linkacc= values.Row[7].ToString();
            string State= values.Row[8].ToString();

            memid.SelectedValue=MID;
            accid.Text=Acc;
            typee.Text=Type;
            currency.Text=Currency;
            balanc.Text=Blnc;
            sbalanc.Text=Sblnc;
            linkacc.Text=Linkacc;
            stat.Text=State;
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string memID = cid;
            string accID = accid.Text;
            string type = typee.Text;
            string currenc = currency.Text;
            string blnc = balanc.Text;
            string sblnc = sbalanc.Text;
            string linkAcc = linkacc.Text;
            string state = stat.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.ClearingAccounts (memberid, account, type, currency, blnc, sblnc, linkaccount, state,modified) values" +
                " ('" + memID+ "','" + accID+ "','" + type+ "','" + currenc+ "', '" + blnc+ "', '" + sblnc+ "', '" + linkAcc+ "', '" + state+ "',getdate())";

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
                CmdString = "SELECT ALL [id], [memberid], [account], [type], [currency], [blnc], [sblnc], [linkaccount], [state], [modified] " +
                            "FROM dbo.ClearingAccounts";
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
            typee.Text = null;
            currency.Text = null;
            balanc.Text =null;
            sbalanc.Text = null;
            linkacc.Text = null;
            stat.Text = null;
            id = null;
        }
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.dbo.ClearingAccounts WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        private void update(object sender, RoutedEventArgs e)
        {
            string memID = cid;
            string accID = accid.Text;
            string type = typee.Text;
            string currenc = currency.Text;
            string blnc = balanc.Text;
            string sblnc = sbalanc.Text;
            string linkAcc = linkacc.Text;
            string state = stat.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.ClearingAccounts SET " +
                "memberid= '" + memID + "', " +
                "account= '" + accID + "', " +
                "type= '" + type + "', " +
                "currency= '" + currenc + "', " +
                "blnc= '" + blnc + "', " +
                "sblnc= '" + sblnc + "', " +
                "linkaccount= '" + linkAcc + "', " +
                "state= '" + state + "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        public List<Members> Emp { get; set; }
        private void bindcombo()
        {
            demoEntities1 dc = new demoEntities1();
            var item = dc.Members.ToList();
            Emp = item;
            memid.ItemsSource = Emp;
        }
        private void partid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = memid.SelectedItem as Members;
            cid = item.id.ToString();

        }
    }
}
