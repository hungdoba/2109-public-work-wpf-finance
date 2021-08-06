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

namespace FinanceManagement.Function
{
    /// <summary>
    /// Interaction logic for WindowSelectWorkbookSheet.xaml
    /// </summary>
    public partial class WindowSelectWorkbookSheet : Window
    {

        public List<string> Worksheets { get; set; }

        public string Worksheet { get; set; }

        public WindowSelectWorkbookSheet()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Worksheets != null)
            {
                foreach(string sheet in Worksheets)
                {
                    Button button = new Button()
                    {
                        Content = sheet,
                        MinHeight = 50,
                        FontSize = 18,
                        Margin = new Thickness(5)
                    };

                    button.Click += Button_Click;
                    stackPanelWorkSheets.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Worksheet = button.Content.ToString();
            this.Close();
        }
    }
}
