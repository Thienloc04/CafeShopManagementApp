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
    /// Interaction logic for HomePageWindow.xaml
    /// </summary>
    public partial class HomePageWindow : Window
    {
        public Dictionary<int, CartItem> Cart { get; set; } = new Dictionary<int, CartItem>();
        public User LoginCustomer { get; set; }

        public HomePageWindow()
        {
            InitializeComponent();
        }

        private void HomePage_btn_Click(object sender, RoutedEventArgs e)
        {
            CustomerPageControl.Content = null;
            ListProductControl listProductControl = new ListProductControl();
            listProductControl.AddedCart = Cart;
            listProductControl.LoginHome = LoginCustomer;
            CustomerPageControl.Content = listProductControl;

        }

        private void ViewCart_btn_Click(object sender, RoutedEventArgs e)
        {
            CustomerPageControl.Content = null;
            ViewCart viewCart = new ViewCart();
            viewCart.CartView = Cart;
            viewCart.User = LoginCustomer;
            CustomerPageControl.Content = viewCart;
        }

        private void History_btn_Click(object sender, RoutedEventArgs e)
        {
            CustomerPageControl.Content = null;
            OrderHistory orderHistory = new OrderHistory();
            orderHistory.LoginOrder = LoginCustomer;
            CustomerPageControl.Content = orderHistory;
        }
    }
}
