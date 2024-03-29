﻿using Admin.dbBind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Admin.page
{
    /// <summary>
    /// Interaction logic for CollApprove.xaml
    /// </summary>
    public partial class CollApprove : Page
    {
        public CollApprove()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        #region fill
        private void FillDataGrid()
        {
            demoEntities10 DE = new demoEntities10();
            DateTable2.ItemsSource = DE.Transactions.ToList();
        }
        #endregion
        #region Approve
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Order values = DateTable2.SelectedItem as Order;
            if (null == values) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                Transaction trans1 = new Transaction
                {
                    accountId = values.accountid,
                    assetId = Convert.ToInt32(values.assetid),
                    amount = Convert.ToInt32(values.qty),
                    memberid = Convert.ToInt32(values.memberid),
                    modified = DateTime.Now,
                    tdate = values.modified,
                    type = values.side,
                };
                Transaction std = conx.Transactions.FirstOrDefault(r => r.id == values.id);
                conx.Transactions.Remove(std);
                conx.Transactions.Add(trans1);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region deny state=0 is denied
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Transaction values = DateTable2.SelectedItem as Transaction;
            if (null == values) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                Transaction std = conx.Transactions.FirstOrDefault(r => r.id == values.id);
                std.state = 0;
                //std.reason = Convert.ToInt16(Reasons.SelectedValue);
                conx.SaveChanges();
            }
        FillDataGrid();
        }
        #endregion
        #region combos
        public List<Reason> reas { get; set; }
        private void bindCombo()
        {
            demoEntities10 de = new demoEntities10();
            var reasonss = de.Reasons.ToList();
            reas = reasonss;
            Reasons.ItemsSource = reas;
        }
        #endregion
    }
}
