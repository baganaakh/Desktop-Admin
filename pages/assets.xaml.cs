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
using System.Data;
using System.Text.RegularExpressions;
using pages.dbBind;

namespace pages
{
    /// <summary>
    /// Interaction logic for assets.xaml
    /// </summary>
    public partial class assets : Page
    {
        public assets()
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
            if (null == values) return;
            id = values.Row[0].ToString();
            string codev = values.Row[1].ToString();
            string namev = values.Row[2].ToString();
            string pricev = values.Row[3].ToString();
            string descv = values.Row[4].ToString();
            string expirev = values.Row[5].ToString();
            string statev = values.Row[6].ToString();
            string ratiov = values.Row[8].ToString();

            acode.Text = codev;
            aname.Text = namev;
            aprice.Text = pricev;
            anote.Text = descv;
            Decimal ratio = Decimal.Parse(ratiov)*100;
            artio.Text =ratio.ToString();
            aexpire.SelectedDate = DateTime.Parse(expirev);
            astate.SelectedValue = statev;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (aexpire.SelectedDate == null)
            {
                MessageBox.Show("Please Set Date !!!!!");
                return;
            }
            string code = acode.Text;
            string name = aname.Text;
            string value = aprice.Text;
            string note = anote.Text;
            string expireDate = aexpire.SelectedDate.Value.ToShortDateString();
            string rati = artio.Text;
            decimal ratio = Decimal.Parse(rati)/100;
            string state = astate.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.assets(code, name, value, note, expireDate, state, modified, ratio) values" +
                " ('" + code + "',N'" + name + "',N'" + value + "',N'" + note + "', '" + expireDate+ "', '" + statid+ "', getdate(), '" + ratio+ "')";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region fill
        private void FillDataGrid()
        {
            string CmdString = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                CmdString = "SELECT ALL [id], [code], [name], [value], [note], [expireDate], [state], [modified], [ratio], [secId] "+
                    "FROM dbo.assets";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Securities");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        #endregion
        #region ref new
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            acode.Text = null;
            aname.Text = null;
            aprice.Text = null;
            anote.Text = null;
            artio.Text = null;
            aexpire.SelectedDate = null;
            astate.Text = null;
            id = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.dbo.assets WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region number
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
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            string code = acode.Text;
            string name = aname.Text;
            string value = aprice.Text;
            string note = anote.Text;
            string expireDate = aexpire.SelectedDate.Value.ToShortDateString();
            string rati = artio.Text;
            decimal ratio = Decimal.Parse(rati) / 100;
            string state = astate.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);


            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.assets SET " +
                "code= '" + code+ "', " +
                "name= '" + name + "', " +
                "value= '" + value+ "', " +
                "note= '" + note + "', " +
                "expireDate= '" + expireDate+ "', " +
                "state= '" + statid + "', " +
                "modified = getdate(), " +
                "ratio= '" + ratio+ "' " +
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
            astate.ItemsSource = statt;
        }
        private void sstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = astate.SelectedItem as State;
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
