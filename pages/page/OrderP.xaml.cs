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
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class OrderP : Page
    {
        public OrderP()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        string sides;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var values = DateTable2.SelectedItem as Order;
            if (null == values) return;
            
            memid.SelectedValue = values.memberid;
            accountid.SelectedValue = values.accountid;
            stat.SelectedIndex=Convert.ToInt32(values.state+1);
            boardid.SelectedValue =values.boardId;
            assetid.SelectedValue = values.assetid;
            quantity.Text = values.qty.ToString();
            price.Text = values.price.ToString();
            if(values.side == -1) 
            {
                Side.SelectedIndex = 0;
                return;
            }
            Side.SelectedIndex =Convert.ToInt16(values.side);
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if(memid.SelectedItem== null || accountid.SelectedItem== null || boardid.SelectedItem==null
                || stat.SelectedItem ==null || assetid.SelectedItem==null || assetid.SelectedItem == null
                || string.IsNullOrEmpty(price.Text) ||Side.SelectedItem== null || dealtype.SelectedItem== null)
            {
                MessageBox.Show("Талбар дутууу !!!!");
                return;
            }
            sides = Side.SelectedIndex.ToString();
            if (sides == "0")
                sides = "-1";
            using(demoEntities10 contx=new demoEntities10())
            {
                Order ord = new Order
                {
                    side=Convert.ToInt16(sides),
                    memberid=Convert.ToInt64(memid.SelectedValue),
                    accountid=Convert.ToInt64(accountid.SelectedValue),
                    boardId=Convert.ToInt64(boardid.SelectedValue),
                    state=Convert.ToInt16(stat.SelectedIndex-1),
                    assetid=Convert.ToInt32(assetid.SelectedValue),
                    qty=Convert.ToInt32(quantity.Text),
                    price=Convert.ToDecimal(price.Text),
                    dealType=(short)dealtype.SelectedIndex,
                };
                contx.Orders.Add(ord);
                contx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region ref new number fill
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {            
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.Orders.ToList();
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            memid.SelectedValue = null;
            accountid.SelectedValue = null;
            stat.SelectedValue = null;
            boardid.SelectedValue = null;
            assetid.SelectedValue = null;
            quantity.Text = null;
            price.Text = null;
            Side.SelectedValue = null;
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as Order;
            if (value == null) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.Orders.Where(x => x.id == value.id).First();
                conx.Orders.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            sides = Side.SelectedIndex.ToString();
            if (sides == "0")
                sides = "-1";
            var ac = DateTable2.SelectedItem as Order;
            using (demoEntities10 conx = new demoEntities10())
            {
                Order or = conx.Orders.FirstOrDefault(r => r.id == ac.id);
                or.memberid =Convert.ToInt64(memid.SelectedValue);
                or.side =Convert.ToInt16(sides);
                or.accountid =Convert.ToInt64(accountid.SelectedValue);
                or.assetid =Convert.ToInt64(assetid.SelectedValue);
                or.qty = Convert.ToInt32(quantity.Text);
                or.price = Convert.ToDecimal(price.Text);
                or.state = Convert.ToInt16(stat.SelectedIndex - 1);
                or.modified = DateTime.Now;
                or.dealType = (short)dealtype.SelectedIndex;
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region combos
        private void bindCombo()
        {
            demoEntities10 dc = new demoEntities10();
            memid.ItemsSource = dc.Members.ToList();
            boardid.ItemsSource = dc.Boards.ToList();
            accountid.ItemsSource = dc.Accounts.ToList();
            assetid.ItemsSource = dc.Assets.ToList();
        }
        #endregion
    }
}
