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

namespace CafeShopManagement
{
    /// <summary>
    /// Interaction logic for OrderHistory.xaml
    /// </summary>
    public partial class OrderHistory : UserControl
    {
        public User LoginOrder {  get; set; }
        private OrderService _service = new();
        public OrderHistory()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDateGrid(_service.GetOrdersByUserId(LoginOrder.UserId));
        }

        private void FillDateGrid(List<OrderDetail> arr)
        {
            OrderDataGrid.ItemsSource = null;
            OrderDataGrid.ItemsSource = arr;
        }
    }
}
