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
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id,cid;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            string codepar = value.Row[1].ToString();
            string namepar = value.Row[2].ToString();
            string countrypar = value.Row[3].ToString();
            string addresspar= value.Row[4].ToString();
            string phonepar= value.Row[5].ToString();
            string faxpar = value.Row[6].ToString();
            string emailpar= value.Row[7].ToString();
            string contactpar= value.Row[8].ToString();
            string statepar = value.Row[9].ToString();
            string modifypar = value.Row[10].ToString();
            string webidpar = value.Row[11].ToString();
            string csidpar= value.Row[12].ToString();

            pcode.Text = codepar;
            pname.Text = namepar;
            pcountry.Text = countrypar;
            pcontact.Text = contactpar;
            pstate.SelectedValue = statepar;
            pmodify.Text = modifypar;
            paddress.Text = addresspar;
            pphone.Text = phonepar;
            pfax.Text = faxpar;
            pmail.Text = emailpar;
            pwebid.Text = webidpar;
            pcsid.Text = csidpar;
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string code = pcode.Text;
            string name = pname.Text;
            string country = pcountry.Text;
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
            cmd.CommandText = "insert into dbo.participants (code, name, country, address, phone, fax, email, contact, state, modified, csid, webid) values" +
                " ('" + code + "',N'" + name + "',N'" + country + "',N'" + address+ "', '" + phone+ "', '" + fax+ "', '" + email+ "', '" + contact+ "', '" + cid +
                "', getdate(), '"+csid+ "', '"+webid+"')";
            checkDAta.Text = cmd.CommandText;

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
                CmdString = "SELECT ALL [id], [code], [name], [country], [address], [phone], [fax], [email], [contact], [state], [modified], [webid], [csid], [webid] " +
                    "FROM dbo.participants ";
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
            pcountry.Text= null;
            pcontact.Text= null;
            pstate.Text = null;
            pmodify.Text = null;
            paddress.Text = null;
            pphone.Text = null;
            pfax.Text = null;
            pmail.Text = null;
            pwebid.Text= null;
            pcsid.Text= null;
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
            cmd.CommandText = "DELETE demo.dbo.participants WHERE id='" + id + "'";
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
            string country = pcountry.Text;
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
            cmd.CommandText = "UPDATE demo.dbo.participants SET " +
                "code = '" + code+ "', " +
                "name= '" + name + "', " +
                "country = '" + country+ "', " +
                "address= '" + address + "', " +
                "phone= '" + phone+ "', " +
                "fax= '" + fax+ "', " +
                "email= '" + email+ "', " +
                "contact= '" + contact+ "', " +
                "state= '" + cid + "', " +
                "modified = getdate(), " +
                "webid= '" + webid+ "', " +
                "csid= '" + csid+ "' " +
                "WHERE id = '" + id + "'";

            checkDAta.Text = cmd.CommandText;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        public List<States> boa { get; set; }
        private void bindCombo()
        {
            demoEntities3 dE = new demoEntities3();
            var item = dE.States.ToList();
            boa = item;
            pstate.ItemsSource = boa;
        }

        private void sstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = pstate.SelectedItem as States;
            try
            {
                cid = item.id.ToString();
            }
            catch
            {
                return;
            }
        }
    }
}
