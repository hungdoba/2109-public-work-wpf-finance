using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace FinanceManagement.Function
{
    public static class ExcelHandler
    {

        #region Fee

        public static void ExportExcelFee(ObservableCollection<MMFee> mMFees)
        {
            if (mMFees == null) return;


            string feeName = mMFees[0].FeeName.ToString();
            string feeType = mMFees[0].FeeType.ToString();
            int year = mMFees[0].Year;

            MMFeeStruct  mMFieldName = DatabaseHandler.GetFieldName(feeName);

            Application excelApp = null;
            Workbooks workbooks = null;
            Workbook workbook = null;
            Sheets worksheets = null;
            Worksheet worksheet = null;
            Range range = null;

            try
            {
                excelApp = new Application();
                workbooks = excelApp.Workbooks;
                workbook = workbooks.Add();
                worksheets = workbook.Sheets;
                worksheet = worksheets[1];

                worksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                worksheet.PageSetup.BottomMargin = 1.9;
                worksheet.PageSetup.TopMargin = 1.9;
                worksheet.PageSetup.LeftMargin = 0.6;
                worksheet.PageSetup.RightMargin = 0.6;
                worksheet.PageSetup.HeaderMargin = 0.8;
                worksheet.PageSetup.FooterMargin = 0.8;
                worksheet.PageSetup.Zoom = false;
                worksheet.PageSetup.FitToPagesWide = 1;
                worksheet.PageSetup.FitToPagesTall = false;

                range = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 23]];
                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                range.Merge();

                if (feeType != feeName)
                    range.Value2 = $"{feeName}（{feeType}）{year}年度";
                else
                    range.Value2 = $"{feeName}{year}年度";
                range.Cells.Font.Size = 16; range.Cells.Font.Bold = true;

                range = worksheet.Cells[4, 1]; range.ColumnWidth = 15; range.Value2 = "項目";
                range = worksheet.Cells[4, 2]; range.ColumnWidth = 0; range.Value2 = mMFieldName.Field1;
                range = worksheet.Cells[4, 3]; range.ColumnWidth = 0; range.Value2 = mMFieldName.Field2;
                range = worksheet.Cells[4, 4]; range.ColumnWidth = 0; range.Value2 = mMFieldName.Field3;
                range = worksheet.Cells[4, 5]; range.ColumnWidth = 0; range.Value2 = mMFieldName.Field4;
                range = worksheet.Cells[4, 6]; range.ColumnWidth = 0; range.Value2 = mMFieldName.Field5;
                range = worksheet.Cells[4, 7]; range.ColumnWidth = 0; range.Value2 = mMFieldName.Field6;
                range = worksheet.Cells[4, 8]; range.ColumnWidth = 0; range.Value2 = mMFieldName.Field7;
                range = worksheet.Cells[4, 9]; range.ColumnWidth = 0; range.Value2 = mMFieldName.Field8;
                range = worksheet.Cells[4, 10]; range.ColumnWidth = 0; range.Value2 = mMFieldName.Field9;
                range = worksheet.Cells[4, 11]; range.ColumnWidth = 8; range.Value2 = year.ToString() + "年４月";
                range = worksheet.Cells[4, 12]; range.ColumnWidth = 8; range.Value2 = year.ToString() + "年５月";
                range = worksheet.Cells[4, 13]; range.ColumnWidth = 8; range.Value2 = year.ToString() + "年６月";
                range = worksheet.Cells[4, 14]; range.ColumnWidth = 8; range.Value2 = year.ToString() + "年７月";
                range = worksheet.Cells[4, 15]; range.ColumnWidth = 8; range.Value2 = year.ToString() + "年８月";
                range = worksheet.Cells[4, 16]; range.ColumnWidth = 8; range.Value2 = year.ToString() + "年９月";
                range = worksheet.Cells[4, 17]; range.ColumnWidth = 8; range.Value2 = year.ToString() + "年１０月";
                range = worksheet.Cells[4, 18]; range.ColumnWidth = 8; range.Value2 = year.ToString() + "年１１月";
                range = worksheet.Cells[4, 19]; range.ColumnWidth = 8; range.Value2 = year.ToString() + "年１２月";
                range = worksheet.Cells[4, 20]; range.ColumnWidth = 8; range.Value2 = (year + 1).ToString() + "年１月";
                range = worksheet.Cells[4, 21]; range.ColumnWidth = 8; range.Value2 = (year + 1).ToString() + "年２月";
                range = worksheet.Cells[4, 22]; range.ColumnWidth = 8; range.Value2 = (year + 1).ToString() + "年３月";
                range = worksheet.Cells[4, 23]; range.ColumnWidth = 8; range.Value2 = "合計";


                Marshal.ReleaseComObject(range);

                int row = 5;

                foreach (var mMFee in mMFees)
                {
                    range = worksheet.Cells[row, 1]; range.Value2 = mMFee.Item;

                    if(!string.IsNullOrEmpty(mMFee.Field1)) { range = worksheet.Cells[row, 2];  range.Value2 = mMFee.Field1; range.ColumnWidth = 8; }
                    if(!string.IsNullOrEmpty(mMFee.Field2)) { range = worksheet.Cells[row, 3];  range.Value2 = mMFee.Field2; range.ColumnWidth = 8; }
                    if(!string.IsNullOrEmpty(mMFee.Field3)) { range = worksheet.Cells[row, 4];  range.Value2 = mMFee.Field3; range.ColumnWidth = 8; }
                    if(!string.IsNullOrEmpty(mMFee.Field4)) { range = worksheet.Cells[row, 5];  range.Value2 = mMFee.Field4; range.ColumnWidth = 8; }
                    if(!string.IsNullOrEmpty(mMFee.Field5)) { range = worksheet.Cells[row, 6];  range.Value2 = mMFee.Field5; range.ColumnWidth = 8; }
                    if(!string.IsNullOrEmpty(mMFee.Field6)) { range = worksheet.Cells[row, 7];  range.Value2 = mMFee.Field6; range.ColumnWidth = 8; }
                    if(!string.IsNullOrEmpty(mMFee.Field7)) { range = worksheet.Cells[row, 8];  range.Value2 = mMFee.Field7; range.ColumnWidth = 8; }
                    if(!string.IsNullOrEmpty(mMFee.Field8)) { range = worksheet.Cells[row, 9];  range.Value2 = mMFee.Field8; range.ColumnWidth = 8; }
                    if(!string.IsNullOrEmpty(mMFee.Field9)) { range = worksheet.Cells[row, 10]; range.Value2 = mMFee.Field9; range.ColumnWidth = 8; }

                    range = worksheet.Cells[row, 11]; range.Value2 = mMFee.Month4; range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 12]; range.Value2 = mMFee.Month5; range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 13]; range.Value2 = mMFee.Month6; range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 14]; range.Value2 = mMFee.Month7; range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 15]; range.Value2 = mMFee.Month8; range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 16]; range.Value2 = mMFee.Month9; range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 17]; range.Value2 = mMFee.Month10; range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 18]; range.Value2 = mMFee.Month11; range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 19]; range.Value2 = mMFee.Month12; range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 20]; range.Value2 = mMFee.Month1; range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 21]; range.Value2 = mMFee.Month2; range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 22]; range.Value2 = mMFee.Month3; range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 23]; range.Value2 = mMFee.Sum; range.NumberFormat = "#,#";


                    if (mMFee.Department == "合計")
                    {
                        range = worksheet.Range[worksheet.Cells[row, 1], worksheet.Cells[row, 23]]; range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
                    }

                    row++;

                    Marshal.ReleaseComObject(range);

                }

                int lastRow = 0;
                lastRow = worksheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row;

                range = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[lastRow, 23]];
                range.Cells.Font.Size = 8;
                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignRight;
                range.Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                range.Columns.AutoFit();

                range = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[lastRow, 2]];
                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                range = worksheet.Rows[4];
                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                excelApp.Visible = true;
                worksheet.PrintPreview();
                //return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return false;
            }
            finally
            {
                if (range != null) Marshal.ReleaseComObject(range);
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (worksheets != null) Marshal.ReleaseComObject(worksheets);
                if (workbook != null) Marshal.ReleaseComObject(workbook);
                if (workbooks != null) Marshal.ReleaseComObject(workbooks);
                if (excelApp != null) Marshal.ReleaseComObject(excelApp);
            }
        }

        #endregion


        #region Sale

        public static void ExportExcelSale(ObservableCollection<MMSale> mMSales)
        {
            if (mMSales == null) return;

            int year = mMSales[0].Year;

            Application excelApp = null;
            Workbooks workbooks = null;
            Workbook workbook = null;
            Sheets worksheets = null;
            Worksheet worksheet = null;
            Range range = null;

            try
            {
                excelApp = new Application();
                workbooks = excelApp.Workbooks;
                workbook = workbooks.Add();
                worksheets = workbook.Sheets;
                worksheet = worksheets[1];

                worksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                worksheet.PageSetup.BottomMargin = 1.9;
                worksheet.PageSetup.TopMargin = 1.9;
                worksheet.PageSetup.LeftMargin = 0.6;
                worksheet.PageSetup.RightMargin = 0.6;
                worksheet.PageSetup.HeaderMargin = 0.8;
                worksheet.PageSetup.FooterMargin = 0.8;
                worksheet.PageSetup.Zoom = false;
                worksheet.PageSetup.FitToPagesWide = 1;
                worksheet.PageSetup.FitToPagesTall = false;

                range = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 14]];
                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                range.Merge(); range.Value2 = $"売上{year}年度";
                range.Cells.Font.Size = 16; range.Cells.Font.Bold = true;

                range = worksheet.Cells[4, 1]; range.ColumnWidth = 15; range.Value2 = "会社名";
                range = worksheet.Cells[4, 2]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年４月";
                range = worksheet.Cells[4, 3]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年５月";
                range = worksheet.Cells[4, 4]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年６月";
                range = worksheet.Cells[4, 5]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年７月";
                range = worksheet.Cells[4, 6]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年８月";
                range = worksheet.Cells[4, 7]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年９月";
                range = worksheet.Cells[4, 8]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年１０月";
                range = worksheet.Cells[4, 9]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年１１月";
                range = worksheet.Cells[4, 10]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年１２月"; ;
                range = worksheet.Cells[4, 11]; range.ColumnWidth = 8; range.Value2 = (year + 1).ToString() + "年１月";
                range = worksheet.Cells[4, 12]; range.ColumnWidth = 8; range.Value2 = (year + 1).ToString() + "年２月";
                range = worksheet.Cells[4, 13]; range.ColumnWidth = 8; range.Value2 = (year + 1).ToString() + "年３月";
                range = worksheet.Cells[4, 14]; range.ColumnWidth = 8; range.Value2 = "合計";


                Marshal.ReleaseComObject(range);

                int row = 5;

                foreach (var mMSale in mMSales)
                {
                    range = worksheet.Cells[row, 1]; range.Value2 = mMSale.Customer;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 2]; range.Value2 = mMSale.Month4;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 3]; range.Value2 = mMSale.Month5;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 4]; range.Value2 = mMSale.Month6;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 5]; range.Value2 = mMSale.Month7;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 6]; range.Value2 = mMSale.Month8;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 7]; range.Value2 = mMSale.Month9;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 8]; range.Value2 = mMSale.Month10;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 9]; range.Value2 = mMSale.Month11;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 10]; range.Value2 = mMSale.Month12;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 11]; range.Value2 = mMSale.Month1;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 12]; range.Value2 = mMSale.Month2;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 13]; range.Value2 = mMSale.Month3;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 14]; range.Value2 = mMSale.Sum;range.NumberFormat = "#,#";


                    if (mMSale.Department == "合計")
                    {
                        range = worksheet.Range[worksheet.Cells[row, 1], worksheet.Cells[row, 14]]; range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
                    }

                    row++;

                    Marshal.ReleaseComObject(range);

                }

                int lastRow = 0;
                lastRow = worksheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row;

                range = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[lastRow, 14]];
                range.Cells.Font.Size = 8;
                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignRight;
                range.Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                range.Columns.AutoFit();

                range = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[lastRow, 2]];
                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                range = worksheet.Rows[4];
                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                excelApp.Visible = true;
                worksheet.PrintPreview();
                //return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return false;
            }
            finally
            {
                if (range != null) Marshal.ReleaseComObject(range);
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (worksheets != null) Marshal.ReleaseComObject(worksheets);
                if (workbook != null) Marshal.ReleaseComObject(workbook);
                if (workbooks != null) Marshal.ReleaseComObject(workbooks);
                if (excelApp != null) Marshal.ReleaseComObject(excelApp);
            }
        }


        public static ObservableCollection<MMSale> GetExcelMMSale(string fileName, int year, string department)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            bool isHasGokeiWorkSheet = false;

            Application excelApp = null;
            Workbooks workbooks = null;
            Workbook workbook = null;
            Worksheet worksheet = null;
            Range range = null;

            List<string> workbookSheets = new List<string>();
            ObservableCollection<MMSale> mMSales = new ObservableCollection<MMSale>();

            try
            {
                excelApp = new Application
                {
                    Visible = false
                };
                workbooks = excelApp.Workbooks;

                workbook = workbooks.Open(fileName, 0, true, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false, false);

                foreach (Worksheet temp in workbook.Worksheets)
                {
                    if(temp.Name == "合計")
                    {
                        isHasGokeiWorkSheet = true;
                        break;
                    }
                }

                if(!isHasGokeiWorkSheet)
                {
                    workbook.Close();
                    excelApp.Quit();

                    if (workbook != null) Marshal.ReleaseComObject(workbook);
                    if (workbooks != null) Marshal.ReleaseComObject(workbooks);
                    if (excelApp != null) Marshal.ReleaseComObject(excelApp);

                    System.Windows.MessageBox.Show("「合計」のシートがありませんから。登録ができません。","エラー");
                    return null;
                }

                //foreach (Worksheet temp in workbook.Worksheets)
                //{
                //    workbookSheets.Add(temp.Name);
                //}

                //WindowSelectWorkbookSheet windowSelectWorkbookSheet = new WindowSelectWorkbookSheet()
                //{
                //    Worksheets = workbookSheets
                //};

                //_ = windowSelectWorkbookSheet.ShowDialog();

                //if (!string.IsNullOrEmpty(windowSelectWorkbookSheet.Worksheet))
                //{

                worksheet = workbook.Worksheets["合計"];

                range = worksheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing);

                int lastRow = range.Row;
                int lastColumn = range.Column;


                int colMonth1 = 11;
                int colMonth2 = 12;
                int colMonth3 = 13;
                int colMonth4 = 2;
                int colMonth5 = 3;
                int colMonth6 = 4;
                int colMonth7 = 5;
                int colMonth8 = 6;
                int colMonth9 = 7;
                int colMonth10 = 8;
                int colMonth11 = 9;
                int colMonth12 = 10;

                for (int row = 1; row <= lastRow; row++)
                {
                    range = worksheet.Cells[row, 1];

                    dynamic content = range.Value2;

                    if (content == null || content.ToString().Contains("売上") || content.ToString().Contains("合計"))
                    {
                        continue;
                    }

                    MMSale mMSale = new MMSale()
                    {
                        Department = department,
                        Customer = content.ToString(),
                        Year = year
                    };

                    range = worksheet.Cells[row, colMonth1]; if (range.Value2 != null) { if (int.TryParse(range.Value2.ToString(), out int sale)) { mMSale.Month1 = sale; } else { mMSale.Month1 = 0; } } else { mMSale.Month1 = 0; }
                    range = worksheet.Cells[row, colMonth2]; if (range.Value2 != null) { if (int.TryParse(range.Value2.ToString(), out int sale)) { mMSale.Month2 = sale; } else { mMSale.Month2 = 0; } } else { mMSale.Month2 = 0; }
                    range = worksheet.Cells[row, colMonth3]; if (range.Value2 != null) { if (int.TryParse(range.Value2.ToString(), out int sale)) { mMSale.Month3 = sale; } else { mMSale.Month3 = 0; } } else { mMSale.Month3 = 0; }
                    range = worksheet.Cells[row, colMonth4]; if (range.Value2 != null) { if (int.TryParse(range.Value2.ToString(), out int sale)) { mMSale.Month4 = sale; } else { mMSale.Month4 = 0; } } else { mMSale.Month4 = 0; }
                    range = worksheet.Cells[row, colMonth5]; if (range.Value2 != null) { if (int.TryParse(range.Value2.ToString(), out int sale)) { mMSale.Month5 = sale; } else { mMSale.Month5 = 0; } } else { mMSale.Month5 = 0; }
                    range = worksheet.Cells[row, colMonth6]; if (range.Value2 != null) { if (int.TryParse(range.Value2.ToString(), out int sale)) { mMSale.Month6 = sale; } else { mMSale.Month6 = 0; } } else { mMSale.Month6 = 0; }
                    range = worksheet.Cells[row, colMonth7]; if (range.Value2 != null) { if (int.TryParse(range.Value2.ToString(), out int sale)) { mMSale.Month7 = sale; } else { mMSale.Month7 = 0; } } else { mMSale.Month7 = 0; }
                    range = worksheet.Cells[row, colMonth8]; if (range.Value2 != null) { if (int.TryParse(range.Value2.ToString(), out int sale)) { mMSale.Month8 = sale; } else { mMSale.Month8 = 0; } } else { mMSale.Month8 = 0; }
                    range = worksheet.Cells[row, colMonth9]; if (range.Value2 != null) { if (int.TryParse(range.Value2.ToString(), out int sale)) { mMSale.Month9 = sale; } else { mMSale.Month9 = 0; } } else { mMSale.Month9 = 0; }
                    range = worksheet.Cells[row, colMonth10]; if (range.Value2 != null) { if (int.TryParse(range.Value2.ToString(), out int sale)) { mMSale.Month10 = sale; } else { mMSale.Month10 = 0; } } else { mMSale.Month10 = 0; }
                    range = worksheet.Cells[row, colMonth11]; if (range.Value2 != null) { if (int.TryParse(range.Value2.ToString(), out int sale)) { mMSale.Month11 = sale; } else { mMSale.Month11 = 0; } } else { mMSale.Month11 = 0; }
                    range = worksheet.Cells[row, colMonth12]; if (range.Value2 != null) { if (int.TryParse(range.Value2.ToString(), out int sale)) { mMSale.Month12 = sale; } else { mMSale.Month12 = 0; } } else { mMSale.Month12 = 0; }

                    mMSale.Sum = mMSale.Month1 + mMSale.Month2 + mMSale.Month3 + mMSale.Month4 + mMSale.Month5 + mMSale.Month6 + mMSale.Month7 + mMSale.Month8 + mMSale.Month9 + mMSale.Month10 + mMSale.Month11 + mMSale.Month12;

                    mMSales.Add(mMSale);

                    if (range != null) Marshal.ReleaseComObject(range);
                    //}

                }

                workbook.Close();
                excelApp.Quit();

                #region insert sum
                //MMSale mMSaleSum = new MMSale()
                //{
                //    Department = "合計",
                //    Customer = "合計",
                //    Year = year
                //};

                //foreach(MMSale mSale in mMSales)
                //{
                //    mMSaleSum.Month1 += mSale.Month1;
                //    mMSaleSum.Month2 += mSale.Month2;
                //    mMSaleSum.Month3 += mSale.Month3;
                //    mMSaleSum.Month4 += mSale.Month4;
                //    mMSaleSum.Month5 += mSale.Month5;
                //    mMSaleSum.Month6 += mSale.Month6;
                //    mMSaleSum.Month7 += mSale.Month7;
                //    mMSaleSum.Month8 += mSale.Month8;
                //    mMSaleSum.Month9 += mSale.Month9;
                //    mMSaleSum.Month10 += mSale.Month10;
                //    mMSaleSum.Month11 += mSale.Month11;
                //    mMSaleSum.Month12 += mSale.Month12;
                //    mMSaleSum.Sum += mSale.Sum;
                //}

                //mMSales.Add(mMSaleSum);
                #endregion

                return mMSales;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (range != null) Marshal.ReleaseComObject(range);
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (workbook != null) Marshal.ReleaseComObject(workbook);
                if (workbooks != null) Marshal.ReleaseComObject(workbooks);
                if (excelApp != null) Marshal.ReleaseComObject(excelApp);
            }
        }

        #endregion


        #region Revenue

        public static void ExportExcelRevenue(ObservableCollection<MMRevenue> mMRevenues)
        {
            if (mMRevenues == null) return;

            int year = mMRevenues[0].Year;

            Application excelApp = null;
            Workbooks workbooks = null;
            Workbook workbook = null;
            Sheets worksheets = null;
            Worksheet worksheet = null;
            Range range = null;

            try
            {
                excelApp = new Application();
                workbooks = excelApp.Workbooks;
                workbook = workbooks.Add();
                worksheets = workbook.Sheets;
                worksheet = worksheets[1];

                worksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                worksheet.PageSetup.BottomMargin = 1.9;
                worksheet.PageSetup.TopMargin = 1.9;
                worksheet.PageSetup.LeftMargin = 0.6;
                worksheet.PageSetup.RightMargin = 0.6;
                worksheet.PageSetup.HeaderMargin = 0.8;
                worksheet.PageSetup.FooterMargin = 0.8;
                worksheet.PageSetup.Zoom = false;
                worksheet.PageSetup.FitToPagesWide = 1;
                worksheet.PageSetup.FitToPagesTall = false;

                range = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 14]];
                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                range.Merge(); range.Value2 = $"収支報告{year}年度";
                range.Cells.Font.Size = 16; range.Cells.Font.Bold = true;

                range = worksheet.Cells[4, 1]; range.ColumnWidth = 15; range.Value2 = "項目";
                range = worksheet.Cells[4, 2]; range.ColumnWidth = 15; range.Value2 = "摘要";
                range = worksheet.Cells[4, 3]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年４月";
                range = worksheet.Cells[4, 4]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年５月";
                range = worksheet.Cells[4, 5]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年６月";
                range = worksheet.Cells[4, 6]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年７月";
                range = worksheet.Cells[4, 7]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年８月";
                range = worksheet.Cells[4, 8]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年９月";
                range = worksheet.Cells[4, 9]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年１０月";
                range = worksheet.Cells[4, 10]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年１１月";
                range = worksheet.Cells[4, 11]; range.ColumnWidth = 0; range.Value2 = year.ToString() + "年１２月"; ;
                range = worksheet.Cells[4, 12]; range.ColumnWidth = 8; range.Value2 = (year + 1).ToString() + "年１月";
                range = worksheet.Cells[4, 13]; range.ColumnWidth = 8; range.Value2 = (year + 1).ToString() + "年２月";
                range = worksheet.Cells[4, 14]; range.ColumnWidth = 8; range.Value2 = (year + 1).ToString() + "年３月";
                range = worksheet.Cells[4, 15]; range.ColumnWidth = 8; range.Value2 = "合計";


                Marshal.ReleaseComObject(range);

                int row = 5;

                foreach (var mMRevenue in mMRevenues)
                {
                    range = worksheet.Cells[row, 1]; range.Value2 = mMRevenue.Item;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 2]; range.Value2 = mMRevenue.Sumary;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 3]; range.Value2 = mMRevenue.Month4;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 4]; range.Value2 = mMRevenue.Month5;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 5]; range.Value2 = mMRevenue.Month6;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 6]; range.Value2 = mMRevenue.Month7;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 7]; range.Value2 = mMRevenue.Month8;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 8]; range.Value2 = mMRevenue.Month9;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 9]; range.Value2 = mMRevenue.Month10;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 10]; range.Value2 = mMRevenue.Month11;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 11]; range.Value2 = mMRevenue.Month12;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 12]; range.Value2 = mMRevenue.Month1;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 13]; range.Value2 = mMRevenue.Month2;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 14]; range.Value2 = mMRevenue.Month3;range.NumberFormat = "#,#";
                    range = worksheet.Cells[row, 15]; range.Value2 = mMRevenue.Sum;range.NumberFormat = "#,#";


                    if (mMRevenue.Department == "合計")
                    {
                        range = worksheet.Range[worksheet.Cells[row, 1], worksheet.Cells[row, 15]]; range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
                    }

                    row++;

                    Marshal.ReleaseComObject(range);

                }

                int lastRow = 0;
                lastRow = worksheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row;

                range = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[lastRow, 15]];
                range.Cells.Font.Size = 8;
                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignRight;
                range.Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                range.Columns.AutoFit();

                range = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[lastRow, 2]];
                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                range = worksheet.Rows[4];
                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                excelApp.Visible = true;
                worksheet.PrintPreview();
                //return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return false;
            }
            finally
            {
                if (range != null) Marshal.ReleaseComObject(range);
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (worksheets != null) Marshal.ReleaseComObject(worksheets);
                if (workbook != null) Marshal.ReleaseComObject(workbook);
                if (workbooks != null) Marshal.ReleaseComObject(workbooks);
                if (excelApp != null) Marshal.ReleaseComObject(excelApp);
            }
        }

        #endregion
    }
}
