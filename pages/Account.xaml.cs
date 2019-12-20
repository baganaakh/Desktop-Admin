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
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class account : Page
    {
        public account()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id, memId,stat,mask,cname;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            id = values.Row[0].ToString();
            string memId= values[1].ToString();
            string Trading = values[2].ToString();
            string Clearing = values[3].ToString();
            string Settlement = values[4].ToString();
            string Collateral = values[5].ToString();

            memid.SelectedValue = memId;
            trading.Text = Trading;
            clearing.Text= Clearing;
            settlement.Text = Settlement;
            collateral.Text = Collateral;
        }
        #region insert func
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string Trading = trading.Text;
            string Clearing = clearing.Text;
            string Settlement = settlement.Text;
            string Collateral = collateral.Text;
            string newMask = "";
            if (broker.IsChecked == true)
            {
                if (newMask != "") newMask += ",";
                newMask +=  "('" + memId + "', N'" + mask + "u1" + Trading + "', getdate()," + stat + ")," +
                            "('" + memId + "', N'" + mask + "u3" + Settlement+ "', getdate()," + stat + ")," +
                            "('" + memId + "', N'" + mask + "u4" + Collateral+ "', getdate()," + stat + ")," +
                            "('" + memId + "', N'" + mask + "u2" + Clearing+ "', getdate()," + stat + ")";
            }
            if (dealer.IsChecked == true)
            {
                if (newMask != "") newMask += ",";
                newMask +=  "('" + memId + "', N'" + mask + "c1" + Trading + "', getdate()," + stat + ")," +
                            "('" + memId + "', N'" + mask + "c3" + Settlement+ "', getdate()," + stat + ")," +
                            "('" + memId + "', N'" + mask + "c4" + Collateral+ "', getdate()," + stat + ")," +
                            "('" + memId + "', N'" + mask + "c2" + Clearing+ "', getdate()," + stat + ")";
            }
            if (ander.IsChecked == true)
            {
                if (newMask != "") newMask += ",";
                newMask +=  "('" + memId + "', N'" + mask + "u1" + Trading + "', getdate()," + stat + ")," +
                            "('" + memId + "', N'" + mask + "u3" + Settlement+ "', getdate()," + stat + ")," +
                            "('" + memId + "', N'" + mask + "u4" + Collateral+ "', getdate()," + stat + ")," +
                            "('" + memId + "', N'" + mask + "u2" + Clearing+ "', getdate()," + stat + ")";
            }
            if (nominal.IsChecked == true)
            {
                if (newMask != "") newMask += ",";
                newMask +=  "('" + memId + "', N'" + mask + "o1" + Trading + "', getdate()," + stat + ")," +
                            "('" + memId + "', N'" + mask + "o3" + Settlement+ "', getdate()," + stat + ")," +
                            "('" + memId + "', N'" + mask + "o4" + Collateral+ "', getdate()," + stat + ")," +
                            "('" + memId + "', N'" + mask + "o2" + Clearing+"', getdate(),"+ stat +")";
            }
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            #region main insert query 
            cmd.CommandText = "insert into dbo.Account (memberid, mask, modified,state) values " +
                newMask;
            
            #endregion
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
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
                CmdString = "SELECT ALL [id], [memberid], [trading], [clearing], [settlement], [collateral], [modified], [mask] " +
                            "FROM dbo.Account";
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
            memid.Text = null;
            trading.Text = null;
            clearing.Text = null;
            settlement.Text = null;
            collateral.Text = null;
            id = null;
            memId= null;
        }
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.dbo.Account WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        private void update(object sender, RoutedEventArgs e)
        {
            string Trading = trading.Text;
            string Clearing = clearing.Text;
            string Settlement = settlement.Text;
            string Collateral = collateral.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.Account SET " +
                "memberid= '" + memId + "', " +
                "trading= '" + Trading+ "', " +
                "clearing= '" + Clearing + "', " +
                "settlement= '" + Settlement+ "', " +
                "collateral= '" + Collateral+ "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        public List<Members> Emp { get; set; }
        public List<States> boa{ get; set; }

        private void bindCombo()
        {
            demoEntities10 dc = new demoEntities10();
            var mitem = dc.Members.ToList();
            Emp = mitem;
            memid.ItemsSource = Emp;

            var item = dc.States.ToList();
            boa = item;
            pstate.ItemsSource = boa;
        }

        private void pstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = pstate.SelectedItem as States;

            try
            {
                stat = item.id.ToString();
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
                mask = item.mask.ToString();
                cname = item.name.ToString();
                companyName.Content = cname;
            }
            catch
            {
                return;
            }

        }
    }
}
