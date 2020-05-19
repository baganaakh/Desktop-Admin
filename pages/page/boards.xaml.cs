using Admin.dbBind;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Admin
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
        }
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var value = DateTable2.SelectedItem as Board;
            if (null == value) return;

            bname.Text = value.name;
            btype.Text = value.type.ToString();
            tdayss.Text = value.tdays;
            desc.Text = value.description;
            state.SelectedIndex = value.state + 1;
            dealtype.SelectedValue = value.dealType;
            string[] times = value.expTime.ToString().Split(':');
            dhour.Text = times[0];
            dminute.Text = times[1];
            dsecond.Text = times[2];
            enddate.Text = value.expDate.ToString();
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(bname.Text) || btype.SelectedItem == null || state.SelectedItem == null 
                || dhour.SelectedItem==null || dminute.SelectedItem==null || dsecond.SelectedItem==null
                || string.IsNullOrEmpty(tdayss.Text) || dealtype.SelectedItem==null || string.IsNullOrEmpty(enddate.Text)
                || string.IsNullOrEmpty(desc.Text)
                )
            {
                MessageBox.Show("Талбар дутууу !!!!");
                return;
            }
            using (demoEntities10 contx = new demoEntities10())
            {
                TimeSpan times = new TimeSpan(
                    Convert.ToInt32(dhour.Text),
                    Convert.ToInt32(dminute.Text),
                    Convert.ToInt32(dsecond.Text));
                Board boa = new Board
                {
                    name = bname.Text,
                    expTime = times,
                    tdays = tdayss.Text,
                    description = desc.Text,
                    dealType = Convert.ToInt16(dealtype.SelectedIndex),
                    expDate = Convert.ToInt16(enddate.Text),
                    state = Convert.ToInt16(state.SelectedIndex - 1),
                    modified = DateTime.Now,
                    type = Convert.ToInt16(btype.SelectedIndex),
                };
                contx.Boards.Add(boa);
                contx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            var ac = DateTable2.SelectedItem as Board;
            TimeSpan times = new TimeSpan(
                    Convert.ToInt32(dhour.Text),
                    Convert.ToInt32(dminute.Text),
                    Convert.ToInt32(dsecond.Text));
            using (demoEntities10 conx = new demoEntities10())
            {
                Board board = conx.Boards.FirstOrDefault(r => r.id == ac.id);
                board.name = bname.Text;
                board.dealType = Convert.ToInt16(dealtype.SelectedIndex);
                board.type = Convert.ToInt16(btype.SelectedIndex);
                board.tdays = tdayss.Text;
                board.description = desc.Text;
                board.expDate = Convert.ToInt16(enddate.Text);
                board.expTime = times;
                board.state = Convert.ToInt16(state.SelectedIndex - 1);
                board.modified = DateTime.Now;
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as Board;
            if (null == value) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.Boards.Where(x => x.id == value.id).First();
                conx.Boards.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region fillGrid, NUmber, refresh and new
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.Boards.ToList();
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void semicolonInput(object sender, TextCompositionEventArgs e)
        {
            App.semicolon(sender, e);
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
            dhour.Text = null;
            dminute.Text = null;
            dsecond.Text = null;
            dealtype.SelectedItem = null;
            enddate.Text = null;
        }
        #endregion
    }
}
