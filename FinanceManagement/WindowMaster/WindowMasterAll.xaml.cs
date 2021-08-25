using System.Windows;
using FinanceManagement.Function;

namespace FinanceManagement.WindowMaster
{
    /// <summary>
    /// Interaction logic for WindowMasterAll.xaml
    /// </summary>
    public partial class WindowMasterAll : Window
    {
        public WindowMasterAll()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gridMasterAll.ItemsSource = DatabaseHandler.GetFixedFee();
        }
    }
}
