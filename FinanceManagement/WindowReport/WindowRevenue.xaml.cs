using System;
using System.Windows;
using System.Collections.ObjectModel;
using FinanceManagement.Function;

namespace FinanceManagement.WindowReport
{
    /// <summary>
    /// Interaction logic for WindowHQIncomeReport.xaml
    /// </summary>
    public partial class WindowRevenue: Window
    {

        //public string Department;

        ObservableCollection<MMRevenue> observableRevenues;

        public WindowRevenue()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int yearNow = setYear();
            //cbDepartment.Text = Department;

            getData(cbDepartment.Text, yearNow);
        }


        private void getData(string department, int yearNow)
        {

            if (string.IsNullOrEmpty(department)) return;

            observableRevenues = DatabaseHandler.GetRevenue(department, yearNow);

            gridRevenue.ItemsSource = observableRevenues;

            setGridHeader(observableRevenues, yearNow);

            if (observableRevenues == null) return;

            if (observableRevenues.Count == 0)
            {
                btnPrint.IsEnabled = false;
            }
            else
            {
                btnPrint.IsEnabled = true;
            }

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

        private void setGridHeader(ObservableCollection<MMRevenue> mMRevenues, int year)
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (observableRevenues == null)
            {
                return;
            }

            ExcelHandler.ExportExcelRevenue(observableRevenues);
        }

        private void GridSplitter_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (colControl.Width != new GridLength(0))
                colControl.Width = new GridLength(0);
            else
                colControl.Width = new GridLength(300);
        }

        private void cbYear_DropDownClosed(object sender, EventArgs e)
        {
            getData(cbDepartment.Text, (int)cbYear.SelectedValue);
        }

        private void cbDepartment_DropDownClosed(object sender, EventArgs e)
        {
            getData(cbDepartment.Text, (int)cbYear.SelectedValue);
        }
    }
}
