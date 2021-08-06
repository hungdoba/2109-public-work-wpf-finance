using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FinanceManagement.Function;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;

namespace FinanceManagement.WindowMaster
{
    /// <summary>
    /// Interaction logic for WindowHQIncomeReport.xaml
    /// </summary>
    public partial class WindowFeeMaster: Window
    {
        private bool isDragging { get; set; }

        private MMFeeMaster draggedItem { get; set; }

        private ObservableCollection<MMFeeMaster> mMFeeMasters;

        public MMFeeStruct MMFeeStruct;

        public WindowFeeMaster()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbWindowName.Content = MMFeeStruct.FeeName;

            setGridFeeHeader();

            getData(MMFeeStruct.FeeName);
        }

        private void setGridFeeHeader()
        {
            MMFeeStruct mMFieldName = DatabaseHandler.GetFieldName(MMFeeStruct.FeeName);

            if(mMFieldName == null)
            {
                colField1.Visibility = Visibility.Hidden;
                colField2.Visibility = Visibility.Hidden;
                colField3.Visibility = Visibility.Hidden;
                colField4.Visibility = Visibility.Hidden;
                colField5.Visibility = Visibility.Hidden;
                colField6.Visibility = Visibility.Hidden;
                colField7.Visibility = Visibility.Hidden;
                colField8.Visibility = Visibility.Hidden;
                colField9.Visibility = Visibility.Hidden;
                return;
            }

            colField1.Header = !string.IsNullOrEmpty(mMFieldName.Field1) ? mMFieldName.Field1 : null;
            colField2.Header = !string.IsNullOrEmpty(mMFieldName.Field2) ? mMFieldName.Field2 : null;
            colField3.Header = !string.IsNullOrEmpty(mMFieldName.Field3) ? mMFieldName.Field3 : null;
            colField4.Header = !string.IsNullOrEmpty(mMFieldName.Field4) ? mMFieldName.Field4 : null;
            colField5.Header = !string.IsNullOrEmpty(mMFieldName.Field5) ? mMFieldName.Field5 : null;
            colField6.Header = !string.IsNullOrEmpty(mMFieldName.Field6) ? mMFieldName.Field6 : null;
            colField7.Header = !string.IsNullOrEmpty(mMFieldName.Field7) ? mMFieldName.Field7 : null;
            colField8.Header = !string.IsNullOrEmpty(mMFieldName.Field8) ? mMFieldName.Field8 : null;
            colField9.Header = !string.IsNullOrEmpty(mMFieldName.Field9) ? mMFieldName.Field9 : null;

            colField1.Visibility = string.IsNullOrEmpty(mMFieldName.Field1) ? Visibility.Hidden : Visibility.Visible;
            colField2.Visibility = string.IsNullOrEmpty(mMFieldName.Field2) ? Visibility.Hidden : Visibility.Visible;
            colField3.Visibility = string.IsNullOrEmpty(mMFieldName.Field3) ? Visibility.Hidden : Visibility.Visible;
            colField4.Visibility = string.IsNullOrEmpty(mMFieldName.Field4) ? Visibility.Hidden : Visibility.Visible;
            colField5.Visibility = string.IsNullOrEmpty(mMFieldName.Field5) ? Visibility.Hidden : Visibility.Visible;
            colField6.Visibility = string.IsNullOrEmpty(mMFieldName.Field6) ? Visibility.Hidden : Visibility.Visible;
            colField7.Visibility = string.IsNullOrEmpty(mMFieldName.Field7) ? Visibility.Hidden : Visibility.Visible;
            colField8.Visibility = string.IsNullOrEmpty(mMFieldName.Field8) ? Visibility.Hidden : Visibility.Visible;
            colField9.Visibility = string.IsNullOrEmpty(mMFieldName.Field9) ? Visibility.Hidden : Visibility.Visible;
        }

        private void getData(string feeName)
        {
            if (string.IsNullOrEmpty(feeName))
            {
                return;
            }

            mMFeeMasters = DatabaseHandler.GetFeeMaster(feeName);

            gridFee.ItemsSource = mMFeeMasters;
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.IsEnabled == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("データを保存しますか？", "報告", MessageBoxButton.YesNoCancel);

                if (messageBoxResult == MessageBoxResult.Cancel)
                {
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
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            saveData();
        }

        private bool isDuplicateValue(ObservableCollection<MMFeeMaster> mMFeeMasters)
        {
            IEnumerable<string> temp = mMFeeMasters.GroupBy(x => x.Item).Where(g => g.Count() > 1).Select(y => y.Key);

            if(temp == null || temp.Count() == 0)
            {
                return false;
            }

            string duplicateValues = string.Join("、", temp.ToList());

            MessageBox.Show($"「{duplicateValues}」を２つあります。", "エラー");

            return true;

        }

        private void saveData()
        {
            if (isDuplicateValue(mMFeeMasters))
            {
                return;
            }

            bool saveResult = DatabaseHandler.OverwriteFeeMaster(mMFeeMasters, MMFeeStruct.FeeName);

            btnSave.IsEnabled = false;

            _ = saveResult ? MessageBox.Show("成功") : MessageBox.Show("エラー");
        }


        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (btnSave.IsEnabled)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("データを保存しますか？", "報告", MessageBoxButton.YesNoCancel);

                if (messageBoxResult == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (messageBoxResult == MessageBoxResult.Yes)
                {
                    saveData();
                }
            }
        }

        private void gridFee_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            btnSave.IsEnabled = true;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = true;
            //e.Handled = true;

            if (gridFee.SelectedItem is MMFeeMaster @feeMaster)
            {
                if(feeMaster.IsFixedFee == false)
                {
                    return;
                }

                feeMaster.FeeName = MMFeeStruct.FeeName;
                WindowFixedFee windowFixedFee = new WindowFixedFee()
                {
                    MMFeeMaster = feeMaster
                };
                windowFixedFee.ShowDialog();

                if (windowFixedFee.DialogResult == true)
                {
                    feeMaster.IsFixedFee = windowFixedFee.MMFeeMaster.IsFixedFee;
                    feeMaster.TimeFrom = windowFixedFee.MMFeeMaster.TimeFrom;
                    feeMaster.TimeTo = windowFixedFee.MMFeeMaster.TimeTo;
                    feeMaster.Amount = windowFixedFee.MMFeeMaster.Amount;
                }
                else
                {
                    feeMaster.IsFixedFee = false;
                }
            }
        }

        private void CheckBox_Modify(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = true;
        }

        private void gridFee_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var row = UIHelpers.TryFindFromPoint<DataGridRow>((UIElement)sender, e.GetPosition(gridFee));
            if (row == null) return;
            if (row.Item is MMFeeMaster mMFeeMaster)
            {
                isDragging = true;
                draggedItem = mMFeeMaster;
            }
        }

        private void gridFee_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isDragging || e.LeftButton != MouseButtonState.Pressed) return;
            if(!popupDragDrop.IsOpen)
            {
                gridFee.IsReadOnly = true;
                popupDragDrop.IsOpen = true;
            }

            Size popupSize = new Size(popupDragDrop.ActualWidth, popupDragDrop.ActualHeight);
            popupDragDrop.PlacementRectangle = new Rect(e.GetPosition(this), popupSize);

            Point position = e.GetPosition(gridFee);
            var row = UIHelpers.TryFindFromPoint<DataGridRow>(gridFee, position);
            if (row != null) gridFee.SelectedItem = row.Item;
            else reSetDragDrop();
        }

        private void reSetDragDrop()
        {
            isDragging = false;
            popupDragDrop.IsOpen = false;
            gridFee.IsReadOnly = false;
        }

        private void gridFee_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isDragging)
            {
                return;
            }

            int indexTarget = gridFee.SelectedIndex;

            if (mMFeeMasters.Contains(draggedItem))
            {
                mMFeeMasters.Remove(draggedItem);
            }

            mMFeeMasters.Insert(indexTarget, draggedItem);

            gridFee.SelectedItem = draggedItem;

            btnSave.IsEnabled = true;

            reSetDragDrop();
        }
    }
}
