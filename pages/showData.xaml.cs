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
using System.Configuration;
using System.Data;
using System.Data.Entity;
using pages.dbBind;

namespace pages
{
    /// <summary>
    /// Interaction logic for showData.xaml
    /// </summary>
    public partial class showData : Page
    {
        demoEntities10 data = new demoEntities10();
        CollectionViewSource feeViewSource;

        public showData()
        {
            InitializeComponent();
            feeViewSource = ((CollectionViewSource)(FindResource("feeViewSource")));
            DataContext = this;
            data.Fees.Load();
            feeViewSource.Source = data.Fees.Local;
            feeViewSource.View.MoveCurrentToFirst();
        }

        
    }
}
