using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

using AjentiExplorer.ViewModels;
using System.Threading.Tasks;

namespace AjentiExplorer.Views
{
    public class SearchMapPage : ContentPage
    {
        private SearchViewModel viewModel;

		public SearchMapPage(SearchViewModel viewModel)
        {
            BindingContext = this.viewModel = viewModel;
            this.viewModel.Navigation = this.Navigation;

            Title = "Map";

			// Listen for messages from the modelview(s)
			MessagingCenter.Subscribe<InRangeViewModel, MessagingCenterAlert>(this, "alert", async (src, alert) =>
			{
				var _alert = alert as MessagingCenterAlert;
				await DisplayAlert(alert.Title, alert.Message, alert.Cancel);
			});
			MessagingCenter.Subscribe<SearchViewModel, MessagingCenterAlert>(this, "alert", async (src, alert) =>
			{
				var _alert = alert as MessagingCenterAlert;
				await DisplayAlert(alert.Title, alert.Message, alert.Cancel);
			});
            MessagingCenter.Subscribe<LocationViewModel, MessagingCenterAlert>(this, "alert", async (src, alert) =>
			{
				var _alert = alert as MessagingCenterAlert;
				await DisplayAlert(alert.Title, alert.Message, alert.Cancel);
			});

            var navigationDrawer = LayoutFactories.NavigationDrawer.Create(viewModel);
			Content = navigationDrawer;

			var grid = new Grid();
			navigationDrawer.ContentView = grid;

			var busyIndicator = new Controls.BusyIndicator(viewModel);
			busyIndicator.SetBinding(IsVisibleProperty, new Binding("IsBusy"));
            grid.Children.Add(busyIndicator);

			Map map = null;

            this.Appearing += async (sender, e) => 
            {
				NavigationPage.SetHasNavigationBar(this, false);

				// Only create the map on first appearance
				if (map != null) return; 

                // This will not return null. It will return the default location (Hobart) if it fails to determine anything else.
                var lastKnownPosition = await this.viewModel.GetLastKnownPosition();

				map = new Map(
                        MapSpan.FromCenterAndRadius(
                            new Position(lastKnownPosition.Latitude, lastKnownPosition.Longitude),
                            Distance.FromKilometers(20)))
                {
                    IsShowingUser = false,
                    MapType = MapType.Street,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };
				grid.Children.Insert(0, map);

                // If Searching....
                this.viewModel.SearchString = "FISH";
                var currentPositionTask = this.viewModel.GetCurrentPosition();
                var searchTask = this.viewModel.SearchAsync();
                await Task.WhenAll(currentPositionTask, searchTask);
				var currentPosition = await currentPositionTask;

                // If InRanging...
				//var currentPosition = await this.viewModel.GetCurrentPosition();

				//            if (currentPosition == null)
				//            {
				//	this.viewModel.Latitude = lastKnownPosition.Latitude;
				//	this.viewModel.Longitude = lastKnownPosition.Longitude;
				//	await this.viewModel.InRangeAsync();
				//}
				//           else
				//           {
				//this.viewModel.Latitude = currentPosition.Latitude;
				//this.viewModel.Longitude = currentPosition.Longitude;
				//await this.viewModel.InRangeAsync();

				if (currentPosition != null)
                {
                    if (map != null)
                    {
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(
                                new Position(currentPosition.Latitude, currentPosition.Longitude),
                                Distance.FromKilometers(20)));
                        map.IsShowingUser = true;
                    }
                    else
                    {
                        map = new Map(
                                MapSpan.FromCenterAndRadius(
                                    new Position(currentPosition.Latitude, currentPosition.Longitude),
                                    Distance.FromKilometers(20)))
                        {
                            IsShowingUser = true,
                            MapType = MapType.Street,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                        };
                        grid.Children.Insert(0, map);
					}
                }

                if (map != null)
                {
                    foreach (var location in this.viewModel.Locations)
                    {
                        System.Diagnostics.Debug.WriteLine($"Location {location.Latitude},{location.Longitude} lbl={location.Name} addr={location.Address}");
						var pin = new Pin
						{
                            Type = PinType.SearchResult,
                            BindingContext = location,
                            Label = location.Name, // Note: There is no binding property for Label. (Why the &(*$ not?)
						};
						pin.SetBinding(Pin.PositionProperty, new Binding("Position"));
						pin.SetBinding(Pin.AddressProperty, new Binding("Address"));

						pin.Clicked += Pin_Clicked;
						map.Pins.Add(pin);
                    }
                }
            };

			this.Disappearing += (sender, e) => NavigationPage.SetHasNavigationBar(this, true);
        }

        async void Pin_Clicked(object sender, EventArgs e)
        {
            var pin = sender as Pin;
            await this.Navigation.PushAsync(new LocationPage(pin.BindingContext as LocationViewModel));
        }
    }
}

