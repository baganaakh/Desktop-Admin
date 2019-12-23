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
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using pages.dbBind;

namespace pages
{
    /// <summary>
    /// Interaction logic for Spread.xaml
    /// </summary>
    public partial class Spread : Page
    {
        public Spread()
        {
            InitializeComponent();
            FillDataGrid();
            bindcombo();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id, coId;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            id = values.Row[0].ToString();
            string contId= values.Row[1].ToString();
            string sessId= values.Row[2].ToString();
            string rspre= values.Row[3].ToString();
            string ispre= values.Row[4].ToString();
            string rpara= values.Row[5].ToString();

            contractid.SelectedValue=contId;
            sessionid.Text=sessId;
            rspread.Text=rspre;
            ispread.Text=ispre;
            rparam.Text=rpara;
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string sessid= sessionid.Text;
            string rsprea= rspread.Text;
            string isprea= ispread.Text;
            string rpara= rparam.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.Spreads(contractid, sessionid, rspread, ispread, rparam, modified) values" +
                " ('" + coId + "',N'" + sessid+ "',N'"+ rsprea+ "',N'" + isprea+ "',N'" + rpara+"', getdate())";

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
                CmdString = "SELECT ALL [id], [contractid], [sessionid], [rspread], [ispread], [rparam], [modified] " +
                            "FROM dbo.Spreads";
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
            contractid.SelectedItem= null;
            sessionid.SelectedItem = null;
            rspread.Text = null;
            ispread.Text = null;
            rparam.Text = null;
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
            cmd.CommandText = "DELETE demo.dbo.Spreads WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        public List<Contract> Cont { get; set; }
        public List<Session> Sessionss { get; set; }
        private void contractid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var citem = contractid.SelectedItem as Contract;
            try
            {
                coId = citem.id.ToString();
            }
            catch
            {
                return;
            }
        }
        private void bindcombo()
        {
            demoEntities10 ct = new demoEntities10();
            var citem = ct.Contracts.ToList();
            Cont = citem;
            contractid.ItemsSource = Cont;

            //var meitems =ct.Sessions.ToList();
            //Sessionss = meitems;
            //sessionid.ItemsSource = Sessionss;
        }

        private void sessionid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void update(object sender, RoutedEventArgs e)
        {
            string sessid = sessionid.Text;
            string rsprea = rspread.Text;
            string isprea = ispread.Text;
            string rpara = rparam.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.Spreads SET " +
                "contractid= '" + coId+ "', " +
                "sessionid= '" + sessid+ "', " +
                "rspread= '" + rsprea+ "', " +
                "ispread= '" + isprea+ "', " +
                "rparam= '" + rpara+ "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
    }
}
