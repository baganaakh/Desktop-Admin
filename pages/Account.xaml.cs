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
        string connectionString = Properties.Settings.Default.ConnectionString;
        static string id, memId,stat,mnominal,ander,dealer,broker,mask, acType, linkA="NULL",values,mtypee;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as DataRowView;
            if (null == values) return;
            id = values.Row[0].ToString();
            string memId= values[1].ToString();
            string accNum = values[2].ToString();
            string accType = values[3].ToString();
            string linkAcc = values[4].ToString();
            string stat= values[10].ToString();
        
            memid.SelectedValue = memId;
            accno.Text = accNum;
            acctype.SelectedValue = accType;
            pstate.SelectedValue = stat;
        }
        #endregion
        #region insert func
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string accNo = accno.Text;
            if (stat == null) {
                MessageBox.Show("Please select state !!!");
                return;
            }
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.Account (memberid, accNum, accType, LinkAcc, state) values " +
                "("+memId+", "+accNo+", "+acType+", "+linkA+", "+stat+")";
            
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException EX)
            {
                if(EX.Number == 2627)
                {
                    MessageBox.Show("Account number " + accNo + " is already inserted try different number");
                    return;
                }
                else
                throw;
            }
            catch(Exception)
            {
                throw;
            }
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
        #region filldatas
        private void FillDataGrid()
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT * FROM dbo.Account";
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
            memid.Text = null;
            id = null;
            memId= null;
            pstate.SelectedItem = null;
            linkacc.SelectedItem = null;
            acctype.SelectedItem = null;
            accno.Text = null;
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
            cmd.CommandText = "DELETE demo.dbo.Account WHERE id='" + id + "'";
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
            string accNo = accno.Text;
            try
            {
                linkA = linkacc.SelectedValue.ToString();
            }
            catch (NullReferenceException)
            {
                MessageBoxResult res = MessageBox.Show("link acc is Empty is it okey ? ", "Empty",MessageBoxButton.OK);
                switch (res)
                {
                    case MessageBoxResult.OK:
                        linkA = "NULL";
                        break;
                }
            }

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.Account SET " +
                "memberid= '" + memId + "', " +
                "accNum= '" + accNo + "', " +
                "accType= '" + acType + "', " +
                "LinkAcc= '" + linkA+ "', " +
                "state= '" + stat+ "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region combos control
        public List<Member> Emp { get; set; }
        public List<accType> acct { get; set; }
        public List<State> boa{ get; set; }
        private void bindCombo()
        {
            demoEntities10 dc = new demoEntities10();
            var mitem = dc.Members.ToList();
            Emp = mitem;
            memid.ItemsSource = Emp;

            var item = dc.States.ToList();
            boa = item;
            pstate.ItemsSource = boa;
            
            var accitem = dc.accTypes.ToList();
            acct = accitem;
            acctype.ItemsSource = acct;
        }
        private void acctype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            linkacc.SelectedItem = null;
            linkacc.ItemsSource = null;
            var item = acctype.SelectedItem as accType;
            try
            {
                if (item == null)
                    return;
                acType = item.id.ToString();
                if(acType == "0")  //Төлбөр
                {
                    linkacc.IsEnabled = false;
                }
                else if(acType == "1")  //Барьцаа
                {
                    acType = "0";
                    linkacc.IsEnabled = true;
                }
                else if(acType == "2")  //Клиринг
                {
                    acType = "1";
                    linkacc.IsEnabled = true;
                }
                else if(acType == "3")  //Арилжаа
                {
                    acType = "2";
                    linkacc.IsEnabled = true;
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string CmdString = "SELECT [id],[accNum] FROM [demo].[dbo].[Account] WHERE accType = " +acType;
                    SqlCommand cmd = new SqlCommand(CmdString, conn);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("khfjkh");
                    sda.Fill(dt);
                    DataView view = dt.DefaultView;
                    linkacc.ItemsSource = view;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void pstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = pstate.SelectedItem as State;

            try
            {
                stat = item.id.ToString();
            }
            catch
            {
                return;
            }
        }
        private void memid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            demoEntities10 dc = new demoEntities10();
            var accitem = dc.accTypes.ToList();
            var accitem2 = dc.accTypes.ToList();
            accitem2.RemoveAt(3);
            var item = memid.SelectedItem as Member;
            try
            {
                mtypee = item.type.ToString();
                memId = item.id.ToString();
                mask = item.mask.ToString();

                mnominal= item.nominal.ToString();
                ander = item.ander.ToString();
                dealer = item.dealer.ToString();
                broker = item.broker.ToString();

                companyName.Content = mask;
                if(mtypee == "0")
                {
                    acctype.ItemsSource = accitem;
                    acctype.SelectedValue = 3;
                    acctype.IsEnabled = false;
                    linkacc.IsEnabled = false;
                }
                else if(mtypee == "1" && (broker == "False" && dealer == "False" && ander == "False"))
                {
                    acctype.ItemsSource = accitem2;
                    linkacc.IsEnabled = true;
                    acctype.IsEnabled = true;
                }
                else if(mtypee == "1" && (broker == "True" || dealer == "True" || ander == "True"))
                {
                    acctype.ItemsSource = accitem;
                    acctype.SelectedItem = null;
                    linkacc.IsEnabled = true;
                    acctype.IsEnabled = true;
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
                    return;
            }
            
        }
        #endregion
    }
}
