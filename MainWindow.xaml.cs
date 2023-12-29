﻿using Smart_Irrigation_System.Pages;
using Smart_Irrigation_System.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Smart_Irrigation_System
{
    public partial class MainWindow : Window
    {
        private Login? loginPage = null;
        public Home? homePage = null;
        private Products? productsPage = null;
        private InstantData? instantDataPage = null;
        public static Frame? MainFrame { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            MainFrame = mainFrame;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadHomePage();
        }

        private void LoadHomePage()
        {
            if (homePage == null)
            {
                homePage = new Home();
            }
            mainFrame.Navigate(homePage);
        }
        public void UpdateUserDisplay()
        {
            if (AppSession.LoggedInUser != null)
            {
                loginButton.Visibility = Visibility.Collapsed;
                userDropdown.Visibility = Visibility.Visible;
                userDropdown.Items.Clear();
                userDropdown.Items.Add(AppSession.LoggedInUser.Username);
                userDropdown.Items.Add("Çıkış Yap");

                userDropdown.SelectedIndex = 0;
            }           
            else
            {
                loginButton.Visibility = Visibility.Visible;
                userDropdown.Visibility = Visibility.Collapsed;
            }
        }
        private void NavigateToHomePage(object sender, RoutedEventArgs e)
        {
            if(homePage == null)
            {
                homePage = new Home();  
                mainFrame.Navigate(homePage);
            } 
            else
            {
                mainFrame.Navigate(homePage);
            }
        }

        private void NavigateToLoginPage(object sender, RoutedEventArgs e)
        {
            if(loginPage == null && (AppSession.LoggedInUser == null))
            {
                loginPage = new Login();
                mainFrame.Navigate(loginPage);  
            }
            else if(loginPage !=null && AppSession.LoggedInUser == null)
            {
                mainFrame.Navigate(loginPage);
            }
        }

        public void NavigateToProducts(object sender, RoutedEventArgs e)
        {
            if(productsPage == null)
            {
                productsPage = new Products();
                mainFrame.Navigate(productsPage);
            }
            else
            {
                mainFrame.Navigate(productsPage);
            }
        }

        public void NavigateToProducts()
        {
            if (productsPage == null)
            {
                productsPage = new Products();
                mainFrame.Navigate(productsPage);
            }
            else
            {
                mainFrame.Navigate(productsPage);
            }
        }

        private void NavigateToInstantData(object sender, RoutedEventArgs e)
        {
            if(instantDataPage == null)
            {
                instantDataPage = new InstantData();
                mainFrame.Navigate(instantDataPage);
            }
            else
            {
                mainFrame.Navigate(instantDataPage);
            }
        }

        private void UserDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userDropdown.SelectedItem != null)
            {
                string? selectedOption = userDropdown.SelectedItem.ToString();
                if (selectedOption != null)
                {
                    if (selectedOption.Equals("Çıkış Yap"))
                    {
                        if (MessageBox.Show("Çıkış yapmak istediğinizden emin misiniz?", "Onay", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            AppSession.LoggedInUser = null;
                            UpdateUserDisplay();
                            mainFrame.Navigate(homePage);
                        }
                        else
                        {
                            userDropdown.SelectedIndex = 0;
                        }
                    }
                }
            }
        }

    }
}