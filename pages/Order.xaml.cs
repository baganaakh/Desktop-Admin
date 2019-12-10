using pages.dbBind;
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

namespace pages
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        public Order()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id, statid, memId, accId, dealTypes,assetId;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            id = values.Row[0].ToString();
            dealTypes = values.Row[1].ToString();
            memId = values.Row[3].ToString();
            assetId = values.Row[5].ToString();
            accId = values.Row[4].ToString();
            statid= values.Row[8].ToString();
            string Side = values.Row[2].ToString();
            string QTY= values.Row[6].ToString();
            string Price= values.Row[7].ToString();

            memid.SelectedValue = memId;
            accountid.SelectedValue = accId;
            stat.SelectedValue = statid;
            dealtype.SelectedValue = dealTypes;
            assetid.SelectedValue = accId;
            quantity.Text = QTY;
            price.Text = Price;
            side.Text = Side;
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string QTY = quantity.Text;
            string Price = price.Text;
            string Side = side.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into [demo].[dbo].[Order] (dealType, side, memberid, accountid, assetid, qty, price, state, modified) values" +
                " ('" +  dealTypes + "',N'" + Side+ "',N'" + memId+ "',N'" + accId + "',N'" + assetId+ "',N'" + QTY+ "',N'" + Price+ "',N'" + statid+ "', getdate())";

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
                CmdString = "SELECT ALL [id],[dealType], [side], [memberid], [accountid], [assetid], [qty], [price], [state], [modified] " +
                            "FROM [demo].[dbo].[Order]";
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
            memid.SelectedValue = null;
            accountid.SelectedValue = null;
            stat.SelectedValue = null;
            dealtype.SelectedValue = null;
            assetid.SelectedValue = null;
            quantity.Text = null;
            price.Text = null;
            side.Text = null;
        }
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
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
        private void update(object sender, RoutedEventArgs e)
        {
            string QTY = quantity.Text;
            string Price = price.Text;
            string Side = side.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.[dbo].[Order] SET " +
                "dealType= '" + dealTypes+ "', " +
                "side= '" + Side+ "', " +
                "memberid= '" + memId + "', " +
                "accountid= '" + accId+ "', " +
                "assetid= '" + assetId + "', " +
                "qty= '" + QTY+ "', " +
                "price= '" + Price+ "', " +
                "state= '" + statid + "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        public List<States> statt { get; set; }
        public List<Members> Emp { get; set; }
        public List<Dealtype> Dtype { get; set; }
        public List<Account> ACCT { get; set; }
        public List<Assets> ASST { get; set; }

        private void bindCombo()
        {
            demoEntities7 dc = new demoEntities7();
            var item = dc.Members.ToList();
            Emp = item;
            memid.ItemsSource = Emp;

            demoEntities7 st = new demoEntities7();
            var items = st.States.ToList();
            statt = items;
            stat.ItemsSource = statt;
            
            demoEntities7 dt= new demoEntities7();
            var dts = dt.Dealtype.ToList();
            Dtype = dts;
            dealtype.ItemsSource = Dtype;

            demoEntities7 ac = new demoEntities7();
            var act = ac.Account.ToList();
            ACCT = act;
            accountid.ItemsSource = ACCT;

            demoEntities7 ass = new demoEntities7();
            var asst = ass.Assets.ToList();
            ASST = asst;
            assetid.ItemsSource = ASST;
        }


        private void sstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = stat.SelectedItem as States;
            try
            {
                statid = items.id.ToString();
            }
            catch
            {
                return;
            }
        }
        private void partid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = memid.SelectedItem as Members;
            try
            {
                memId = item.id.ToString();
            }
            catch
            {
                return;
            }
        }
        private void dealtype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dti = dealtype.SelectedItem as Dealtype;
            try
            {
                dealTypes = dti.id.ToString();
            }
            catch
            {
                return;
            }
        }
        private void accountid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var accitems = accountid.SelectedItem as Account;
            try
            {
                accId = accitems.id.ToString();
            }
            catch
            {
                return;
            }
        }
        private void assetid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var assItems = assetid.SelectedItem as Assets;
            try
            {
                assetId = assItems.id.ToString();
            }
            catch
            {
                return;
            }
        }
        

    }
}
