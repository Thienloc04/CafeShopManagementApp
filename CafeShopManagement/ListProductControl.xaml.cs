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
    /// Interaction logic for ListProductControl.xaml
    /// </summary>
    public partial class ListProductControl : UserControl
    {
        private ProductService _service = new();
        public Dictionary<int, CartItem> AddedCart { get; set; }

        public User LoginHome { get; set; }
        public ListProductControl()
        {
            InitializeComponent();
        }

        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            FillListView(_service.GetAll());
        }

        private void FillListView(List<Product> arr)
        {
            ProductListView.ItemsSource = null;
            ProductListView.ItemsSource = arr;
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var product = button.Tag as Product;

            if (product == null) return;
            if (AddedCart.ContainsKey(product.ProductId))
            {
                AddedCart[product.ProductId].Quantity++;
            }
            else
            {
                AddedCart[product.ProductId] = new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    UnitPrice = product.Price,
                    Quantity = 1
                };
            }

            MessageBox.Show($"Added {product.ProductName} to the cart.","Succes", MessageBoxButton.OK);
        }
    }
}
