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
using System.Configuration;
using System.Data;
namespace pages
{
    /// <summary>
    /// Interaction logic for showData.xaml
    /// </summary>
    public partial class showData : Page
    {
        public showData()
        {
            InitializeComponent();
        }
       
      
        
        //private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        //{
        //    List<string> data = new List<string>();
        //    data.Add("apple");
        //    data.Add("mango");
        //    data.Add("banana");
        //    var combo = sender as ComboBox;
        //    combo.ItemsSource = data;
        //    combo.SelectedIndex = 0;

        //}

        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var selectedComboItem = sender as ComboBox;
        //    string name = selectedComboItem.SelectedItem as string;
        //    MessageBox.Show(name);

        //}
    }
}
