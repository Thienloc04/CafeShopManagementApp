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
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace CafeShopManagement
{
    /// <summary>
    /// Interaction logic for ManageUserControl.xaml
    /// </summary>
    public partial class ManageUserControl : UserControl
    {
        private UserService _service = new();
        private RoleService _roleService = new();
        private string _imagePath = "";
        public ManageUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid(_service.GetAllUser());
            FillComboBox();
        }

        private void FillDataGrid(List<User> users)
        {
            UserDataGrid.ItemsSource = null;
            UserDataGrid.ItemsSource = users;
        }

        private void FillComboBox()
        {
            RoleIdCombobox.ItemsSource = _roleService.GetAllRoles();
            RoleIdCombobox.SelectedValuePath = "RoleId";
            RoleIdCombobox.DisplayMemberPath = "RoleName";

            var statusList = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Active", 1),
                new KeyValuePair<string, int>("Inactive", 2)
            };

            StatusCombobox.ItemsSource = statusList;
            StatusCombobox.DisplayMemberPath = "Key";       
            StatusCombobox.SelectedValuePath = "Value";     

        }

        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dialog = new();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
              
                if (dialog.ShowDialog() != true)
                {
                    return;
                }
                
                _imagePath = dialog.FileName;
                UserImg.Source = new BitmapImage(new Uri(_imagePath));
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.UserName = UserNameTextBox.Text;
            user.Password = PasswordTextBox.Text;
            user.Status = int.Parse(StatusCombobox.SelectedValue.ToString());
            user.RoleId = int.Parse(RoleIdCombobox.SelectedValue.ToString());
            user.CreatedDate = DateTime.Now;
            user.ProfileImg = _imagePath;

            _service.CreateUser(user);
            MessageBox.Show("User is added successfully!", "Success", MessageBoxButton.OK);
            FillDataGrid(_service.GetAllUser());
            ClearFields();
            
        }

        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserDataGrid.SelectedItem is User selectedUser)
            {
                UserIdTextBox.Text = selectedUser.UserId.ToString();
                UserNameTextBox.Text = selectedUser.UserName;
                PasswordTextBox.Text = selectedUser.Password;
                RoleIdCombobox.SelectedValue = selectedUser.RoleId;
                StatusCombobox.SelectedValue = selectedUser.Status;

                if (string.IsNullOrWhiteSpace(selectedUser.ProfileImg))
                {
                    _imagePath = "";
                    UserImg.Source = null;
                }
                else
                {
                    _imagePath = selectedUser.ProfileImg;
                    UserImg.Source = new BitmapImage(new Uri(selectedUser.ProfileImg));
                }
                AddBtn.IsEnabled = false;
            }
        }

        private void ClearFields()
        {
            UserIdTextBox.Text = "";
            UserNameTextBox.Text = "";
            PasswordTextBox.Text = "";
            RoleIdCombobox.SelectedIndex = -1;
            StatusCombobox.SelectedValue = -1;
            UserImg.Source = null;
            _imagePath = ""; 
            UserDataGrid.SelectedItem = null;
            AddBtn.IsEnabled = true;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem is User selectedUser)
            {
                selectedUser.UserId = int.Parse(UserIdTextBox.Text.ToString());
                selectedUser.UserName = UserNameTextBox.Text;
                selectedUser.Password = PasswordTextBox.Text;
                selectedUser.RoleId = int.Parse(RoleIdCombobox.SelectedValue.ToString());
                selectedUser.Status = int.Parse(StatusCombobox.SelectedValue.ToString());

                if (!string.IsNullOrEmpty(_imagePath))
                {
                    selectedUser.ProfileImg = _imagePath; 
                }

                _service.UpdateUser(selectedUser);
                MessageBox.Show("User updated successfully " + selectedUser.RoleId, "Success", MessageBoxButton.OK);
                FillDataGrid(_service.GetAllUser());
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please select a user to edit.");
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem is User selectedUser)
            {
                var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _service.DeleteUser(selectedUser);
                    MessageBox.Show("User deleted successfully");
                    FillDataGrid(_service.GetAllUser());
                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButton.OK);
                return;
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
    }
}
