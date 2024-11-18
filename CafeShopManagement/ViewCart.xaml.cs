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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace CafeShopManagement
{
    /// <summary>
    /// Interaction logic for ViewCart.xaml
    /// </summary>
    public partial class ViewCart : UserControl
    {
        public Dictionary<int, CartItem> CartView { get; set; }
        private OrderService _service = new();
        public User User { get; set; }
        public ViewCart()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCart();
        }

        private void LoadCart()
        {
            CartListView.ItemsSource = CartView.Values.ToList();
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            decimal total = CartView.Values.Sum(item => item.Quantity * item.UnitPrice);
            TotalPriceText.Text = total.ToString("C");
        }
        private void UpdateQuantity_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cartItem = button?.Tag as CartItem;

            if (cartItem == null) return;

            var parent = VisualTreeHelper.GetParent(button) as StackPanel;
            var quantityBox = parent?.Children.OfType<TextBox>().FirstOrDefault();

            if (quantityBox != null && int.TryParse(quantityBox.Text, out int quantity) && quantity > 0)
            {
                cartItem.Quantity = quantity;
                LoadCart();
            }
            else
            {
                MessageBox.Show("Invalid quantity. Please enter a positive number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.Tag as CartItem;

            if (CartView.ContainsKey(item.ProductId))
            {
                CartView.Remove(item.ProductId);
                MessageBox.Show($"{item.ProductName} removed from cart.");
                LoadCart();
            }
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (!CartView.Any())
            {
                MessageBox.Show("Your cart is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            decimal total = CartView.Values.Sum(x => x.UnitPrice * x.Quantity);
            _service.SaveOrder(User.UserId, CartView, total);

            CartView.Clear();
            LoadCart();

            MessageBox.Show("Your order has been placed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
