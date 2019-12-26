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
using pages.dbBind;

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
            bindcombo();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id, coId;
        #region edit
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
            coId = values.Row[8].ToString();

            mbuy.Text=buy;
            msell.Text=sell;
            mbuyType.Text=buytype;
            mselltype.Text=selltype;
            mbuy_Copy.Text=mBuy;
            mmsell.Text=mSell;
            contractid.SelectedValue = coId;
        }
        #endregion
        #region insert
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
            cmd.CommandText = "insert into dbo.margins (buy, sell, buyType, sellType, mbuy, msell, modified, coid) values" +
                " ('" + buy+ "',N'" + sell+ "',N'" + buyType+ "',N'" + sellType+ "', '" + mmbuy+ "', '" + mmSell+ "', getdate(), "+coId+")";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region number
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        #endregion
        #region fill
        private void FillDataGrid()
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT ALL [id], [buy], [sell], [buytype], [selltype], [mbuy], [msell], [modified], [coid] "+
                            "FROM [dbo].[Margins]";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Securities");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        #endregion
        #region ref new
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
            contractid.SelectedItem = null;
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
            cmd.CommandText = "DELETE demo.dbo.margins WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region combos
        public List<Contract> Cont { get; set; }

        private void bindcombo()
        {
            demoEntities10 ct = new demoEntities10();
            var citem = ct.Contracts.ToList();
            Cont = citem;
            contractid.ItemsSource = Cont;
        }
        private void contractid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var citem = contractid.SelectedItem as Contract;
            try
            {
                coId = citem.id.ToString();
            }
            catch
            {
                return;
            }
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
            cmd.CommandText = "UPDATE demo.dbo.margins SET " +
                "buy= '" + buy+ "', " +
                "sell= '" + sell+ "', " +
                "buytype= '" + buyType+ "', " +
                "selltype= '" + sellType+ "', " +
                "mbuy= '" + mmbuy+ "', " +
                "msell= '" + mmSell + "', " +
                "coid= '" + coId+ "', " +
                "modified = getdate() " +
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
