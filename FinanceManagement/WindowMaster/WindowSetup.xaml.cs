using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using FinanceManagement.Function;
using System.Windows.Controls;
using System.Collections.Generic;
using FinanceManagement.WindowMaster;

namespace FinanceManagement.WindowReport
{
    /// <summary>
    /// Interaction logic for WindowHQIncomeReport.xaml
    /// </summary>
    public partial class WindowSetup : Window
    {
        public string FeeNameToSetField;

        private ObservableCollection<MMFeeStruct> mainMMFeeStructs;

        private ObservableCollection<MMFeeTypeStruct> mainMMFeeTypeStructs;

        public WindowSetup()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            mainMMFeeStructs = DatabaseHandler.GetFeeName("セイキョウ");

            gridFeeStruct.ItemsSource = mainMMFeeStructs;

            if(!string.IsNullOrEmpty(FeeNameToSetField))
            {
                MMFeeStruct mMFeeStruct = DatabaseHandler.GetFeeStruct(FeeNameToSetField);

                List<MMFeeStruct> mMFeeStructs = new List<MMFeeStruct>()
                {
                    mMFeeStruct
                };

                tabFeeField.IsEnabled = true;

                tabFeeType.IsEnabled = true;

                gridFeeField.ItemsSource = DatabaseHandler.GetFeeTypeStruct(FeeNameToSetField);


                mainMMFeeTypeStructs = DatabaseHandler.GetFeeTypeStruct(mMFeeStruct.FeeName);

                gridFeeType.ItemsSource = mainMMFeeTypeStructs;

                tabFeeType.Header = mMFeeStruct.FeeName + "の費タイプ";

                gridFeeField.ItemsSource = mMFeeStructs;

                tabFeeField.Header = mMFeeStruct.FeeName + "の項目";

                _ = tabFeeField.Focus();
            }

        }

        private bool saveData(int tabSelectedIndex)
        {
            if(tabSelectedIndex == -1)
            {
                tabSelectedIndex = tabControl.SelectedIndex;
            }

            bool saveResult = false;

            if (tabSelectedIndex == 0)
            {
                if (mainMMFeeStructs == null || mainMMFeeStructs.Count == 0)
                {
                    _ = MessageBox.Show("データが空ですから、保存できない。");
                    return false;
                }

                IEnumerable<MMFeeStruct> emptyFeeName = mainMMFeeStructs.Where(x => string.IsNullOrEmpty(x.FeeName));
                if (emptyFeeName.Count() > 0)
                {
                    _ = MessageBox.Show("空費タイプがありますから。保存できないです。");
                    return false;
                }

                saveResult = DatabaseHandler.OverWriteFeeStruct(mainMMFeeStructs);
            }
            else if (tabSelectedIndex == 1 || tabSelectedIndex == 2)
            {
                string feeName = tabFeeType.Header.ToString().Replace("の費タイプ", "");

                saveResult = mainMMFeeTypeStructs == null || mainMMFeeTypeStructs.Count == 0
                    ? DatabaseHandler.DeleteFeeTypeStruct(feeName)
                    : DatabaseHandler.OverWriteFeeTypeStruct(feeName, mainMMFeeTypeStructs);

                MMFeeStruct temp = (MMFeeStruct)gridFeeField.Items[0];
                saveResult = DatabaseHandler.UpdateFeeStruct(temp);
            }

            if (saveResult)
            {
                btnSave.IsEnabled = false;
                return true;
            }
            else
            {
                btnSave.IsEnabled = true;
                return false;
            }
        }

        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (gridFeeStruct.SelectedItem == null || gridFeeStruct.SelectedItem.GetType() != typeof(MMFeeStruct))
            {
                return;
            }

            if (isHandledDeleteFee())
            {
                e.Handled = true;
            }
        }

        private bool isHandledDeleteFee()
        {
            MMFeeStruct mMFeeStruct = (MMFeeStruct)gridFeeStruct.SelectedItem;

            if (DatabaseHandler.IsFeeNameDelectable(mMFeeStruct))
            {
                if (MessageBox.Show($"{mMFeeStruct.FeeName}を削除しますか？", "報告", MessageBoxButton.YesNoCancel) != MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    if (mainMMFeeStructs.Count < 2)
                    {
                        MessageBox.Show("削除ができません。まず新しい項目を追加して、後は削除ができます。");
                        return true;
                    }
                    else
                    {
                        mainMMFeeStructs.Remove(mMFeeStruct);
                        btnSave.IsEnabled = true;
                        return false;
                    }
                }
            }
            else
            {
                MessageBox.Show("削除が出来ません、データベースに費名を使用しています");
                return true;
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _ = saveData(-1) ? MessageBox.Show("成功") : MessageBox.Show("エラー");
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.IsEnabled)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("データを保存しますか？", "報告", MessageBoxButton.YesNoCancel);

                if (messageBoxResult == MessageBoxResult.Cancel)
                {
                    return;
                }
                else if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _ = saveData(-1) ? MessageBox.Show("成功") : MessageBox.Show("エラー");
                }
                else
                {
                    btnSave.IsEnabled = false;
                }
            }
            Close();
        }

        private void gridFeeStruct_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Track for Commit
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var textBox = e.EditingElement as TextBox;

                if (string.IsNullOrEmpty(textBox.Text))
                    e.Cancel = true;

                string header = e.Column.Header.ToString();

                if (header == "費名")
                {
                    foreach (var temp in mainMMFeeStructs)
                    {
                        if (temp.FeeName == textBox.Text)
                        {
                            MessageBox.Show("費名を存在しました");
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                btnSave.IsEnabled = true;
            }
        }

        private void gridFeeStruct_PreviewCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                if (isHandledDeleteFee() == true)
                    e.Handled = true;
            }
        }

        private void gridFeeType_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Track for Commit
            if (e.EditAction == DataGridEditAction.Commit)
            {
                TextBox textBox = e.EditingElement as TextBox;

                if (string.IsNullOrEmpty(textBox.Text))
                {
                    e.Cancel = true;
                }

                string header = e.Column.Header.ToString();

                if (header == "費タイプ")
                {
                    foreach (MMFeeTypeStruct temp in mainMMFeeTypeStructs)
                    {
                        if (temp.FeeType == textBox.Text)
                        {
                            _ = MessageBox.Show("費タイプを存在しました");
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                btnSave.IsEnabled = true;
            }
        }

        private void gridFeeType_PreviewCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                if (isHandledDeleteFeeType() == true)
                {
                    e.Handled = true;
                }
                else
                {
                    btnSave.IsEnabled = true;
                }
            }
        }

        private bool isHandledDeleteFeeType()
        {
            var deleteItem = gridFeeType.SelectedItem;

            if (deleteItem.GetType() != typeof(MMFeeTypeStruct)) return true;

            MMFeeTypeStruct mMFeeTypeStruct = (MMFeeTypeStruct)deleteItem;

            if (DatabaseHandler.IsFeeTypeDelectable(mMFeeTypeStruct))
            {
                if (MessageBox.Show($"{mMFeeTypeStruct.FeeType}を削除しますか？", "報告", MessageBoxButton.YesNoCancel) != MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("削除が出来ません、データベースに費タイプを使用しています");
                return true;
            }
        }

        private void btnDeleteFeeTypeRow_Click(object sender, RoutedEventArgs e)
        {
            var deleteItem = gridFeeType.SelectedItem;

            if (deleteItem.GetType() != typeof(MMFeeTypeStruct)) return;

            MMFeeTypeStruct mMFeeTypeStruct = (MMFeeTypeStruct)deleteItem;

            if (DatabaseHandler.IsFeeTypeDelectable(mMFeeTypeStruct))
            {
                if (MessageBox.Show($"{mMFeeTypeStruct.FeeType}を削除しますか？", "報告", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                {
                    mainMMFeeTypeStructs.Remove(mMFeeTypeStruct);
                    btnSave.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("削除が出来ません、データベースに費タイプを使用しています");
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (tabFeeStruct.IsSelected)
                {
                    tabFeeType.IsEnabled = false;
                    tabFeeField.IsEnabled = false;

                    if(btnSave.IsEnabled)
                    {
                        _ = MessageBox.Show("費タイプや費項目を保存しますか？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes ? MessageBox.Show("成功") : MessageBox.Show("エラー");
                    }
                }
            }
        }

        private void gridFeeField_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Track for Commit
            if (e.EditAction == DataGridEditAction.Commit)
            {
                btnSave.IsEnabled = true;
            }

        }

        private void gridFeeField_PreviewCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                e.Handled = true;
            }
        }

        private void btnFeeSetup_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.IsEnabled)
            {
                MessageBoxResult result = MessageBox.Show("費名のデータが保存しますか？", "警告", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    if (saveData(-1) == false)
                    {
                        return;
                    }
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
                else
                {
                    mainMMFeeStructs = DatabaseHandler.GetFeeName("セイキョウ");
                    gridFeeStruct.ItemsSource = mainMMFeeStructs;
                    return;
                }
            }

            object selectItem = gridFeeStruct.SelectedItem;
            if (selectItem == null)
            {
                return;
            }

            if (selectItem is MMFeeStruct @struct)
            {
                tabFeeField.IsEnabled = true;
                tabFeeType.IsEnabled = true;


                MMFeeStruct mMFeeStruct = @struct;

                mainMMFeeTypeStructs = DatabaseHandler.GetFeeTypeStruct(mMFeeStruct.FeeName);
                gridFeeType.ItemsSource = mainMMFeeTypeStructs;

                tabFeeType.Header = mMFeeStruct.FeeName + "の費タイプ";
                _ = tabFeeType.Focus();

                List<MMFeeStruct> mMFeeStructs = new List<MMFeeStruct>()
                {
                    mMFeeStruct
                };

                gridFeeField.ItemsSource = mMFeeStructs;
                tabFeeField.Header = mMFeeStruct.FeeName + "の項目";
            }
        }

        private void GridSplitter_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            colControl.Width = colControl.Width != new GridLength(0) ? new GridLength(0) : new GridLength(300);
        }

        private void gridFeeType_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            MMFeeTypeStruct mMFeeTypeStruct = e.Row.DataContext as MMFeeTypeStruct;
            if(e.Column.Header.ToString().Contains("費タイプ") && !string.IsNullOrEmpty(mMFeeTypeStruct.FeeType))
            {
                e.Cancel = true;
            }
        }

        private void gridFeeStruct_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            MMFeeStruct mMFeeStruct = e.Row.DataContext as MMFeeStruct;
            if(e.Column.Header.ToString().Contains("費名") && !string.IsNullOrEmpty(mMFeeStruct.FeeName))
            {
                e.Cancel = true;
            }
        }

        private void btnFixedFee_Click(object sender, RoutedEventArgs e)
        {
            //WindowFixedFee windowFixedFee = new WindowFixedFee();
            //windowFixedFee.ShowDialog();
        }

        private void btnMaster_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = gridFeeStruct.SelectedItem;

            if(selectedItem == null)
            {
                return;
            }

            if(selectedItem is MMFeeStruct mMFeeStruct)
            {
                WindowFeeMaster windowFeeMaster = new WindowFeeMaster()
                {
                    MMFeeStruct = mMFeeStruct
                };
                windowFeeMaster.ShowDialog();
            }

        }
    }
}
