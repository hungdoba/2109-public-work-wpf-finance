using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FinanceManagement.Function
{
    /// <summary>
    /// Interaction logic for WindowSelectConflictValue.xaml
    /// </summary>
    public partial class WindowSelectConflictValue : Window
    {

        private dynamic mMInput { get; set; }

        public dynamic MMConfirmed { get; set; }

        public WindowSelectConflictValue()
        {
            InitializeComponent();
        }


        public void Init<T>(List<T>[] MMInput)
        {
            MMConfirmed = new ObservableCollection<T>();
            mMInput = MMInput;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mMInput == null)
            {
                Close();
                return;
            }


            if(mMInput[0] != null && mMInput[1] != null)
            {
                gridNewImport.ItemsSource = mMInput[0];
                gridOldData.ItemsSource = mMInput[1];
                gridNewImport.SelectAll();
            }

        }

        private void MouseEnterHandler(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.OriginalSource is DataGridRow row)
            {
                row.IsSelected = !row.IsSelected;

                if (row.IsSelected == true)
                {
                    if (GetDatagridName(row) == "gridOldData")
                    {
                        gridNewImport.SelectedItems.Remove(gridNewImport.Items[row.GetIndex()]);
                    }
                    else
                    {
                        gridOldData.SelectedItems.Remove(gridOldData.Items[row.GetIndex()]);
                    }
                }

                e.Handled = true;
            }
        }

        private void PreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.OriginalSource is FrameworkElement element && GetVisualParentOfType<DataGridRow>(element) is DataGridRow row)
            {
                row.IsSelected = !row.IsSelected;
                e.Handled = true;

                if (row.IsSelected == true)
                {
                    if (GetDatagridName(row) == "gridOldData")
                    {
                        gridNewImport.SelectedItems.Remove(gridNewImport.Items[row.GetIndex()]);
                    }
                    else
                    {
                        gridOldData.SelectedItems.Remove(gridOldData.Items[row.GetIndex()]);
                    }
                }
            }
        }

        private string GetDatagridName(DataGridRow dataGridRow)
        {
            DependencyObject dependencyObject = dataGridRow;

            while(dependencyObject.GetType() != typeof(DataGrid))
            {
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            return ((DataGrid)dependencyObject).Name;

        }

        private static DependencyObject GetVisualParentOfType<T>(DependencyObject startObject)
        {
            DependencyObject parent = startObject;

            while (IsNotNullAndNotOfType<T>(parent))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent is T ? parent : throw new Exception($"Parent of type {typeof(T)} could not be found");
        }

        private static bool IsNotNullAndNotOfType<T>(DependencyObject obj)
        {
            return obj != null && !(obj is T);
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            foreach (var temp in gridNewImport.SelectedItems)
            {
                MMConfirmed.Add((dynamic)temp);
            }

            foreach (var temp in gridOldData.SelectedItems)
            {
                MMConfirmed.Add((dynamic)temp);
            }

            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
