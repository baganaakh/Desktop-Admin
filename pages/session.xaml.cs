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
using pages.dbBind;

namespace pages
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class session : Page
    {
        public session()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id, cid, statid;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            string bid = value.Row[1].ToString();
            string name = value.Row[2].ToString();
            string sTime = value.Row[3].ToString();
            string duration = value.Row[4].ToString();
            string algorithm = value.Row[5].ToString();
            string match = value.Row[6].ToString();
            string allowtyp = value.Row[7].ToString();
            string description = value.Row[8].ToString();
            string state = value.Row[9].ToString();
            string isACT = value.Row[11].ToString();
            string startTime = value.Row[12].ToString();
            string endTime = value.Row[13].ToString();
            string tduartion = value.Row[14].ToString();
            string matched = value.Row[15].ToString();
            string editoreder = value.Row[16].ToString();
            string deleteorder = value.Row[17].ToString();
            string markettype = value.Row[18].ToString();

            char seperator = ':';
            string[] arrays = duration.Split(seperator);
            string dHours = arrays[0];
            string dMinute = arrays[1];
            string dSecond= arrays[2];
            string[] stimes = sTime.Split(seperator);
            string shours = stimes[0];
            string sminutes="";
            string ssecond="";
            try
            {
            sminutes= stimes[1];
            ssecond= stimes[2];
            }
            catch
            {
                sminutes = "";
                ssecond = "";
            }
            
            stimeSecond.Text= ssecond;
            dminute.Text= dMinute;
            dhour.Text= dHours;
            dsecond.Text= dSecond;
            stimehour.Text= shours;
            stimeminute.Text= sminutes;

            sboardid.SelectedValue = bid;
            sname.Text = name;

            algo.Text = algorithm;
            match1.Text = match;
            allowT.Text = allowtyp;
            sdesc.Text = description;
            sstate.SelectedValue = state;
            eOrder.IsChecked =bool.Parse(editoreder);
            dOrder.IsChecked=bool.Parse(deleteorder);
            markT.Text = markettype;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string boardid = cid;
            string name = sname.Text;
            string sstimeminute = stimeminute.Text;
            string sstimehour = stimehour.Text;
            string dhours = dhour.Text;
            string dminutes = dminute.Text;
            string alogor = algo.Text;
            string match11 = match1.Text;
            string allowedT = allowT.Text;
            string descrip = sdesc.Text;
            string EDorder = eOrder.IsChecked.ToString();
            string DEorder = dOrder.IsChecked.ToString();
            string markType = markT.Text;
            string state = statid;
            string dSecond = dsecond.Text;
            string sSecond = stimeSecond.Text;
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.session (boardid, name, stime, duration, algorithm, match, allowedtypes, description, state, modified, editorder, delorder, markettype) values" +
                " ('" + boardid + "',N'" + name + "',N'" + sstimehour + ":"+sstimeminute+":"+sSecond+"' ,N'" + dhours + ":" + dminutes + ":"+dSecond+"', '" + alogor + "', '" + match11 + "', '" + allowedT + "', '" + descrip + "', '" + state +
                "', getdate(),'" + EDorder + "', '" + DEorder + "', '" + markType + "')";
            cmd.Parameters.AddWithValue("@modified", DateTime.Now);

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
                CmdString = "SELECT ALL [id], [boardid], [name], [stime], [duration], [algorithm], [match], [allowedtypes], [description], [state] ,[modified], " +
                    "[isactive], [starttime], [endtime], [tduration], [matched], [editorder], [delorder], [markettype] FROM dbo.session ";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                // DataRowView dr = new DataRowView();
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
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
            cmd.CommandText = "DELETE demo.dbo.session WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region new
        private void newData(object sender, RoutedEventArgs e)
        {
            sboardid.SelectedValue = null;
            sname.Text = null;
            stimeminute.Text=null;
            stimehour.Text=null;
            dhour.SelectedValue = null;
            algo.Text = null;
            match1.Text = null;
            allowT.Text = null;
            sdesc.Text = null;
            sstate.Text = null;
            eOrder.IsChecked = null;
            dOrder.IsChecked= null;
            markT.Text = null;
            id = null;
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            string boardid = cid;
            string name = sname.Text;
            string sstimeminute = stimeminute.Text;
            string sstimehour = stimehour.Text;
            string dhours = dhour.Text;
            string dminutes = dminute.Text;
            string alogor = algo.Text;
            string match11 = match1.Text;
            string allowedT = allowT.Text;
            string descrip = sdesc.Text;
            string state = statid;
            string EDorder = eOrder.IsChecked.ToString();
            string DEorder = dOrder.IsChecked.ToString();
            string markType = markT.Text;
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.session SET " +
                "boardid = '" + boardid + "', " +
                "name= '" + name + "', " +
                "stime = " + sstimehour + ":" + sstimeminute + ", " +
                "duration = '" + dhours + ":" + dminutes + "', " +
                "algorithm= '" + alogor + "', " +
                "match= '" + match11 + "', " +
                "allowedtypes= '" + allowedT + "', " +
                "description= '" + descrip + "', " +
                "state= '" + state + "', " +
                "modified = getdate(), " +
                "editorder= '" + EDorder + "', " +
                "delorder= '" + DEorder + "', " +
                "markettype= '" + markType + "' " +
                "WHERE id = '" + id + "'";

            checkDAta.Text = cmd.CommandText;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region combos
        public List<Board> boa { get; set; }
        public List<State> statt { get; set; }

        private void bindCombo()
        {
            demoEntities10 dE = new demoEntities10();
            var item = dE.Boards.ToList();
            boa = item;
            sboardid.ItemsSource = boa;

            demoEntities10 st = new demoEntities10();
            var items = st.States.ToList();
            statt = items;
            sstate.ItemsSource = statt;
        }
        private void sstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items =sstate.SelectedItem as State;
            try
            {
                statid = items.id.ToString();
            }
            catch
            {
                return;
            }
        }

        private void boardid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = sboardid.SelectedItem as Board;
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
