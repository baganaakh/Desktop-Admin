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

namespace Admin
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
        string connectionString = Properties.Settings.Default.ConnectionString;
        string id;
        #region fill
        private void FillDataGrid()
        {
            //string connectionString = Properties.Settings.Default.ConnectionString;
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    string CmdString = "SELECT * FROM [dbo].[Deal2]";
            //    SqlCommand cmd = new SqlCommand(CmdString, conn);
            //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable("Securities");
            //    sda.Fill(dt);
            //    DateTable2.ItemsSource = dt.DefaultView;
            //}
        }
        #endregion
        #region deal2 to invoice 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 =
         new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into demo.dbo.Invoices (boardid, dealno, side, accountid, assetid," +
                " dealType, qty, totalPrice, state, fee, expiredate, expiretime) select a.boardid, a.dealno, " +
                " a.side, a.accountid, a.assetid, a.dealType, a.qty, a.totalPrice, a.state, a.fee, " +
                "dateadd(day, b.expDate, cast(GETDATE() as date)), b.expTime from demo.dbo.Deal2 a " +
                "left outer join demo.dbo.Boards b on a.boardid = b.id where a.invoice = 0 " +
                "update demo.dbo.Deal2 set invoice = 1 where invoice = 0; ";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
        }
        #endregion
    }
}
