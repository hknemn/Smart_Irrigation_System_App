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
    }
}
