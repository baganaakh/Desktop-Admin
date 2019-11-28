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
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            string countrypar = value.Row[1].ToString();
            string codepar = value.Row[2].ToString();
            string namepar = value.Row[3].ToString();
            string addresspar = value.Row[4].ToString();
            string phonepar = value.Row[5].ToString();
            string faxpar = value.Row[6].ToString();
            string emailpar = value.Row[7].ToString();
            string contactpar = value.Row[8].ToString();
            string statepar = value.Row[9].ToString();
            string modifypar = value.Row[10].ToString();
            string webidpar = value.Row[11].ToString();
            string csidpar = value.Row[12].ToString();

            pcode.Text = codepar;
            pname.Text = namepar;
            mtype.Text = countrypar;
            pcontact.Text = contactpar;
            pstate.Text = statepar;
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
            string type = mtype.Text;
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
            cmd.CommandText = "insert into dbo.members (type,code, name, address, phone, fax, email, contact, state, modified, csid, webid) values" +
                " ('" + type + "','" + code + "','" + name + "','" + address + "', '" + phone + "', '" + fax + "', '" + email + "', '" + contact + "', '" + state +
                "', getdate(), '" + csid + "', '" + webid + "')";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

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
                CmdString = "SELECT ALL [id], [code], [name], [type], [address], [phone], [fax], [email], [contact], [state], [modified], [csid], [webid] " +
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
                "mtype = '" + Mtype+ "', " +
                "address= '" + address + "', " +
                "phone= '" + phone + "', " +
                "fax= '" + fax + "', " +
                "email= '" + email + "', " +
                "contact= '" + contact + "', " +
                "state= '" + state + "', " +
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
    }
}
