using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace pages
{
    /// <summary>
    /// Interaction logic for deal2.xaml
    /// </summary>
    public partial class deal2 : Page
    {
        public deal2()
        {
            InitializeComponent();
            FillDataGrid();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        string id;
        #region fill
        private void FillDataGrid()
        {
            string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT * FROM [dbo].[Deal2]";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Securities");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        #endregion
        #region deal to invoice 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            id = values.Row[0].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "insert into dbo.Deal2 (accountid, assetid, price, dealType)" +
                    "SELECT [accountid], [assetid], SUM([totalPrice]) as price, dealType FROM [demo].[dbo].[Deals]" +
                        "where cast(modified as date) =cast(GETDATE() as date) and dealType =  " +
                                "group by accountid, dealType, assetid ";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
            }
        }
        #endregion
    }
}
