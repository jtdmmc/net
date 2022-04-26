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

namespace MT.PrismApp.ModulePowerMgr.Views
{
    /// <summary>
    /// AccountWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AccountWindow : Window
    {
        public AccountWindow()
        {
            InitializeComponent();
        }



        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string password1 = PasswordBox1.Password;
            string password2 = PasswordBox2.Password;
            if (string.IsNullOrEmpty(password1) && string.IsNullOrEmpty(password2))
            {
                msgLabel.Visibility = Visibility.Hidden;
            }
            else if (!string.IsNullOrEmpty(password1) && !string.IsNullOrEmpty(password2) && password1.Equals(password2))
            {
                msgLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                msgLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
