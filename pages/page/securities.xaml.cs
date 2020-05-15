using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using Admin.dbBind;

namespace Admin
{
    /// <summary>
    /// Interaction logic for securities.xaml
    /// </summary>
    public partial class securities : Page
    {
        public securities()
        {
            InitializeComponent();
            FillDataGrid();
            bindcombo();
        }
        int id;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var value = DateTable2.SelectedItem as Security;
            if (null == value) return;
            id = value.id;
            partId.SelectedValue= value.partid;
            stype.SelectedIndex= Convert.ToInt16(value.type);
            scode.Text = value.code;
            sname.Text = value.name;
            totalquant.Text = value.totalQty.ToString();
            fprice.Text = value.firstPrice.ToString();
            Decimal intRase = Convert.ToDecimal(value.intRate) * 100;
            srate.Text = intRase.ToString();
            state.SelectedIndex=Convert.ToInt32(value.state+1);
            sdate.SelectedDate= value.sdate;
            edate.SelectedDate= value.edate;
        }
        #endregion
        #region insert func
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (partId.SelectedValue == null ||
                stype.SelectedValue == null ||
                assetId.SelectedValue == null ||
                sname.Text == null ||
                totalquant.Text == null ||
                fprice.Text == null ||
                srate.Text == null ||
                sdate.SelectedDate == null ||
                edate.SelectedDate == null ||
                state.SelectedValue == null ||
                scode.Text == null)
            {
                MessageBox.Show("Талбарууд дутуу байна");
                return;
            }
            using (demoEntities10 conx=new demoEntities10())
            {
                var secu = new Security
                {
                    partid = Convert.ToInt32(partId.SelectedValue),
                    type = Convert.ToInt16(stype.SelectedIndex),
                    assetId = Convert.ToInt32(assetId.SelectedValue),
                    name = sname.Text,
                    totalQty = Convert.ToInt32(totalquant.Text),
                    firstPrice = Convert.ToDecimal(fprice.Text),
                    intRate = Convert.ToDecimal(srate.Text),
                    sdate = sdate.SelectedDate,
                    edate = edate.SelectedDate,
                    modified=DateTime.Now,
                    state=Convert.ToInt16(state.SelectedIndex -1),
                    code=scode.Text,
                };
                conx.Securities.Add(secu);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region ref new number fill
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.Securities.ToList();            
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            partId.Text = null;
            stype.Text = null;
            scode.Text = null;
            sname.Text = null;
            totalquant.Text = null;
            fprice.Text = null;
            srate.Text = null;
            state.Text = null;
            sdate.Text = null;
            edate.Text = null;
            id = 0;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as Security;
            if (value == null) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.Securities.Where(x => x.id == value.id).First();
                conx.Securities.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            using (demoEntities10 conx = new demoEntities10())
            {
                Security se = conx.Securities.FirstOrDefault(r => r.id == id);
                se.partid =Convert.ToInt32(partId.SelectedValue);
                se.type =Convert.ToInt16(stype.SelectedIndex);
                se.code = scode.Text;
                se.name = sname.Text;
                se.totalQty =Convert.ToInt32(totalquant.Text);
                se.firstPrice = Convert.ToDecimal(fprice.Text);
                se.intRate = Convert.ToDecimal(srate.Text);
                se.sdate = sdate.SelectedDate;
                se.edate = edate.SelectedDate;
                se.state = Convert.ToInt16(state.SelectedIndex - 1);
                se.modified = DateTime.Now;
                se.assetId =Convert.ToInt32(assetId.SelectedValue);
                conx.SaveChanges();
            }
                FillDataGrid();
        }
        #endregion
        #region combos
        private void bindcombo()
        {
            demoEntities10 dc = new demoEntities10();            
            partId.ItemsSource = dc.Participants.ToList(); ;
            assetId.ItemsSource = dc.Assets.ToList();
        }
        #endregion
    }
}
