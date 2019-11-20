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
    /// Interaction logic for participants.xaml
    /// </summary>
    public partial class participants : Page
    {
        public participants()
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
            string code = pcode.Text;
            string name = pname.Text;
            string country = pcountry.Text;
            string address = paddress.Text;
            string phone = pphone.Text;
            string fax = pfax.Text;
            string email = pmail.Text;
            string contact = pcontact.Text;
            string state = pstate.Text;
            string csid = pcsid.Text;
            string webid = pwebid.Text;
           
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);


            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.participants (code, name, country, address, phone, fax, email, contact, state, modified, csid, webid) values" +
                " ('" + code + "','" + name + "','" + country + "','" + address+ "', '" + phone+ "', '" + fax+ "', '" + email+ "', '" + contact+ "', '" + state +
                "', getdate(), '"+csid+ "', '"+webid+"')";
            checkDAta.Text = cmd.CommandText;

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
