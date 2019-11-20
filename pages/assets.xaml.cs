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
            string code = acode.Text;
            string name = aname.Text;
            string value = aprice.Text;
            string note = anote.Text;
            string expireDate = aexpire.SelectedDate.Value.ToShortDateString();
            string ratio = artio.Text;
            string state = astate.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);


            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.assets(code, name, value, note, expireDate, state, modified, ratio) values" +
                " ('" + code + "','" + name + "','" + value + "','" + note + "', '" + expireDate+ "', '" + state+ "', getdate(), '" + ratio+ "')";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

        }
    }
}
