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
        int partid;
        #region combos
        private void bindCombo()
        {
            demoEntities10 st = new demoEntities10();
            
            List<Participant> paitems = st.Participants.ToList();
            part = paitems;
            participants.ItemsSource = part;
        }
        public List<Participant> part { get; set; }

        private void participants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = participants.SelectedItem as Participant;
            try
            {
                partid = Convert.ToInt32(items.id);
            }
            catch
            {
                return;
            }
        }
        #endregion
        private void Button_Click(object sender, RoutedEventArgs e)//insert
        {
            if(participants.SelectedItem == null)
            {
                MessageBox.Show("Please select participant:");
                return;
            }
            using (var context = new demoEntities10())
            {
                var std = new user()
                {
                    uname = usname.Text,
                    password = upass.Password,
                    role = urole.Text,
                    parId = partid,
                    modified = DateTime.Now
                };
                context.users.Add(std);
                context.SaveChanges();
            }
            FillGrid();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//delete
        {
            user iiid = DateTable2.SelectedItem as user;
            using (demoEntities10 context = new demoEntities10())
            {
                user acc = context.users.FirstOrDefault(r => r.parId == iiid.parId);
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
