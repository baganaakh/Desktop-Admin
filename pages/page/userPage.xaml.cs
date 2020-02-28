using pages.dbBind;
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

namespace pages.page
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
        int memid;
        #region combos
        private void bindCombo()
        {
            demoEntities10 st = new demoEntities10();
            
            List<Member> paitems = st.Members.ToList();
            part = paitems;
            members.ItemsSource = part;
        }
        public List<Member> part { get; set; }

        private void members_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = members.SelectedItem as Member;
            try
            {
                memid = Convert.ToInt32(items.id);
            }
            catch
            {
                return;
            }
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
                    role = urole.Text,
                    memId = memid,
                    modified = DateTime.Now
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
            var Accountss = CE.users;
            DateTable2.ItemsSource = Accountss.ToList();
        }
    }
}
