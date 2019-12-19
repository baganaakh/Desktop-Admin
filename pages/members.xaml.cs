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
    /// Interaction logic for members.xaml
    /// </summary>
    public partial class members : Page
    {
        public members()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id,statid,metype, partid,oldMask;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            metype= value.Row[1].ToString();
            string codepar = value.Row[2].ToString();
            statid= value.Row[3].ToString();
            oldMask= value.Row[5].ToString();
            string sdate= value.Row[6].ToString();
            string edate= value.Row[7].ToString();
            string Broker= value.Row[8].ToString();
            string Dealer= value.Row[9].ToString();
            string Ander= value.Row[10].ToString();
            string Nominal= value.Row[11].ToString();
            partid= value.Row[12].ToString();

            pcode.Text = codepar;
            mtype.SelectedValue = metype;
            pstate.SelectedValue = statid;
            participants.SelectedValue = partid;
            starttime.SelectedDate = DateTime.Parse(sdate);
            endtime.SelectedDate = DateTime.Parse(edate);
            broker.IsChecked = bool.Parse(Broker);
            dealer.IsChecked = bool.Parse(Dealer);
            ander.IsChecked = bool.Parse(Ander);
            nominal.IsChecked = bool.Parse(Nominal);
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string code = pcode.Text;
            string mask="";
            string h = "";
            if (starttime.SelectedDate == null || endtime.SelectedDate == null)
            {
                MessageBox.Show("Please Set Date !!!!!");
                return;
            }
            string startT = starttime.SelectedDate.Value.ToShortDateString();
            string endT = endtime.SelectedDate.Value.ToShortDateString();
            switch (metype)
            {
                case "0":
                    mask = mask + "70";
                    break;
                case "1":
                    mask = mask + "61";
                    break;
                case "2":
                    mask = mask + "70";
                    break;
                default:
                    MessageBox.Show("No Me Type match");
                    break;
            }
            mask = mask + code;
            string Broker = broker.IsChecked.ToString();
            string Dealer= dealer.IsChecked.ToString();
            string Ander= ander.IsChecked.ToString();
            string Nominal= nominal.IsChecked.ToString();
            if (broker.IsChecked ?? false)
            {
                h = h + "UPDATE DEMO.dbo.Account SET "+
                "state = 0 "+
                "WHERE memberid = IDENT_CURRENT('demo.dbo.members') AND mask LIKE('%h%') " +
                "GO ";
            }
            if (dealer.IsChecked ?? false)
            {
                h = h + "UPDATE DEMO.dbo.Account SET " +
                 "state = 0 " +
                 "WHERE memberid = IDENT_CURRENT('demo.dbo.members') AND mask LIKE('%c%') " +
                 "GO ";
            }
            if (ander.IsChecked ?? false)
            {
                h = h + "UPDATE DEMO.dbo.Account SET " +
                  "state = 0 " +
                  "WHERE memberid = IDENT_CURRENT('demo.dbo.members') AND mask LIKE('%u%') " +
                  "GO ";
            }
            if (nominal.IsChecked ?? false)
            {
                h = h + "UPDATE DEMO.dbo.Account SET " +
                "state = 0 " +
                "WHERE memberid = IDENT_CURRENT('demo.dbo.members') AND mask LIKE('%o%') " +
                "GO ";
            }
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.members (partid, startdate, enddate, type, code, state, broker, dealer, ander, nominal, modified,mask) values " +
                " (" + partid + ",'" + startT + "','" + endT + "','" + metype + "',N'" + code + "','" + statid + "','" + Broker + "','" + Dealer + "','"
                + Ander + "','" + Nominal + "', getdate(),'"+mask+"'); " +
                "insert into dbo.Account(memberid,modified,mask,startdate,enddate,state) values " +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "o100','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "o200','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "o300','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "o400','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "u100','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "u200','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "u300','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "u400','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "c100','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "c200','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "c300','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "c400','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "h100','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "h200','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "h300','" + startT + "','" + endT + "',-1)," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "h400','" + startT + "','" + endT + "',-1); " +
                "GO --insert the update \n" +
                " "+ h;
            
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void FillDataGrid()
        {
            string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
            string CmdString = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                CmdString = "SELECT ALL [id], [type], [code], [state], [modified], [mask]," +
                    "[startdate], [enddate], [broker], [dealer], [ander], [nominal], [partid] FROM dbo.members ";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Participant");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            pcode.Text = null;
            id = null;
            mtype.SelectedValue = null;
            pstate.SelectedValue = null;
            participants.SelectedValue = null;
            starttime.SelectedDate = null;
            endtime.SelectedDate = null;
            broker.IsChecked = null;
            dealer.IsChecked = null;
            ander.IsChecked = null;
            nominal.IsChecked = null;
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.dbo.members WHERE id='" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        private void update(object sender, RoutedEventArgs e)
        {
            string mask = "";
            string h = "";
            if (starttime.SelectedDate == null || endtime.SelectedDate == null)
            {
                MessageBox.Show("Please Set Date !!!!!");
                return;
            }
            string startT = starttime.SelectedDate.Value.ToShortDateString();
            string endT = endtime.SelectedDate.Value.ToShortDateString();
            string Broker = broker.IsChecked.ToString();
            string Dealer = dealer.IsChecked.ToString();
            string Ander = ander.IsChecked.ToString();
            string Nominal = nominal.IsChecked.ToString();
            if (broker.IsChecked ?? false)
            {
                h = h + " insert into dbo.Account(memberid,modified,mask,startdate,enddate) values " +
                 " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "h100','" + startT + "','" + endT + "')," +
                 " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "h200','" + startT + "','" + endT + "')," +
                 " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "h300','" + startT + "','" + endT + "')," +
                 " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "h400','" + startT + "','" + endT + "')";
            }
            if (dealer.IsChecked ?? false)
            {
                h = h + " insert into dbo.Account(memberid,modified,mask,startdate,enddate) values " +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "c100','" + startT + "','" + endT + "')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "c200','" + startT + "','" + endT + "')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "c300','" + startT + "','" + endT + "')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "c400','" + startT + "','" + endT + "')";
            }
            if (ander.IsChecked ?? false)
            {
                h = h + " insert into dbo.Account(memberid,modified,mask,startdate,enddate) values " +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "u100','" + startT + "','" + endT + "')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "u200','" + startT + "','" + endT + "')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "u300','" + startT + "','" + endT + "')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "u400','" + startT + "','" + endT + "')";
            }
            if (nominal.IsChecked ?? false)
            {
                h = h + " insert into dbo.Account(memberid,modified,mask,startdate,enddate) values " +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "o100','" + startT + "','" + endT + "')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "o200','" + startT + "','" + endT + "')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "o300','" + startT + "','" + endT + "')," +
                " (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "o400','" + startT + "','" + endT + "')";
            }
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.members SET " +
                "type = '" + metype+ "', " +
                "state= '" + statid + "', " +
                "broker= '" + Broker+ "', " +
                "dealer= '" + Dealer+ "', " +
                "ander= '" + Ander+ "', " +
                "nominal= '" + Nominal+ "', " +
                "startdate= '" + startT+ "', " +
                "enddate= '" + endT+ "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "' " +

                "UPDATE DEMO.dbo.Account SET "+
                " modified = GETDATE()," +
                " startdate = " + startT + "," +
                " enddate = " + endT + "," +
                " state = " + statid + "" +
                " WHERE memberid = " + id + "";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }
        public List<States> statt { get; set; }
        public List<mtype> mt { get; set; }
        public List<Participants> part { get; set; }
        private void bindCombo()
        {
            demoEntities10 st = new demoEntities10();
            var items = st.States.ToList();
            statt = items;
            pstate.ItemsSource = statt;

            var meitems = st.mtype.ToList();
            mt = meitems;
            mtype.ItemsSource = mt;

            var paitems = st.Participants.ToList();
            part = paitems;
            participants.ItemsSource = part;
        }

        private void participants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = participants.SelectedItem as Participants;
            try
            {
                partid = items.id.ToString();
            }
            catch
            {
                return;
            }
        }

        private void mtype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = mtype.SelectedItem as mtype;
            try
            {
                metype = items.id.ToString();
            }
            catch
            {
                return;
            }
        }

        private void sstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = pstate.SelectedItem as States;
            try
            {
                statid = items.id.ToString();
            }
            catch
            {
                return;
            }
        }
    }
}
