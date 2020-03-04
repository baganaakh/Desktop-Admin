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
using Admin.dbBind;

namespace Admin
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class boards : Page
    {
        public boards()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = Properties.Settings.Default.ConnectionString;
        string id, cid, dealTypes;
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
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            string name = value.Row[1].ToString();
            string type = value.Row[2].ToString();
            string tdays = value.Row[3].ToString();
            string stat = value.Row[4].ToString();
            string description = value.Row[6].ToString();
            dealTypes = value.Row[7].ToString();
            string time= value.Row[8].ToString();
            string eday= value.Row[9].ToString();

            bname.Text = name;
            btype.Text = type;
            tdayss.Text = tdays;
            desc.Text = description;
            state.SelectedValue = stat;
            dealtype.SelectedValue = dealTypes;
            string[] times = time.Split(':');
            dhour.Text= times[0];
            dminute.Text= times[1];
            dsecond.Text= times[2];
            enddate.Text = eday;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (bname.Text == null)
            {
                MessageBox.Show("bname null!!!!");
            }
            string boardName = bname.Text;
            string type = btype.Text;
            string tdays = tdayss.Text;
            string descr = desc.Text;
            string ehour = dhour.Text;
            string eminute = dminute.Text;
            string esecond = dsecond.Text;
            string exdate = enddate.Text;
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.boards (name, type, tdays, description, state,dealType,expTime,expDate) values" +
                " (N'" + boardName + "',N'" + type + "',N'" + tdays + "', N'" + descr + "', N'" + cid + "',"+ dealTypes + "," +
                "'"+ehour+":"+eminute+":"+esecond+"',"+exdate+")";

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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT * FROM dbo.boards ";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            string boardName = bname.Text;
            string type = btype.Text;
            string tdays = tdayss.Text;
            string descr = desc.Text;
            string ehour = dhour.Text;
            string eminute = dminute.Text;
            string esecond = dsecond.Text;
            string exdate = enddate.Text;
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.boards SET " +
                "name = N'" + boardName + "', " +
                "type = N'" + type + "', " +
                "tdays = N'" + tdays + "', " +
                "description = N'" + descr + "', " +
                "state = N'" + cid + "', " +
                "dealType = " + dealTypes + ", " +
                "expDate = " + exdate + ", " +
                "expTime = '"+ehour+":"+eminute+":"+esecond+"', " +
                "modified = getdate() " +
                "WHERE id = " + id;

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
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
            cmd.CommandText = "DELETE demo.dbo.boards WHERE id=N'" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region reg and new
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void newData(object sender, RoutedEventArgs e)
        {
            bname.Text = null;
            btype.Text = null;
            tdayss.Text = null;
            desc.Text = null;
            state.SelectedValue = null; 
            dhour.Text=null;
            dminute.Text=null;
            dsecond.Text=null;
            dealtype.SelectedItem = null;
            enddate.Text = null;
        }
        #endregion
        #region combos
        public List<State> boa { get; set; }
        public List<Dealtype> Dtype { get; set; }

        private void bindCombo()
        {
            demoEntities10 dE = new demoEntities10();
            
            var item = dE.States.ToList();
            boa = item;
            state.ItemsSource = boa;

            var dts = dE.Dealtypes.ToList();
            Dtype = dts;
            dealtype.ItemsSource = Dtype;
        }

        private void tdayss_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dealtype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dti = dealtype.SelectedItem as Dealtype;
            try
            {
                dealTypes = dti.id.ToString();
            }
            catch
            {
                return;
            }
        }

        private void stat_change(object sender, SelectionChangedEventArgs e)
        {
            var item = state.SelectedItem as State;
            try
            {
                cid = item.id.ToString();
            }
            catch
            {
                return;
            }
        }
        #endregion

    }
}
