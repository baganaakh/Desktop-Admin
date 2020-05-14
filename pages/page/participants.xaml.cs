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
        int id;
        public participants()
        {
            InitializeComponent();
            FillDataGrid();
        }        
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var value = DateTable2.SelectedItem as Participant;
            if (null == value) return;
            id = value.id;
            pname.Text = value.name;
            pcountry.Text = value.country;
            pphone.Text = value.phone;
            pmail.Text = value.email;
            //pcontact.Text = value.contact;
            pstate.SelectedIndex =Convert.ToInt32(value.state+1);
            pcity.Text = value.pcity;
            pdistr1.Text = value.pdistr;
            phoroo.Text = value.phoroo;
            pstreet.Text = value.pstreet;
            pwebpage.Text = value.pwebpage;
            numofemp.Text = value.numofemp;
            ptype.SelectedIndex=Convert.ToInt32(value.companyType);
            spetype.SelectedIndex=Convert.ToInt32(value.specialType);
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
                                string message = string.Format("{0}:{1}",validationErrors.Entry.Entity.ToString(),validationError.ErrorMessage);
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
            newData(null,null);
        }
        #endregion
        #region Number FillGrid new and refresh
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.Participants.ToList();
        }
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
            id = 0;
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
            var value = DateTable2.SelectedItem as Participant;
            if (value == null) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.Participants.Where(x => x.id == value.id).First();
                conx.Participants.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            using (demoEntities10 conx = new demoEntities10())
            {
                Participant pa = conx.Participants.FirstOrDefault(r => r.id == id);
                pa.name = pname.Text;
                pa.country =pcountry.Text;
                pa.phone =pphone.Text;
                pa.email =pmail.Text;
                pa.state =Convert.ToInt16(pstate.SelectedIndex-1);
                pa.pcity =pcity.Text;
                pa.pdistr =pdistr1.Text;
                pa.phoroo =phoroo.Text;
                pa.pstreet =pstreet.Text;
                pa.pwebpage =pwebpage.Text;
                pa.numofemp =numofemp.SelectedIndex.ToString();
                pa.modified =DateTime.Now;
                pa.specialType =Convert.ToInt16(spetype.SelectedIndex);
                pa.companyType =Convert.ToInt16(ptype.SelectedIndex);
                conx.SaveChanges();
            }
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
