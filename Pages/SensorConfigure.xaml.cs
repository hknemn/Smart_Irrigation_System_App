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

        public string? SelectedProductName { get; set; }

        public string? CityName { get; set; }

        public SensorConfigure()
        {
            InitializeComponent();
            LoadProductNames();
            this.DataContext = this;
        }
        private void LoadProductNames()
        {
            string databasePath = "C:/Users/hknem/OneDrive/Masaüstü/shared/Smart_Agriculture/products.sqlite";
            //string databasePath = "R:/Smart_Agriculture/products.sqlite";      
            string connectionString = $"Data Source={databasePath};Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT name FROM products";
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
        }

        private void productNameComboBox_DropDownOpened(object sender, EventArgs e)
        {
            ProductNames.Clear();
            LoadProductNames();
        }

        private void configureButton_Click(object sender, RoutedEventArgs e)
        {
            if (productNameComboBox.SelectedItem != null && !string.IsNullOrEmpty(cityNameTextBox.Text))
            {
                string? selectedProductName = productNameComboBox.SelectedItem.ToString();
                string cityName = cityNameTextBox.Text;

                string databasePath = "C:/Users/hknem/OneDrive/Masaüstü/shared/Smart_Agriculture/selected_items.sqlite";
                //string databasePath = "R:/Smart_Agriculture/selected_items.sqlite";      
                string connectionString = $"Data Source={databasePath};Version=3;";

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
                MessageBox.Show("Sensör başarıyla yapılandırıldı!", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
