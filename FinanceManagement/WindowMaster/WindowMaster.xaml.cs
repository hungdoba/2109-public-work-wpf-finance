using System.Windows;
using System.Windows.Input;

namespace FinanceManagement.WindowMaster
{
    /// <summary>
    /// Interaction logic for WindowMaster.xaml
    /// </summary>
    public partial class WindowMaster : Window
    {
        public WindowMaster()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowCustomerMaster windowCustomerMaster = new WindowCustomerMaster();
            windowCustomerMaster.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GridSplitter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (colControl.Width != new GridLength(0, GridUnitType.Star))
                colControl.Width = new GridLength(0, GridUnitType.Star);
            else
                colControl.Width = new GridLength(250);
        }
    }
}
