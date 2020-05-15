using Admin.dbBind;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Admin
{
    /// <summary>
    /// Interaction logic for contracts.xaml
    /// </summary>
    public partial class contracts : Page
    {
        public contracts()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as Contract;
            if (null == values) return;

            securityid_Copy.SelectedValue = values.securityId;
            ctype.SelectedIndex = Convert.ToInt32(values.type) - 1;
            ccode.Text = values.code;
            cname.Text = values.name;
            clot.Text = values.lot.ToString();
            ctick.Text = values.tickTable.ToString();
            csdate.SelectedDate = values.sdate;
            cedate.SelectedDate = values.edate;
            cgroupid.Text = values.groupId.ToString();
            cstate.SelectedIndex = Convert.ToInt16(values.state);
            mmorderLim.Text = values.mmorderLimit.ToString();
            orderLim.Text = values.orderLimit.ToString();
            refpricePara.Text = values.refpriceParam.ToString();
            boardid.SelectedValue = values.bid;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (csdate.SelectedDate == null || cedate.SelectedDate == null || string.IsNullOrEmpty(ccode.Text)
                    || string.IsNullOrEmpty(cname.Text) || string.IsNullOrEmpty(clot.Text) || string.IsNullOrEmpty(cgroupid.Text)
                    || string.IsNullOrEmpty(mmorderLim.Text) || string.IsNullOrEmpty(orderLim.Text) ||
                    string.IsNullOrEmpty(refpricePara.Text) || cstate.SelectedItem == null || securityid_Copy.SelectedItem == null
                    || ctype.SelectedItem == null || boardid.SelectedItem == null || ctick.SelectedItem == null
                )
            {
                MessageBox.Show("Талбар дутуу !!!!!");
                return;
            }
            using (demoEntities10 contx = new demoEntities10())
            {
                Contract con = new Contract
                {
                    securityId = Convert.ToInt64(securityid_Copy.SelectedValue),
                    type = Convert.ToInt16(ctype.SelectedIndex + 1),
                    code = ccode.Text,
                    name = cname.Text,
                    lot = Convert.ToDecimal(clot.Text),
                    tickTable = Convert.ToInt32(ctick.SelectedValue),
                    sdate = csdate.SelectedDate,
                    edate = cedate.SelectedDate,
                    groupId = Convert.ToInt16(cgroupid.Text),
                    state = Convert.ToInt16(cstate.SelectedIndex - 1),
                    modified = DateTime.Now,
                    mmorderLimit = Convert.ToInt32(mmorderLim.Text),
                    orderLimit = Convert.ToInt32(orderLim.Text),
                    refpriceParam = Convert.ToDecimal(refpricePara.Text),
                    bid = Convert.ToInt64(boardid.SelectedValue),
                };
                contx.Contracts.Add(con);
                contx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region fill
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.Contracts.ToList();
        }
        #endregion
        #region ref new
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            securityid_Copy.Text = null;
            ctype.SelectedValue = null;
            ccode.Text = null;
            cname.Text = null;
            clot.Text = null;
            ctick.Text = null;
            csdate.SelectedDate = null;
            cedate.SelectedDate = null;
            cgroupid.Text = null;
            cstate.Text = null;
            mmorderLim.Text = null;
            orderLim.Text = null;
            refpricePara.Text = null;
            boardid.SelectedItem = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as Contract;
            if (null == value) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.Contracts.Where(x => x.id == value.id).First();
                conx.Contracts.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            var ac = DateTable2.SelectedItem as Contract;
            using (demoEntities10 conx = new demoEntities10())
            {
                Contract ca = conx.Contracts.FirstOrDefault(r => r.id == ac.id);
                ca.securityId = Convert.ToInt64(securityid_Copy.SelectedValue);
                ca.type = Convert.ToInt16(ctype.SelectedIndex);
                ca.code = ccode.Text;
                ca.lot = Convert.ToDecimal(clot.Text);
                ca.name = cname.Text;
                ca.tickTable = Convert.ToInt32(ctick.SelectedValue);
                ca.sdate = csdate.SelectedDate;
                ca.edate = cedate.SelectedDate;
                ca.groupId = Convert.ToInt16(cgroupid.Text);
                ca.state = Convert.ToInt16(cstate.SelectedIndex - 1);
                ca.modified = DateTime.Now;
                ca.mmorderLimit = Convert.ToInt32(mmorderLim.Text);
                ca.orderLimit = Convert.ToInt32(orderLim.Text);
                ca.refpriceParam = Convert.ToDecimal(refpricePara.Text);
                ca.bid = Convert.ToInt64(boardid.SelectedValue);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region combos
        private void bindCombo()
        {
            demoEntities10 dE = new demoEntities10();
            securityid_Copy.ItemsSource = dE.Securities.ToList();
            boardid.ItemsSource = dE.Boards.ToList();
            ctick.ItemsSource = dE.TickSizeTables.ToList();
        }
        #endregion
    }
}
