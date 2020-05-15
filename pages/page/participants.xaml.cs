using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data;
using Admin.dbBind;
using Admin.validator;
using FluentValidation.Results;
using System.ComponentModel;

namespace Admin
{
    /// <summary>
    /// Interaction logic for participants.xaml
    /// </summary>
    public partial class participants : Page
    {
        int id;
        BindingList<string> errors = new BindingList<string>();
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
            if (string.IsNullOrEmpty(pname.Text) || string.IsNullOrEmpty(pcountry.Text)
                    || string.IsNullOrEmpty(pphone.Text) || string.IsNullOrEmpty(pmail.Text)
                    || string.IsNullOrEmpty(pcity.Text) || string.IsNullOrEmpty(numofemp.Text)
                    || string.IsNullOrEmpty(pdistr1.Text) || string.IsNullOrEmpty(phoroo.Text)
                    || string.IsNullOrEmpty(pwebpage.Text) || pstate.SelectedItem == null
                    || ptype.SelectedItem == null || spetype.SelectedItem == null
                    || string.IsNullOrEmpty(pstreet.Text)
                )
            {
                MessageBox.Show("Талбарууд дутуу байна");
                return;
            }
            try
            {
                using (var context = new demoEntities10())
                {
                    var std = new Participant();
                        std.name = pname.Text;
                        std.country = pcountry.Text;
                        std.phone = pphone.Text;
                        std.email = pmail.Text;
                        std.state = Convert.ToInt16(pstate.SelectedIndex -1);
                        std.pcity = pcity.Text;
                        std.pdistr = pdistr1.Text;
                        std.phoroo = phoroo.Text;
                        std.pstreet = pstreet.Text;
                        std.pwebpage = pwebpage.Text;
                        std.numofemp = numofemp.Text;
                        std.specialType= Convert.ToInt16(spetype.SelectedIndex +1);
                        std.companyType= Convert.ToInt16(ptype.SelectedIndex +1);
                        std.modified = DateTime.Now;
                    
                    PartValidator validator = new PartValidator();
                    FluentValidation.Results.ValidationResult result = validator.Validate(std);
                    if(result.IsValid == false)
                    {
                        foreach(ValidationFailure failure in result.Errors)
                        {
                            //errors.Add(failure.ErrorMessage);
                            ((MainWindow)Window.GetWindow(this)).btmstat.Foreground = Brushes.Red;
                            ((MainWindow)Window.GetWindow(this)).btmstat.Text = failure.ErrorMessage;
                            return;
                        }
                    }
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
    }
}
