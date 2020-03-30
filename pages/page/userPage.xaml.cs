using Admin.dbBind;
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

namespace Admin.page
{
    /// <summary>
    /// Interaction logic for user.xaml
    /// </summary>
    public partial class userPage : Page
    {
        public userPage()
        {
            InitializeComponent();
            bindCombo();
            FillGrid();
        }
        int id;
        #region combos
        private void bindCombo()
        {
            demoEntities10 st = new demoEntities10();
            members.ItemsSource = st.Members.ToList();
        }
        #endregion
        #region insert
        private void Button_Click(object sender, RoutedEventArgs e)//insert
        {
            if(members.SelectedItem == null)
            {
                MessageBox.Show("Please select Member:");
                return;
            }
            using (demoEntities10 context = new demoEntities10())
            {
                var exist = context.users.Count(a => a.uname == usname.Text);
                if (exist != 0)
                {
                    MessageBox.Show("Username exists " + usname.Text+ " !!!");
                    return;
                }
            }
            using (var context = new demoEntities10())
            {
                var std = new user()
                {
                    uname = usname.Text,
                    password = upass.Password,
                    role = urole.SelectedIndex.ToString(),
                    memId = (int)members.SelectedValue,
                    modified = DateTime.Now,
                    serverip=server.Text,
                    serverDatabase=database.Text,
                    serverUname=serveruname.Text,
                    serverPassword=serverpassword.Text,
                };
                context.users.Add(std);
                context.SaveChanges();
            }
            FillGrid();
        }
        #endregion
        private void Button_Click_1(object sender, RoutedEventArgs e)//delete
        {
            user iiid = DateTable2.SelectedItem as user;
            using (demoEntities10 context = new demoEntities10())
            {
                user acc = context.users.FirstOrDefault(r => r.memId == iiid.memId);
                context.users.Remove(acc);
                context.SaveChanges();
            }
            FillGrid();
        }
        private void FillGrid()
        {
            demoEntities10 CE = new demoEntities10();
            DateTable2.ItemsSource = CE.users.ToList();
        }
        #region update
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (demoEntities10 conx = new demoEntities10())
            {
                user us = conx.users.FirstOrDefault(r => r.id == id);
                us.uname = usname.Text;
                us.password = upass.Password;
                us.role = urole.SelectedIndex.ToString();
                //us.memId = (int)members.SelectedValue;
                us.modified = DateTime.Now;
                us.serverip = server.Text;
                us.serverDatabase = database.Text;
                us.serverUname = serveruname.Text;
                us.serverPassword = serverpassword.Text;
                us.modified = DateTime.Now;
                conx.SaveChanges();
            }
            FillGrid();
        }
        #endregion
        #region edit
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            members.IsEnabled = false;
            var values = DateTable2.SelectedItem as user;
            id = values.id;
            members.SelectedValue = values.memId;
            usname.Text = values.uname;
            upass.Password = values.password;
            urole.SelectedIndex = Convert.ToInt32(values.role);
            server.Text = values.serverip;
            database.Text = values.serverDatabase;
            serveruname.Text = values.serverUname;
            serverpassword.Text = values.serverPassword;
        }
        #endregion
        #region new
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            members.SelectedValue = null;
            usname.Text = null;
            upass.Password = null;
            urole.SelectedItem = null;
            server.Text = null;
            database.Text = null;
            serveruname.Text = null;
            serverpassword.Text = null;
            members.IsEnabled = true;
        }
        #endregion
    }
}
