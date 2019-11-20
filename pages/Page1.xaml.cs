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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
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
            string boardName = bname.Text;
            string type = btype.Text;
            string tdays = tdayss.Text;
            string descr = desc.Text;
            string stat = state.Text;
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);


            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.boards (name, type, tdays, description, state, modified) values" +
                " ('" + boardName + "','" + type + "','" + tdays + "', '" + descr + "', '" + stat + "',@modified)";

            cmd.Parameters.AddWithValue("@modified", DateTime.Now);

            checkDAta.Content = cmd.CommandText;

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

        }

    }
}
