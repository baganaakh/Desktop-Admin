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

namespace Admin
{
    /// <summary>
    /// Interaction logic for contracts.xaml
    /// </summary>
    public partial class contracts : Page
    {
        public contracts()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = Properties.Settings.Default.ConnectionString;
        static string id, cid, statid, ctypee,boaID;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as DataRowView;
            if (null == values) return;
            id = values.Row[0].ToString();
            string SID= values.Row[1].ToString();
            string Type= values.Row[2].ToString();
            string Code= values.Row[3].ToString();
            string Name= values.Row[4].ToString();
            string Lot= values.Row[5].ToString();
            string Tick= values.Row[6].ToString();
            string Sdate= values.Row[7].ToString();
            string Edate= values.Row[8].ToString();
            string GID= values.Row[9].ToString();
            string State= values.Row[10].ToString();
            string MMol= values.Row[12].ToString();
            string Olimit= values.Row[13].ToString();
            string refprPAram= values.Row[14].ToString();
            string bId= values.Row[15].ToString();

            securityid_Copy.SelectedValue= SID;
            ctype.SelectedValue = Type;
            ccode.Text = Code;
            cname.Text = Name;
            clot.Text = Lot;
            ctick.Text = Tick;
            csdate.SelectedDate = DateTime.Parse(Sdate);
            cedate.SelectedDate = DateTime.Parse(Edate);
            cgroupid.Text = GID;
            cstate.SelectedValue = State;
            mmorderLim.Text = MMol;
            orderLim.Text = Olimit;
            refpricePara.Text = refprPAram;
            boardid.SelectedValue = bId;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (csdate.SelectedDate == null || cedate.SelectedDate == null)
            {
                MessageBox.Show("Please Set Date !!!!!");
                return;
            }
            using(demoEntities10 contx=new demoEntities10())
            {
                Contract con = new Contract
                {
                    securityId=Convert.ToInt64( securityid_Copy.SelectedValue),
                    type=
                };
                contx.Contracts.Add(con);
                contx.SaveChanges();
            }
           // string secId = cid;
           // string code = ccode.Text;
           // string name = cname.Text;
           // string lot = clot.Text;
           // string tick = ctick.Text;
           // string csdates = csdate.SelectedDate.Value.ToShortDateString();
           // string cedates = cedate.SelectedDate.Value.ToShortDateString();
           // string groupID = cgroupid.Text;
           // string mmorderLimit = mmorderLim.Text;
           // string orderlimit = orderLim.Text;
           // string refpricePar = refpricePara.Text;

           // System.Data.SqlClient.SqlConnection sqlConnection1 =
           //new System.Data.SqlClient.SqlConnection(connectionString);

           // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
           // cmd.CommandType = System.Data.CommandType.Text;
           // cmd.CommandText = "insert into dbo.contracts (securityId, type, code, name, lot, tick, sdate, edate, groupId, state, modified, mmorderLimit, orderLimit, refpriceParam,bid) values" +
           //     " ('" + secId+ "',N'" + ctypee + "',N'" + code + "',N'" + name + "', '" + lot+ "', '" + tick+ "', '" + csdates+ "', '" + cedates+ "', '" + groupID +
           //     "',N'" + statid+ "', getdate(),N'" + mmorderLimit+ "',N'" + orderlimit+ "',N'" + refpricePar+ "',"+boaID+")";

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
                string CmdString = "SELECT ALL [id], [securityId], [type], [code], [name], [lot], [tick], [sdate], [edate], [groupId], [state], [modified], " +
                    "[mmorderLimit], [orderLimit], [refpriceParam], [bid] FROM dbo.contracts";
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
            securityid_Copy.Text = null;
            ctype.SelectedValue = null;
            ccode.Text = null;
            cname.Text = null;
            clot.Text = null;
            ctick.Text = null;
            csdate.SelectedDate = null;
            cedate.SelectedDate = null;
            cgroupid.Text = null;
            cstate.Text = null;
            mmorderLim.Text = null;
            orderLim.Text = null;
            refpricePara.Text = null;
            id = null;
            statid = null;
            boardid.SelectedItem = null;
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
            cmd.CommandText = "DELETE demo.dbo.contracts WHERE id='" + id + "'";
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
            string secId = cid;
            string code = ccode.Text;
            string name = cname.Text;
            string lot = clot.Text;
            string tick = ctick.Text;
            string csdates = csdate.SelectedDate.Value.ToShortDateString();
            string cedates = cedate.SelectedDate.Value.ToShortDateString();
            string groupID = cgroupid.Text;
            string mmorderLimit = mmorderLim.Text;
            string orderlimit = orderLim.Text;
            string refpricePar = refpricePara.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.contracts SET " +
                "securityId= '" + secId+ "', " +
                "type= '" + ctypee + "', " +
                "code= N'" + code+ "', " +
                "name= N'" + name + "', " +
                "lot= '" + lot+ "', " +
                "tick= '" + tick + "', " +
                "sdate= '" + csdates+ "', " +
                "edate= '" + cedates+ "', " +
                "groupId= '" + groupID+ "', " +
                "state= '" + statid+ "', " +
                "modified = getdate(), " +
                "mmorderLimit= '" + mmorderLimit+ "', " +
                "orderLimit= '" + orderlimit+ "', " +
                "bid= '" + boaID+ "', " +
                "refpriceParam= '" + refpricePar+ "'" +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region combos
        public List<Security> boa { get; set; }
        public List<ctype> ctypp{ get; set; }
        public List<Board> boardd { get; set; }

        private void bindCombo()
        {
            demoEntities10 dE = new demoEntities10();
            var item = dE.Securities.ToList();
            boa = item;
            securityid_Copy.ItemsSource = boa;

            var citems = dE.ctypes.ToList();
            ctypp = citems;
            ctype.ItemsSource = ctypp;

            var botems = dE.Boards.ToList();
            boardd = botems;
            boardid.ItemsSource = boardd;
        }

        private void ctick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void boardid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = boardid.SelectedItem as Board;
            try
            {
                boaID = item.id.ToString();
            }
            catch
            {
                return;
            }
        }

        private void ctype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ctype.SelectedItem as ctype;
            try
            {
                ctypee = item.id.ToString();
            }
            catch
            {
                return;
            }
        }
        private void secid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = securityid_Copy.SelectedItem as Security;
            try
            {
                cid = item.id.ToString();
            }
            catch
            {
                return;
            }
        }
        #endregion
    }
}
