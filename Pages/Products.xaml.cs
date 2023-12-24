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
        public Products()
        {
            InitializeComponent();
            LoadProducts();
        }
        private void LoadProducts()
        {
            string databasePath = "C:/Users/hknem/source/repos/SIS_App/Smart_Irrigation_System/Databases/products.sqlite";
            string connectionString = $"Data Source={databasePath};Version=3;";

            ObservableCollection<Product> productList = new ObservableCollection<Product>();

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
                            double temperature = reader.GetDouble(2);
                            double humidity = reader.GetDouble(3);

                            productList.Add(new Product { Id = id, Name = name, Temperature = temperature, Humidity = humidity });
                        }
                    }
                }
            }
            productsListView.ItemsSource = productList;
        }
    }
}
