using Admin.dbBind;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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

namespace Admin
{
    /// <summary>
    /// Interaction logic for price.xaml
    /// </summary>
    public partial class Fee : Page
    {
        demoEntities10 data = new demoEntities10();
        CollectionViewSource feeViewSource;

        public Fee()
        {
            InitializeComponent();
            feeViewSource = ((CollectionViewSource)(FindResource("feeViewSource")));
            DataContext = this;
            data.Fees.Load();
            feeViewSource.Source = data.Fees.Local;
            feeViewSource.View.MoveCurrentToFirst();
        }

        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            data.SaveChanges();
            feeViewSource.View.Refresh();
        }
        #endregion
    }
}
