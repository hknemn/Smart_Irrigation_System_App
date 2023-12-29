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
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        private ObservableCollection<Product>? allProducts;
        private CreateProduct? createProductPage = null;
        private MainWindow mainWindow  = new MainWindow();

        public Products()
        {
            InitializeComponent();
            LoadProducts();
        }
        public void LoadProducts()
        {
            string databasePath = "C:/Users/hknem/source/repos/SIS_App/Smart_Irrigation_System/Databases/products.sqlite";
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
                if(createProductPage == null)
                {
                    createProductPage = new CreateProduct();
                    NavigationService.GetNavigationService(this).Navigate(createProductPage);
                } 
                else
                {
                    NavigationService.GetNavigationService(this).Navigate(createProductPage);
                }
            }
        }
    }
}
