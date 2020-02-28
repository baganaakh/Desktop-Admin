﻿using pages.dbBind;
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
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace pages
{
    /// <summary>
    /// Interaction logic for demo1.xaml
    /// </summary>
    public partial class demo1 : Page
    {
        public demo1()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = Properties.Settings.Default.ConnectionString;
        string id, cid, dealTypes;
        #region fill
        private void FillDataGrid()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT * FROM dbo.deals";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        #endregion
        #region combo
        public List<Dealtype> Dtype { get; set; }
        private void bindCombo()
        {
            demoEntities10 dE = new demoEntities10();
            var dts = dE.Dealtypes.ToList();
            Dtype = dts;
            dealtype.ItemsSource = Dtype;
        }
        private void dealtype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dti = dealtype.SelectedItem as Dealtype;
            try
            {
                dealTypes = dti.id.ToString();
            }
            catch
            {
                return;
            }
        }
        #endregion
        #region button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText="insert into demo.dbo.Deal2 (boardid, accountid, assetid, totalPrice, dealType, qty, side, memberid) " +
                    "SELECT [boardid], [accountid], [assetid], SUM([totalPrice]) as totalPrice, dealType, qty,side, memberid FROM [demo].[dbo].[Deals] " +
                        "where cast(modified as date) = cast(GETDATE() as date) and dealType = " + dealTypes+" " +
                                "group by accountid, dealType, assetid, boardid, qty, side, memberid ";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
                FillDataGrid();
            
        }
        #endregion
    }
}