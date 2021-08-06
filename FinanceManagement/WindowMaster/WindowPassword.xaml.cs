using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinanceManagement.WindowMaster
{
    /// <summary>
    /// Interaction logic for WindowPassword.xaml
    /// </summary>
    public partial class WindowPassword : Window
    {

        public bool IsLogIn = true;

        public WindowPassword()
        {
            InitializeComponent();
        }

        private void btnSetup_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Password = pbPassword.Password;
            Properties.Settings.Default.Save();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(pbPassword.Password != Properties.Settings.Default.Password)
            {
                MessageBox.Show("パスワードが間違いました。");
            }
            else
            {
                Close();
                DialogResult = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(IsLogIn)
            {
                btnSetup.Visibility = Visibility.Hidden;
                btnLogin.Visibility = Visibility.Visible;
            }
            else
            {
                btnSetup.Visibility = Visibility.Visible;
                btnLogin.Visibility = Visibility.Hidden;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = false;
        }
    }
}
