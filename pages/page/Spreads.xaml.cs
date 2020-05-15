using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using Admin.dbBind;

namespace Admin
{
    /// <summary>
    /// Interaction logic for Spread.xaml
    /// </summary>
    public partial class Spreads : Page
    {
        public Spreads()
        {
            InitializeComponent();
            FillDataGrid();
            bindcombo();
        }        
        long id;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as Spread;
            if (null == values) return;
            id = values.id;
            contractid.SelectedValue=values.contractid;
            sessionid.SelectedValue=values.sessionid;
            rspread.Text=values.rspread.ToString();
            ispread.Text=values.ispread.ToString();
            rparam.Text=values.rparam.ToString();
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (contractid.SelectedItem== null || string.IsNullOrEmpty(ispread.Text) || 
                string.IsNullOrEmpty(rspread.Text) || sessionid.SelectedItem== null || string.IsNullOrEmpty(rparam.Text))
            {
                MessageBox.Show("Талбар дутууу !!!!");
                return;
            }
            using (demoEntities10 conx = new demoEntities10())
            {
                var secu = new Spread
                {
                    contractid=Convert.ToInt32(contractid.SelectedValue),
                    sessionid=Convert.ToInt32(sessionid.SelectedValue),
                    ispread=Convert.ToInt32(ispread.Text),
                    rspread=Convert.ToInt32(rspread.Text),
                    rparam=Convert.ToInt32(rparam.Text),
                    modified=DateTime.Now,
                };
                conx.Spreads.Add(secu);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region fill number refres шинэ
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.Spreads.ToList();
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            contractid.SelectedItem= null;
            sessionid.SelectedItem = null;
            rspread.Text = null;
            ispread.Text = null;
            rparam.Text = null;
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
                var del = conx.Spreads.Where(x => x.id == value.id).First();
                conx.Spreads.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region combos
        public List<Session> Sessionss { get; set; }
        private void contractid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var citem = contractid.SelectedItem as Contract;
            sessionid.ItemsSource = null;
            try
            {
                long coId = citem.id;
                demoEntities10 de = new demoEntities10();
                //var bid = de.Contracts.Where(c => c.id == coId).FirstOrDefault<Contract>();
                //var veiw = de.Sessions.Where(s => s.boardid == (bid.bid)).ToList();



                var t = from tt in de.Contracts.Where(c => c.id == coId)
                        join se in de.Sessions on tt.bid equals se.boardid
                        select new {se.name, se.id};

                  //using (SqlConnection conn = new SqlConnection(connectionString))
                  //{
                  //    string CmdString = "SELECT [id] ,[name] FROM [demo].[dbo].[Session] WHERE [boardid] =" +
                  //        " ( SELECT [bid] FROM [demo].[dbo].[Contracts] WHERE id = "+coId+")";
                  //    SqlCommand cmd = new SqlCommand(CmdString, conn);
                  //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                  //    DataTable dt = new DataTable("khfjkh");
                  //    sda.Fill(dt);

                  //    DataView view = dt.DefaultView;

                  //}
                  sessionid.ItemsSource =t.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
        private void bindcombo()
        {
            demoEntities10 ct = new demoEntities10();
            contractid.ItemsSource = ct.Contracts.ToList(); ;
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            using (demoEntities10 conx = new demoEntities10())
            {
                Spread sp = conx.Spreads.FirstOrDefault(s => s.id == id);
                sp.contractid = Convert.ToInt32(contractid.SelectedValue);
                    sp.sessionid = Convert.ToInt32(sessionid.SelectedValue);
                    sp.ispread = Convert.ToInt32(ispread.Text);
                    sp.rspread = Convert.ToInt32(rspread.Text);
                    sp.rparam = Convert.ToInt32(rparam.Text);
                    sp.modified = DateTime.Now;
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
    }
}
