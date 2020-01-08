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
    /// Interaction logic for TickSizeTable.xaml
    /// </summary>
    public partial class TickSizeTable : Page
    {
        public TickSizeTable()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id,statid;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            id = values.Row[0].ToString();
            string tableId = values.Row[1].ToString();
            string Tick= values.Row[2].ToString();
            string price = values.Row[3].ToString();
            string State = values.Row[4].ToString();

            tableid.Text=tableId;
            tickk.Text=Tick;
            pricee.Text=price;
            stat.SelectedValue= State ;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            string tableId = tableid.Text;
            string tick= tickk.Text;
            string price = pricee.Text;
            string state = stat.Text;
          
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.TickSizeTable (tableid, tick, price, state, modified) values" +
                " ('" + tableId+ "',N'" + tick+ "',N'" + price+ "',N'" + statid+ "', getdate())";

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
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                 string CmdString = "SELECT ALL [id], [tableid], [tick], [price], [state], [modified] " +
                            "FROM dbo.TickSizeTable";
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
            tableid.Text = null;
            tickk.Text = null;
            pricee.Text = null;
            stat.Text = null;
            id = null;
            statid = null;
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
            cmd.CommandText = "DELETE demo.dbo.TickSizeTable WHERE id='" + id + "'";
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
            string tableId = tableid.Text;
            string tick = tickk.Text;
            string price = pricee.Text;
            string state = stat.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.TickSizeTable SET " +
                "tableid= '" + tableId + "', " +
                "tick= '" + tick+ "', " +
                "price= '" + price + "', " +
                "state= '" + statid + "', " +
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
        public List<State> statt { get; set; }

        private void bindCombo()
        {
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
        #endregion
    }
}
