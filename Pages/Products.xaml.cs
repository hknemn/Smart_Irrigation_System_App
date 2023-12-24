using Smart_Irrigation_System.Models;
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
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        private ObservableCollection<Product>? allProducts;

        public Products()
        {
            InitializeComponent();
            LoadProducts();
        }
        private void LoadProducts()
        {
            string databasePath = "C:/Users/hknem/source/repos/SIS_App/Smart_Irrigation_System/Databases/products.sqlite";
            string connectionString = $"Data Source={databasePath};Version=3;";

            allProducts = new ObservableCollection<Product>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id, name, temperature, humidity FROM Products";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            double temperature = reader.GetInt32(2);
                            double humidity = reader.GetInt32(3);

                            allProducts.Add(new Product { Id = id, Name = name, Temperature = temperature, Humidity = humidity });
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

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            string name = txtProductName.Text;
            string temperatureStr = txtTemperature.Text;
            string humidityStr = txtHumidity.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(temperatureStr) || string.IsNullOrWhiteSpace(humidityStr))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

            if (!double.TryParse(temperatureStr, out double temperature) || !double.TryParse(humidityStr, out double humidity))
            {
                MessageBox.Show("Sıcaklık ve Nem değerleri sayı olmalıdır!");
                return;
            }

            string databasePath = "C:/Users/hknem/source/repos/SIS_App/Smart_Irrigation_System/Databases/products.sqlite";
            string connectionString = $"Data Source={databasePath};Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Products (name, temperature, humidity) VALUES (@name, @temperature, @humidity)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@temperature", temperature);
                    cmd.Parameters.AddWithValue("@humidity", humidity);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Ürün başarıyla eklendi!");
                        LoadProducts();
                        txtProductName.Text = "";
                        txtTemperature.Text = "";
                        txtHumidity.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Ürün eklenirken bir hata oluştu!");
                        txtProductName.Text = "";
                        txtTemperature.Text = "";
                        txtHumidity.Text = "";
                    }
                }
            }
        }
        private void txtProductName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtProductName.Text == "Ürünün İsmini Giriniz")
            {
                txtProductName.Text = "";
            }
        }

        private void txtProductName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                txtProductName.Text = "Ürünün İsmini Giriniz";
            }
        }

        private void txtTemperature_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTemperature.Text == "Gerekli Sıcaklık Değerini Giriniz")
            {
                txtTemperature.Text = "";
            }
        }

        private void txtTemperature_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTemperature.Text))
            {
                txtTemperature.Text = "Gerekli Sıcaklık Değerini Giriniz";
            }
        }

        private void txtHumidity_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtHumidity.Text == "Gerekli Nem Değerini Giriniz")
            {
                txtHumidity.Text = "";
            }
        }

        private void txtHumidity_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHumidity.Text))
            {
                txtHumidity.Text = "Gerekli Nem Değerini Giriniz";
            }
        }

    }
}
