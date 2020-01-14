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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
namespace pages
{
    /// <summary>
    /// Interaction logic for loginScreen.xaml
    /// </summary>
    public partial class loginScreen : Window
    {
        public loginScreen()
        {
            InitializeComponent();
        }
        public static class MyGlobals
        {
            public const string Prefix = "ID_";
            public static string U_ID;
        }
        #region first submit
        private void log_submit(object sender, RoutedEventArgs e)
        {
            string connectionString = Properties.Settings.Default.ConnectionString;
            string roless;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                    string CmdString = "SELECT role ,id FROM [dbo].[users] WHERE uname= '" + userName.Text + "' AND password= '" + passBox.Password + "'";
                    SqlCommand cmd = new SqlCommand(CmdString, conn);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Securities");
                    sda.Fill(dt);
                    string neo = dt.Rows[0][0].ToString();
                    string adminstr = "admin     ";
                    string substr = "subs     ";
                    if (Equals(neo, adminstr))
                    {
                        MainWindow dashboard = new MainWindow();
                        MyGlobals.U_ID = dt.Rows[0][1].ToString();

                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username and password is incorrect");
                    }
                }







                using (DatabaseEntities context = new DatabaseEntities())
                {
                    context.Connection.Open();
                    // the rest
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            void Switches(string valuee)
            {
                switch (valuee)
                {
                    case "admin":
                        {
                            MainWindow dashboard = new MainWindow();
                            dashboard.Show();
                            dashboard.Content = new adminPage();
                            this.Close();
                            break;
                        }
                    case "subs":
                        {
                            MainWindow dashboard = new MainWindow();
                            dashboard.Show();
                            this.Close();
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("default opened");
                            break;
                        }
                }
            }
        }
        #endregion
    }
}
