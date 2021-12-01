using System;
using System.Windows;
using System.Windows.Controls;
using FinanceManagement.Function;
using FinanceManagement.WindowReport;
using FinanceManagement.WindowImport;
using FinanceManagement.WindowMaster;

namespace FinanceManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setStackPanelButton();
        }

        private void setStackPanelButton()
        {
            // Reset Children
            stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
            stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
            stackPanel3.Children.RemoveRange(0, stackPanel3.Children.Count);
            stackPanel4.Children.RemoveRange(0, stackPanel4.Children.Count);
            stackPanel5.Children.RemoveRange(0, stackPanel5.Children.Count);
            stackPanel6.Children.RemoveRange(0, stackPanel6.Children.Count);

            double i = 1;
            foreach (MMFeeStruct temp in DatabaseHandler.GetFeeName("セイキョウ"))
            {
                Button button = new Button()
                {
                    Content = temp.FeeName,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    MinHeight = 126,
                    MinWidth = 374,
                    Margin = new Thickness(5),
                    ToolTip = temp.Remark,
                };

                button.Click += Button_Click;

                double row = Math.Ceiling(i / 5);

                switch (row)
                {
                    case 1:
                        _ = stackPanel1.Children.Add(button);
                        break;
                    case 2:
                        _ = stackPanel2.Children.Add(button);
                        break;
                    case 3:
                        _ = stackPanel3.Children.Add(button);
                        break;
                    case 4:
                        _ = stackPanel4.Children.Add(button);
                        break;
                    case 5:
                        _ = stackPanel5.Children.Add(button);
                        break;
                    case 6:
                        _ = stackPanel6.Children.Add(button);
                        break;
                }

                i++;
            }

            rowStackPanel1.Height = stackPanel1.Children.Count == 0 ? new GridLength(0) : new GridLength(2, GridUnitType.Star);
            rowStackPanel2.Height = stackPanel2.Children.Count == 0 ? new GridLength(0) : new GridLength(2, GridUnitType.Star);
            rowStackPanel3.Height = stackPanel3.Children.Count == 0 ? new GridLength(0) : new GridLength(2, GridUnitType.Star);
            rowStackPanel4.Height = stackPanel4.Children.Count == 0 ? new GridLength(0) : new GridLength(2, GridUnitType.Star);
            rowStackPanel5.Height = stackPanel5.Children.Count == 0 ? new GridLength(0) : new GridLength(2, GridUnitType.Star);
            rowStackPanel6.Height = stackPanel6.Children.Count == 0 ? new GridLength(0) : new GridLength(2, GridUnitType.Star);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowFee windowFee = new WindowFee()
            {
                FeeName = ((Button)sender).Content.ToString()
            };
            _ = windowFee.ShowDialog();
        }

        private void btnSetup_Click(object sender, RoutedEventArgs e)
        {
            WindowSetup windowSetup = new WindowSetup();
            _ = windowSetup.ShowDialog();
            setStackPanelButton();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSetupMaster_Click(object sender, RoutedEventArgs e)
        {
            MMFeeStruct mMFeeStruct = new MMFeeStruct()
            {
                FeeName = "売上"
            };

            WindowFeeMaster WindowFeeMaster = new WindowFeeMaster()
            {
                MMFeeStruct = mMFeeStruct
            };

            WindowFeeMaster.ShowDialog();
        }

        private void btnSale_Click(object sender, RoutedEventArgs e)
        {
            WindowSale windowSale = new WindowSale();
            _ = windowSale.ShowDialog();
        }

        private void btnRevenue_Click(object sender, RoutedEventArgs e)
        {
            WindowRevenue windowRevenue = new WindowRevenue();
            _ = windowRevenue.ShowDialog();
        }

        private void btnSaleImport_Click(object sender, RoutedEventArgs e)
        {
            WindowImportSale windowImportSale = new WindowImportSale();
            _ = windowImportSale.ShowDialog();
        }

        private void btnSetupPassword_Click(object sender, RoutedEventArgs e)
        {
            WindowPassword windowPassword = new WindowPassword() { IsLogIn = false };
            _ = windowPassword.ShowDialog();
        }

        private void btnSetupEmployeeMaster_Click(object sender, RoutedEventArgs e)
        {
            MMFeeStruct mMFeeStruct = new MMFeeStruct()
            {
                FeeName = "会社員"
            };

            WindowFeeMaster WindowFeeMaster = new WindowFeeMaster()
            {
                MMFeeStruct = mMFeeStruct
            };

            _ = WindowFeeMaster.ShowDialog();
        }
    }
}
