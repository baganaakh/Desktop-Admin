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
using System.Text.RegularExpressions;
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
        string connectionString = Properties.Settings.Default.ConnectionString;
        static string cid, statid, setype;
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
            state.SelectedIndex=Convert.ToInt32(value.state-1);
            sdate.SelectedDate= value.sdate;
            edate.SelectedDate= value.edate;
        }
        #endregion
        #region insert func
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            using(demoEntities10 conx=new demoEntities10())
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
           // if (sdate.SelectedDate == null || edate.SelectedDate == null)
           // {
           //     MessageBox.Show("Please Set Date !!!!!");
           //     return;
           // }
           // string partid = cid;
           // string code = scode.Text;
           // string name = sname.Text;
           
           // string total = totalquant.Text;
           // string fPirce = fprice.Text;
           // string intRat = srate.Text;
           // //Decimal intRate= Decimal.Parse(intRat) / 100;
           // string stat = state.Text;
           // string starTime = sdate.SelectedDate.Value.ToShortDateString();
           // string endTime = edate.SelectedDate.Value.ToShortDateString();
        

           // System.Data.SqlClient.SqlConnection sqlConnection1 =
           //new System.Data.SqlClient.SqlConnection(connectionString);

           // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
           // cmd.CommandType = System.Data.CommandType.Text;
           // cmd.CommandText = "insert into dbo.securities (partid, type, code, name, refno, regno, totalQty, " +
           //     "firstPrice, intRate, sdate, edate, groupid, state, modified) values" +
           //     " ('" + partid + "'," + setype + ",N'" + code + "',N'" + name + "', '" + refno + "', '" + regno +
           //     "', '" + total + "', '" + fPirce + "', '" + intRat +"',N'" + starTime + "',N'" + endTime + "',N'" +
           //     groupId + "',N'" + statid + "', getdate())  " +
           //     "insert into dbo.assets(code, name, value, expireDate, state, modified, secId) values" +
           //     " ('" + code + "',N'" + name + "',N'" + fPirce + "', '" + endTime + "', '" + statid + "', getdate(), IDENT_CURRENT('securities'))";

           // cmd.Connection = sqlConnection1;
           // sqlConnection1.Open();
           // cmd.ExecuteNonQuery();
           // sqlConnection1.Close();
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
                se.partid=
            }

                //string partid = cid;
                //string code = scode.Text;
                //string name = sname.Text;

                //string total = totalquant.Text;
                //string fPirce = fprice.Text;
                //string intRat = srate.Text;
                ////Decimal intRate = Decimal.Parse(intRat) / 100;
                //string stat = state.Text;
                //string starTime = sdate.SelectedDate.Value.ToShortDateString();
                //string endTime = edate.SelectedDate.Value.ToShortDateString();

                //System.Data.SqlClient.SqlConnection sqlConnection1 =
                //new System.Data.SqlClient.SqlConnection(connectionString);

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = "UPDATE demo.dbo.securities SET " +
                //    "partid= '" + partid + "', " +
                //    "type= '" + setype + "', " +
                //    "code= '" + code + "', " +
                //    "name= N'" + name + "', " +
                //    "totalQty= '" + total + "', " +
                //    "firstPrice= '" + fPirce + "', " +
                //    "intRate= '" + intRat + "', " +
                //    "sdate= '" + starTime + "', " +
                //    "edate= '" + endTime + "', " +
                //    "state= '" + statid + "', " +
                //    "modified = getdate() " +
                //    "WHERE id = " + id+

                //    " UPDATE demo.dbo.assets SET " +
                //    "code= '" + code + "', " +
                //    "name= '" + name + "', " +
                //    "value= '" + fPirce + "', " +
                //    "expireDate= '" + endTime + "', " +
                //    "state= '" + statid + "', " +
                //    "modified = getdate() " +
                //    "WHERE secId = '" + id + "'";

                //cmd.Connection = sqlConnection1;
                //sqlConnection1.Open();
                //cmd.ExecuteNonQuery();
                //sqlConnection1.Close();
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
