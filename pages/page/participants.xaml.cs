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
using System.Data.Entity.Validation;

namespace Admin
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

            pname.Text = namepar;
            pcountry.Text = countrypar;
            pphone.Text = phonepar;
            pmail.Text = emailpar;
            pcontact.Text = contactpar;
            pstate.SelectedValue = statepar; cid = statepar;
            pcity.Text = Pcity;
            pdistr1.Text = Pdistr;
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
            try
            {
                using (var context = new demoEntities10())
                {
                    var std = new Participant()
                    {
                        name = pname.Text,
                        country=pcountry.Text,
                        phone = pphone.Text,
                        email = pmail.Text,
                        contact = pcontact.Text,
                        state = Convert.ToInt16(pstate.SelectedIndex -1),
                        pcity = pcity.Text,
                        pdistr = pdistr1.Text,
                        phoroo = phoroo.Text,
                        pstreet = pstreet.Text,
                        pwebpage = pwebpage.Text,
                        numofemp = numofemp.Text,
                        specialType= Convert.ToInt16(spetype.SelectedIndex +1),
                        companyType= Convert.ToInt16(ptype.SelectedIndex +1),
                        modified = DateTime.Now,
                    };
                    context.Participants.Add(std);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting
                                // the current instance as InnerException
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                MessageBox.Show("Бүртгэгдсэн байна та өөр ");
                return;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
                return;
            }
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
        #region new and refresh
        private void newData(object sender, RoutedEventArgs e)
        {
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
            pdistr1.Text=null;
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
            string name = pname.Text;
            string country = pcountry.Text;
            string phone = pphone.Text;
            string email = pmail.Text;
            string contact = pcontact.Text;
            string csid = pcsid.Text;
            string webid = pwebid.Text;
            string Pcity = pcity.Text;
            string Pdistr = pdistr1.Text;
            string Phoroo = phoroo.Text;
            string Pstreet = pstreet.Text;
            string Pwebpage = pwebpage.Text;
            string Numofemp = numofemp.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.participants SET " +
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
    }
}
