﻿using pages.dbBind;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace pages
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class account : Page
    {
        public account()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id, memId,stat,mnominal,cname, acType;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = DateTable2.SelectedItem as DataRowView;
            id = values.Row[0].ToString();
            string memId= values[1].ToString();
            string Trading = values[2].ToString();
            string Clearing = values[3].ToString();
        

            memid.SelectedValue = memId;
            trading.Text = Trading;
           
        }
        #endregion
        #region insert func
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string Trading = trading.Text;
      
            string newMask = "";
            if (memid.SelectedItem == null || pstate.SelectedItem == null)
            {
                MessageBox.Show("Please Set  values !!!!!");
                return;
            }
            
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            #region main insert query 
            cmd.CommandText = "insert into dbo.Account (memberid, mask, modified,state) values " +
                newMask;
            
            #endregion
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region number
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        #endregion
        #region filldatas
        private void FillDataGrid()
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string CmdString = "SELECT ALL [id], [memberid], [trading], [clearing], [settlement], [collateral], [modified], [mask] " +
                            "FROM dbo.Account";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Securities");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        #endregion
        #region ref new
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            memid.Text = null;
            trading.Text = null;
            id = null;
            memId= null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.dbo.Account WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            string Trading = trading.Text;
         

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.Account SET " +
                "memberid= '" + memId + "', " +
                "trading= '" + Trading+ "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region combos control
        public List<Member> Emp { get; set; }
        public List<accType> acct { get; set; }

        private void linkacc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public List<State> boa{ get; set; }

        private void bindCombo()
        {
            demoEntities10 dc = new demoEntities10();
            var mitem = dc.Members.ToList();
            Emp = mitem;
            memid.ItemsSource = Emp;

            var item = dc.States.ToList();
            boa = item;
            pstate.ItemsSource = boa;
            
            var accitem = dc.accTypes.ToList();
            acct = accitem;
            acctype.ItemsSource = acct;
        }
        private void acctype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = acctype.SelectedItem as accType;
            try
            {
                acType = item.id.ToString();
                if(acType == "0")
                {
                    linkacc.IsEnabled = false;
                }
                else
                {
                    linkacc.IsEnabled = true;
                }
            }
            catch
            {
                return;
            }
        }

        private void pstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = pstate.SelectedItem as State;

            try
            {
                stat = item.id.ToString();
            }
            catch
            {
                return;
            }
        }

        private void partid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = memid.SelectedItem as Member;

            try
            {
                memId = item.id.ToString();
                cname = item.name.ToString();
                mnominal= item.nominal.ToString();
                companyName.Content = cname;
                if(mnominal == "True")
                {

                }
            }
            catch
            {
                return;
            }

        }
        #endregion
    }
}
