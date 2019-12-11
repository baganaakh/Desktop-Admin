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
    /// Interaction logic for price.xaml
    /// </summary>
    public partial class fee : Page
    {
        readonly string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";

        public fee()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT ALL [id], [tradBuyC], [tradBuyH], [tradSellC], [tradSellH], [clearBuyC], [clearBuyH], [clearSellC]" +
          ",[clearSellH], [settBuyC], [settBuyH], [settSellC], [settSellH], [posBuyC], [posBuyH], [posSellC], [posSellH], [brokBuyC], [brokBuyH], [brokSellC],[brokSellH], [lossBuyC] " +
          ",[lossBuyH], [lossSellC], [lossSellH], [repoBuyC], [repoBuyH], [repoSellC], [repoSellH], [negoBuyC], [negoBuyH], [negoSellC], [negoSellH], [otcBuyC], [otcBuyH], " +
          "[otcSellC],[otcSellH], [other] FROM [demo].[dbo].[fee]";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Securities");
                sda.Fill(dt);

            }
        }
        private void update(object sender, RoutedEventArgs e)
        {
            string Trbuyc = tradbuyc.Text;
            string Clbuyc = clearbuyc.Text;
            string Setbuyc = settbuyc.Text;
            string Pobuyc = posbuyc.Text;
            string Brobuyc = brokbuyc.Text;
            string Losbuyc = lossbuyc.Text;
            string Repbuyc = repobuyc.Text;
            string Negobuyc = negobuyc.Text;
            string Otcbuyc = otcbuyc.Text;

            string Trbuyh = tradbuyh.Text;
            string Clbuyh = clearbuyh.Text;
            string Setbuyh = settbuyh.Text;
            string Posbuyh = posbuyh.Text;
            string Brobuyh = brokbuyh.Text;
            string Losbuyh = lossbuyh.Text;
            string Repbuyh = repobuyh.Text;
            string Negbuyh = negobuyh.Text;
            string Otc = otcbuyh.Text;

            string Trsellc = tradsellc.Text;
            string Clsellc = clearsellc.Text;
            string Sesellc = settsellc.Text;
            string Posellc = possellc.Text;
            string Brsellc = broksellc.Text;
            string Losellc = lossselc.Text;
            string Resellc = reposellc.Text;
            string Nesellc = negosellc.Text;
            string Otcsellc = otcsellc.Text;

            string Trsellh = tradsellh.Text;
            string Clsellh = clearsellh.Text;
            string Sesellh = settsellh.Text;
            string Posellh = possellh.Text;
            string Brsellh = broksellh.Text;
            string Losellh = losssellh.Text;
            string Resellh = reposellh.Text;
            string Nesellh = negosellh.Text;
            string Otsellh = otcsellh.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.[dbo].[fee] SET" +
                " [tradBuyC]=" + Trbuyc + ", [tradBuyH]=" + Trbuyh + ", [tradSellC]=" + Trsellc + ", [tradSellH]=" + Trsellh + ", [clearBuyC]=" + Clbuyc + ", [clearBuyH]=" + Clbuyh + "" +
                ", [clearSellC]=" + Clsellc + ",[clearSellH]=" + Clsellh + ", [settBuyC]=" + Setbuyc + ", [settBuyH]=" + Setbuyh + ", [settSellC]=" + Sesellc + ", [settSellH]=" + Sesellh + "" +
                ", [posBuyC]=" + Pobuyc + ", [posBuyH]=" + Posbuyh + ", [posSellC]=" + Posellc + ", [posSellH]=" + Posellh + ", [brokBuyC]=" + Brobuyc + ", [brokBuyH]=" + Brobuyh + "" +
                ", [brokSellC]=" + Brsellc + ",[brokSellH]=" + Brsellh + ", [lossBuyC]=" + Losbuyc + ",[lossBuyH]=" + Losbuyh + ", [lossSellC]=" + Losellc + ", [lossSellH]=" + Losellh + "" +
                ", [repoBuyC]=" + Repbuyc + ", [repoBuyH]=" + Repbuyh + ", [repoSellC]=" + Resellc + ", [repoSellH]=" + Resellh + ", [negoBuyC]=" + Negobuyc + ", [negoBuyH]=" + Negbuyh + "" +
                ", [negoSellC]=" + Nesellc + ", [negoSellH]=" + Nesellh + ", [otcBuyC]=" + Otcbuyc + ", [otcBuyH]=" + Otcbuyc + ", " + "[otcSellC]=" + Otcsellc + "" +
                ",[otcSellH]=" + Otsellh + "  WHERE id= 1";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
    }
}
