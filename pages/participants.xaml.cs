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
    /// Interaction logic for participants.xaml
    /// </summary>
    public partial class participants : Page
    {
        
        public participants()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = Properties.Settings.Default.ConnectionString;
        static string id,cid, spid, ptid;

        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            string codepar = value.Row[1].ToString();
            string namepar = value.Row[2].ToString();
            string countrypar = value.Row[3].ToString();
            string addresspar= value.Row[4].ToString();
            string phonepar= value.Row[5].ToString();
            string emailpar= value.Row[6].ToString();
            string contactpar= value.Row[7].ToString();
            string statepar = value.Row[8].ToString();
            string Pcity= value.Row[9].ToString();
            string Pdistr = value.Row[10].ToString();
            string Phoroo= value.Row[11].ToString();
            string Pstreet= value.Row[12].ToString();
            string Pwebpage= value.Row[13].ToString();
            string numEmploy= value.Row[14].ToString();
            string CSid= value.Row[16].ToString();
            string WEBid= value.Row[17].ToString();
            string speType= value.Row[18].ToString();
            string coType= value.Row[19].ToString();

            pcode.Text = codepar;
            pname.Text = namepar;
            pcountry.Text = countrypar;
            pphone.Text = phonepar;
            pmail.Text = emailpar;
            pcontact.Text = contactpar;
            pstate.SelectedValue = statepar; cid = statepar;
            pcity.Text = Pcity;
            pdistr.Text = Pdistr;
            phoroo.Text = Phoroo;
            pstreet.Text = Pstreet;
            pwebpage.Text = Pwebpage;
            numofemp.Text = numEmploy;
            pcsid.Text = CSid;
            pwebid.Text = WEBid;
            ptype.SelectedValue = coType; ptid = coType; //cotype
            spetype.SelectedValue = speType; spid = speType; //special type
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string code = pcode.Text;
            string name = pname.Text;
            string country = pcountry.Text;
            string phone = pphone.Text;
            string email = pmail.Text;
            string contact = pcontact.Text;
            string csid = pcsid.Text;
            string webid = pwebid.Text;
            string Pcity = pcity.Text;
            string Pdistr = pdistr.Text;
            string Phoroo = phoroo.Text;
            string Pstreet = pstreet.Text;
            string Pwebpage = pwebpage.Text;
            string Numofemp= numofemp.Text;
            if(ptid == null || spid == null)
            {
                MessageBox.Show("please set the participant types");
                return;
            }
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);
            
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SET ANSI_PADDING OFF; insert into dbo.participants (code, name, country, phone, email, contact, state, modified," +
                " csid, webid,pcity,pdistr,phoroo,pstreet,pwebpage,numofemp,coType,spType) values" +
                " (N'" + code + "',N'" + name + "',N'" + country + "', '" + phone+ "', N'" + email+ "', '" + contact+ "', '" + cid +"', getdate(), '"+csid+ 
                "', '"+webid+"',N'"+ Pcity + "',N'" + Pdistr + "',N'" + Phoroo + "',N'" + Pstreet + "',N'" + Pwebpage + "',N'" + Numofemp + "',"+ptid+","+spid+")";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
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
        #region fill
        private void FillDataGrid()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT * FROM dbo.participants ";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Participant");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        #endregion
        #region new ref
        private void newData(object sender, RoutedEventArgs e)
        {
            pcode.Text = null;
            pname.Text = null;
            pcountry.Text= null;
            pcontact.Text= null;
            pstate.Text = null;
            pphone.Text = null;
            pmail.Text = null;
            pwebid.Text= null;
            pcsid.Text= null;
            id = null;
            pcity.Text=null;
            pdistr.Text=null;
            phoroo.Text=null;
            pstreet.Text=null;
            pwebpage.Text=null;
            numofemp.Text=null;
            spetype.SelectedItem = null;
            ptype.SelectedItem = null;
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
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
            cmd.CommandText = "DELETE demo.dbo.participants WHERE id='" + id + "'";
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
            string code = pcode.Text;
            string name = pname.Text;
            string country = pcountry.Text;
            string phone = pphone.Text;
            string email = pmail.Text;
            string contact = pcontact.Text;
            string csid = pcsid.Text;
            string webid = pwebid.Text;
            string Pcity = pcity.Text;
            string Pdistr = pdistr.Text;
            string Phoroo = phoroo.Text;
            string Pstreet = pstreet.Text;
            string Pwebpage = pwebpage.Text;
            string Numofemp = numofemp.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.participants SET " +
                "code = N'" + code+ "', " +
                "name= N'" + name + "', " +
                "country = N'" + country+ "', " +
                "phone= '" + phone+ "', " +
                "email= N'" + email+ "', " +
                "contact= '" + contact+ "', " +
                "state= '" + cid + "', " +
                "modified = getdate(), " +
                "webid= '" + webid+ "', " +
                "pcity= N'" + Pcity+ "', " +
                "pdistr= N'" + Pdistr+ "', " +
                "phoroo= N'" + Phoroo+ "', " +
                "pstreet= N'" + Pstreet+ "', " +
                "pwebpage= '" + Pwebpage+ "', " +
                "numofemp= '" + Numofemp+ "', " +
                "coType= '" + ptid+ "', " +
                "spType= '" + spid+ "', " +
                "csid= '" + csid+ "' " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region email validation
        //bool IsValidEmail(string email)
        //{
        //    try
        //    {
        //        var addr = new System.Net.Mail.MailAddress(email);
        //        return addr.Address == email;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //public static class ValidatorExtensions
        //{
        //    public static bool IsValidEmailAddress(this string s)
        //    {
        //        Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        //        return regex.IsMatch(s);
        //    }
        //}

        //private void pmail_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    bool result = ValidatorExtensions.IsValidEmailAddress(pmail.Text);
        //}

        #endregion
        #region combos
        public List<State> boa { get; set; }
        public List<SpecialType> sptype { get; set; }
        public List<Ptype> ptyp{ get; set; }
        private void bindCombo()
        {
            demoEntities10 dE = new demoEntities10();
            var item = dE.States.ToList();
            boa = item;
            pstate.ItemsSource = boa;
            
            demoEntities10 sp = new demoEntities10();
            var spitem = sp.SpecialTypes.ToList();
            sptype = spitem;
            spetype.ItemsSource = sptype;

            demoEntities10 ptp = new demoEntities10();
            var ptitem = ptp.Ptypes.ToList();
            ptyp= ptitem;
            ptype.ItemsSource = ptyp;
        }
        private void ptype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var spitem = ptype.SelectedItem as Ptype;
            try
            {
                ptid = spitem.id.ToString();
            }
            catch
            {
                return;
            }
        }

        private void spetype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var spitem = spetype.SelectedItem as SpecialType;
            try
            {
                spid = spitem.id.ToString();
            }
            catch
            {
                return;
            }
        }

        private void sstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = pstate.SelectedItem as State;
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
