using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using FinanceManagement.Function;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace FinanceManagement.WindowImport
{
    /// <summary>
    /// Interaction logic for WindowImportSale.xaml
    /// </summary>
    public partial class WindowImportSale : Window
    {

        private int year;

        private string department = "工事";

        ObservableCollection<MMSale> mainMMSales;

        public WindowImportSale()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setYear();
            setColumnHeader();
            setTabSaleHeader();
            setCustomerMaster();
        }

        private void setYear()
        {
            year = DateTime.Today.Year;
            for(int i = year; i > 2012; i--)
            {
                _ = cbYear.Items.Add(i);
            }
            cbYear.SelectedIndex = 0;
        }

        private void setColumnHeader()
        {
            columnMonth1.Header  = $"{year - 2000 + 1}年１月";
            columnMonth2.Header  = $"{year - 2000 + 1}年２月";
            columnMonth3.Header  = $"{year - 2000 + 1}年３月";
            columnMonth4.Header  = $"{year - 2000}年４月";
            columnMonth5.Header  = $"{year - 2000}年５月";
            columnMonth6.Header  = $"{year - 2000}年６月";
            columnMonth7.Header  = $"{year - 2000}年７月";
            columnMonth8.Header  = $"{year - 2000}年８月";
            columnMonth9.Header  = $"{year - 2000}年９月";
            columnMonth10.Header  = $"{year - 2000}年１０月";
            columnMonth11.Header  = $"{year - 2000}年１１月";
            columnMonth12.Header  = $"{year - 2000}年１２月";
        }


        private void setTabSaleHeader()
        {
            tabSale.Header = department + "の売上";
        }

        private void setCustomerMaster()
        {
            cbCustomer.ItemsSource = DatabaseHandler.GetCustomerUsed();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            setColumnHeader();

             if (department == "工事")
            {
                mainMMSales = DatabaseHandler.GetHQMMSale(year);
                gridSale.ItemsSource = mainMMSales;
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
                };
                _ = openFileDialog.ShowDialog();

                if (!string.IsNullOrEmpty(openFileDialog.FileName) && File.Exists(openFileDialog.FileName))
                {
                    mainMMSales = ExcelHandler.GetExcelMMSale(openFileDialog.FileName, year, department);
                    gridSale.ItemsSource = mainMMSales;
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cbYear_DropDownClosed(object sender, EventArgs e)
        {
            int newYear = (int)cbYear.SelectedValue;
            if(newYear != year)
            {
                year = newYear;
                gridSale.ItemsSource = null;
                setColumnHeader();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(mainMMSales == null || mainMMSales.Count == 0)
            {
                _ = MessageBox.Show("データが空です。", "エラー");
                return;
            }

            if(btnSave.Content.ToString() == "検査や保存")
            {
                List<MMSale>[] mmInput = DatabaseHandler.GetSaleConflictValue(mainMMSales, year, department);

                if(mmInput[0].Count != 0)
                {
                    WindowSelectConflictValue windowSelectConflictValue = new WindowSelectConflictValue();
                    windowSelectConflictValue.Init(mmInput);
                    _ = windowSelectConflictValue.ShowDialog();

                    if (windowSelectConflictValue.DialogResult == false)
                    {
                        return;
                    }

                    foreach (MMSale mMSale in windowSelectConflictValue.MMConfirmed)
                    {
                        foreach (MMSale mSale in mainMMSales)
                        {
                            if (mSale.Customer == mMSale.Customer)
                            {
                                mSale.Month1 = mMSale.Month1;
                                mSale.Month2 = mMSale.Month2;
                                mSale.Month3 = mMSale.Month3;
                                mSale.Month4 = mMSale.Month4;
                                mSale.Month5 = mMSale.Month5;
                                mSale.Month6 = mMSale.Month6;
                                mSale.Month7 = mMSale.Month7;
                                mSale.Month8 = mMSale.Month8;
                                mSale.Month9 = mMSale.Month9;
                                mSale.Month10 = mMSale.Month10;
                                mSale.Month11 = mMSale.Month11;
                                mSale.Month12 = mMSale.Month12;
                                mSale.Sum = mSale.Month1 + mSale.Month2 + mSale.Month3 + mSale.Month4 + mSale.Month5 + mSale.Month6 + mSale.Month7 + mSale.Month8 + mSale.Month9 + mSale.Month10 + mSale.Month11 + mSale.Month12;
                                break;
                            }
                        }
                    }
                }

                MessageBox.Show("データの検査が完了しました、保存するために「保存」ボタンをクリックしてください。","報告");

                btnSave.Content = "保存";
            }
            else
            {
                if (DatabaseHandler.SaveSale(mainMMSales))
                {
                    btnSave.Content = "検査や保存";
                    MessageBox.Show("成功");
                }
                else
                {
                    MessageBox.Show("エラー");
                }

            }
        }

        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            MMSale mMSale = ((FrameworkElement)sender).DataContext as MMSale;

            _ = mainMMSales.Remove(mMSale);
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton)sender;
            if(toggleButton.IsChecked == true)
            {
                btnHQ.IsChecked = false;
                btnOta.IsChecked = false;
                btnSDC.IsChecked = false;
                btnHQWork.IsChecked = false;

                department = toggleButton.Content.ToString();
                mainMMSales = new ObservableCollection<MMSale>() ;
                gridSale.ItemsSource = null;
                setTabSaleHeader();
            }
            toggleButton.IsChecked = true;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cbCustomer.Text) || cbCustomer.Text == "会社名")
            {
                MessageBox.Show("会社名を記入してください。");
                return;
            }

            if(mainMMSales == null)
            {
                mainMMSales = new ObservableCollection<MMSale>();
            }

            foreach(var temp in mainMMSales)
            {
                if(temp.Customer == cbCustomer.Text)
                {
                    gridSale.SelectedValue = temp;
                    MessageBox.Show("会社が存在しました。追加ができません");
                    gridSale.Focus();
                    return;
                }
            }

            MMSale mMSale = new MMSale()
            {
                Department = department,
                Customer = cbCustomer.Text,
                Year = year
            };

            mainMMSales.Add(mMSale);
            if(mainMMSales.Count == 1)
            {
                gridSale.ItemsSource = mainMMSales;
            }
        }

        private void btnAllowDeleteRow_Click(object sender, RoutedEventArgs e)
        {

            colDeleteRow.Visibility = btnAllowDeleteRow.IsChecked == true? Visibility.Visible : Visibility.Hidden;

        }

        private void gridSale_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                MMSale mmSale = (MMSale)e.Row.Item;

                if (!(e.EditingElement is TextBox textBox))
                {
                    return;
                }

                if (!int.TryParse(textBox.Text, out int newValue))
                {
                    newValue = 0;
                }

                string header = e.Column.Header.ToString();

                if (header.Contains("１０月"))
                {
                    mmSale.Month10 = newValue;
                }
                else if (header.Contains("１１月"))
                {
                    mmSale.Month11 = newValue;
                }
                else if (header.Contains("１２月"))
                {
                    mmSale.Month12 = newValue;
                }
                else if (header.Contains("１月"))
                {
                    mmSale.Month1 = newValue;
                }
                else if (header.Contains("２月"))
                {
                    mmSale.Month2 = newValue;
                }
                else if (header.Contains("３月"))
                {
                    mmSale.Month3 = newValue;
                }
                else if (header.Contains("４月"))
                {
                    mmSale.Month4 = newValue;
                }
                else if (header.Contains("５月"))
                {
                    mmSale.Month5 = newValue;
                }
                else if (header.Contains("６月"))
                {
                    mmSale.Month6 = newValue;
                }
                else if (header.Contains("７月"))
                {
                    mmSale.Month7 = newValue;
                }
                else if (header.Contains("８月"))
                {
                    mmSale.Month8 = newValue;
                }
                else if (header.Contains("９月"))
                {
                    mmSale.Month9 = newValue;
                }

                DatabaseHandler.UpdateSumInSale(ref mainMMSales);

                btnSave.IsEnabled = true;
            }
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

            DatabaseHandler.UpdateSumInSale(mainMMSales);

        }
    }
}
