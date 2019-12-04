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
        static string id, cid;
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
            checkDAta.Text = duration;
            string[] arrays = duration.Split(seperator);
            string dHours = arrays[0];
            string dMinute = arrays[1];

            dminute.Text = dMinute;
            dhour.Text = dHours;
            sboardid.SelectedValue = bid;
            sname.Text = name;
            stime.SelectedDate = DateTime.Parse(sTime);
            algo.Text = algorithm;
            match1.Text = match;
            allowT.Text = allowtyp;
            sdesc.Text = description;
            sstate.Text = state;
            iAct.Text = isACT;
            starttime.SelectedDate = DateTime.Parse(startTime);
            sEndTime.SelectedDate = DateTime.Parse(endTime);
            sduration.Text = tduartion;
            matchedN.Text = matched;
            eOrder.Text = editoreder;
            dOrder.Text = deleteorder;
            markT.Text = markettype;
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (stime.SelectedDate == null || starttime.SelectedDate == null || sEndTime.SelectedDate == null)
            {
                MessageBox.Show("Please Set Date !!!!!");
                return;
            }
            string boardid = cid;
            string name = sname.Text;
            string sstime = stime.SelectedDate.Value.ToShortDateString();
            string dhours = dhour.Text;
            string dminutes = dminute.Text;
            string alogor = algo.Text;
            string match11 = match1.Text;
            string allowedT = allowT.Text;
            string descrip = sdesc.Text;
            string iActive = iAct.Text;
            string state = sstate.Text;
            string startT = starttime.SelectedDate.Value.ToShortDateString();
            string endTime = sEndTime.SelectedDate.Value.ToShortDateString();
            string tduration = sduration.Text;
            string prevMatch = matchedN.Text;
            string EDorder = eOrder.Text;
            string DEorder = dOrder.Text;
            string markType = markT.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.session (boardid, name, stime, duration, algorithm, match, allowedtypes, description, state, modified, isactive," +
                " starttime, endtime, tduration, matched, editorder, delorder, markettype) values" +
                " ('" + boardid + "','" + name + "','" + sstime + "','" + dhours + ":" + dminutes + ":00', '" + alogor + "', '" + match11 + "', '" + allowedT + "', '" + descrip + "', '" + state +
                "', getdate(), '" + iActive + "', '" + startT + "', '" + endTime + "', '" + tduration + "','" + prevMatch + "', '" + EDorder + "', '" + DEorder + "', '" + markType + "')";
            cmd.Parameters.AddWithValue("@modified", DateTime.Now);
            //checkDAta.Text = cmd.CommandText;

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        private void FillDataGrid()
        {
            string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
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
        private void newData(object sender, RoutedEventArgs e)
        {
            sboardid.SelectedValue = null;
            sname.Text = null;
            stime.SelectedDate = null;
            dhour.SelectedValue = null;
            algo.Text = null;
            match1.Text = null;
            allowT.Text = null;
            sdesc.Text = null;
            sstate.Text = null;
            iAct.Text = null;
            starttime.SelectedDate = null;
            sEndTime.SelectedDate = null;
            sduration.Text = null;
            matchedN.Text = null;
            eOrder.Text = null;
            dOrder.Text = null;
            markT.Text = null;
            id = null;
        }
        private void update(object sender, RoutedEventArgs e)
        {
            if (stime.SelectedDate == null || starttime.SelectedDate == null || sEndTime.SelectedDate == null)
            {
                MessageBox.Show("Please Set Date !!!!!");
                return;
            }
            string boardid = cid;
            string name = sname.Text;
            string sstime = stime.SelectedDate.Value.ToShortDateString();
            string dhours = dhour.Text;
            string dminutes = dminute.Text;
            string alogor = algo.Text;
            string match11 = match1.Text;
            string allowedT = allowT.Text;
            string descrip = sdesc.Text;
            string state = sstate.Text;
            string iActive = iAct.Text;
            string startT = starttime.SelectedDate.Value.ToShortDateString();
            string endTime = sEndTime.SelectedDate.Value.ToShortDateString();
            string tduration = sduration.Text;
            string prevMatch = matchedN.Text;
            string EDorder = eOrder.Text;
            string DEorder = dOrder.Text;
            string markType = markT.Text;
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.session SET " +
                "boardid = '" + boardid + "', " +
                "name= '" + name + "', " +
                "stime = '" + sstime + "', " +
                "duration = '" + dhours + ":" + dminutes + "', " +
                "algorithm= '" + alogor + "', " +
                "match= '" + match11 + "', " +
                "allowedtypes= '" + allowedT + "', " +
                "description= '" + descrip + "', " +
                "state= '" + state + "', " +
                "modified = getdate(), " +
                "isactive= '" + iActive + "', " +
                "starttime= '" + startT + "', " +
                "endtime= '" + endTime + "', " +
                "tduration= '" + tduration + "', " +
                "matched= '" + prevMatch + "', " +
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
        public List<Boards> boa { get; set; }
        private void bindCombo()
        {
            demoEntities1 dE = new demoEntities1();
            var item = dE.Boards.ToList();
            boa = item;
            sboardid.ItemsSource = boa;
        }

        private void boardid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = sboardid.SelectedItem as Boards;
            cid = item.id.ToString();
        }
    }
}
