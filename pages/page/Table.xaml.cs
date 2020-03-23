using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Admin
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : Page
    {
        public Table()
        {
            InitializeComponent();
            FillDataGrid();
        }
        string connectionString = Properties.Settings.Default.ConnectionString;
        static string id;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as DataRowView;
            if (null == values) return;
            id = values.Row[0].ToString();
            string Name = values.Row[6].ToString();
            name.Text = Name;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string Name = name.Text;
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into [dbo].[Ttable] (name) values(N'"+Name+"')";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region numbers
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        #endregion
        #region fill
        private void FillDataGrid()
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT * FROM [dbo].[Ttable]";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Ttable");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        #endregion
        #region new
        private void newData(object sender, RoutedEventArgs e)
        {
            name.Text = null;
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
            cmd.CommandText = "DELETE demo.dbo.Ttable WHERE id='" + id + "'";
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
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE [demo].[dbo].[Ttable] SET " +
                "name= N'" + name.Text+ "' " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
    }
}
