using Admin.dbBind;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for invoices.xaml
    /// </summary>
    public partial class invoices : Page
    {
        public invoices()
        {
            InitializeComponent();
            FillDataGrid();
        }
        #region filldatas
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
    DateTable2.ItemsSource = de.Invoices.ToList();
            
        }
        #endregion
    }
}
