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
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id,coId,meId,statid;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as DataRowView;
            id = values.Row[0].ToString();
            string CID = values.Row[1].ToString();
            string MID = values.Row[2].ToString();
            string AID= values.Row[3].ToString();
            string SDate= values.Row[4].ToString();
            string EDate= values.Row[5].ToString();
            string Ticks = values.Row[6].ToString();
            string Desc = values.Row[7].ToString();
            string orderLimitt= values.Row[8].ToString();
            string State= values.Row[9].ToString();

            markcontact.SelectedValue=CID;
            markmember.SelectedValue=MID;
            markaccount.Text=AID;
            sdat.SelectedDate=DateTime.Parse(SDate);
            edat.SelectedDate=DateTime.Parse(EDate);
            markticks.Text = Ticks;
            markdesc.Text=Desc;
            markorderl.Text=orderLimitt;
            markstat.SelectedValue=State;
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
            string contId = coId;
            string memId = meId;
            string accId = markaccount.Text;
            string sdate = sdat.SelectedDate.Value.ToShortDateString();
            string edate = edat.SelectedDate.Value.ToShortDateString();
            string ticks = markticks.Text;
            string desc= markdesc.Text;
            string orderL = markorderl.Text;
            string state = markstat.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.marketMakers (contactid, memberid, accountid, startdate, enddate, ticks, description, orderlimit, state, modified) values" +
                " ('" + contId+ "',N'" + memId+ "',N'" + accId+ "',N'" + sdate+ "', '" + edate+ "', '" + ticks+ "', '" + desc+ "', '" + orderL+ "', '" + statid +
                "', getdate())";

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
        #region fill
        private void FillDataGrid()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT ALL [id], [contactid], [memberid], [accountid], [startdate], [enddate], [ticks], [description], [orderlimit], [state], [modified] " +
                            "FROM dbo.marketMakers";
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
            markcontact.Text = null;
            markmember.Text = null;
            markaccount.Text = null;
            sdat.SelectedDate = null;
            edat.SelectedDate = null;
            markticks.Text = null;
            markdesc.Text = null;
            markorderl.Text = null;
            markstat.Text = null;
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
            cmd.CommandText = "DELETE demo.dbo.marketMakers WHERE id='" + id + "'";
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
            string contId = coId;
            string memId = meId;
            string accId = markaccount.Text;
            string sdate = sdat.SelectedDate.Value.ToShortDateString();
            string edate = edat.SelectedDate.Value.ToShortDateString();
            string ticks = markticks.Text;
            string desc = markdesc.Text;
            string orderL = markorderl.Text;
            string state = markstat.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.marketMakers SET " +
                "contactid= '" + contId+ "', " +
                "memberid= '" + memId+ "', " +
                "accountid= '" + accId+ "', " +
                "startdate= '" + sdate + "', " +
                "enddate= '" + edate+ "', " +
                "ticks= '" + ticks+ "', " +
                "description= '" + desc+ "', " +
                "orderlimit= '" + orderL+ "', " +
                "state= '" + statid+ "', " +
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
        public List<Contract> Cont { get; set; }
        public List<Member> Emp { get; set; }
        public List<State> statt { get; set; }

        private void bindcombo()
        {
            demoEntities10 dc = new demoEntities10();
            var item = dc.Members.ToList();
            Emp = item;
            markmember.ItemsSource= Emp;

            demoEntities10 ct = new demoEntities10();
            var citem = ct.Contracts.ToList();
            Cont = citem;
            markcontact.ItemsSource = Cont;

            demoEntities10 st = new demoEntities10();
            var items = st.States.ToList();
            statt = items;
            markstat.ItemsSource = statt;
        }
        private void partid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = markmember.SelectedItem as Member;
            try
            {
            meId = item.id.ToString();
            }
            catch
            {
                return;
            }
        }
        private void contid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var citem = markcontact.SelectedItem as Contract;
            try
            {
            coId = citem.id.ToString();
            }
            catch
            {
                return;
            }
        }
        private void sstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = markstat.SelectedItem as State;
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
