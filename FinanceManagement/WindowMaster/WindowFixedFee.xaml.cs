using System;
using System.Windows;

namespace FinanceManagement.WindowMaster
{
    /// <summary>
    /// Interaction logic for WindowFixedFee.xaml
    /// </summary>
    public partial class WindowFixedFee : Window
    {
        public MMFeeMaster MMFeeMaster;

        public WindowFixedFee()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtItem.Text = MMFeeMaster.Item;
            dateFrom.SelectedDate = MMFeeMaster.TimeFrom == null || MMFeeMaster.TimeFrom == new DateTime(1900, 1, 1) ? DateTime.Today : MMFeeMaster.TimeFrom;
            dateTo.SelectedDate =  MMFeeMaster.TimeTo == null || MMFeeMaster.TimeTo == new DateTime(1900, 1, 1) ? DateTime.Today : MMFeeMaster.TimeTo;
            txtAmout.Text = "10000";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MMFeeMaster.IsFixedFee = cbxIsFixedFee.IsChecked == true;
            MMFeeMaster.TimeFrom = dateFrom.SelectedDate;
            MMFeeMaster.TimeTo = dateTo.SelectedDate;
            MMFeeMaster.Amount = int.TryParse(txtAmout.Text, out int amount) ? amount : 0;

            DialogResult = cbxIsFixedFee.IsChecked == true;

            Close();
        }

        private void txtAmout_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if(!System.Text.RegularExpressions.Regex.IsMatch(txtAmout.Text, "^[0-9]*$"))
            {
                txtAmout.Text = "";
            }
        }
    }
}
