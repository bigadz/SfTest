using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AjentiExplorer
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";

        public static IDictionary<string, string> LoginParameters => null;

        public App()
        {
            InitializeComponent();
            MessagingCenterAlert.Init();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            if (true) //(!UseMockDataStore && !Settings.IsLoggedIn)
            {
                Current.MainPage = new NavigationPage(new Views.LoginPage(new LoginViewModel()))
                {
                    BarBackgroundColor = (Color)Current.Resources["Primary"],
                    BarTextColor = Color.White
                };
            }
            else
            {
                GoToMainPage();
            }
        }

        public static void GoToMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children = {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse",
                        Icon = Device.OnPlatform("tab_feed.png", null, null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png", null, null)
                    },
                }
            };
        }
    }
}
