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
            bindCombo();
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
            using(demoEntities10 conx=new demoEntities10())
            {  
                Transaction trans1 = new Transaction
                {
                    accountId= values.accId.ToString(),
                    assetId= Convert.ToInt32(values.assetId),
                    totalNumber= Convert.ToDecimal(values.value),
                    memberid= Convert.ToInt32(values.memid),
                    modified=DateTime.Now,
                    tdate=values.modified,
                    type=values.mode,
                };
                ColReq std = conx.ColReqs.FirstOrDefault(r => r.id == values.id);
                conx.ColReqs.Remove(std);
                conx.Transactions.Add(trans1);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region deny state=0 is denied
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ColReq values = DateTable2.SelectedItem as ColReq;
            if (null == values) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                ColReq std = conx.ColReqs.FirstOrDefault(r => r.id == values.id);
                std.state = 0;
                std.reason = Convert.ToInt16(Reasons.SelectedValue);
                conx.SaveChanges();
            }
        FillDataGrid();
        }
        #endregion
        #region combos
        public List<Reason> reas { get; set; }
        private void bindCombo()
        {
            demoEntities10 de = new demoEntities10();
            var reasonss = de.Reasons.ToList();
            reas = reasonss;
            Reasons.ItemsSource = reas;
        }
        #endregion
    }
}
