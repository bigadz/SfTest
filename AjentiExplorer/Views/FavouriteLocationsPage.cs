using System;
using AjentiExplorer.ViewModels;
using Xamarin.Forms;

namespace AjentiExplorer.Views
{
    public class FavouriteLocationsPage : BaseContentPage
    {
        private FavouriteLocationsViewModel viewModel;

		public FavouriteLocationsPage(FavouriteLocationsViewModel viewModel)
		{
			BindingContext = this.viewModel = viewModel;

			SetBinding(TitleProperty, new Binding("Title"));

			NavigationPage.SetHasNavigationBar(this, false);


			// Listen for messages from the modelview(s)
			MessagingCenter.Subscribe<FavouriteLocationsViewModel, MessagingCenterAlert>(this, "alert", HandleMessagingCenterAlert);
			MessagingCenter.Subscribe<LocationViewModel, MessagingCenterAlert>(this, "alert", HandleMessagingCenterAlert);

			var navigationDrawer = LayoutFactories.NavigationDrawer.Create(viewModel);
			Content = navigationDrawer;

			var layoutGrid = new Grid
			{
				RowDefinitions =
				{
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
				}
			};
			layoutGrid.Children.Add(LayoutFactories.NavigationDrawer.NavigationBar, 0, 1, 0, 1);
			navigationDrawer.ContentView = layoutGrid;

			this.Appearing += (sender, e) =>
			{
				NavigationPage.SetHasNavigationBar(this, false);

				layoutGrid.Children.Add(LayoutFactories.BusyIndicator.Create(viewModel), 0, 1, 0, 2);

                // Do stuff
			};

			this.Disappearing += (sender, e) => { if (!App.SwitchingTopLevelPages) NavigationPage.SetHasNavigationBar(this, true); };
		}
    }
}

