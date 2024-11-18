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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public User Login { get; set; }
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void ManageUserBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagementControl.Content = null;
            ManagementControl.Content = new ManageUserControl();
        }

        private void ManageProductBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagementControl.Content = null;
            ManagementControl.Content = new ManageProduct();
        }
    }
}
