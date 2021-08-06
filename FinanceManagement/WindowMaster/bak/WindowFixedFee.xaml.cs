using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using FinanceManagement.Function;
using System.Collections.ObjectModel;

namespace FinanceManagement.WindowMaster
{
    /// <summary>
    /// Interaction logic for WindowFixedFee.xaml
    /// </summary>
    public partial class WindowFixedFee : Window
    {

        //ObservableCollection<MMFixedFee> mainMMFixedFees;

        public MMFeeStruct MMFeeStruct;

        public WindowFixedFee()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtFeeName.Text = MMFeeStruct.FeeName;

            ObservableCollection<MMFeeTypeStruct> itemSource =  DatabaseHandler.GetFeeTypeStruct(MMFeeStruct.FeeName);

            if(itemSource.Count() == 0)
            {
                itemSource.Add(new MMFeeTypeStruct()
                {
                    FeeType = MMFeeStruct.FeeName
                });
            }

            cbFeeType.ItemsSource = itemSource;
            cbFeeType.SelectedIndex = 0;

            cbDepartment.ItemsSource = DatabaseHandler.GetDepartmentMaster();
            cbDepartment.SelectedIndex = 0;

            gridFee.ItemsSource = DatabaseHandler.GetFixedFee();
            showAllHeader();
        }

        private void cbFeeName_DropDownClosed(object sender, EventArgs e)
        {
            var itemSource =  DatabaseHandler.GetFeeTypeStruct(cbFeeName.Text);

            if(itemSource.Count() == 0)
            {
                itemSource.Add(new MMFeeTypeStruct()
                {
                    FeeType = cbFeeName.Text
                });
            }

            cbFeeType.ItemsSource = itemSource;
            cbFeeType.SelectedIndex = 0;

            getFixedFee();
        }

        private void getFixedFee()
        {
            if (string.IsNullOrEmpty(cbFeeType.Text))
            {
                return;
            }

            if(cbDepartment.Text == "全部")
            {
                gridFee.ItemsSource = DatabaseHandler.GetFixedFee();
                showAllHeader();
            }
            else
            {
                mainMMFixedFees = DatabaseHandler.GetFixedFee(cbFeeName.Text, cbFeeType.Text, cbDepartment.Text);

                gridFee.ItemsSource = mainMMFixedFees;

                setHeaderVisibility();
            }
        }

        private void setHeaderVisibility()
        {
            MMFeeStruct mMFeeStruct = DatabaseHandler.GetFeeStruct(cbFeeName.Text);

            colDepartment.Visibility = Visibility.Hidden;
            colFeeName.Visibility = Visibility.Hidden;
            colFeeType.Visibility = Visibility.Hidden;

            if (string.IsNullOrEmpty(mMFeeStruct.Field1)) { colField1.Visibility = Visibility.Hidden; } else { colField1.Visibility = Visibility.Visible; colField1.Header = mMFeeStruct.Field1; }
            if (string.IsNullOrEmpty(mMFeeStruct.Field2)) { colField2.Visibility = Visibility.Hidden; } else { colField2.Visibility = Visibility.Visible; colField2.Header = mMFeeStruct.Field2; }
            if (string.IsNullOrEmpty(mMFeeStruct.Field3)) { colField3.Visibility = Visibility.Hidden; } else { colField3.Visibility = Visibility.Visible; colField3.Header = mMFeeStruct.Field3; }
            if (string.IsNullOrEmpty(mMFeeStruct.Field4)) { colField4.Visibility = Visibility.Hidden; } else { colField4.Visibility = Visibility.Visible; colField4.Header = mMFeeStruct.Field4; }
            if (string.IsNullOrEmpty(mMFeeStruct.Field5)) { colField5.Visibility = Visibility.Hidden; } else { colField5.Visibility = Visibility.Visible; colField5.Header = mMFeeStruct.Field5; }
            if (string.IsNullOrEmpty(mMFeeStruct.Field6)) { colField6.Visibility = Visibility.Hidden; } else { colField6.Visibility = Visibility.Visible; colField6.Header = mMFeeStruct.Field6; }
            if (string.IsNullOrEmpty(mMFeeStruct.Field7)) { colField7.Visibility = Visibility.Hidden; } else { colField7.Visibility = Visibility.Visible; colField7.Header = mMFeeStruct.Field7; }
            if (string.IsNullOrEmpty(mMFeeStruct.Field8)) { colField8.Visibility = Visibility.Hidden; } else { colField8.Visibility = Visibility.Visible; colField8.Header = mMFeeStruct.Field8; }
            if (string.IsNullOrEmpty(mMFeeStruct.Field9)) { colField9.Visibility = Visibility.Hidden; } else { colField9.Visibility = Visibility.Visible; colField9.Header = mMFeeStruct.Field9; }
            colDeleteRow.Visibility = Visibility.Visible;
            gridFee.IsEnabled = true;
        }

        private void cbFeeType_DropDownClosed(object sender, EventArgs e)
        {
            getFixedFee();
        }


        private void showAllHeader()
        {
            colDepartment.Visibility = Visibility.Visible;
            colFeeName.Visibility = Visibility.Visible;
            colFeeType.Visibility = Visibility.Visible;
            colField2.Visibility = Visibility.Hidden;
            colField3.Visibility = Visibility.Hidden;
            colField4.Visibility = Visibility.Hidden;
            colField5.Visibility = Visibility.Hidden;
            colField6.Visibility = Visibility.Hidden;
            colField7.Visibility = Visibility.Hidden;
            colField8.Visibility = Visibility.Hidden;
            colField9.Visibility = Visibility.Hidden;
            colDeleteRow.Visibility = Visibility.Hidden;
            gridFee.IsEnabled = false;
        }

        private void GridSplitter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if(cbDepartment.Text == "全部")
            {
                MessageBox.Show("部門を選んでください。");
                return;
            }

            if(string.IsNullOrEmpty(cbFeeType.Text))
            {
                MessageBox.Show("費タイプを選んでください。");
                return;
            }

            setHeaderVisibility();

            if(mainMMFixedFees == null)
            {
                mainMMFixedFees = new ObservableCollection<MMFixedFee>();
                gridFee.ItemsSource = mainMMFixedFees;
            }

            mainMMFixedFees.Add(new MMFixedFee()
            {
                FeeName = cbFeeName.Text,
                Department = cbDepartment.Text,
                FeeType = cbFeeType.Text,
                TimeFrom = DateTime.Today,
                TimeTo = DateTime.Today,
                Amount = 1000
            });

            btnSave.IsEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            saveData();
        }


        private void saveData()
        {
            if (mainMMFixedFees == null || mainMMFixedFees.Count() == 0)
            {
                if(DatabaseHandler.DeleteFixedFee(cbFeeName.Text, cbFeeType.Text, cbDepartment.Text))
                {
                    MessageBox.Show("成功");
                    btnSave.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("エラー");
                }
                return;
            }

            if (isAllowToSaveFixedFee(mainMMFixedFees))
            {
                if(DatabaseHandler.OverwriteFixedFee(mainMMFixedFees))
                {
                    MessageBox.Show("成功");
                    btnSave.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("エラー");
                }
            }
        }

        private bool isAllowToSaveFixedFee(ObservableCollection<MMFixedFee> mMFixedFees)
        {
            foreach(var temp in mMFixedFees)
            {
                if(string.IsNullOrEmpty(temp.Item) || (temp.TimeFrom >= temp.TimeTo) || temp.Amount == 0)
                {
                    _ = MessageBox.Show("入力データをエラーがあります。「項目が空又は始期、終期、金額のエラーがあります。」", "エラー");
                    return false;
                }
            }

            return true;
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.IsEnabled == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("データを保存しますか？", "報告", MessageBoxButton.YesNoCancel);

                if (messageBoxResult == MessageBoxResult.Cancel)
                    return;
                else if (messageBoxResult == MessageBoxResult.Yes)
                    saveData();
                else
                    btnSave.IsEnabled = false;
            }
            this.Close();
        }

        private void gridFee_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                btnSave.IsEnabled = true;
            }
        }

        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            MMFixedFee mMFee = ((FrameworkElement)sender).DataContext as MMFixedFee;

            if(MessageBox.Show(mMFee.Department + "の" + mMFee.Item + "を削除しますか？","報告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                mainMMFixedFees.Remove(mMFee);
                btnSave.IsEnabled = true;
            }
        }

        private void cbDepartment_DropDownClosed(object sender, EventArgs e)
        {
            getFixedFee();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (btnSave.IsEnabled == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("データを保存しますか？", "報告", MessageBoxButton.YesNoCancel);

                if (messageBoxResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
                else if (messageBoxResult == MessageBoxResult.Yes)
                    saveData();
            }
        }
    }
}
