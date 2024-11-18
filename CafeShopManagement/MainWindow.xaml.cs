using BLL.Services;
using DAL.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CafeShopManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserService _service = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            // Chuyển nội dung từ PasswordBox sang TextBox để hiển thị mật khẩu
            PasswordVisibleTextBox.Text = PasswordTextBox.Password;
            PasswordTextBox.Visibility = Visibility.Collapsed;
            PasswordVisibleTextBox.Visibility = Visibility.Visible;
        }

        private void ShowCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Chuyển nội dung từ TextBox trở lại PasswordBox để ẩn mật khẩu
            PasswordTextBox.Password = PasswordVisibleTextBox.Text;
            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordVisibleTextBox.Visibility = Visibility.Collapsed;
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();

            this.Hide();
        }

        private bool validation()
        {
            if(string.IsNullOrWhiteSpace(UsernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                MessageBox.Show("All field are required!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!validation()) return;
            User? user = _service.Login(UsernameTextBox.Text);

            if (user == null)
            {
                MessageBox.Show("Incorrect Username or Password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (user.Password != PasswordTextBox.Password)
            {
                MessageBox.Show("Incorrect Password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (user.RoleId == 1 ||  user.RoleId == 2)
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Login = user;
                this.Close();
                adminWindow.ShowDialog();
            } else if(user.RoleId == 3)
            {
                HomePageWindow homePageWindow = new HomePageWindow();
                homePageWindow.LoginCustomer = user;
                this.Close();
                homePageWindow.ShowDialog();
            }

        }
    }
}