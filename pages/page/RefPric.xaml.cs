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
    /// Interaction logic for RefPrice.xaml
    /// </summary>
    public partial class RefPric : Page
    {
        public RefPric()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        static string id;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as RefPrice;
            if (null == values) return;
            id = values.contractId.ToString();

            //refprice.Text = refPrice;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if(markcontact.SelectedItem== null || string.IsNullOrEmpty(refprice.Text) || string.IsNullOrEmpty(name.Text))
            {
                MessageBox.Show("Талбар дутууу !!!!");
                return;
            }
            long contract=Convert.ToInt64(markcontact.SelectedValue);
            using(demoEntities10 conx =new demoEntities10())
            {
            var idexist = conx.RefPrices.Count(a => a.contractId== contract);
            if (idexist != 0)
            {
                MessageBox.Show("contract давтагдсан байна " + markcontact.SelectedValue.ToString() + " !!!");
                return;
            }
                var re = new RefPrice()
                {
                    contractId = contract,
                    refprice1 = Convert.ToDecimal(refprice.Text),
                    name = name.Text,
                    modified = DateTime.Now,
                };
                conx.RefPrices.Add(re);
                conx.SaveChanges();
            }
            // string refPrice = refprice.Text;

            // System.Data.SqlClient.SqlConnection sqlConnection1 =
            //new System.Data.SqlClient.SqlConnection(connectionString);

            // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            // cmd.CommandType = System.Data.CommandType.Text;
            // cmd.CommandText = "DELETE demo.dbo.RefPrice WHERE id = '" + contrid + "'; " +
            //     "insert into dbo.RefPrice (id,refprice, modified) values" +
            //     " ("+ contrid + ",'" + refPrice + "', getdate())";

            // cmd.Connection = sqlConnection1;
            // sqlConnection1.Open();
            // try
            // {
            //     cmd.ExecuteNonQuery();
            // }
            // catch (SqlException ex)
            // {
            //     if (ex.Number == 2)
            //     {
            //         MessageBox.Show("contract 2 is already price edit it !!!");
            //     }
            //     else throw;
            // }
            // sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region ref new mumber fill
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.RefPrices.ToList();
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            rId.Content = null;
            refprice.Text = null;
            id = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as RefPrice;
            if (value == null) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.RefPrices.Where(x => x.contractId == value.contractId).First();
                conx.RefPrices.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion 
        #region combos
        private void bindCombo()
        {
            demoEntities10 st = new demoEntities10();
            markcontact.ItemsSource = st.Contracts.ToList();
        }
        #endregion
    }
}
