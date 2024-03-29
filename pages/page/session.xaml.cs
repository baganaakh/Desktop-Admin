﻿using Admin.dbBind;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Admin
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class session : Page
    {
        public session()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        long id;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            var value = DateTable2.SelectedItem as Session;
            if (null == value) return;
            id = value.id;
            TimeSpan arrays = (TimeSpan)value.stime;
            sboardid.SelectedValue = value.boardid;
            sname.Text = value.name;
            stimehour.SelectedIndex = arrays.Hours;
            stimeminute.SelectedIndex = arrays.Minutes;
            stimeSecond.SelectedIndex = arrays.Seconds;
            duration.Text = value.duration.ToString();
            Захзээлүүсгэгч.IsChecked = value.Захзээлүүсгэгч;
            Тохиролцсон.IsChecked = value.Тохиролцсон;
            Кросс.IsChecked=value.Кросс;
            Нөхцөлт.IsChecked=value.Нөхцөлт;
            ЗахЗээлийн.IsChecked=value.ЗахЗээлийн;
            sdesc.Text = value.description;
            sstate.SelectedIndex = Convert.ToInt32(value.state + 1);
            algo.SelectedIndex = Convert.ToInt32(value.algorithm);
            match1.Text = value.match.ToString();
            markT.SelectedIndex = Convert.ToInt32(value.markettype);
            if (value.delorder != null)
            {
                delOrder.IsChecked = bool.Parse(value.delorder.ToString());
            }
            if (value.editorder != null)
            {
                editOrder.IsChecked = bool.Parse(value.editorder.ToString());
            }
            if (value.isactive != null)
            {
                isAct.IsChecked = bool.Parse(value.isactive.ToString());
            }
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (sboardid.SelectedItem == null || string.IsNullOrEmpty(sname.Text)
                || string.IsNullOrEmpty(match1.Text) || string.IsNullOrEmpty(sname.Text) ||
                string.IsNullOrEmpty(sdesc.Text) || stimehour.SelectedItem == null
                || stimeminute.SelectedItem == null || stimeSecond.SelectedItem == null || sstate.SelectedItem == null
                || dhour.SelectedItem == null|| dminute.SelectedItem == null|| dsecond.SelectedItem == null 
                )
            {
                MessageBox.Show("Талбар дутууу !!!!");
                return;
            }
            TimeSpan startTime = new TimeSpan(
                Convert.ToInt32(stimehour.Text), Convert.ToInt32(stimeminute.Text), Convert.ToInt32(stimeSecond.Text));
            TimeSpan dur = new TimeSpan(
                Convert.ToInt32(dhour.Text), Convert.ToInt32(dminute.Text), Convert.ToInt32(dsecond.Text));

            using (demoEntities10 conx = new demoEntities10())
            {
                Session ses = new Session
                {
                    boardid = Convert.ToInt64(sboardid.SelectedValue),
                    name = sname.Text,
                    stime = startTime,
                    duration = dur,
                    algorithm = Convert.ToInt16(algo.SelectedIndex),
                    match = Convert.ToInt32(match1.Text),
                    description = sdesc.Text,
                    state = Convert.ToInt16(sstate.SelectedIndex - 1),
                    modified = DateTime.Now,
                    isactive = isAct.IsChecked,
                    delorder = delOrder.IsChecked,
                    editorder = editOrder.IsChecked,
                    markettype = Convert.ToInt16(markT.SelectedIndex),
                    Захзээлүүсгэгч= Захзээлүүсгэгч.IsChecked,
                    Тохиролцсон= Тохиролцсон.IsChecked,
                    Кросс= Кросс.IsChecked,
                    Нөхцөлт= Нөхцөлт.IsChecked,
                    ЗахЗээлийн=ЗахЗээлийн.IsChecked,
                };
                conx.Sessions.Add(ses);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region fill number
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.Sessions.ToList();
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as Session;
            if (value == null) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.Sessions.Where(x => x.id == value.id).First();
                conx.Sessions.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region new
        private void newData(object sender, RoutedEventArgs e)
        {
            sboardid.SelectedValue = null;
            sname.Text = null;
            stimeminute.Text = null;
            stimehour.Text = null;
            dhour.SelectedValue = null;
            algo.Text = null;
            match1.Text = null;
            Захзээлүүсгэгч.IsChecked=null;
            Тохиролцсон.IsChecked=null;
            Кросс.IsChecked=null;
            Нөхцөлт.IsChecked=null;
            ЗахЗээлийн.IsChecked = null;
            sdesc.Text = null;
            sstate.Text = null;
            editOrder.IsChecked = null;
            delOrder.IsChecked = null;
            markT.Text = null;
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            if (sboardid.SelectedItem == null || string.IsNullOrEmpty(sname.Text)
                || string.IsNullOrEmpty(match1.Text) || string.IsNullOrEmpty(sname.Text) ||
                string.IsNullOrEmpty(sdesc.Text) || stimehour.SelectedItem == null
                || stimeminute.SelectedItem == null || stimeSecond.SelectedItem == null || sstate.SelectedItem == null
                || dhour.SelectedItem == null || dminute.SelectedItem == null || dsecond.SelectedItem == null
                )
            {
                MessageBox.Show("Талбар дутууу !!!!");
                return;
            }
            TimeSpan startTime = new TimeSpan(
                   Convert.ToInt32(stimehour.Text), Convert.ToInt32(stimeminute.Text), Convert.ToInt32(stimeSecond.Text));
            TimeSpan dur = new TimeSpan(
                Convert.ToInt32(dhour.Text), Convert.ToInt32(dminute.Text), Convert.ToInt32(dsecond.Text));
            using (demoEntities10 conx = new demoEntities10())
            {
                Session se = conx.Sessions.FirstOrDefault(r => r.id == id);
                se.boardid = Convert.ToInt64(sboardid.SelectedValue);
                se.name = sname.Text;
                se.stime = startTime;
                se.duration = dur;
                se.algorithm = Convert.ToInt16(algo.SelectedIndex);
                se.match = Convert.ToInt32(match1.Text);                
                se.description = sdesc.Text;
                se.state = Convert.ToInt16(sstate.SelectedIndex - 1);
                se.modified = DateTime.Now;
                se.isactive = isAct.IsChecked;
                se.delorder = delOrder.IsChecked;
                se.editorder = editOrder.IsChecked;
                se.markettype = Convert.ToInt16(markT.SelectedIndex);
                se.Захзээлүүсгэгч = Захзээлүүсгэгч.IsChecked;
                se.Тохиролцсон = Тохиролцсон.IsChecked;
                se.Кросс = Кросс.IsChecked;
                se.Нөхцөлт = Нөхцөлт.IsChecked;
                se.ЗахЗээлийн = ЗахЗээлийн.IsChecked;
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region combos
        private void bindCombo()
        {
            demoEntities10 dE = new demoEntities10();
            sboardid.ItemsSource = dE.Boards.ToList();
        }
        #endregion
    }
}
