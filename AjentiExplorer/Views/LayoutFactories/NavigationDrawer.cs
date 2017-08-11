using System;
using Syncfusion.SfNavigationDrawer.XForms;
using Xamarin.Forms;
using AjentiExplorer.Models;

namespace AjentiExplorer.Views.LayoutFactories
{
    public static class NavigationDrawer
    {
        private const int DrawerWidth = 200;
        private const int DrawerHeaderHeight = 100;
        private const int DrawerFooterHeight = 50;

        private static Grid navigationBar;

        public static SfNavigationDrawer Create(ViewModels.BaseViewModel viewModel)
        {
            var navigationDrawer = new SfNavigationDrawer
            {
                Position = Position.Left,
                Transition = Transition.Reveal,

                TouchThreshold = 50,
                DrawerWidth = NavigationDrawer.DrawerWidth,
                DrawerHeight = 100,
                DrawerHeaderHeight = NavigationDrawer.DrawerHeaderHeight,
                DrawerFooterHeight = NavigationDrawer.DrawerFooterHeight,
                DrawerHeaderView = NavigationDrawer.Header,
                DrawerContentView = NavigationDrawer.DrawContent(viewModel.MenuItems),
                DrawerFooterView = NavigationDrawer.Footer,
			};

			var connectedNavBar = new Grid
			{
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = new GridLength(60, GridUnitType.Absolute) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
				},
				RowDefinitions =
				{
                    new RowDefinition { Height = new GridLength(60, GridUnitType.Absolute) },
				},
                BackgroundColor = Settings.NavBarColor,
			};

            var header = new Label
            {
                Text = viewModel.Title,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.End,
                Margin = new Thickness(0, 30, 0, 10),
			};
			connectedNavBar.Children.Add(header, 0, 2, 0, 1);

            var menuButton = new Button
            {
                TextColor = Color.White,
                Image = (FileImageSource)ImageSource.FromFile("slideout.png"),
				Margin = new Thickness(0, 25, 0, 0),
                Command = new Command(() => navigationDrawer.ToggleDrawer()),
			};

			connectedNavBar.Children.Add(menuButton, 0, 0);

			NavigationDrawer.navigationBar = connectedNavBar;
			return navigationDrawer;
        }

		public static Layout NavigationBar
		{
			get
			{
				return navigationBar;
			}
		}

        private static Layout Header
        {
            get
            {
				var headerLayout = new Grid
				{
                    BackgroundColor = Settings.YouTubeRed,
				};

				var header = new Label
				{
					Text = "Ajenti Explorer",
					FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
					TextColor = Color.White,
					HorizontalTextAlignment = TextAlignment.Center,
					VerticalTextAlignment = TextAlignment.Center,
					BackgroundColor = Settings.YouTubeRed,
				};
				headerLayout.Children.Add(header);
                return headerLayout;
			}
        }

        private static Layout DrawContent(ObservableRangeCollection<DrawerMenuItem> menuItems)
        {
            var menuStack = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				HeightRequest = 500,
			};

            ListView listView = new ListView
            {
                WidthRequest = NavigationDrawer.DrawerWidth,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Settings.Dark4,
                SeparatorColor = Settings.DarkGray,

				ItemsSource = menuItems,
				ItemTemplate = new DataTemplate(typeof(Cells.DrawerMenuCell)),
			};

            listView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
			{
				var navPage = Application.Current.MainPage as NavigationPage;
				try
                {
                    var menuText = e.SelectedItem.ToString();

                    switch (menuText)
                    {
						case "Map":
                            await App.SwitchToPage(navPage.Navigation, new SearchMapPage(new ViewModels.SearchMapViewModel()));
							break;
						case "Search":
							await App.SwitchToPage(navPage.Navigation, new SearchListPage(new ViewModels.SearchListViewModel()));
							break;
						case "Recent":
                            await App.SwitchToPage(navPage.Navigation, new RecentLocationsPage(new ViewModels.RecentLocationsViewModel()));
							break;
						case "Favourites":
                            await App.SwitchToPage(navPage.Navigation, new FavouriteLocationsPage(new ViewModels.FavouriteLocationsViewModel()));
							break;
						case "Logout":
							Settings.Logout();
							await App.SwitchToPage(navPage.Navigation, new LoginPage(new ViewModels.LoginViewModel()));
							break;
					}

                    /*
                    if (menuText.Equals("Login"))
                    {
                        await App.SwitchToPage(navPage.Navigation, new LoginPage(new ViewModels.LoginViewModel()));
                    }

                    if (menuText.Equals("Logout"))
                    {
                        Settings.Logout();
						await App.SwitchToPage(navPage.Navigation, new LoginPage(new ViewModels.LoginViewModel()));
						//navigationDrawer.EnableSwipeGesture = true;
						//navigationDrawer.ContentView = new NumericUpDown().Content;
					}
					*/
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                }
			};


            menuStack.Children.Add(listView);
            return menuStack;
        }

		private static Layout Footer
		{
			get
			{
				var footerLayout = new StackLayout
				{
					//BackgroundColor = Color.FromHex("#1aa1d6"),
				};
                //var footer = new Label
                //{
                //	Text = "Footer View",
                //	FontSize = 14,
                //	TextColor = Color.White,
                //	HorizontalOptions = LayoutOptions.CenterAndExpand,
                //	VerticalOptions = LayoutOptions.CenterAndExpand,
                //	BackgroundColor = Color.FromHex("#1aa1d6"),
                //};
                var footer = new Image
                {
                    Source = ImageSource.FromFile("entura_logo.png"),
                    WidthRequest = NavigationDrawer.DrawerWidth - 20,
                    HeightRequest = NavigationDrawer.DrawerFooterHeight - 20,
				    HorizontalOptions = LayoutOptions.CenterAndExpand,
				    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Aspect = Aspect.AspectFit,
				};
				footerLayout.Children.Add(footer);
				return footerLayout;
			}
		}

	}
}
