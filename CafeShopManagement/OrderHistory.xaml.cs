using BLL.Services;
using DAL.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        public User LoginOrder { get; set; }
        private OrderService _service = new();
        public OrderHistory()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDateGrid(_service.GetOrdersByUserId(LoginOrder.UserId));
        }

        private void FillDateGrid(List<Order> arr)
        {
            OrderDataGrid.ItemsSource = null;
            OrderDataGrid.ItemsSource = arr;
        }

        private void Receipt_btn_Click(object sender, RoutedEventArgs e)
        {
            
            Order selected = OrderDataGrid.SelectedItem as Order;

            if (selected == null)
            {
                MessageBox.Show("Please select an order first.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Tạo danh sách các sản phẩm từ OrderDetails
            var orderDetails = _service.GetOrderDetails(selected.OrderId);
            string receipt = "========== Receipt ==========\n";
            receipt += $"Order ID: {selected.OrderId}\n";
            receipt += $"Order Date: {selected.OrderDate}\n";
            receipt += $"Total Price: {selected.TotalPrice:C}\n\n";
            receipt += "Products:\n";

            foreach (var detail in orderDetails)
            {
                receipt += $"- {detail.Product.ProductName} x{detail.Quantity} Price: {detail.UnitPrice:C}\n";
            }

            receipt += "\n=============================";

            MessageBox.Show(receipt, "Receipt", MessageBoxButton.OK, MessageBoxImage.Information);
            ExportToPDF(receipt);

        }

        private void ExportToPDF(string receipt)
        {
            // Tạo đường dẫn tạm thời trong thư mục Temp
            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "TemporaryReceipt.pdf");

            // Tạo file PDF
            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                var doc = new iTextSharp.text.Document();
                PdfWriter.GetInstance(doc, stream);

                // Mở document và thêm nội dung
                doc.Open();
                doc.Add(new Paragraph(receipt));
                doc.Close();
            }

            // Mở file PDF bằng trình đọc mặc định
            try
            {
                var process = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = tempPath,
                        UseShellExecute = true
                    }
                };

                process.Start();

                process.WaitForExit();

                // Sau khi trình đọc PDF đóng, xóa file tạm
                if (System.IO.File.Exists(tempPath))
                {
                    System.IO.File.Delete(tempPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
