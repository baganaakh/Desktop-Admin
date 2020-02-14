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
    public partial class user : Page
    {
        public user()
        {
            InitializeComponent();
            bindCombo();
        }
        string partid, pname;
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
                partid = items.id.ToString();
                pname = items.name.ToString();
            }
            catch
            {
                return;
            }
        }
    }
}
