using System;
using Syncfusion.SfNavigationDrawer.XForms;
using Xamarin.Forms;

namespace AjentiExplorer.Views.LayoutFactories
{
    public static class NavigationDrawer
    {
        private static Grid navigationBar;

        public static SfNavigationDrawer Create(ViewModels.BaseViewModel viewModel)
        {
            var navigationDrawer = new SfNavigationDrawer
            {
                Position = Position.Left,
                Transition = Transition.Push,

                TouchThreshold = 50,
                DrawerWidth = 200,
                DrawerHeight = 100,
                DrawerHeaderHeight = 100,
                DrawerFooterHeight = 50,
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
				BackgroundColor = (Color)Application.Current.Resources["Primary"],
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
					BackgroundColor = Color.FromHex("#1aa1d6"),
				};

				var header = new Label
				{
					Text = "Ajenti Explorer",
					FontSize = 14,
					TextColor = Color.White,
					HorizontalTextAlignment = TextAlignment.Center,
					VerticalTextAlignment = TextAlignment.Center,
					BackgroundColor = Color.FromHex("#1aa1d6"),
				};
				headerLayout.Children.Add(header);
                return headerLayout;
			}
        }

        private static Layout DrawContent(ObservableRangeCollection<string> menuItems)
        {
            var menuStack = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				HeightRequest = 500,
			};

			ListView listView = new ListView
			{
				WidthRequest = 200,
				VerticalOptions = LayoutOptions.FillAndExpand,
				ItemsSource = menuItems,
			};

            listView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
			{
				var navPage = Application.Current.MainPage as NavigationPage;
				try
                {
                    if (e.SelectedItem.ToString().Equals("Login"))
                    {
                        await App.SwitchToPage(navPage.Navigation, new Views.LoginPage(new ViewModels.LoginViewModel()));
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
				return footerLayout;
			}
		}

	}
}
