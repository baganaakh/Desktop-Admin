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
    /// Interaction logic for contracts.xaml
    /// </summary>
    public partial class contracts : Page
    {
        public contracts()
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
            string partid = partId.Text;
            string type = stype.Text;
            string code = scode.Text;
            string name = sname.Text;
            string refno = refNo.Text;
            string regno = regNo.Text;
            string total = totalquant.Text;
            string fPirce = fprice.Text;
            string intRate = srate.Text;
            string stat = state.Text;
            string starTime = sdate.SelectedDate.Value.ToShortDateString();
            string endTime = edate.SelectedDate.Value.ToShortDateString();
            string groupId = groupid.Text;


            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);


            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.securities (partid, type, code, name, refno, regno, totalQty, firstPrice, intRate, sdate, edate, groupid, state, modified) values" +
                " ('" + partid + "','" + type + "','" + code + "','" + name + "', '" + refno + "', '" + regno + "', '" + total + "', '" + fPirce + "', '" + intRate +
                "','" + starTime + "','" + endTime + "','" + groupId + "','" + stat + "', getdate())";

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
