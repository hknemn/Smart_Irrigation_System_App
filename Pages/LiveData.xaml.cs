using Smart_Irrigation_System.Models;
using System;
using System.Collections.Generic;
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
using LiveCharts;
using LiveCharts.Wpf;
using Smart_Irrigation_System.Gauges;

namespace Smart_Irrigation_System.Pages
{
    public partial class LiveData : Page
    {
        private ViewModel.LiveData? liveData;
        public LiveData()
        {
            InitializeComponent();
            liveData = new ViewModel.LiveData();
            this.DataContext = liveData;
        }

        private void liveWeather_Click(object sender, RoutedEventArgs e)
        {
            liveSensorPanel.Visibility = Visibility.Collapsed;
            liveWeatherPanel.Visibility = Visibility.Visible;
            updateWeatherGauge();
        }

        private void liveSensor_Click(object sender, RoutedEventArgs e)
        {
            liveWeatherPanel.Visibility = Visibility.Collapsed;
            liveSensorPanel.Visibility = Visibility.Visible;
            updateSensorGauge();
        }

        private void updateSensorGauge()
        {
            string databasePath = "C:/Users/hknem/OneDrive/Masaüstü/shared/Smart_Agriculture/live_humidity.sqlite";
            //string databasePath = "R:/Smart_Agriculture/live_humidity.sqlite";      
            string connectionString = $"Data Source={databasePath};Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT name, humidity FROM product";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(0);
                        double humidity = reader.GetDouble(1);
                        if (liveData != null)
                        {
                            liveData.HumidityValue = humidity;
                        }
                        productName.Text = reader.GetString(0);
                    }
                }
                connection.Close();
            }
        }

        private void updateWeatherGauge()
        {
            string databasePath = "C:/Users/hknem/OneDrive/Masaüstü/shared/Smart_Agriculture/weathers.sqlite";
            //string databasePath = "R:/Smart_Agriculture/weathers.sqlite";      
            string connectionString = $"Data Source={databasePath};Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT city_name, humidity, temperature, description, date FROM weathers";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string city_name = reader.GetString(0);
                        double humidity = reader.GetDouble(1);
                        double temperature = reader.GetDouble(2);
                        string description = reader.GetString(3);
                        string date = reader.GetString(4);
                        if (liveData != null)
                        {
                            liveData.WeatherTemperature = temperature;
                            liveData.WeatherHumidity = humidity;

                            cityNameTextBlock.Text = city_name;
                            descriptionTextBlock.Text = description;
                            dateTextBlock.Text = date;
                        }
                    }
                }
                connection.Close();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            liveWeatherPanel.Visibility = Visibility.Collapsed;
            liveSensorPanel.Visibility = Visibility.Collapsed;
        }
    }
}
