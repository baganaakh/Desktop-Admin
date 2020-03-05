using Admin.dbBind;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for CollApprove.xaml
    /// </summary>
    public partial class CollApprove : Page
    {
        public CollApprove()
        {
            InitializeComponent();
            FillDataGrid();
        }
        #region fill
        private void FillDataGrid()
        {
            demoEntities10 DE = new demoEntities10();
                DateTable2.ItemsSource = DE.ColReqs.ToList();
        }
        #endregion
        #region Approve
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ColReq values = DateTable2.SelectedItem as ColReq;
            if (null == values) return;
            //int id =Convert.ToInt32( values.Row[0]);
            using(demoEntities10 conx=new demoEntities10())
            {  
                long accid = Convert.ToInt64(values.accId);
                int assid = Convert.ToInt32(values.assetId);
                decimal amu = Convert.ToDecimal(values.value);
                int memi = Convert.ToInt32(values.memid);
                Tran trans1 = new Tran
                {
                    accountId=accid,
                    currency=assid,
                    amount=amu,
                    memberid=memi,
                    modified=DateTime.Now
                };
                ColReq std = conx.ColReqs.FirstOrDefault(r => r.id == values.id);
                std.state = 1;
                conx.Trans.Add(trans1);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region deny
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
