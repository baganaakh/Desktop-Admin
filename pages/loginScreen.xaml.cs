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

        private void log_submit(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Server=MSX-1003; Database=demo;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                    string CmdString = string.Empty;
                    CmdString = "SELECT role FROM [dbo].[users] WHERE uname= '" + userName.Text + "' AND password= '" + passBox.Password + "'";
                    SqlCommand cmd = new SqlCommand(CmdString, conn);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Securities");
                    sda.Fill(dt);
                    Object o = dt.Rows[0][0];
                    string dataa = o.ToString();

                    if (dt.Rows.Count == 1) { 
                    switch (dataa)
                    {
                        case "admin":
                            {
                                this.Hide();
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
                                break;
                            }
                    }
                    }
                }




                else
                {
                    MessageBox.Show("Username and password is incorrect");
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
        }
    }
}
