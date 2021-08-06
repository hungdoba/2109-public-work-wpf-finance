using System;
using System.Windows;
using System.Collections.ObjectModel;
using FinanceManagement.Function;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;

namespace FinanceManagement.WindowReport
{
    /// <summary>
    /// Interaction logic for WindowHQIncomeReport.xaml
    /// </summary>
    public partial class WindowSale: Window
    {

        int cbYearPreviousValue = 0;

        ObservableCollection<MMFeeMaster> itemFeeMasters;

        ObservableCollection<MMSale> observableSales;

        public WindowSale()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int yearNow = setYear();

            getData(yearNow);

            getItemMaster("売上");
        }

        private void getItemMaster(string feeName)
        {
            itemFeeMasters = DatabaseHandler.GetFeeMaster(feeName);
            cbCustomer.ItemsSource = itemFeeMasters;
        }

        private void getData(int year)
        {
            if (year < 2000) return;

            observableSales = DatabaseHandler.GetSale(year);

            setGridHeader(year);

            gridSale.ItemsSource = observableSales;

        }


        private int setYear()
        {
            int yearNow = DateTime.Today.Year;
            for(int i = yearNow; i > 2012; i--)
            {
                cbYear.Items.Add(i);
            }
            cbYear.SelectedIndex = 0;
            return yearNow;
        }


        private void setGridHeader(int year)
        {
            columnMonth1.Header =   (year - 2000 + 1).ToString() + "年１月";
            columnMonth2.Header =   (year - 2000 + 1).ToString() + "年２月";
            columnMonth3.Header =   (year - 2000 + 1).ToString() + "年３月";
            columnMonth4.Header =   (year - 2000).ToString() + "年４月";
            columnMonth5.Header =   (year - 2000).ToString() + "年５月";
            columnMonth6.Header =   (year - 2000).ToString() + "年６月";
            columnMonth7.Header =   (year - 2000).ToString() + "年７月";
            columnMonth8.Header =   (year - 2000).ToString() + "年８月";
            columnMonth9.Header =   (year - 2000).ToString() + "年９月";
            columnMonth10.Header =  (year - 2000).ToString() + "年１０月";
            columnMonth11.Header =  (year - 2000).ToString() + "年１１月";
            columnMonth12.Header =  (year - 2000).ToString() + "年１２月";

        }


        private void gridSale_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {

                MMSale mMSale = (MMSale)e.Row.Item;

                TextBox textBox = e.EditingElement as TextBox;
                if (!int.TryParse(textBox.Text, out int newValue))
                {
                    newValue = 0;
                }

                string header = e.Column.Header.ToString();

                if (header.Contains("１０月"))
                {
                    mMSale.Month10 = newValue;
                }
                else if (header.Contains("１１月"))
                {
                    mMSale.Month11 = newValue;
                }
                else if (header.Contains("１２月"))
                {
                    mMSale.Month12 = newValue;
                }
                else if (header.Contains("１月"))
                {
                    mMSale.Month1 = newValue;
                }
                else if (header.Contains("２月"))
                {
                    mMSale.Month2 = newValue;
                }
                else if (header.Contains("３月"))
                {
                    mMSale.Month3 = newValue;
                }
                else if (header.Contains("４月"))
                {
                    mMSale.Month4 = newValue;
                }
                else if (header.Contains("５月"))
                {
                    mMSale.Month5 = newValue;
                }
                else if (header.Contains("６月"))
                {
                    mMSale.Month6 = newValue;
                }
                else if (header.Contains("７月"))
                {
                    mMSale.Month7 = newValue;
                }
                else if (header.Contains("８月"))
                {
                    mMSale.Month8 = newValue;
                }
                else if (header.Contains("９月"))
                {
                    mMSale.Month9 = newValue;
                }

                DatabaseHandler.UpdateSumInSale(ref observableSales, mMSale.Department);

                btnSave.IsEnabled = true;
            }
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            saveData();
        }

        private void saveData()
        {
            if (observableSales == null || observableSales.Count == 0) return;

            bool saveResult = DatabaseHandler.OverwriteSale(observableSales);

            btnSave.IsEnabled = false;

            if(saveResult == true)
                MessageBox.Show("成功");
            else
                MessageBox.Show("エラー");
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {

            if (observableSales == null || observableSales.Count == 0) 
            {
                observableSales = new ObservableCollection<MMSale>();
            };

            if (string.IsNullOrEmpty(cbCustomer.Text) || cbCustomer.Text.Contains("会社名"))
            {
                cbCustomer.Text = "会社名";
                MessageBox.Show("会社名を記入してください。");
                return;
            }

            string department = string.Empty;
            if (btnOta.IsChecked == true) department = "太田";
            else if (btnHQ.IsChecked == true) department = "本社";
            else if (btnSDC.IsChecked == true) department = "SDC";
            else if (btnHQWork.IsChecked == true) department = "工事";

            MMSale mMSale = new MMSale()
            {
                Department = department,
                Customer = cbCustomer.Text,
                Year = (int)cbYear.SelectedItem,
                Month1 = 0,
                Month2 = 0,
                Month3 = 0,
                Month4 = 0,
                Month5 = 0,
                Month6 = 0,
                Month7 = 0,
                Month8 = 0,
                Month9 = 0,
                Month10 = 0,
                Month11 = 0,
                Month12 = 0,
                Sum = 0,
                Remark = null
            };

            int index = -1;

            foreach(var temp in observableSales)
            {
                if (temp.Department.Contains(department))
                {
                    index = observableSales.IndexOf(temp);
                    if(temp.Customer == cbCustomer.Text)
                    {
                        MessageBox.Show("同じ会社の売上を存在しました、追加ができない。");
                        return;
                    }
                }
            }

            observableSales.Insert(index + 1, mMSale);

            if (index == -1) // Not have value yet
            {
                mMSale = new MMSale()
                {
                    Department = "合計",
                    Customer = department + "合計",
                    Year = (int)cbYear.SelectedItem,
                    Month1 = 0,
                    Month2 = 0,
                    Month3 = 0,
                    Month4 = 0,
                    Month5 = 0,
                    Month6 = 0,
                    Month7 = 0,
                    Month8 = 0,
                    Month9 = 0,
                    Month10 = 0,
                    Month11 = 0,
                    Month12 = 0,
                    Sum = 0,
                    Remark = null
                };

                observableSales.Insert(1, mMSale);

                gridSale.ItemsSource = observableSales;
            }

            btnSave.IsEnabled = true;

        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            btnHQ.IsChecked = false;
            btnOta.IsChecked = false;
            btnHQWork.IsChecked = false;
            btnSDC.IsChecked = false;

            ToggleButton toggleButton = (ToggleButton)sender;

            toggleButton.IsChecked = true;
        }

        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            MMSale mMSale = ((FrameworkElement)sender).DataContext as MMSale;

            if(MessageBox.Show(mMSale.Department + "の" + mMSale.Customer + "を削除しますか？","報告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                observableSales.Remove(mMSale);

                DatabaseHandler.UpdateSumInSale(ref observableSales, mMSale.Department);

                btnSave.IsEnabled = true;
            }
        }


        private void btnAllowDeleteRow_Click(object sender, RoutedEventArgs e)
        {

            colDeleteRow.Visibility = btnAllowDeleteRow.IsChecked == true? Visibility.Visible : Visibility.Hidden;

        }

        private void cbYear_DropDownClosed(object sender, EventArgs e)
        {

            if (observableSales != null && observableSales.Count != 0 && observableSales[0].Year == (int)cbYear.SelectedItem)
                return;

            if (btnSave.IsEnabled == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("データを保存しますか？", "報告", MessageBoxButton.YesNoCancel);

                if (messageBoxResult == MessageBoxResult.Cancel)
                {
                    cbYear.SelectedItem = cbYearPreviousValue;
                    return;
                }
                else if (messageBoxResult == MessageBoxResult.Yes)
                {
                    saveData();
                }
                else
                {
                    btnSave.IsEnabled = false;
                }
            }

            ComboBox comboBox = (ComboBox)sender;
            getData((int)comboBox.SelectedItem);
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

        private void cbYear_DropDownOpened(object sender, EventArgs e)
        {
            cbYearPreviousValue = (int)cbYear.SelectedItem;
        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = int.TryParse(Clipboard.GetText(), out int _);
            e.Handled = true;
        }

        private void Paste(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (!int.TryParse(Clipboard.GetText(), out int pasteValue))
            {
                return;
            }

            btnSave.IsEnabled = true;

            var dataGridCellInfos = gridSale.SelectedCells;

            foreach (var dataGridCellInfo in dataGridCellInfos)
            {

                MMSale mMSale = (MMSale)dataGridCellInfo.Item;

                if (mMSale.Department == "合計") continue;

                string header = dataGridCellInfo.Column.Header.ToString();

                if (header.Contains("１０月"))
                {
                    mMSale.Month10 = pasteValue;
                }
                else if (header.Contains("１１月"))
                {
                    mMSale.Month11 = pasteValue;
                }
                else if (header.Contains("１２月"))
                {
                    mMSale.Month12 = pasteValue;
                }
                else if (header.Contains("１月"))
                {
                    mMSale.Month1 = pasteValue;
                }
                else if (header.Contains("２月"))
                {
                    mMSale.Month2 = pasteValue;
                }
                else if (header.Contains("３月"))
                {
                    mMSale.Month3 = pasteValue;
                }
                else if (header.Contains("４月"))
                {
                    mMSale.Month4 = pasteValue;
                }
                else if (header.Contains("５月"))
                {
                    mMSale.Month5 = pasteValue;
                }
                else if (header.Contains("６月"))
                {
                    mMSale.Month6 = pasteValue;
                }
                else if (header.Contains("７月"))
                {
                    mMSale.Month7 = pasteValue;
                }
                else if (header.Contains("８月"))
                {
                    mMSale.Month8 = pasteValue;
                }
                else if (header.Contains("９月"))
                {
                    mMSale.Month9 = pasteValue;
                }
            }

            DatabaseHandler.UpdateSumInSale(observableSales);

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (observableSales == null) return;
            ExcelHandler.ExportExcelSale(observableSales);
        }

        private void GridSplitter_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (colControl.Width != new GridLength(0))
            {
                colControl.Width = new GridLength(0);
            }
            else
            {
                colControl.Width = new GridLength(300);
            }
        }

    }
}
