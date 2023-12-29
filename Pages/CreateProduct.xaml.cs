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

namespace Smart_Irrigation_System.Pages
{
    /// <summary>
    /// Interaction logic for CreateProduct.xaml
    /// </summary>
    public partial class CreateProduct : Page
    {
        public CreateProduct()
        {
            InitializeComponent();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            string name = txtProductName.Text;
            string min_temperature = txtTemperature.Text;
            string max_temperature = txtTemperature2.Text;
            string min_humidity = txtHumidity.Text;
            string max_humidity = txtHumidity2.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(min_temperature) || string.IsNullOrWhiteSpace(max_temperature) || string.IsNullOrWhiteSpace(min_humidity) || string.IsNullOrWhiteSpace(max_humidity))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

            string databasePath = "C:/Users/hknem/source/repos/SIS_App/Smart_Irrigation_System/Databases/products.sqlite";
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
                            txtProductName.Text = "";
                            txtTemperature.Text = "";
                            txtTemperature2.Text = "";
                            txtHumidity.Text = "";
                            txtHumidity2.Text = "";
                            Application.Current.MainWindow.Content = new Products();
                        }
                        else
                        {
                            MessageBox.Show("Ürün eklenirken bir hata oluştu!");
                            txtProductName.Text = "";
                            txtTemperature.Text = "";
                            txtTemperature2.Text = "";
                            txtHumidity.Text = "";
                            txtHumidity2.Text = "";
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Aynı ürün isminden zaten mevcut!");
                        txtProductName.Text = "";
                        txtTemperature.Text = "";
                        txtTemperature2.Text = "";
                        txtHumidity.Text = "";
                        txtHumidity2.Text = "";
                    }
                }
            }
        }
    }
}
