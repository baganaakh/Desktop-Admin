using pages.dbBind;
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
namespace pages
{
    /// <summary>
    /// Interaction logic for demo1.xaml
    /// </summary>
    public partial class demo1 : Page
    {
        private demoEntities10 data = new demoEntities10();
        private CollectionViewSource assetView;
        public demo1()
        {
            InitializeComponent();
            assetView = ((CollectionViewSource)(FindResource("assetsViewSource")));
            DataContext = this;
            data.Assets.Load();
            assetView.Source = data.Assets.Local;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //prev
            assetView.View.MoveCurrentToPrevious();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //next
            assetView.View.MoveCurrentToNext();
        }
    }
}
