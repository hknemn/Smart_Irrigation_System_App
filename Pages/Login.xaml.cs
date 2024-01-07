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
using System.Windows.Shapes;
using System.IO;
using Smart_Irrigation_System.Models;
using Smart_Irrigation_System.Services;

namespace Smart_Irrigation_System.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            CheckUserCredentials();
        }

        private void CheckUserCredentials()
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            if (AuthenticateUser(username, password))
            {
                MessageBox.Show("Giriş başarılı!");
                AppSession.LoggedInUser = new User { Username = username };
                if (Application.Current.MainWindow is MainWindow mainWnd)
                {
                    if (mainWnd.homePage == null)
                    {
                        mainWnd.homePage = new Home();
                    }
                    mainWnd.mainFrame.Navigate(mainWnd.homePage);
                    mainWnd.UpdateUserDisplay();
                    txtUsername.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış!");
                txtPassword.Password = "";
            }
        }

        private static bool AuthenticateUser(string username, string password)
        {
            string databasePath = "C:/Users/hknem/source/repos/SIS_App/Smart_Irrigation_System/Databases/users.sqlite";
            string connectionString = $"Data Source={databasePath};Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM users WHERE username=@username AND password=@password";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;  
                    }
                }
            }
        }
    }
}
