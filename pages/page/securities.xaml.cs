﻿using System;
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
using pages.dbBind;

namespace pages
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
        static string id, cid, statid, setype;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            string pidse = value.Row[1].ToString();
            string typese = value.Row[2].ToString();
            string codese = value.Row[3].ToString();
            string namese = value.Row[4].ToString();
            string refnose = value.Row[5].ToString();
            string regnose = value.Row[6].ToString();
            string totqtse = value.Row[7].ToString();
            string fpricese = value.Row[8].ToString();
            string intrase = value.Row[9].ToString();
            string sdatese = value.Row[10].ToString();
            string edatese = value.Row[11].ToString();
            string gidse = value.Row[12].ToString();
            string statese = value.Row[13].ToString();

            partId.SelectedValue= pidse;
            stype.SelectedValue = typese;
            scode.Text = codese;
            sname.Text = namese;
            refNo.Text = refnose;
            regNo.Text = regnose;
            totalquant.Text = totqtse;
            fprice.Text = fpricese;
            Decimal intRase = Decimal.Parse(intrase) * 100;
            srate.Text = intRase.ToString();
            state.SelectedValue= statese;
            sdate.Text = sdatese;
            edate.Text = edatese;
            groupid.Text = gidse;
        }
        #endregion
        #region insert func
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (sdate.SelectedDate == null || edate.SelectedDate == null)
            {
                MessageBox.Show("Please Set Date !!!!!");
                return;
            }
            string partid = cid;
            string code = scode.Text;
            string name = sname.Text;
            string refno = refNo.Text;
            string regno = regNo.Text;
            string total = totalquant.Text;
            string fPirce = fprice.Text;
            string intRat = srate.Text;
            //Decimal intRate= Decimal.Parse(intRat) / 100;
            string stat = state.Text;
            string starTime = sdate.SelectedDate.Value.ToShortDateString();
            string endTime = edate.SelectedDate.Value.ToShortDateString();
            string groupId = groupid.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.securities (partid, type, code, name, refno, regno, totalQty, " +
                "firstPrice, intRate, sdate, edate, groupid, state, modified) values" +
                " ('" + partid + "'," + setype + ",N'" + code + "',N'" + name + "', '" + refno + "', '" + regno +
                "', '" + total + "', '" + fPirce + "', '" + intRat +"',N'" + starTime + "',N'" + endTime + "',N'" +
                groupId + "',N'" + statid + "', getdate())  " +
                "insert into dbo.assets(code, name, value, expireDate, state, modified, secId) values" +
                " ('" + code + "',N'" + name + "',N'" + fPirce + "', '" + endTime + "', '" + statid + "', getdate(), IDENT_CURRENT('securities'))";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region number only
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
        #region fill
        private void FillDataGrid()
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
               string CmdString = "SELECT ALL [id], [partid], [type], [code], [name], [refno], [regno], [totalQty], " +
                "[firstPrice], [intRate], [sdate], [edate], [groupId], [state], [modified] FROM dbo.securities";
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
            partId.Text = null;
            stype.Text = null;
            scode.Text = null;
            sname.Text = null;
            refNo.Text = null;
            regNo.Text = null;
            totalquant.Text = null;
            fprice.Text = null;
            srate.Text = null;
            state.Text = null;
            sdate.Text = null;
            edate.Text = null;
            groupid.Text = null;
            id = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.dbo.securities WHERE id='" + id + "'";
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
            string partid = cid;
            string code = scode.Text;
            string name = sname.Text;
            string refno = refNo.Text;
            string regno = regNo.Text;
            string total = totalquant.Text;
            string fPirce = fprice.Text;
            string intRat = srate.Text;
            //Decimal intRate = Decimal.Parse(intRat) / 100;
            string stat = state.Text;
            string starTime = sdate.SelectedDate.Value.ToShortDateString();
            string endTime = edate.SelectedDate.Value.ToShortDateString();
            string groupId = groupid.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
            new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.securities SET " +
                "partid= '" + partid + "', " +
                "type= '" + setype + "', " +
                "code= '" + code + "', " +
                "name= N'" + name + "', " +
                "refno= '" + refno + "', " +
                "regno= '" + regno + "', " +
                "totalQty= '" + total + "', " +
                "firstPrice= '" + fPirce + "', " +
                "intRate= '" + intRat + "', " +
                "sdate= '" + starTime + "', " +
                "edate= '" + endTime + "', " +
                "groupId= '" + groupId + "', " +
                "state= '" + statid + "', " +
                "modified = getdate() " +
                "WHERE id = " + id+

                " UPDATE demo.dbo.assets SET " +
                "code= '" + code + "', " +
                "name= '" + name + "', " +
                "value= '" + fPirce + "', " +
                "expireDate= '" + endTime + "', " +
                "state= '" + statid + "', " +
                "modified = getdate() " +
                "WHERE secId = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        #endregion
        #region combos
        public List<Participant> Emp { get; set; }
        public List<State> statt { get; set; }
        public List<stype> stypes { get; set; }

        private void bindcombo()
        {
            demoEntities10 dc = new demoEntities10();
            var item = dc.Participants.ToList();
            Emp = item;
            partId.ItemsSource = Emp;

            var items = dc.States.ToList();
            statt = items;
            state.ItemsSource = statt;

            var titems = dc.stypes.ToList();
            stypes = titems;
            stype.ItemsSource = stypes;
        }

        private void stype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = stype.SelectedItem as stype;
            try
            {
                setype = item.id.ToString();
            }
            catch
            {
                return;
            }
        }

        private void partid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = partId.SelectedItem as Participant;
            try
            {
                cid = item.id.ToString();
            }
            catch
            {
                return;
            }
        }
        private void sstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = state.SelectedItem as State;
            try
            {
            statid = items.id.ToString();
            }
            catch
            {
                return;
            }
        }
        #endregion
    }
}