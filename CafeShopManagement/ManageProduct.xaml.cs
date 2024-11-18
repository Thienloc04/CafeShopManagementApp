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
    /// Interaction logic for ManageProduct.xaml
    /// </summary>
    public partial class ManageProduct : UserControl
    {
        private ProductService _service = new();
        private CategoryService _categoryService = new();
        private string _imagePath = "";
        public User LoginProuct {  get; set; }
        public ManageProduct()
        {
            InitializeComponent();
        }

        private void Import_Btn_Click(object sender, RoutedEventArgs e)
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
                ProductImg.Source = new BitmapImage(new Uri(_imagePath));
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK);
            }
        }
        private void FillDataGrid(List<Product> arr)
        {
            ProductDataGrid.ItemsSource = null;
            ProductDataGrid.ItemsSource = arr;
        }

        private void FillComboBox()
        {
            CategoryIdCombo.ItemsSource = _categoryService.GetCategories();
            CategoryIdCombo.SelectedValuePath = "CategoryId";
            CategoryIdCombo.DisplayMemberPath = "CategoryName";

            var statusList = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Active", 1),
                new KeyValuePair<string, int>("Inactive", 2)
            };

            Status_Combo.ItemsSource = statusList;
            Status_Combo.DisplayMemberPath = "Key";
            Status_Combo.SelectedValuePath = "Value";

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid(_service.GetAll());
            FillComboBox();
        }

        private void Add_Btn_Click(object sender, RoutedEventArgs e)
        {
            Product obj = new(); 
            obj.ProductName = ProductNameTextBox.Text;
            obj.Price = decimal.Parse(PriceTextBox.Text);
            obj.Quantity = int.Parse(QuantityTextBox.Text);
            obj.Status = int.Parse(Status_Combo.SelectedValue.ToString());
            obj.CreatedDate = DateTime.Now;
            obj.CategoryId = int.Parse(CategoryIdCombo.SelectedValue.ToString());
            if(!string.IsNullOrEmpty(_imagePath))
                obj.ProImg = _imagePath;

            _service.CreateProduct(obj);
            MessageBox.Show("Create new product successfully", "Success", MessageBoxButton.OK);
            ClearFields();
            FillDataGrid(_service.GetAll());
        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            Product? selected = ProductDataGrid.SelectedItem as Product;
            if(selected == null)
            {
                MessageBox.Show("Please choose a product before editing!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Product obj = new();
            obj.ProductId = int.Parse(ProductIdTextBox.Text);
            obj.ProductName = ProductNameTextBox.Text;
            obj.Price = decimal.Parse(PriceTextBox.Text);
            obj.Quantity = int.Parse(QuantityTextBox.Text);
            obj.Status = int.Parse(Status_Combo.SelectedValue.ToString());
            obj.CreatedDate = DateTime.Now;
            obj.CategoryId = int.Parse(CategoryIdCombo.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(_imagePath))
                obj.ProImg = _imagePath;

            _service.UpdateProduct(obj);
            MessageBox.Show("Update product successfully", "Success", MessageBoxButton.OK);
            ClearFields();
            FillDataGrid(_service.GetAll());
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            Product? selected = ProductDataGrid.SelectedItem as Product;
            if (selected == null)
            {
                MessageBox.Show("Please choose a product before deleting!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult result = MessageBox.Show("Are you really want to delete this product", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No) return;

            Product obj = new();
            obj.ProductId = int.Parse(ProductIdTextBox.Text);
            obj.ProductName = ProductNameTextBox.Text;
            obj.Price = decimal.Parse(PriceTextBox.Text);
            obj.Quantity = int.Parse(QuantityTextBox.Text);
            obj.Status = int.Parse(Status_Combo.SelectedValue.ToString());
            obj.CreatedDate = DateTime.Now;
            obj.CategoryId = int.Parse(CategoryIdCombo.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(_imagePath))
                obj.ProImg = _imagePath;

            _service.DeleteProduct(obj);
            MessageBox.Show("Delete product successfully", "Success", MessageBoxButton.OK);
            ClearFields();
            FillDataGrid(_service.GetAll());
        }

        private void Clear_Btn_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ProductDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductDataGrid.SelectedItem is Product selected)
            {
                ProductIdTextBox.Text = selected.ProductId.ToString();
                ProductNameTextBox.Text = selected.ProductName;
                PriceTextBox.Text = selected.Price.ToString();
                QuantityTextBox.Text = selected.Quantity.ToString();
                CategoryIdCombo.SelectedValue = selected.CategoryId;
                Status_Combo.SelectedValue = selected.Status;

                if (!string.IsNullOrWhiteSpace(selected.ProImg))
                {
                    ProductImg.Source = new BitmapImage(new Uri(selected.ProImg));
                    _imagePath = selected.ProImg;
                }
                else
                {
                    _imagePath = "";
                    ProductImg.Source = null;
                }
                Add_Btn.IsEnabled = false;
            }
        }

        private void ClearFields()
        {
            ProductIdTextBox.Text = "";
            ProductNameTextBox.Text = "";
            PriceTextBox.Text = "";
            QuantityTextBox.Text = "";
            Status_Combo.SelectedIndex = -1;
            CategoryIdCombo.SelectedValue = -1;
            ProductImg.Source = null;
            _imagePath = "";
            ProductDataGrid.SelectedItem = null;
            Add_Btn.IsEnabled = true;
        }


    }
}
