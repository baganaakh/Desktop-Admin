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
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            MessageBox.Show("connection Open!");
            cnn.Close();
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string boardid = sboardid.Text;
            string name = sname.Text;
            string formatted = stime.SelectedDate.Value.ToShortDateString();
            string dhours = dhour.Text;
            string alogor = algo.Text;
            string match11 = match1.Text;
            string allowedT = allowT.Text;
            string iActive = iAct.Text;
            string descrip = sdesc.Text;
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
                " starttime, endtime, editorder, delorder, markettype) values" +
                " ('" + boardid + "','" + name + "','"+formatted+"','" + dhours + "', '" + alogor + "', '"+match11+ "', '"+allowedT+ "', '"+descrip+ "', '"+state+ 
                "', getdate(), '"+iActive+"', '"+ startT + "', '"+endTime+ "', '"+EDorder+ "', '"+DEorder+ "', '"+markType+"')";
            // cmd.Parameters.AddWithValue("@startTime", stime.SelectedDate);2019-11-20 12:19:51.950
            cmd.Parameters.AddWithValue("@modified", DateTime.Now);
            //checkDAta.Text = cmd.CommandText;

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

        }
        private void FillDataGrid()
        {
            string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
            string CmdString = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                CmdString = "SELECT TOP (1000) [id], [boardid], [name], [stime], [duration], [algorithm], [match], [allowedtypes], [description], [state] ,[modified], " +
                    "[isactive], [starttime], [endtime], [tduration], [matched], [editorder], [delorder], [markettype] FROM dbo.session ";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                // DataRowView dr = new DataRowView();
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
    }
}
