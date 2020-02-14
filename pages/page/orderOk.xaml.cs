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
        string connectionString = Properties.Settings.Default.ConnectionString;
        static string accId2,id;
        #region fill
        private void FillDataGrid()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT * FROM [demo].[dbo].[Order]";
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
            string boardid = values.Row[1].ToString();
            string Side = values.Row[2].ToString();
            string memId = values.Row[3].ToString();
            string accId = values.Row[4].ToString();
            string assetId = values.Row[5].ToString();
            string QTY = values.Row[6].ToString();
            string Price = values.Row[7].ToString();
            string statid = values.Row[8].ToString();
            int qty = Int32.Parse(QTY);
            Decimal price = Decimal.Parse(Price);
            decimal total1, total2;
            int side1=0, side2=0, s1qty=0, s2qty=0;
            if (Side == "1")
            {
                total1 = qty*price;
                total2 = qty*price*(-1);
                side1 = -1;
                side2 = 1;
                s2qty = qty * (-1);
                s1qty = qty;
            }
            else
            {
                total1 = qty*price * (-1);
                total2 = qty*price;
                side1 = 1;
                side2 = -1;
                s1qty = qty * (-1);
                s2qty = qty;
            }

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into [dbo].[Deals] (dealType, side, memberid, accountid, assetid" +
                ", qty, price, state, modified,dealno,totalPrice,boardid) values" +

                " ((SELECT [dealType] as dealType from [demo].[dbo].[Boards] where id=" + boardid + ")," + side1 + ",N'" +
                memId + "',N'" + accId + "',N'" +assetId + "',N'" + s1qty + "',N'" + Price + "',N'" + statid + "', getdate()," +
                "IDENT_CURRENT('deals')+1," + total1 + ","+boardid+")," +

                "((SELECT [dealType] as dealType from [demo].[dbo].[Boards] where id=" + boardid + ")," + side2 + ",N'" +
                MyGlobals.U_ID + "',N'" + accId2 + "',N'" + assetId + "',N'" + s2qty + "',N'" + Price 
                + "',N'" + statid + "', getdate(),IDENT_CURRENT('deals')+1," + total2 + "," + boardid + "); " +
                "DELETE demo.[dbo].[Order] WHERE id= " + id + " ";

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
