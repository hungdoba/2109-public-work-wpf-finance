using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FinanceManagement.Function;

namespace FinanceManagement
{
    /// <summary>
    /// Interaction logic for WindowImport.xaml
    /// </summary>
    public partial class WindowImport : Window
    {

        private int year;

        private string department;

        public WindowImport()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            noticeSave(false);

            //tabMain.Header = cbDepartment.Text + cbCostType.Text;

            if (((ComboBoxItem)cbDepartment.SelectedItem).Tag.ToString() == "Ota" && ((ComboBoxItem)cbCostType.SelectedItem).Tag.ToString() == "Sales")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFileDialog.ShowDialog();

                if (!string.IsNullOrEmpty(openFileDialog.FileName) && File.Exists(openFileDialog.FileName))
                {
                    //List<MMOtaQuotationSynthetic> mMOtaQuotationSynthetics = ExcelHandler.GetOtaQuotationSynthetic(openFileDialog.FileName);
                    //modifyMMOtaQuotationSyntheticOrderTime(ref mMOtaQuotationSynthetics);
                    //gridInputData.ItemsSource = mMOtaQuotationSynthetics;


                    gridSale.ItemsSource = ExcelHandler.GetOtaMMSale(openFileDialog.FileName, (int)cbYear.SelectedValue);

                    tabConverted.Focus();

                }

            }
            else if (((ComboBoxItem)cbDepartment.SelectedItem).Tag.ToString() == "HeadQuarter" && ((ComboBoxItem)cbCostType.SelectedItem).Tag.ToString() == "Sales")
            {
                //HQDataDataContext hQDataDataContext = new HQDataDataContext();

                //DateTime dateTime = (DateTime)cbMonth.SelectedItem;

                //var query = from temp in hQDataDataContext.QuotationSynthetics
                //            where temp.MadeTime != null && ((DateTime)temp.MadeTime).Year == dateTime.Year && ((DateTime)temp.MadeTime).Month == dateTime.Month
                //            select temp;


                //if (query.Count() == 0)
                //{
                //    MessageBox.Show("データ空です");
                //    return;
                //}

                //gridInputData.ItemsSource = ConvertType.HQQuotationSyntheticToMMHQQuotationSynthetic(query.ToList());

                gridSale.ItemsSource = DatabaseHandler.GetHQMMSale((int)cbYear.SelectedValue);

                tabConverted.Focus();

            }


        }

        private void setColumnHeader(int year)
        {
            string headerYear = (year - 2000).ToString();
            columnMonth1.Header  = headerYear + "１月";
            columnMonth2.Header  = headerYear + "２月";
            columnMonth3.Header  = headerYear + "３月";
            columnMonth4.Header  = headerYear + "４月";
            columnMonth5.Header  = headerYear + "５月";
            columnMonth6.Header  = headerYear + "６月";
            columnMonth7.Header  = headerYear + "７月";
            columnMonth8.Header  = headerYear + "８月";
            columnMonth9.Header  = headerYear + "９月";
            columnMonth10.Header = headerYear + "１０月";
            columnMonth11.Header = headerYear + "１１月";
            columnMonth12.Header = headerYear + "１２月";
        }


        //private void modifyMMOtaQuotationSyntheticOrderTime(ref List<MMOtaQuotationSynthetic> mMOtaQuotationSynthetics)
        //{
        //    DateTime timeNow = (DateTime)cbMonth.SelectedItem;

        //    foreach(var temp in mMOtaQuotationSynthetics)
        //    {
        //        if(temp.OrderTime == null || temp.OrderTime == new DateTime())
        //        {
        //            temp.OrderTime = timeNow;
        //        }
        //    }
        //}

        //private void btnSave_Click(object sender, RoutedEventArgs e)
        //{

        //    if (gridInputData.Items.Count < 1) return;

        //    Type dataSourceType = gridInputData.ItemsSource.GetType();

        //    bool isInsertToDatabase = true;

        //    if (dataSourceType == typeof(List<MMOtaQuotationSynthetic>) && gridInputData.Items.Count > 0)
        //    {

        //        List<MMOtaQuotationSynthetic> mMOtaQuotationSynthetics = new List<MMOtaQuotationSynthetic>();

        //        foreach (MMOtaQuotationSynthetic mMOtaQuotationSynthetic in gridInputData.ItemsSource)
        //        {
        //            mMOtaQuotationSynthetics.Add(mMOtaQuotationSynthetic);
        //        }

        //        if (noticeSave() == false)
        //        {
        //            List<MMOtaQuotationSynthetic>[] mMOtaQuotationSyntheticsExist = DatabaseHandler.MMOtaQuotationSyntheticExist(mMOtaQuotationSynthetics);

        //            if (mMOtaQuotationSyntheticsExist != null)
        //            {
        //                WindowSelectConflictValue windowSelectConflictValue = new WindowSelectConflictValue();
        //                windowSelectConflictValue.Init(mMOtaQuotationSyntheticsExist);

        //                windowSelectConflictValue.ShowDialog();

        //                if (windowSelectConflictValue.DialogResult == true)
        //                {

        //                    mMOtaQuotationSynthetics = new List<MMOtaQuotationSynthetic>();

        //                    foreach (var temp in gridInputData.ItemsSource)
        //                    {
        //                        mMOtaQuotationSynthetics.Add((MMOtaQuotationSynthetic)temp);
        //                    }

        //                    foreach (var temp in mMOtaQuotationSyntheticsExist[0])
        //                    {
        //                        mMOtaQuotationSynthetics.Remove(temp);
        //                    }

        //                    foreach (var temp in windowSelectConflictValue.MMConfirmed)
        //                    {
        //                        mMOtaQuotationSynthetics.Add(temp);
        //                    }

        //                    gridInputData.ItemsSource = mMOtaQuotationSynthetics;

        //                    noticeSave(true);

        //                }
        //                isInsertToDatabase = false;
        //            }
        //        }

        //        if (isInsertToDatabase)
        //        {
        //            noticeSave(false);
        //            MessageBox.Show(DatabaseHandler.MMOtaQuotationSyntheticsInsert(mMOtaQuotationSynthetics));
        //        }

        //    }
        //    else if (dataSourceType == typeof(List<MMHQQuotationSynthetic>))
        //    {

        //        List<MMHQQuotationSynthetic> mMHQQuotationSynthetics = new List<MMHQQuotationSynthetic>();

        //        foreach (MMHQQuotationSynthetic mMHQQuotationSynthetic in gridInputData.ItemsSource)
        //        {
        //            mMHQQuotationSynthetics.Add(mMHQQuotationSynthetic);
        //        }

        //        if (noticeSave() == false)
        //        {
        //            List<MMHQQuotationSynthetic>[] mMHQQuotationSyntheticsExist = DatabaseHandler.MMHQQuotationSyntheticExist(mMHQQuotationSynthetics);

        //            if (mMHQQuotationSyntheticsExist != null)
        //            {
        //                WindowSelectConflictValue windowSelectConflictValue = new WindowSelectConflictValue();
        //                windowSelectConflictValue.Init(mMHQQuotationSyntheticsExist);

        //                windowSelectConflictValue.ShowDialog();

        //                if (windowSelectConflictValue.DialogResult == true)
        //                {

        //                    mMHQQuotationSynthetics = new List<MMHQQuotationSynthetic>();

        //                    foreach (var temp in gridInputData.ItemsSource)
        //                    {
        //                        mMHQQuotationSynthetics.Add((MMHQQuotationSynthetic)temp);
        //                    }


        //                    foreach (var temp in mMHQQuotationSyntheticsExist[0])
        //                    {
        //                        mMHQQuotationSynthetics.Remove(temp);
        //                    }

        //                    foreach (var temp in windowSelectConflictValue.MMConfirmed)
        //                    {
        //                        mMHQQuotationSynthetics.Add(temp);
        //                    }

        //                    gridInputData.ItemsSource = mMHQQuotationSynthetics;

        //                    noticeSave(true);

        //                }
        //                isInsertToDatabase = false;
        //            }
        //        }

        //        if (isInsertToDatabase)
        //        {
        //            noticeSave(false);
        //            MessageBox.Show(DatabaseHandler.MMHQQuotationSyntheticsInsert(mMHQQuotationSynthetics));
        //        }
        //    }
        //}

        private void noticeSave(bool active)
        {
            if (active)
            {
                btnSave.Foreground = Brushes.Red;
            }
            else
            {
                btnSave.Foreground = Brushes.DarkBlue;
            }
        }

        //private bool noticeSave()
        //{
        //    if (btnSave.Foreground == Brushes.Red)
        //        return true;
        //    return false;
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //setMonthList();
            setYear();
        }

        private void setYear()
        {
            year = DateTime.Today.Year;
            for(int i = year; i > 2012; i--)
            {
                cbYear.Items.Add(i);
            }
            cbYear.SelectedIndex = 0;
        }

        private void setDepartment()
        {

        }
        //private void setMonthList()
        //{
        //    DateTime dateTime = DateTime.Today;

        //    DateTime firstDay = new DateTime(dateTime.Year, dateTime.Month, 1);
        //    firstDay = firstDay.AddMonths(6);

        //    cbMonth.Items.Add(firstDay);

        //    for (int i = 1; i < 25; i++)
        //    {
        //        firstDay = firstDay.AddMonths(-1);
        //        cbMonth.Items.Add(firstDay);
        //    }

        //    cbMonth.SelectedIndex = 6;

        //}

        //private void gridInputData_AutoGeneratedColumns(object sender, EventArgs e)
        //{
        //    foreach(DataGridTextColumn column in gridInputData.Columns)
        //    {
        //        if (column.Header.ToString().Contains("Time"))                  column.Binding.StringFormat = @"yyyy/MM/dd";
        //        if (column.Header.ToString().Contains("Cost"))                  column.Binding.StringFormat = "N0";

        //        if (column.Header.ToString().Contains("OrderTime"))             column.Width = new DataGridLength(2, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("PaidTime"))         column.Width = new DataGridLength(2, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("QuotationNo"))      column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("WorkNumber"))       column.Width = new DataGridLength(1.2, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("OrderCode"))        column.Width = new DataGridLength(1.2, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("CustomerName"))     column.Width = new DataGridLength(4, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("WorkPlace"))        column.Width = new DataGridLength(4, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("WorkName"))         column.Width = new DataGridLength(6, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("WorkType"))         column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("QuotationCost"))    column.Width = new DataGridLength(2, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("FinalCost"))        column.Width = new DataGridLength(2, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("Remark"))           column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("EndUser"))          column.Width = new DataGridLength(4, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("CabinetsAmount"))   column.Width = new DataGridLength(2, DataGridLengthUnitType.Star);
        //        else if (column.Header.ToString().Contains("ProductName"))      column.Width = new DataGridLength(4, DataGridLengthUnitType.Star);


        //        if (column.Header.ToString().Contains("OrderTime")) 			column.Header = "注文日";
        //        else if (column.Header.ToString().Contains("PaidTime")) 		column.Header = "計上日";
        //        else if (column.Header.ToString().Contains("QuotationNo")) 		column.Header = "見積番号";
        //        else if (column.Header.ToString().Contains("WorkNumber")) 		column.Header = "作番";
        //        else if (column.Header.ToString().Contains("OrderCode")) 		column.Header = "注番";
        //        else if (column.Header.ToString().Contains("CustomerName")) 	column.Header = "会社名";
        //        else if (column.Header.ToString().Contains("WorkPlace")) 		column.Header = "工事場所";
        //        else if (column.Header.ToString().Contains("WorkName")) 		column.Header = "工事名所";
        //        else if (column.Header.ToString().Contains("WorkType")) 		column.Header = "工事タイプ";
        //        else if (column.Header.ToString().Contains("QuotationCost")) 	column.Header = "見積金額";
        //        else if (column.Header.ToString().Contains("FinalCost")) 		column.Header = "決定金額";
        //        else if (column.Header.ToString().Contains("Remark")) 			column.Header = "備考";
        //        else if (column.Header.ToString().Contains("EndUser")) 			column.Header = "納入先";
        //        else if (column.Header.ToString().Contains("CabinetsAmount")) 	column.Header = "面数";
        //        else if (column.Header.ToString().Contains("ProductName")) 		column.Header = "品名";

        //    }
        //}

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void btnDataFromDatabase_Click(object sender, RoutedEventArgs e)
        //{
        //    tabMain.Header = cbDepartment.Text + cbCostType.Text;

        //    HQDataDataContext hQDataDataContext = new HQDataDataContext();

        //    DateTime dateTime = (DateTime)cbMonth.SelectedItem;

        //    if (((ComboBoxItem)cbDepartment.SelectedItem).Tag.ToString() == "Ota" && ((ComboBoxItem)cbCostType.SelectedItem).Tag.ToString() == "Sales")
        //    {

        //        var query = from temp in hQDataDataContext.MMOtaQuotationSynthetics
        //                    where temp.OrderTime != null && ((DateTime)temp.OrderTime).Year == dateTime.Year && ((DateTime)temp.OrderTime).Month == dateTime.Month
        //                    select temp;

        //        gridInputData.ItemsSource = query.ToList();

        //    }
        //    else if (((ComboBoxItem)cbDepartment.SelectedItem).Tag.ToString() == "HeadQuarter" && ((ComboBoxItem)cbCostType.SelectedItem).Tag.ToString() == "Sales")
        //    {

        //        var query = from temp in hQDataDataContext.MMHQQuotationSynthetics
        //                    where temp.OrderTime != null && ((DateTime)temp.OrderTime).Year == dateTime.Year && ((DateTime)temp.OrderTime).Month == dateTime.Month
        //                    select temp;

        //        gridInputData.ItemsSource = query.ToList();

        //    }
        //}

        private void btnConvertData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbYear_DropDownOpened(object sender, EventArgs e)
        {
            year = (int)cbYear.SelectedValue;
        }

        private void cbYear_DropDownClosed(object sender, EventArgs e)
        {
            int newYear = (int)cbYear.SelectedValue;
            if(newYear != year)
            {
                year = newYear;
                gridSale.ItemsSource = null;
            }
        }

        private void cbDepartment_DropDownOpened(object sender, EventArgs e)
        {
            department = cbDepartment.Text;
        }

        private void cbDepartment_DropDownClosed(object sender, EventArgs e)
        {
            if(cbDepartment.Text != department)
            {
                gridSale.ItemsSource = null;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
