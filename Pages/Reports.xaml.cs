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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        private ObservableCollection<Report>? allReports;
        public Reports()
        {
            InitializeComponent();
            LoadProducts();
        }
        public void LoadProducts()
        {
            //string databasePath = "C:/Users/hknem/OneDrive/Masaüstü/shared/Smart_Agriculture/products.sqlite";
            string databasePath = "R:/Smart_Agriculture/irrigation_report.sqlite";
            string connectionString = $"Data Source={databasePath};Version=3;";

            allReports = new ObservableCollection<Report>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id, city_name, product_name, before_irrigation_humidity, after_irrigation_humidity, description, date FROM report";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string city_name = reader.GetString(1);
                            string product_name = reader.GetString(2);
                            double before_irrigation_humidity = reader.GetDouble(3);
                            double after_irrigation_humidity = reader.GetDouble(4);
                            string description = reader.GetString(5);
                            string date = reader.GetString(6);

                            allReports.Add(new Report { Id = id, City_Name = city_name, Product_Name = product_name, Before_Irrigation_Humidity = before_irrigation_humidity, After_Irrigation_Humidity = after_irrigation_humidity, Description = description, Date = date });
                        }
                    }
                }
            }
            reportsListView.ItemsSource = allReports;
        }
    }
}
