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
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        static string id, cid;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
            string statese = value.Row[13].ToString();
            string gidse = value.Row[12].ToString();

            partId.SelectedValue= pidse;
            stype.Text = typese;
            scode.Text = codese;
            sname.Text = namese;
            refNo.Text = refnose;
            regNo.Text = regnose;
            totalquant.Text = totqtse;
            fprice.Text = fpricese;
            srate.Text = intrase;
            state.Text = statese;
            sdate.Text = sdatese;
            edate.Text = edatese;
            groupid.Text = gidse;
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (sdate.SelectedDate == null || edate.SelectedDate == null)
            {
                MessageBox.Show("Please Set Date !!!!!");
                return;
            }
            string partid = cid;
            string type = stype.Text;
            string code = scode.Text;
            string name = sname.Text;
            string refno = refNo.Text;
            string regno = regNo.Text;
            string total = totalquant.Text;
            string fPirce = fprice.Text;
            string intRate = srate.Text;
            string stat = state.Text;
            string starTime = sdate.SelectedDate.Value.ToShortDateString();
            string endTime = edate.SelectedDate.Value.ToShortDateString();
            string groupId = groupid.Text;

            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.securities (partid, type, code, name, refno, regno, totalQty, firstPrice, intRate, sdate, edate, groupid, state, modified) values" +
                " ('" + partid + "','" + type + "','" + code + "','" + name + "', '" + refno + "', '" + regno + "', '" + total + "', '" + fPirce + "', '" + intRate +
                "','" + starTime + "','" + endTime + "','" + groupId + "','" + stat + "', getdate())";

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
                CmdString = "SELECT ALL [id], [partid], [type], [code], [name], [refno], [regno], [totalQty], [firstPrice], [intRate], [sdate], [edate], [groupId], [state],[modified] " +
                    "FROM dbo.securities";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Securities");
                sda.Fill(dt);
                DateTable2.ItemsSource = dt.DefaultView;
            }
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
        private void update(object sender, RoutedEventArgs e)
        {
            string partid = cid;
            string type = stype.Text;
            string code = scode.Text;
            string name = sname.Text;
            string refno = refNo.Text;
            string regno = regNo.Text;
            string total = totalquant.Text;
            string fPirce = fprice.Text;
            string intRate = srate.Text;
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
                "type= '" + type + "', " +
                "code= '" + code + "', " +
                "name= '" + name + "', " +
                "refno= '" + refno + "', " +
                "regno= '" + regno + "', " +
                "totalQty= '" + total + "', " +
                "firstPrice= '" + fPirce + "', " +
                "intRate= '" + intRate + "', " +
                "sdate= '" + starTime + "', " +
                "edate= '" + endTime + "', " +
                "groupId= '" + groupId + "', " +
                "state= '" + stat + "', " +
                "modified = getdate() " +
                "WHERE id = '" + id + "'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }

        public List<Participants> Emp { get; set; }
        private void bindcombo()
        {
            demoEntities1 dc = new demoEntities1();
            var item = dc.Participants.ToList();
            Emp = item;
            partId.ItemsSource = Emp;
        }
        private void partid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = partId.SelectedItem as Participants;
            cid = item.id.ToString();
//< ComboBox Name = "sboardid" ItemsSource = "{Binding}" SelectionChanged = "boardid_SelectionChanged" DisplayMemberPath = "name" SelectedValuePath = "id" HorizontalAlignment = "Left" Margin = "150,44,0,0" VerticalAlignment = "Top" Width = "120" />

        }
    }
}
