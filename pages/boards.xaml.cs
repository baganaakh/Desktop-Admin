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
using System.Data;
using System.Text.RegularExpressions;
using pages.dbBind;

namespace pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class boards : Page
    {
        public boards()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
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
        string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
        string id, cid;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var value = DateTable1.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            string name = value.Row[1].ToString();
            string type = value.Row[2].ToString();
            string tdays = value.Row[3].ToString();
            string description = value.Row[4].ToString();
            string stat = value.Row[5].ToString();

            bname.Text = name;
            btype.Text = type;
            tdayss.Text = tdays;
            desc.Text = description;
            state.SelectedValue = stat;
        }
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            string boardName = bname.Text;
            string type = btype.Text;
            string tdays = tdayss.Text;
            string descr = desc.Text;
           
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into dbo.boards (name, type, tdays, description, state) values" +
                " (N'" + boardName + "',N'" + type + "',N'" + tdays + "', N'" + descr + "', N'" + cid + "')";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();

        }
        private void FillDataGrid()
        {
            string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
            string CmdString = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                CmdString = "SELECT ALL [id],[name],[type],[tdays],[description],[state],[modified] FROM dbo.boards ";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                DateTable1.ItemsSource = dt.DefaultView;
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            string boardName = bname.Text;
            string type = btype.Text;
            string tdays = tdayss.Text;
            string descr = desc.Text;
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE demo.dbo.boards SET " +
                "name = N'" + boardName + "', " +
                "type = N'" + type + "', " +
                "tdays = N'" + tdays + "', " +
                "description = N'" + descr + "', " +
                "state = N'" + cid + "', " +
                "modified = getdate() " +
                "WHERE id = N'" + id + "N'";

            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable1.SelectedItem as DataRowView;
            if (null == value) return;
            id = value.Row[0].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection1 =
           new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE demo.dbo.boards WHERE id=N'" + id + "'";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            FillDataGrid();
        }

        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void newData(object sender, RoutedEventArgs e)
        {
            bname.Text = null;
            btype.Text = null;
            tdayss.Text = null;
            desc.Text = null;
            state.SelectedValue = null;
        }
        public List<States> boa { get; set; }
        private void bindCombo()
        {
            demoEntities7 dE = new demoEntities7();
            var item = dE.States.ToList();
            boa = item;
            state.ItemsSource = boa;
        }

        private void stat_change(object sender, SelectionChangedEventArgs e)
        {
            var item = state.SelectedItem as States;
            try
            {
                cid = item.id.ToString();
            }
            catch
            {
                return;
            }
        }
    }
}
