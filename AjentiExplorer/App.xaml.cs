using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Syncfusion.SfNavigationDrawer.XForms;

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

        private static void SetMainPage()
        {
            if (true) //(!UseMockDataStore && !Settings.IsLoggedIn)
            {
				//var list = new ObservableRangeCollection<string>();
				//list.Add("Login");
				//list.Add("BusyIndicator");
				//list.Add("NumericUpDown");

                //var list2 = new ObservableRangeCollection<string>() { "Login", "EFN", "Map" };
                /*

                var navigationDrawer = Views.LayoutFactories.NavigationDrawer.Create(new ViewModels.SearchViewModel());



				Button Btnmenu = new Button();
				Btnmenu.Text = "Show Menu";
				Btnmenu.HorizontalOptions = LayoutOptions.CenterAndExpand;
				Btnmenu.VerticalOptions = LayoutOptions.CenterAndExpand;
				Btnmenu.BackgroundColor = Color.FromHex("#1aa1d6");
				Btnmenu.Clicked += (sender_, e3) =>
				{
                    navigationDrawer.ToggleDrawer();
				};

				StackLayout Stack = new StackLayout();
				Stack.BackgroundColor = Color.White;
				Stack.HeightRequest = 100;
				Stack.HorizontalOptions = LayoutOptions.Center;
				Stack.VerticalOptions = LayoutOptions.Center;
				Stack.Children.Add(Btnmenu);
                navigationDrawer.ContentView = Stack;

                var contentPage = new ContentPage
                {
                    Content = navigationDrawer,
                    
                };
*/
				var navPage = new NavigationPage(new Views.LoginPage(new ViewModels.LoginViewModel()))
				{
                    BarBackgroundColor = Settings.NavBarColor, //(Color)Current.Resources["Primary"],
                    BarTextColor = Color.White,
				};
				//var navPage = new NavigationPage(contentPage)
				//{
				//	BarBackgroundColor = (Color)Current.Resources["Primary"],
				//	BarTextColor = Color.White
				//};
                Current.MainPage = navPage;

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

        public static async System.Threading.Tasks.Task SwitchToPage(INavigation navigation, Page page, bool animated = true)
        {
			if (navigation == null) throw new ArgumentNullException("navigation");
			if (page == null) throw new ArgumentNullException("page");

            int numberOfPagesOnStack = navigation.NavigationStack.Count;
            if (numberOfPagesOnStack > 0)
            {
                var topPage = navigation.NavigationStack[numberOfPagesOnStack - 1];
                navigation.InsertPageBefore(page, topPage);
                await navigation.PopAsync(animated);
            }
            else
            {
                await navigation.PushAsync(page, animated);
            }
		}
    }
}
