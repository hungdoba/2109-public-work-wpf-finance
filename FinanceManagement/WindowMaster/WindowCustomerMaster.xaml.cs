using System;
using System.Windows;
using System.Collections.Generic;
using FinanceManagement.Function;
using System.Collections.ObjectModel;

namespace FinanceManagement.WindowMaster
{
    /// <summary>
    /// Interaction logic for WindowCustomerMaster.xaml
    /// </summary>
    public partial class WindowCustomerMaster : Window
    {

        private bool isDataChanged = false;

        public WindowCustomerMaster()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<MMCustomerMaster>[] customerMasters = DatabaseHandler.GetCustomerMaster();

            ObservableCollection<object> collection = new ObservableCollection<object>();

            foreach (var temp in customerMasters[0])
            {
                collection.Add(temp);
            }
            ctrMasterUsed.ObjectMasters = collection;

            collection = new ObservableCollection<object>();
            foreach (var temp in customerMasters[1])
            {
                collection.Add(temp);
            }
            ctrMasterUseless.ObjectMasters = collection;

        }


        private void masterUsed_RequestMove(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ctrMasterUseless.ObjectMasters.Add(e.NewValue);
        }

        private void masterUseless_RequestMove(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ctrMasterUsed.ObjectMasters.Add(e.NewValue);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(cbName.Text))
            {
                MessageBox.Show("情報がまだ足りません。");
            }
            else
            {

                MMCustomerMaster mMCustomerMaster = new MMCustomerMaster()
                {
                    Name = cbName.Text,
                    ShortName = cbShortName.Text,
                    Remark = cbRemark.Text,
                    IsUse = true
                };

                string customerName = cbName.Text.Replace("（株）", "").Replace("（有）", "");

                foreach (var temp in ctrMasterUsed.ObjectMasters)
                {
                    if (((MMCustomerMaster)temp).Name.Replace("（株）", "").Replace("（有）", "") == customerName)
                    {
                        ctrMasterUsed.gridMaster.SelectedItem = temp;
                        ctrMasterUsed.gridMaster.ScrollIntoView(temp);
                        MessageBox.Show($"会社名が存在しました。");
                        return;
                    }
                }

                foreach (var temp in ctrMasterUseless.ObjectMasters)
                {
                    if (((MMCustomerMaster)temp).Name.Replace("（株）", "").Replace("（有）", "") == customerName)
                    {
                        ctrMasterUseless.gridMaster.SelectedItem = temp;
                        ctrMasterUseless.gridMaster.ScrollIntoView(temp);
                        MessageBox.Show($"会社名が存在しました。");
                        return;
                    }
                }

                ctrMasterUsed.ObjectMasters.Add(mMCustomerMaster);

                ctrMasterUsed.gridMaster.ScrollIntoView(mMCustomerMaster);

                isDataChanged = true;

            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ctrMasterUsed.gridMaster.SelectedValue != null)
            {
                object deleteObject = ctrMasterUsed.gridMaster.SelectedItem;
                ctrMasterUsed.ObjectMasters.Remove(deleteObject);
                isDataChanged = true;
            }
            else if(ctrMasterUseless.gridMaster.SelectedValue != null)
            {
                object deleteObject = ctrMasterUseless.gridMaster.SelectedItem;
                ctrMasterUseless.ObjectMasters.Remove(deleteObject);
                isDataChanged = true;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ctrMasterUsed_GotFocus(object sender, RoutedEventArgs e)
        {
            ctrMasterUseless.gridMaster.UnselectAll();
        }

        private void ctrMasterUseless_GotFocus(object sender, RoutedEventArgs e)
        {
            ctrMasterUsed.gridMaster.UnselectAll();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            saveWork();
            isDataChanged = false;
        }

        private void saveWork()
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();
            int Id = 1;
            List<MMCustomerMaster> customerNameMasters = new List<MMCustomerMaster>();
            foreach(var temp in ctrMasterUsed.gridMaster.Items)
            {
                MMCustomerMaster customerNameMaster = (MMCustomerMaster)temp;
                customerNameMaster.ID = Id;
                customerNameMaster.IsUse = true;
                customerNameMasters.Add(customerNameMaster);
                Id++;
            }
            foreach(var temp in ctrMasterUseless.gridMaster.Items)
            {
                MMCustomerMaster customerNameMaster = (MMCustomerMaster)temp;
                customerNameMaster.ID = Id;
                customerNameMaster.IsUse = false;
                customerNameMasters.Add(customerNameMaster);
                Id++;
            }

            hQDataDataContext.MMCustomerMasters.DeleteAllOnSubmit(hQDataDataContext.MMCustomerMasters);
            foreach (MMCustomerMaster temp in customerNameMasters)
            {
                hQDataDataContext.MMCustomerMasters.InsertOnSubmit(temp);
            }
            hQDataDataContext.SubmitChanges();
            MessageBox.Show("成功");
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GridSplitter_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (colControl.Width != new GridLength(0, GridUnitType.Star))
                colControl.Width = new GridLength(0, GridUnitType.Star);
            else
                colControl.Width = new GridLength(250);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(isDataChanged == true)
            {
                if(MessageBox.Show("データを変更しましたが、保存しますか。","報告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    saveWork();
                }
            }
        }
    }
}
