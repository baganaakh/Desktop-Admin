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
        static string id,statid,metype;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            string typeeee = value.Row[3].ToString();
            string codepar = value.Row[1].ToString();
            string namepar = value.Row[2].ToString();
            string addresspar = value.Row[4].ToString();
            string phonepar = value.Row[5].ToString();
            string faxpar = value.Row[6].ToString();
            string emailpar = value.Row[7].ToString();
            string contactpar = value.Row[8].ToString();
            string statepar = value.Row[9].ToString();
            string modifypar = value.Row[10].ToString();
            string webidpar = value.Row[11].ToString();
            string csidpar = value.Row[12].ToString();
            string isd= value.Row[13].ToString();

            pcode.Text = codepar;
            pname.Text = namepar;
            mtype.SelectedValue = typeeee;
            pcontact.Text = contactpar;
            pstate.SelectedValue = statepar;
            pmodify.Text = modifypar;
            paddress.Text = addresspar;
            pphone.Text = phonepar;
            pfax.Text = faxpar;
            pmail.Text = emailpar;
            pwebid.Text = webidpar;
            pcsid.Text = csidpar;
            isdealer.Text = isd;
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string code = pcode.Text;
            string name = pname.Text;
            string type = mtype.Text;
            string address = paddress.Text;
            string phone = pphone.Text;
            string fax = pfax.Text;
            string email = pmail.Text;
            string contact = pcontact.Text;
            string state = pstate.Text;
            string csid = pcsid.Text;
            string webid = pwebid.Text;
            string mask="";
            string h = "";
            string isd=isdealer.Text;
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

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
            if (isd.Equals("1"))
            {
                h = ", (IDENT_CURRENT('demo.dbo.members'),NULL,NULL,NULL,NULL, getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "h100')," +
                " (IDENT_CURRENT('demo.dbo.members'),NULL,NULL,NULL,NULL, getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "h200')," +
                " (IDENT_CURRENT('demo.dbo.members'),NULL,NULL,NULL,NULL, getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "h300')," +
                " (IDENT_CURRENT('demo.dbo.members'),NULL,NULL,NULL,NULL, getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "h400')";
            }
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.members (type,code, name, address, phone, fax, email, contact, state, modified, csid, webid,isDealer) values" +
                " ('" + metype + "',N'" + code + "',N'" + name + "',N'" + address + "', '" + phone + "', '" + fax + "', '" + email + "', '" + contact + "', '" + statid +
                "', getdate(), '" + csid + "', '" + webid + "'," + isd + ");"
                + "insert into dbo.Account(memberid, trading, clearing, settlement, collateral, modified,mask) values" +
                " (IDENT_CURRENT('demo.dbo.members'),NULL,NULL,NULL,NULL, getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "c100')," +
                " (IDENT_CURRENT('demo.dbo.members'),NULL,NULL,NULL,NULL, getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "c200')," +
                " (IDENT_CURRENT('demo.dbo.members'),NULL,NULL,NULL,NULL, getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "c300')," +
                " (IDENT_CURRENT('demo.dbo.members'),NULL,NULL,NULL,NULL, getdate(),'IDENT_CURRENT('demo.dbo.members')" + mask + "c400')" + h;

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
                CmdString = "SELECT ALL [id], [code], [name], [type], [address], [phone], [fax], [email], [contact], [state], [modified], [csid], [webid],[isDealer],[mask] " +
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
            pname.Text = null;
            mtype.Text = null;
            pcontact.Text = null;
            pstate.Text = null;
            pmodify.Text = null;
            paddress.Text = null;
            pphone.Text = null;
            pfax.Text = null;
            pmail.Text = null;
            pwebid.Text = null;
            pcsid.Text = null;
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
            string name = pname.Text;
            string Mtype= mtype.Text;
            string address = paddress.Text;
            string phone = pphone.Text;
            string fax = pfax.Text;
            string email = pmail.Text;
            string contact = pcontact.Text;
            string state = pstate.Text;
            string csid = pcsid.Text;
            string webid = pwebid.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);


            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.members SET " +
                "code = '" + code + "', " +
                "name= '" + name + "', " +
                "type = '" + metype+ "', " +
                "address= '" + address + "', " +
                "phone= '" + phone + "', " +
                "fax= '" + fax + "', " +
                "email= '" + email + "', " +
                "contact= '" + contact + "', " +
                "state= '" + statid + "', " +
                "modified = getdate(), " +
                "webid= '" + webid + "', " +
                "csid= '" + csid + "' " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        public List<States> statt { get; set; }
        public List<mtype> mt { get; set; }
        private void bindCombo()
        {
            demoEntities10 st = new demoEntities10();
            var items = st.States.ToList();
            statt = items;
            pstate.ItemsSource = statt;

            var meitems = st.mtype.ToList();
            mt = meitems;
            mtype.ItemsSource = mt;
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
