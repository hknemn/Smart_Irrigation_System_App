using Smart_Irrigation_System.Models;
using Smart_Irrigation_System.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
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

namespace Smart_Irrigation_System.Pages
{
    public partial class Products : Page
    {
        private ObservableCollection<Product>? allProducts;
        private Product? selectedProduct = null;

        public Products()
        {
            InitializeComponent();
            LoadProducts();
        }
        public void LoadProducts()
        {
            string databasePath = "C:/Users/hknem/OneDrive/Masaüstü/shared/Smart_Agriculture/products.sqlite";
            //string databasePath = "R:/Smart_Agriculture/products.sqlite";      
            string connectionString = $"Data Source={databasePath};Version=3;";

            allProducts = new ObservableCollection<Product>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id, name, min_temperature, max_temperature, min_humidity, max_humidity FROM Products";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            double min_temperature = reader.GetInt32(2);
                            double max_temperature = reader.GetInt32(3);
                            double min_humidity = reader.GetInt32(4);
                            double max_humidity = reader.GetInt32(5);

                            allProducts.Add(new Product { Id = id, Name = name, Min_Temperature = min_temperature, Max_Temperature = max_temperature, Min_Humidity = min_humidity, Max_Humidity = max_humidity });
                        }
                    }
                }
            }
            productsListView.ItemsSource = allProducts;
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();

            ObservableCollection<Product> filteredProducts = new ObservableCollection<Product>();

            if(allProducts != null)
            {
                foreach (var product in allProducts)
                {
                    if (product.Name.ToLower().Contains(searchText))
                    {
                        filteredProducts.Add(product);
                    }
                }
            }
            productsListView.ItemsSource = filteredProducts;
        }

        private void createProductButton_Click(object sender, RoutedEventArgs e)
        {
            if(AppSession.LoggedInUser == null)
            {
                MessageBox.Show("Önce giriş yapmalısınız!");
            }
            else
            {
                productList.Visibility = Visibility.Collapsed;
                updateProduct.Visibility = Visibility.Collapsed;
                addProduct.Visibility = Visibility.Visible;
            }
        }

        private void deleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            if(AppSession.LoggedInUser != null)
            {
                var selectedProduct = productsListView.SelectedItem as Smart_Irrigation_System.Models.Product;

                if (selectedProduct != null)
                {
                    var result = MessageBox.Show("Ürünü silmek istiyor musunuz?", "Ürün Silme", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        DeleteProduct(selectedProduct.Id);
                        LoadProducts();
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir ürün seçiniz.");
                }
            }
            else
            {
                MessageBox.Show("Önce giriş yapmalısınız!");
            }
        }
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            string name = txtProductNameAdd.Text;
            string min_temperature = txtTemperatureAdd.Text;
            string max_temperature = txtTemperatureAdd2.Text;
            string min_humidity = txtHumidityAdd.Text;
            string max_humidity = txtHumidityAdd2.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(min_temperature) || string.IsNullOrWhiteSpace(max_temperature) || string.IsNullOrWhiteSpace(min_humidity) || string.IsNullOrWhiteSpace(max_humidity))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

            string databasePath = "C:/Users/hknem/OneDrive/Masaüstü/shared/Smart_Agriculture/products.sqlite";
            //string databasePath = "R:/Smart_Agriculture/products.sqlite";
            string connectionString = $"Data Source={databasePath};Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Products (name, min_temperature, max_temperature, min_humidity, max_humidity) VALUES (@name, @min_temperature, @max_temperature, @min_humidity, @max_humidity)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@min_temperature", min_temperature);
                    cmd.Parameters.AddWithValue("@max_temperature", max_temperature);
                    cmd.Parameters.AddWithValue("@min_humidity", min_humidity);
                    cmd.Parameters.AddWithValue("@max_humidity", max_humidity);

                    try
                    {
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Ürün başarıyla eklendi!");
                            txtProductNameAdd.Text = "";
                            txtTemperatureAdd.Text = "";
                            txtTemperatureAdd2.Text = "";
                            txtHumidityAdd.Text = "";
                            txtHumidityAdd2.Text = "";
                            addProduct.Visibility = Visibility.Collapsed;
                            productList.Visibility = Visibility.Visible;
                            LoadProducts();
                        }
                        else
                        {
                            MessageBox.Show("Ürün eklenirken bir hata oluştu!");
                            txtProductNameAdd.Text = "";
                            txtTemperatureAdd.Text = "";
                            txtTemperatureAdd2.Text = "";
                            txtHumidityAdd.Text = "";
                            txtHumidityAdd2.Text = "";
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Aynı ürün isminden zaten mevcut!");
                        txtProductNameAdd.Text = "";
                    }
                }
            }
        }
        private void updateProductButton_Click(object sender, RoutedEventArgs e)
        {
            if(AppSession.LoggedInUser != null)
            {
                selectedProduct = productsListView.SelectedItem as Smart_Irrigation_System.Models.Product;
                if (selectedProduct != null)
                {
                    productList.Visibility = Visibility.Collapsed;
                    updateProduct.Visibility = Visibility.Visible;
                    addProduct.Visibility = Visibility.Collapsed;
                    txtProductName.Text = selectedProduct.Name;
                    txtTemperature.Text = selectedProduct.Min_Temperature.ToString();
                    txtTemperature2.Text = selectedProduct.Max_Temperature.ToString();
                    txtHumidity.Text = selectedProduct.Min_Humidity.ToString();
                    txtHumidity2.Text = selectedProduct.Max_Humidity.ToString();
                }
                else
                {
                    MessageBox.Show("Lütfen bir ürün seçiniz.");
                }
            }
            else
            {
                MessageBox.Show("Önce giriş yapmalısınız!");
            }
        }

        public void DeleteProduct(int productId)
        {
            string databasePath = "C:/Users/hknem/OneDrive/Masaüstü/shared/Smart_Agriculture/products.sqlite";
            //string databasePath = "R:/Smart_Agriculture/products.sqlite";
            string connectionString = $"Data Source={databasePath};Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string sql = "DELETE FROM products WHERE Id = @ProductId";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Ürün başarıyla silindi!");
                    }
                    else
                    {
                        MessageBox.Show("Ürün silinirken bir hata oluştu!");
                    }
                }

                conn.Close();
            }
        }
        public void UpdateProduct(Product updatedProduct)
        {
            string databasePath = "C:/Users/hknem/OneDrive/Masaüstü/shared/Smart_Agriculture/products.sqlite";
            //string databasePath = "R:/Smart_Agriculture/products.sqlite";
            string connectionString = $"Data Source={databasePath};Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string sql = @"UPDATE products SET name = @Name, min_temperature = @Min_Temperature, max_temperature = @Max_Temperature, min_humidity = @Min_Humidity, max_humidity = @Max_Humidity WHERE id = @Id";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", updatedProduct.Name);
                    cmd.Parameters.AddWithValue("@Min_Temperature", updatedProduct.Min_Temperature);
                    cmd.Parameters.AddWithValue("@Max_Temperature", updatedProduct.Max_Temperature);
                    cmd.Parameters.AddWithValue("@Min_Humidity", updatedProduct.Min_Humidity);
                    cmd.Parameters.AddWithValue("@Max_Humidity", updatedProduct.Max_Humidity);
                    cmd.Parameters.AddWithValue("@Id", updatedProduct.Id);

                    int result = cmd.ExecuteNonQuery();
                    if(result > 0)
                    {
                        MessageBox.Show("Ürün başarıyla güncellendi!");
                    }
                    else
                    {
                        MessageBox.Show("Ürün güncellenirken bir hata oluştu!");
                    }
                }

                conn.Close();
            }
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct != null)
            {
                selectedProduct.Name = txtProductName.Text;
                selectedProduct.Min_Temperature = Convert.ToDouble(txtTemperature.Text);
                selectedProduct.Max_Temperature = Convert.ToDouble(txtTemperature2.Text);
                selectedProduct.Min_Humidity = Convert.ToDouble(txtHumidity.Text);
                selectedProduct.Max_Humidity = Convert.ToDouble(txtHumidity2.Text);
                UpdateProduct(selectedProduct);
                updateProduct.Visibility = Visibility.Collapsed;
                productList.Visibility = Visibility.Visible;
                LoadProducts();
            }
        }
        private void CloseButton1_Click(object sender, RoutedEventArgs e)
        {
            addProduct.Visibility = Visibility.Collapsed;
            LoadProducts();
            productList.Visibility = Visibility.Visible;
        }
        private void CloseButton2_Click(object sender, RoutedEventArgs e)
        {
            updateProduct.Visibility = Visibility.Collapsed;
            LoadProducts();
            productList.Visibility= Visibility.Visible;
        }

    }
}
