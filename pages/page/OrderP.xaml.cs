using Admin.dbBind;
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
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class OrderP : Page
    {
        public OrderP()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = Properties.Settings.Default.ConnectionString;
        static string id, statid, memId, accId, bid,assetId, sides, dealtype;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as DataRowView;
            if (null == values) return;
            id = values.Row[0].ToString();
            bid = values.Row[1].ToString();
            memId = values.Row[3].ToString();
            assetId = values.Row[5].ToString();
            accId = values.Row[4].ToString();
            statid= values.Row[8].ToString();
            sides= values.Row[2].ToString();
            string QTY= values.Row[6].ToString();
            string Price= values.Row[7].ToString();
            memid.SelectedValue = memId;
            accountid.SelectedValue = accId;
            stat.SelectedValue = statid;
            boardid.SelectedValue = bid;
            assetid.SelectedValue = accId;
            quantity.Text = QTY;
            price.Text = Price;
            Side.SelectedValue= sides;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            sides = Side.SelectedIndex.ToString();
            if (sides == "0")
                sides = "-1";

            using(demoEntities10 contx=new demoEntities10())
            {
                Order ord = new Order
                {
                    side=Convert.ToInt16(sides),
                    memberid=Convert.ToInt64(memid.SelectedValue),
                    accountid=Convert.ToInt64(accountid.SelectedValue),
                    boardId=Convert.ToInt64(boardid.SelectedValue),
                    state=Convert.ToInt16(stat.SelectedIndex-1),
                    assetid=Convert.ToInt32(assetid.SelectedValue),
                    qty=Convert.ToInt32(quantity.Text),
                    price=Convert.ToDecimal(price.Text),
                };
                contx.Orders.Add(ord);
                contx.SaveChanges();
            }
           // string QTY = quantity.Text;
           // string Price = price.Text;
           // System.Data.SqlClient.SqlConnection sqlConnection1 =
           //new System.Data.SqlClient.SqlConnection(connectionString);

           // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
           // cmd.CommandType = System.Data.CommandType.Text;
           // cmd.CommandText = "insert into [demo].[dbo].[Order] (bid, side, memberid, accountid, assetid, qty, price, state, modified,dealtype) values" +
           //     " ('" +  bid + "',N'" + sides+ "',N'" + memId+ "',N'" + accId + "',N'" + assetId+ "',N'" + QTY+ "',N'" + Price+ "',N'" + statid+ "', getdate(), "+dealtype+")";
           // cmd.Connection = sqlConnection1;
           // sqlConnection1.Open();
           // cmd.ExecuteNonQuery();
           // sqlConnection1.Close();
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
                string CmdString = "SELECT * FROM [demo].[dbo].[Order]";
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
            memid.SelectedValue = null;
            accountid.SelectedValue = null;
            stat.SelectedValue = null;
            boardid.SelectedValue = null;
            assetid.SelectedValue = null;
            quantity.Text = null;
            price.Text = null;
            Side.SelectedValue = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.[dbo].[Order] WHERE id='" + id + "'";
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
            string QTY = quantity.Text;
            string Price = price.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.[dbo].[Order] SET " +
                "bid= '" + bid+ "', " +
                "side= '" + sides+ "', " +
                "memberid= '" + memId + "', " +
                "accountid= '" + accId+ "', " +
                "assetid= '" + assetId + "', " +
                "qty= '" + QTY+ "', " +
                "price= '" + Price+ "', " +
                "state= '" + statid + "', " +
                "dealType= '" + dealtype+ "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region combos
        public List<State> statt { get; set; }
        public List<Member> Emp { get; set; }
        public List<Board> Dtype { get; set; }
        public List<Account> ACCT { get; set; }
        public List<Asset> ASST { get; set; }

        private void bindCombo()
        {
            demoEntities10 dc = new demoEntities10();
            var item = dc.Members.ToList();
            Emp = item;
            memid.ItemsSource = Emp;

            var dts = dc.Boards.ToList();
            Dtype = dts;
            boardid.ItemsSource = Dtype;

            var act = dc.Accounts.ToList();
            ACCT = act;
            accountid.ItemsSource = ACCT;

            var asst = dc.Assets.ToList();
            ASST = asst;
            assetid.ItemsSource = ASST;
        }
        #endregion
    }
}
