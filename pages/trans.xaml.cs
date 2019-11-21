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
namespace pages
{
    /// <summary>
    /// Interaction logic for trans.xaml
    /// </summary>
    public partial class trans : Page
    {
        public trans()
        {
            InitializeComponent();
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
            string secId = securityid_Copy.Text;
            string type = ctype.Text;
            string code = ccode.Text;
            string name = cname.Text;
            string lot = clot.Text;
            string tick = ctick.Text;
            string csdates = csdate.SelectedDate.Value.ToShortDateString();
            string cedates = cedate.SelectedDate.Value.ToShortDateString();
            string groupID = cgroupid.Text;
            string stat = cstate.Text;
            string mmorderLimit = mmorderLim.Text;
            string orderlimit = orderLim.Text;
            string refpricePar = refpricePara.Text;


            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);


            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.contracts (securityId, type, code, name, lot, tick, sdate, edate, groupId, state, modified, mmorderLimit, orderLimit, refpriceParam) values" +
                " ('" + secId + "','" + type + "','" + code + "','" + name + "', '" + lot + "', '" + tick + "', '" + csdates + "', '" + cedates + "', '" + groupID +
                "','" + stat + "', getdate(),'" + mmorderLimit + "','" + orderlimit + "','" + refpricePar + "')";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

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
    }
}
