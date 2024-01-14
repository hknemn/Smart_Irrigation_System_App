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

    public partial class Reports : Page
    {
        private ObservableCollection<Report>? allReports;
        //private string databasePath = "C:/Users/hknem/OneDrive/Masaüstü/shared/Smart_Agriculture/Databases/irrigation_report.sqlite";
        private string databasePath = "R:/Smart_Agriculture/Databases/irrigation_report.sqlite";
        public Reports()
        {
            InitializeComponent();
            LoadProducts();
        }
        public void LoadProducts()
        {
            string connectionString = $"Data Source={databasePath};Version=3;";

            allReports = new ObservableCollection<Report>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id, city_name, product_name, before_irrigation_temperature, after_irrigation_temperature, before_irrigation_soilMoisture, after_irrigation_soilMoisture, description, date FROM report";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string city_name = reader.GetString(1);
                            string product_name = reader.GetString(2);
                            double before_irrigation_temperature = reader.GetDouble(3);
                            double after_irrigation_temperature = reader.GetDouble(4);
                            double before_irrigation_soilMoisture = reader.GetDouble(5);
                            double after_irrigation_soilMoisture = reader.GetDouble(6);
                            string description = reader.GetString(7);
                            string date = reader.GetString(8);

                            allReports.Add(new Report { Id = id, City_Name = city_name, Product_Name = product_name, Before_Irrigation_Temperature = before_irrigation_temperature, After_Irrigation_Temperature = after_irrigation_temperature, Before_Irrigation_SoilMoisture = before_irrigation_soilMoisture, After_Irrigation_SoilMoisture = after_irrigation_soilMoisture, Description = description, Date = date });
                        }
                    }
                    connection.Close();
                }
            }
            reportsListView.ItemsSource = allReports;
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }
    }
}
