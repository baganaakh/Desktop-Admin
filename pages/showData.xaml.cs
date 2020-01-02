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
using System.Configuration;
using System.Data;
using System.Data.Entity;
using pages.dbBind;

namespace pages
{
    /// <summary>
    /// Interaction logic for showData.xaml
    /// </summary>
    public partial class showData : Page
    {
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";

        #region fill
        private void FillDataGrid()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT * FROM dbo.deals";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                DateTable1.ItemsSource = dt.DefaultView;
            }
        }
        #endregion


    }

}
