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
        private void FillDataGrid()
        {
            string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT ALL [id],[dealType], [side], [memberid], [accountid], [assetid], [qty], [price], [state], [modified] " +
                            "FROM [demo].[dbo].[Order]";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Securities");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        public List<Account> ACCT { get; set; }

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


            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into [dbo].[Deal2] (dealType, side, memberid, accountid, assetid, qty, price, state, modified,account2id,member2id) values" +
                " ('" + dealTypes + "',N'" + Side + "',N'" + memId + "',N'" + accId + "',N'" + assetId + "',N'" + QTY + "',N'" + Price + "',N'" + statid + "', getdate()," + accId2 + ",1)";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }

        private void bindCombo()
        {
            demoEntities7 ac = new demoEntities7();
            var act = ac.Account.ToList();
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
    }
}
