using pages.dbBind;
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

namespace pages
{
    /// <summary>
    /// Interaction logic for price.xaml
    /// </summary>
    public partial class Fee : Page
    {
        demoEntities10 data = new demoEntities10();
        CollectionViewSource feeViewSource;

        readonly string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";

        public Fee()
        {
            InitializeComponent();
            feeViewSource = ((CollectionViewSource)(FindResource("feeViewSource")));
            DataContext = this;
            data.Fee.Load();
            feeViewSource.Source = data.Fee.Local;
            feeViewSource.View.MoveCurrentToFirst();
        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            data.SaveChanges();
            feeViewSource.View.Refresh();
        }
    }
}
