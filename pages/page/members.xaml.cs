using Admin.dbBind;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Admin
{
    /// <summary>
    /// Interaction logic for members.xaml
    /// </summary>
    public partial class members : Page
    {
        public members()
        {
            InitializeComponent();
            FillDataGrid();
            bindCombo();
        }
        int id;
        #region edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upd.IsEnabled = true;
            participants.IsEnabled = false;
            var value = DateTable2.SelectedItem as Member;
            if (null == value) return;
            id = value.id;
            pcode.Text = value.code;
            mtype.SelectedIndex = Convert.ToInt32(value.type);
            pstate.SelectedIndex = Convert.ToInt32(value.state + 1);
            participants.SelectedValue = value.partid;
            starttime.SelectedDate = value.startdate;
            endtime.SelectedDate = value.enddate;
            broker.IsChecked = value.broker;
            dealer.IsChecked = value.dealer;
            ander.IsChecked = value.ander;
            nominal.IsChecked = value.nominal;
            pcode.IsEnabled = false;
            linkMember.SelectedValue = value.linkMember;
        }
        #endregion
        #region insert
        private void insertFunc(object sender, RoutedEventArgs e)
        {
            if (starttime.SelectedDate == null || endtime.SelectedDate == null || participants.SelectedItem == null ||
                string.IsNullOrEmpty(pcode.Text) || pstate.SelectedItem == null || mtype.SelectedItem == null)
            {
                MessageBox.Show("Талбар дутуу");
                return;
            }

            if ((broker.IsChecked == false && dealer.IsChecked == false 
                && ander.IsChecked == false && nominal.IsChecked == false)
                || (broker.IsChecked == false && dealer.IsChecked == false 
                && ander.IsChecked == false && nominal.IsChecked == true && mtype.SelectedIndex==0))
            {
                MessageBox.Show("Check box selection combos wrong !!!!!");
                return;
            }
            using (demoEntities10 conx = new demoEntities10())
            {
                try
                {
                    var me = new Member
                    {
                        type = Convert.ToInt16(mtype.SelectedIndex),
                        code = pcode.Text,
                        state = Convert.ToInt16(pstate.SelectedIndex - 1),
                        modified = DateTime.Now,
                        partid = Convert.ToInt64(participants.SelectedValue),
                        startdate = starttime.SelectedDate,
                        enddate = endtime.SelectedDate,
                        broker = broker.IsChecked,
                        dealer = dealer.IsChecked,
                        ander = ander.IsChecked,
                        nominal = nominal.IsChecked,
                        linkMember = Convert.ToInt32(linkMember.SelectedValue),
                        name = participants.Text,
                    };
                    conx.Members.Add(me);
                    conx.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
            #region old insert
            // statid=(pstate.SelectedIndex -1).ToString();
            // metype = (mtype.SelectedIndex - 1).ToString();
            // string code = pcode.Text;
            // string mask="";
            // string h = "";
            // string br= "1";
            // string dl = "1";
            // string an = "1";
            // string no = "1";
            // string startT = starttime.SelectedDate.Value.ToShortDateString();
            // string endT = endtime.SelectedDate.Value.ToShortDateString();
            // switch (metype)
            // {
            //     case "0":
            //         mask = mask + "70";
            //         break;
            //     case "1":
            //         mask = mask + "61";
            //         break;
            //     case "2":
            //         mask = mask + "70";
            //         break;
            //     default:
            //         MessageBox.Show("No Me Type match");
            //         break;
            // }
            // mask = mask + code;
            // string Broker = broker.IsChecked.ToString();
            // string Dealer= dealer.IsChecked.ToString();
            // string Ander= ander.IsChecked.ToString();
            // string Nominal= nominal.IsChecked.ToString();
            // if (broker.IsChecked == false)
            // {
            //     br = "0";
            // }
            // if (dealer.IsChecked == false)
            // {
            //     dl = "0";
            // }
            // if (ander.IsChecked == false)
            // {
            //     an = "0";
            // }
            // if (nominal.IsChecked == false)
            // {
            //     no = "0";
            // }
            // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            // cmd.CommandType = System.Data.CommandType.Text;
            // #region main insert query
            // cmd.CommandText = "insert into dbo.members (partid, startdate, enddate, type, code, state, broker, dealer, ander, nominal, modified,mask,name) values " +
            //     " (" + partid + ",'" + startT + "','" + endT + "','" + metype + "',N'" + code + "','" + statid + "','" + Broker + "','" + Dealer + "','"
            //     + Ander + "','" + Nominal + "', getdate(),'" + mask + "',N'" + pname + "'); "; 
            //     //+
            //     //"insert into dbo.Account(memberid,modified,mask,startdate,enddate,state) values " +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "o100','" + startT + "','" + endT + "'," + no + ")," +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "o200','" + startT + "','" + endT + "'," + no + ")," +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "o300','" + startT + "','" + endT + "'," + no + ")," +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "o400','" + startT + "','" + endT + "'," + no + ")," +

            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "u100','" + startT + "','" + endT + "'," + an + ")," +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "u200','" + startT + "','" + endT + "'," + an + ")," +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "u300','" + startT + "','" + endT + "'," + an + ")," +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "u400','" + startT + "','" + endT + "'," + an + ")," +

            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "c100','" + startT + "','" + endT + "'," + dl + ")," +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "c200','" + startT + "','" + endT + "'," + dl + ")," +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "c300','" + startT + "','" + endT + "'," + dl + ")," +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "c400','" + startT + "','" + endT + "'," + dl + ")," +

            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "h100','" + startT + "','" + endT + "'," + br + ")," +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "h200','" + startT + "','" + endT + "'," + br + ")," +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "h300','" + startT + "','" + endT + "'," + br + ")," +
            //     //" (IDENT_CURRENT('demo.dbo.members'),getdate(),'" + mask + "h400','" + startT + "','" + endT + "'," + br + ") ";
            // #endregion
            // System.Data.SqlClient.SqlConnection sqlConnection1 =
            //new System.Data.SqlClient.SqlConnection(connectionString);
            // cmd.Connection = sqlConnection1;
            // sqlConnection1.Open();
            // try
            // {
            //     cmd.ExecuteNonQuery();
            // }
            // catch (SqlException EX)
            // {
            //     if (EX.Number == 2627)
            //     {
            //         MessageBox.Show("mask " + mask + " and "+ code +" is already inserted try different code");
            //         return;
            //     }
            //     else
            //         throw;
            // }
            // catch (Exception)
            // {
            //     throw;
            // }
            // sqlConnection1.Close();
            #endregion
            FillDataGrid();
        }
        #endregion
        #region new ref fill number
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void FillDataGrid()
        {
            demoEntities10 de = new demoEntities10();
            DateTable2.ItemsSource = de.Members.ToList();
        }
        private void newData(object sender, RoutedEventArgs e)
        {
            pcode.Text = null;
            id = 0;
            mtype.SelectedValue = null;
            pstate.SelectedValue = null;
            participants.SelectedValue = null;
            starttime.SelectedDate = null;
            endtime.SelectedDate = null;
            broker.IsChecked = null;
            dealer.IsChecked = null;
            ander.IsChecked = null;
            nominal.IsChecked = null;
            pcode.IsEnabled = true;
            participants.IsEnabled = true;
        }
        private void refreshh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        #endregion
        #region delete
        private void delete(object sender, RoutedEventArgs e)
        {
            var value = DateTable2.SelectedItem as Member;
            if (value == null) return;
            using (demoEntities10 conx = new demoEntities10())
            {
                var del = conx.Members.Where(x => x.id == value.id).First();
                conx.Members.Remove(del);
                conx.SaveChanges();
            }
            FillDataGrid();
        }
        #endregion
        #region update
        private void update(object sender, RoutedEventArgs e)
        {
            if ((broker.IsChecked == false && dealer.IsChecked == false
                && ander.IsChecked == false && nominal.IsChecked == false)
                || (broker.IsChecked == false && dealer.IsChecked == false
                && ander.IsChecked == false && nominal.IsChecked == true && mtype.SelectedIndex == 0))
            {
                MessageBox.Show("Check box selection combos wrong !!!!!");
                return;
            }
            using (demoEntities10 conx = new demoEntities10())
            {
                Member me = conx.Members.FirstOrDefault(r => r.id == id);
                me.type = Convert.ToInt16(mtype.SelectedIndex);
                me.code = pcode.Text;
                me.state = Convert.ToInt16(pstate.SelectedIndex - 1);
                me.modified = DateTime.Now;
                //me.partid = Convert.ToInt64(participants.SelectedValue);
                me.startdate = starttime.SelectedDate;
                me.enddate = endtime.SelectedDate;
                me.broker = broker.IsChecked;
                me.dealer = dealer.IsChecked;
                me.ander = ander.IsChecked;
                me.nominal = nominal.IsChecked;
                me.linkMember = Convert.ToInt32(linkMember.SelectedValue);
                conx.SaveChanges();
            }
            #region old Update
            // string mask = "";
            // string h = "";
            // if (starttime.SelectedDate == null || endtime.SelectedDate == null)
            // {
            //     MessageBox.Show("Please Set Date !!!!!");
            //     return;
            // }
            // string startT = starttime.SelectedDate.Value.ToShortDateString();
            // string endT = endtime.SelectedDate.Value.ToShortDateString();
            // string Broker = broker.IsChecked.ToString();
            // string Dealer = dealer.IsChecked.ToString();
            // string Ander = ander.IsChecked.ToString();
            // string Nominal = nominal.IsChecked.ToString();
            // if (broker.IsChecked == false)
            // {
            //     h = h + " UPDATE DEMO.dbo.Account SET " +
            //     "state = 1 " +
            //     "WHERE memberid = " + id + " AND mask LIKE('%h%')";
            // }   else if (broker.IsChecked == true)
            //         {
            //             h = h + " UPDATE DEMO.dbo.Account SET " +
            //             "state = 0 " +
            //             "WHERE memberid = " + id + " AND mask LIKE('%h%')";
            //         }
            // if (dealer.IsChecked == false)
            // {
            //     h = h + " UPDATE DEMO.dbo.Account SET " +
            //       "state = 1 " +
            //       "WHERE memberid = " + id + " AND mask LIKE('%c%')";
            // }
            //     else if (dealer.IsChecked == true)
            //         {
            //             h = h + " UPDATE DEMO.dbo.Account SET " +
            //             "state = 0 " +
            //             "WHERE memberid = " + id + " AND mask LIKE('%c%')";
            //         }
            // if (ander.IsChecked == false)
            // {
            //     h = h + " UPDATE DEMO.dbo.Account SET " +
            //      "state = 1 " +
            //      "WHERE memberid = " + id + " AND mask LIKE('%u%')";
            // }
            //     else if (ander.IsChecked == true)
            //         {
            //             h = h + " UPDATE DEMO.dbo.Account SET " +
            //             "state = 0 " +
            //             "WHERE memberid = " + id + " AND mask LIKE('%u%')";
            //         }
            // if (nominal.IsChecked == false)
            // {
            //     h = h + " UPDATE DEMO.dbo.Account SET " +
            //      "state = 1 " +
            //      "WHERE memberid = "+id+" AND mask LIKE('%o%')";
            // }
            //     else if (nominal.IsChecked == true)
            //         {
            //             h = h + " UPDATE DEMO.dbo.Account SET " +
            //             "state = 0 " +
            //             "WHERE memberid = " + id + " AND mask LIKE('%o%')";
            //         }
            // System.Data.SqlClient.SqlConnection sqlConnection1 =
            //new System.Data.SqlClient.SqlConnection(connectionString);

            // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            // cmd.CommandType = System.Data.CommandType.Text;
            // cmd.CommandText = " UPDATE demo.dbo.members SET " +
            //     //"type = '" + metype+ "', " +
            //     //"state= '" + statid + "', " +
            //     "broker= '" + Broker+ "', " +
            //     "dealer= '" + Dealer+ "', " +
            //     "ander= '" + Ander+ "', " +
            //     "nominal= '" + Nominal+ "', " +
            //     "startdate= '" + startT+ "', " +
            //     "enddate= '" + endT+ "', " +
            //     "modified = getdate() " +
            //     "WHERE id = '" + id + "' " +

            //     " UPDATE demo.dbo.Account SET "+
            //     " modified = GETDATE()," +
            //     " startdate = '" + startT + "'," +
            //     " enddate = '" + endT + "'," +
            //     //" state = '" + statid + "'" +
            //     " WHERE memberid = " + id + " "+h;

            // cmd.Connection = sqlConnection1;
            // sqlConnection1.Open();
            // cmd.ExecuteNonQuery();
            // sqlConnection1.Close();
            #endregion
            FillDataGrid();
        }
        #endregion
        #region combos
        private void bindCombo()
        {
            demoEntities10 st = new demoEntities10();
            participants.ItemsSource = st.Participants.ToList();
            linkMember.ItemsSource = st.Members.Where(s => s.type == 1).ToList();
        }
        #endregion
    }
}
