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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class boards : Page
    {
        public boards()
        {
            InitializeComponent();
            FillDataGrid();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            var value = DateTable1.SelectedItem as DataRowView;
            if (null == value) return;
            string id = value.Row[0].ToString();
            string name = value.Row[1].ToString();
            string type = value.Row[2].ToString();
            string tdays = value.Row[3].ToString();
            string description = value.Row[4].ToString();
            string stat= value.Row[5].ToString();

            bname.Text= name;
            btype.Text = type;
            tdayss.Text = tdays;
            desc.Text = description;
            state.Text = stat;
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
                CmdString = "SELECT TOP (1000) [id],[name],[type],[tdays],[description],[state],[modified] FROM dbo.boards ";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
               // DataRowView dr = new DataRowView();
                sda.Fill(dt);
                DateTable1.ItemsSource = dt.DefaultView;
            }
        }

    }
}
