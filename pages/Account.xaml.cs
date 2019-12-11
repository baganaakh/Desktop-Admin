using pages.dbBind;
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

namespace pages
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
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id, memId;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            id = values.Row[0].ToString();
            string memId= values[1].ToString();
            string Trading = values[2].ToString();
            string Clearing = values[3].ToString();
            string Settlement = values[4].ToString();
            string Collateral = values[5].ToString();

            memid.SelectedValue = memId;
            trading.Text = Trading;
            clearing.Text= Clearing;
            settlement.Text = Settlement;
            collateral.Text = Collateral;
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string Trading = trading.Text;
            string Clearing = clearing.Text;
            string Settlement = settlement.Text;
            string Collateral = collateral.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.Account (memberid, trading, clearing, settlement, collateral, modified) values" +
                " ('" + memId + "',N'" + Trading+ "',N'" + Clearing+ "',N'" + Settlement + "',N'" + Collateral + "', getdate())";

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
                CmdString = "SELECT ALL [id], [memberid], [trading], [clearing], [settlement], [collateral], [modified] " +
                            "FROM dbo.Account";
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
            trading.Text = null;
            clearing.Text = null;
            settlement.Text = null;
            collateral.Text = null;
            id = null;
            memId= null;
        }
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.dbo.Account WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        private void update(object sender, RoutedEventArgs e)
        {
            string Trading = trading.Text;
            string Clearing = clearing.Text;
            string Settlement = settlement.Text;
            string Collateral = collateral.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.Account SET " +
                "memberid= '" + memId + "', " +
                "trading= '" + Trading+ "', " +
                "clearing= '" + Clearing + "', " +
                "settlement= '" + Settlement+ "', " +
                "collateral= '" + Collateral+ "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        public List<Members> Emp { get; set; }

        private void bindCombo()
        {
            demoEntities10 dc = new demoEntities10();
            var item = dc.Members.ToList();
            Emp = item;
            memid.ItemsSource = Emp;

        }
        
        private void partid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = memid.SelectedItem as Members;

            try
            {
                memId = item.id.ToString();
            }
            catch
            {
                return;
            }

        }
    }
}
