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
using System.Data;
namespace pages
{
    /// <summary>
    /// Interaction logic for margins.xaml
    /// </summary>
    public partial class margins : Page
    {
        public margins()
        {
            InitializeComponent();
            FillDataGrid();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            id = values.Row[0].ToString();
            string buy= values.Row[1].ToString();
            string sell= values.Row[2].ToString();
            string buytype= values.Row[3].ToString();
            string selltype= values.Row[4].ToString();
            string mBuy= values.Row[5].ToString();
            string mSell= values.Row[6].ToString();

            mbuy.Text=buy;
            msell.Text=sell;
            mbuyType.Text=buytype;
            mselltype.Text=selltype;
            mbuy_Copy.Text=mBuy;
            mmsell.Text=mSell;
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string buy = mbuy.Text;
            string sell = msell.Text;
            string buyType = mbuyType.Text;
            string sellType = mselltype.Text;
            string mmbuy = mbuy_Copy.Text;
            string mmSell = mmsell.Text;
            
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);


            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.margin(buy, sell, buyType, sellType, mbuy, msell, modified) values" +
                " ('" + buy+ "','" + sell+ "','" + buyType+ "','" + sellType+ "', '" + mmbuy+ "', '" + mmSell+ "', getdate())";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
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
        private void FillDataGrid()
        {
            string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
            string CmdString = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                CmdString = "SELECT ALL [id], [buy], [sell], [buytype], [selltype], [mbuy], [msell], [modified] "+
                            "FROM dbo.Margin";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Securities");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            mbuy.Text=null;
            msell.Text=null;
            mbuyType.Text=null;
            mselltype.Text=null;
            mbuy_Copy.Text=null;
            mmsell.Text=null;
            id = null;
        }
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.dbo.margin WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        private void update(object sender, RoutedEventArgs e)
        {
            string buy = mbuy.Text;
            string sell = msell.Text;
            string buyType = mbuyType.Text;
            string sellType = mselltype.Text;
            string mmbuy = mbuy_Copy.Text;
            string mmSell = mmsell.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.margin SET " +
                "buy= '" + buy+ "', " +
                "sell= '" + sell+ "', " +
                "buytype= '" + buyType+ "', " +
                "selltype= '" + sellType+ "', " +
                "mbuy= '" + mmbuy+ "', " +
                "msell= '" + mmSell + "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
    }
}
