using pages.dbBind;
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
using static pages.loginScreen;

namespace pages
{
    /// <summary>
    /// Interaction logic for orderOk.xaml
    /// </summary>
    public partial class orderOk : Page
    {
        public orderOk()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string accId2,id;
        #region fill
        private void FillDataGrid()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT ALL [id],[bid], [side], [memberid], [accountid], [assetid], [qty], [price], [state], [modified] " +
                            "FROM [demo].[dbo].[Order]";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Securities");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        #endregion
        #region okey
        private void Okey(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            if(accId2 == null)
            {
                MessageBox.Show("Id oruulna uu !!!!!!!!");
                return;
            }
            try
            {
                id = values.Row[0].ToString();
            }
            catch
            {
                MessageBox.Show("table ee songolno uuu !!!!");
                return;
            }
            string dealTypes = values.Row[1].ToString();
            string memId = values.Row[3].ToString();
            string assetId = values.Row[5].ToString();
            string accId = values.Row[4].ToString();
            string statid = values.Row[8].ToString();
            string Side = values.Row[2].ToString();
            string QTY = values.Row[6].ToString();
            string Price = values.Row[7].ToString();
            int qty = Int32.Parse(QTY);
            Decimal price = Decimal.Parse(Price);
            decimal total1, total2;
            int side1=0, side2=0;
            if (Side == "-1")
            {
                total1 = qty*price;
                total2 = qty*price*(-1);
                side1 = -1;
                side2 = 1;
            }
            else
            {
                total1 = qty*price * (-1);
                total2 = qty*price;
                side1 = 1;
                side2 = -1;
            }

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into [dbo].[Deals] (dealType, side, memberid, accountid, assetid" +
                ", qty, price, state, modified,dealno,totalPrice,boardid) values" +

                " ((SELECT [dealType] as dealType from [demo].[dbo].[Boards] where id=" + dealTypes + ")," + side1 + ",N'" + memId + "',N'" + accId + "',N'" +
                assetId + "',N'" + QTY + "',N'" + Price + "',N'" + statid + "', getdate()," +
                "IDENT_CURRENT('deals')+1," + total1 + ","+dealTypes+")," +

                "((SELECT [dealType] as dealType from [demo].[dbo].[Boards] where id=" + dealTypes + ")," + side2 + ",N'" +
                MyGlobals.U_ID + "',N'" + accId2 + "',N'" + assetId + "',N'" + QTY + "',N'" + Price 
                + "',N'" + statid + "', getdate(),IDENT_CURRENT('deals')+1," + total2 + "," + dealTypes + ")";


            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region combos
        public List<Account> ACCT { get; set; }
        private void bindCombo()
        {
            demoEntities10 ac = new demoEntities10();
            var act = ac.Accounts.ToList();
            ACCT = act;
            accountid.ItemsSource = ACCT;
        }

        private void DateTable2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            accountid.SelectedItem = null;
        }

        private void accountid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var accitems = accountid.SelectedItem as Account;
            try
            {
                accId2 = accitems.id.ToString();
            }
            catch
            {
                return;
            }
        }
        #endregion
    }
}
