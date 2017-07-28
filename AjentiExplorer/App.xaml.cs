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
                var navigationDrawer = new SfNavigationDrawer
                {
					Position = Position.Left,
					Transition = Transition.Push,

					TouchThreshold = 50,
					//Duration = 1000,
                    DrawerWidth = 200,
					DrawerHeight = 100,
					DrawerHeaderHeight = 100,
					DrawerFooterHeight = 100,
                };
                var headerLayout = new Grid
                {
                    BackgroundColor = Color.FromHex("#1aa1d6"),
                };

                var header = new Label
                {
                    Text = "Header View",
                    FontSize = 14,
                    TextColor = Color.White,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    BackgroundColor = Color.FromHex("#1aa1d6"),
                };
				headerLayout.Children.Add(header);
				navigationDrawer.DrawerHeaderView = headerLayout;

				var list = new ObservableRangeCollection<string>();
				list.Add("Login");
				list.Add("BusyIndicator");
				list.Add("NumericUpDown");
                var mainStack = new StackLayout
                {
					Orientation = StackOrientation.Vertical,
				    HeightRequest = 500,
			    };

                ListView listView = new ListView
                {
					WidthRequest = 200,
				    VerticalOptions = LayoutOptions.FillAndExpand,
				    ItemsSource = list,
			    };


				mainStack.Children.Add(listView);
				navigationDrawer.DrawerContentView = mainStack;

                var footerLayout = new StackLayout
                {
                    BackgroundColor = Color.FromHex("#1aa1d6"),
                };
				footerLayout.BackgroundColor = Color.FromHex("#1aa1d6");
                var footer = new Label
                {
					Text = "Footer View",
					FontSize = 14,
					TextColor = Color.White,
					HorizontalOptions = LayoutOptions.CenterAndExpand,
					VerticalOptions = LayoutOptions.CenterAndExpand,
					BackgroundColor = Color.FromHex("#1aa1d6"),
			    };
				footerLayout.Children.Add(footer);
				navigationDrawer.DrawerFooterView = footerLayout;

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

				//var navPage = new NavigationPage(new Views.LoginPage(new ViewModels.LoginViewModel()))
				//{
				//	BarBackgroundColor = (Color)Current.Resources["Primary"],
				//	BarTextColor = Color.White
				//};
				var navPage = new NavigationPage(contentPage)
				{
					BarBackgroundColor = (Color)Current.Resources["Primary"],
					BarTextColor = Color.White
				};
                Current.MainPage = navPage;

				listView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
				{
					if (e.SelectedItem.ToString().Equals("Login"))
					{
                        await SwitchToPage(navPage.Navigation, new Views.LoginPage(new ViewModels.LoginViewModel()));
										//navigationDrawer.EnableSwipeGesture = true;
										//navigationDrawer.ContentView = new RangeSlider().Content;
									}

					if (e.SelectedItem.ToString().Equals("BusyIndicator"))
					{
										//navigationDrawer.EnableSwipeGesture = true;
										//navigationDrawer.ContentView = new BusyIndicator().Content;
									}

					if (e.SelectedItem.ToString().Equals("NumericUpDown"))
					{

										//navigationDrawer.EnableSwipeGesture = true;
										//navigationDrawer.ContentView = new NumericUpDown().Content;
									}
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
