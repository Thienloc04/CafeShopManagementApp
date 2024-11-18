using BLL.Services;
using DAL.Models;
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

namespace CafeShopManagement
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private UserService _service = new();
        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void ShowCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordVisibleTextBox.Text = PasswordTextBox.Password;
            PasswordTextBox.Visibility = Visibility.Collapsed;
            PasswordVisibleTextBox.Visibility = Visibility.Visible;
            
            cPasswordVisibleTextBox.Text = cPasswordTextBox.Password;
            cPasswordTextBox.Visibility = Visibility.Collapsed;
            cPasswordVisibleTextBox.Visibility = Visibility.Visible;
        }

        private void ShowCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Password = PasswordVisibleTextBox.Text;
            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordVisibleTextBox.Visibility = Visibility.Collapsed;

            cPasswordTextBox.Password = cPasswordVisibleTextBox.Text;
            cPasswordTextBox.Visibility = Visibility.Visible;
            cPasswordVisibleTextBox.Visibility = Visibility.Collapsed;
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Hide();
        }

        private bool validation()
        {
            if(string.IsNullOrWhiteSpace(UsernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Password) ||
                string.IsNullOrWhiteSpace(cPasswordTextBox.Password))
            {
                MessageBox.Show("All field are required!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(PasswordTextBox.Password != cPasswordTextBox.Password)
            {
                MessageBox.Show("Password and Confirm Password don's match each other", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            User user = _service.Login(UsernameTextBox.Text);
            if(user != null)
            {
                MessageBox.Show("Your username is existed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!validation()) return; 
            User user = new User();
            user.UserName = UsernameTextBox.Text;
            user.Password = PasswordTextBox.Password;
            user.RoleId = 3;
            user.Status = 1;
            user.CreatedDate = DateTime.Now;

            _service.CreateUser(user);
        }
    }
}
