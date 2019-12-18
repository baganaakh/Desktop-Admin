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
    /// Interaction logic for members.xaml
    /// </summary>
    public partial class members : Page
    {
        public members()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id,statid,metype, partid;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            string typeeee = value.Row[3].ToString();
            string codepar = value.Row[1].ToString();
            string statepar = value.Row[9].ToString();

            pcode.Text = codepar;
            mtype.SelectedValue = typeeee;
            pstate.SelectedValue = statepar;
            
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string code = pcode.Text;
            string type = mtype.Text;
            string mask="";
            string h = "";
            if (starttime.SelectedDate == null || endtime.SelectedDate == null)
            {
                MessageBox.Show("Please Set Date !!!!!");
                return;
            }
            DateTime startT = starttime.SelectedDate.Value;
            DateTime endT= endtime.SelectedDate.Value;
            string startT2 = starttime.SelectedDate.Value.ToShortDateString();
            string endT2 = endtime.SelectedDate.Value.ToShortDateString();
            switch (metype)
            {
                case "0":
                    mask = mask + "70";
                    break;

                case "1":
                    mask = mask + "61";
                    break;

                case "2":
                    mask = mask + "70";
                    break;

                default:
                    MessageBox.Show("No Me Type match");
                    break;
            }
            mask = mask + code;
            string Broker = broker.IsChecked.ToString();
            string Dealer= dealer.IsChecked.ToString();
            string Ander= ander.IsChecked.ToString();
            string Nominal= nominal.IsChecked.ToString();
            if (broker.IsChecked ?? false)
            {
                MessageBox.Show(Broker);

                h = h + ", (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "h100')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "h200')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "h300')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "h400')";
            }
            if (dealer.IsChecked ?? false)
            {
                MessageBox.Show("dealer");

                h = h + ", (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "c100')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "c200')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "c300')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "c400')";
            }
            if (ander.IsChecked ?? false)
            {
                MessageBox.Show("Ander");

                h = h + ", (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "u100')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "u200')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "u300')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "u400')";
            }
            if (nominal.IsChecked ?? false)
            {
                MessageBox.Show("nominal");
                h = h + ", (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "o100')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "o200')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "o300')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "o400')";
            }
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.members (partid, startdate, enddate, type, code, state, broker, dealer, ander, nominal, modified) values" +
                " (" + partid + ",'"+startT2+"','"+endT2+"','" + metype + "',N'" + code + "','" + statid + "','" + Broker + "','" + Dealer + "','"
                + Ander + "','" + Nominal + "', getdate());";
            // + "insert into dbo.Account(memberid,modified,mask) values" +h;
            cmd.Parameters.AddWithValue("@date1", SqlDbType.Date).Value=startT;
            cmd.Parameters.AddWithValue("@date2", SqlDbType.Date).Value=endT;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);
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
                CmdString = "SELECT ALL [id], [code], [type], [state], [modified], [mask] " +
                    "FROM dbo.members ";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Participant");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            pcode.Text = null;
            mtype.Text = null;
            pstate.Text = null;
            id = null;
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.dbo.members WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        private void update(object sender, RoutedEventArgs e)
        {
            string code = pcode.Text;
            

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.members SET " +
                "code = '" + code + "', " +
                "type = '" + metype+ "', " +
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
        public List<mtype> mt { get; set; }
        public List<Participants> part { get; set; }
        private void bindCombo()
        {
            demoEntities10 st = new demoEntities10();
            var items = st.States.ToList();
            statt = items;
            pstate.ItemsSource = statt;

            var meitems = st.mtype.ToList();
            mt = meitems;
            mtype.ItemsSource = mt;

            var paitems = st.Participants.ToList();
            part = paitems;
            participants.ItemsSource = part;
        }

        private void participants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = participants.SelectedItem as Participants;
            try
            {
                partid = items.id.ToString();
            }
            catch
            {
                return;
            }
        }

        private void mtype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = mtype.SelectedItem as mtype;
            try
            {
                metype = items.id.ToString();
            }
            catch
            {
                return;
            }
        }

        private void sstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = pstate.SelectedItem as States;
            try
            {
                statid = items.id.ToString();
            }
            catch
            {
                return;
            }
        }
    }
}
