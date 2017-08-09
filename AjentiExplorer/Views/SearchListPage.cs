using System;

using Xamarin.Forms;


using AjentiExplorer.ViewModels;
using System.Threading.Tasks;

namespace AjentiExplorer.Views
{
	public class SearchListPage : BaseContentPage
	{
		private SearchListViewModel viewModel;

		public SearchListPage(SearchListViewModel viewModel)
		{
			BindingContext = this.viewModel = viewModel;
			this.viewModel.Navigation = this.Navigation;

			SetBinding(TitleProperty, new Binding("Title"));

			NavigationPage.SetHasNavigationBar(this, false);

			// Listen for messages from the modelview(s)
			MessagingCenter.Subscribe<InRangeViewModel, MessagingCenterAlert>(this, "alert", HandleMessagingCenterAlert);
			MessagingCenter.Subscribe<BaseSearchViewModel, MessagingCenterAlert>(this, "alert", HandleMessagingCenterAlert);
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

			this.Appearing += async (sender, e) =>
			{
				NavigationPage.SetHasNavigationBar(this, false);

				// This will not return null. It will return the default location (Hobart) if it fails to determine anything else.
				var lastKnownPosition = await this.viewModel.GetLastKnownPosition();

				//this.map = new Map(
				//		MapSpan.FromCenterAndRadius(
				//			new Position(lastKnownPosition.Latitude, lastKnownPosition.Longitude),
				//			Distance.FromKilometers(20)))
				//{
				//	IsShowingUser = false,
				//	MapType = MapType.Street,
				//	VerticalOptions = LayoutOptions.FillAndExpand,
				//	HorizontalOptions = LayoutOptions.FillAndExpand,
				//};
				//layoutGrid.Children.Add(this.map, 0, 1, 1, 2);
				layoutGrid.Children.Add(LayoutFactories.BusyIndicator.Create(viewModel), 0, 1, 0, 2);

				// If Searching....
				this.viewModel.SearchString = "FISH";
				var currentPositionTask = this.viewModel.GetCurrentPosition();
				var searchTask = this.viewModel.SearchAsync();
				await Task.WhenAll(currentPositionTask, searchTask);

			};

			this.Disappearing += (sender, e) => { if (!App.SwitchingTopLevelPages) NavigationPage.SetHasNavigationBar(this, true); };
		}

		//async void SearchResult_Clicked(object sender, EventArgs e)
		//{
  //          var searchResult = sender as 
		//	await this.Navigation.PushAsync(new LocationPage(searchResult.BindingContext as LocationViewModel));
		//}
	}
}

