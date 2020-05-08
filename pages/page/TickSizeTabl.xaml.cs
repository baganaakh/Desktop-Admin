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
using Admin.dbBind;
using System.Data.Entity;

namespace Admin
{
    /// <summary>
    /// Interaction logic for TickSizeTable.xaml
    /// </summary>
    public partial class TickSizeTabl : Page
    {
        public TickSizeTabl()
        {
            InitializeComponent();
            FillDataGrid();
        }
        
        static string id,statid;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            if (null == values) return;
            id = values.Row[0].ToString();
            string Tick= values.Row[2].ToString();

            upd.IsEnabled = true;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            using(demoEntities10 contx=new demoEntities10())
            {
                TickSizeTable tit = new TickSizeTable
                {
                    name=namee.Text,
                };
                contx.TickSizeTables.Add(tit);
                contx.SaveChanges();
            }
           // if (statid == null || tid == null)
           //     return;
           // upd.IsEnabled = true;
           // string tick= tickk.Text;
           // string price = pricee.Text;
          
           // System.Data.SqlClient.SqlConnection sqlConnection1 =
           //new System.Data.SqlClient.SqlConnection(connectionString);

           // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
           // cmd.CommandType = System.Data.CommandType.Text;
           // cmd.CommandText = "insert into dbo.TickSizeTable (tableid, tick, price, state, name) values" +
           //     " ('" + tid+ "',N'" + tick+ "',N'" + price+ "',N'" + statid+ "', N'"+ name+"')";

           // cmd.Connection = sqlConnection1;
           // sqlConnection1.Open();
           // cmd.ExecuteNonQuery();
           // sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region numbers
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
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.TickSizeTables.ToList();


            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //     string CmdString = "SELECT * FROM dbo.TickSizeTable";
            //    SqlCommand cmd = new SqlCommand(CmdString, conn);
            //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable("Securities");
            //    sda.Fill(dt);
            //    DateTable2.ItemsSource = dt.DefaultView;
            //}
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        #endregion
        #region new
        private void newData(object sender, RoutedEventArgs e)
        {
            id = null;
            statid = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            long iiid = (DateTable2.SelectedItem as TickSizeTable).id;
            demoEntities10 de = new demoEntities10();
            TickSizeTable del = de.TickSizeTables.Find(iiid);
            de.Entry(del).State = EntityState.Deleted;
            de.SaveChanges();
            // var value = DateTable2.SelectedItem as DataRowView;
            // if (null == value) return;
            // id = value.Row[0].ToString();
            // System.Data.SqlClient.SqlConnection sqlConnection1 =
            //new System.Data.SqlClient.SqlConnection(connectionString);

            // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            // cmd.CommandType = System.Data.CommandType.Text;
            // cmd.CommandText = "DELETE demo.dbo.TickSizeTable WHERE id='" + id + "'";
            // cmd.Connection = sqlConnection1;
            // sqlConnection1.Open();
            // cmd.ExecuteNonQuery();
            // sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
           // System.Data.SqlClient.SqlConnection sqlConnection1 =
           //new System.Data.SqlClient.SqlConnection(connectionString);

           // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
           // cmd.CommandType = System.Data.CommandType.Text;
           // cmd.CommandText = "UPDATE demo.dbo.TickSizeTable SET " +
           //     "state= '" + statid + "', " +
           //     "WHERE id = '" + id + "'";

           // cmd.Connection = sqlConnection1;
           // sqlConnection1.Open();
           // cmd.ExecuteNonQuery();
           // sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
    }
}
