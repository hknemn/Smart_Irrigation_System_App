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

    public partial class SensorConfigure : Page
    {
        public ObservableCollection<string> ProductNames { get; set; } = new ObservableCollection<string>();
        //private string databasePath = "C:/Users/hknem/OneDrive/Masaüstü/shared/Smart_Agriculture/Databases/products.sqlite";
        private string databasePath = "R:/Smart_Agriculture/Databases/products.sqlite";
        //private string databasePath2 = "C:/Users/hknem/OneDrive/Masaüstü/shared/Smart_Agriculture/Databases/selected_items.sqlite";
        private string databasePath2 = "R:/Smart_Agriculture/Databases/selected_items.sqlite";    

        public string? SelectedProductName { get; set; }

        public string? CityName { get; set; }

        public SensorConfigure()
        {
            InitializeComponent();
            LoadProductNames();
            LoadSelectedItems();
            this.DataContext = this;
        }
        private void LoadProductNames()
        {    
            string connectionString = $"Data Source={databasePath};Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT name FROM products";
                ProductNames.Add("Ürün Seçiniz");
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string productName = reader.GetString(0);
                            ProductNames.Add(productName);
                        }
                    }
                }
                connection.Close();
            }
            productNameComboBox.SelectedIndex = 0;
        }

        private void productNameComboBox_DropDownOpened(object sender, EventArgs e)
        {
            ProductNames.Clear();
            LoadProductNames();
        }

        private void configureButton_Click(object sender, RoutedEventArgs e)
        {
            if (productNameComboBox.SelectedItem != null && !string.IsNullOrEmpty(cityNameTextBox.Text) && productNameComboBox.SelectedIndex != 0)
            {
                string? selectedProductName = productNameComboBox.SelectedItem.ToString();
                string cityName = cityNameTextBox.Text;
  
                string connectionString = $"Data Source={databasePath2};Version=3;";

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE items SET product_name = @ProductName, city_name = @CityName WHERE id = 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", selectedProductName);
                        cmd.Parameters.AddWithValue("@CityName", cityName);

                        cmd.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                MessageBox.Show("SENSÖR BAŞARIYLA YAPILANDIRILDI", "BAŞARILI", MessageBoxButton.OK, MessageBoxImage.Information);
                productNameComboBox.SelectedIndex = 0;
                cityNameTextBox.Text = "";
                LoadSelectedItems();
            }
            else
            {
                MessageBox.Show("LÜTFEN TÜM ALANLARI DOLDURUN!", "UYARI", MessageBoxButton.OK, MessageBoxImage.Warning);
                productNameComboBox.SelectedIndex = 0;
            }
        }

        private void LoadSelectedItems()
        {
            List<SelectedItems> items = new List<SelectedItems>();

            string connectionString = $"Data Source={databasePath2};Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT city_name, product_name FROM items";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string cityName = reader.GetString(0);
                            string productName = reader.GetString(1);
                            items.Add(new SelectedItems { City_Name = cityName, Product_Name = productName });
                        }
                    }
                }
                connection.Close();
            }
            dataListView.ItemsSource = items;
        }
    }
}
