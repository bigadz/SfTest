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

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            SetMainPage();

            var loginCreds = new JsonMsgs.AccountLoginRequest
			{
				username = "crosera",
				password = "mypassword",
                appname = "AjentiExplorer",
			};
			var body = Newtonsoft.Json.JsonConvert.SerializeObject(loginCreds);
            var mobileAjentiApi = new Services.MobileAjentiApi("Prod");
			System.Threading.Tasks.Task.Run(async () =>
			{
				string response;
				try
				{
                    response = await mobileAjentiApi.PostAsync("/Login", body);
					try
					{
						var loggedIn = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonMsgs.AccountLoginResponse>(response);
						bool success = loggedIn.result;
						string token = loggedIn.token;
					}
					catch (Exception ex)
					{
						string m = ex.Message;
					}
                }
				catch (ApplicationException appEx)
				{
					string m = appEx.Message;
				}

 
			});
        }

        public static void SetMainPage()
        {
            if (!UseMockDataStore && !Settings.IsLoggedIn)
            {
                Current.MainPage = new NavigationPage(new LoginPage())
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
