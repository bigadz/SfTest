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

            var grid = new Grid();
            Content = grid;

			var busyIndicator = new Controls.BusyIndicator(viewModel);
			busyIndicator.SetBinding(ContentView.IsVisibleProperty, new Binding("IsBusy"));
            grid.Children.Add(busyIndicator);

			Map map = null;

            this.Appearing += async (sender, e) => 
            {
                // Only create the map on first appearance
                if (map != null) return; 

                var lastKnownPosition = await this.viewModel.GetLastKnownPosition();

                if (lastKnownPosition != null)
                {
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
				}


                var currentPositionTask = this.viewModel.GetCurrentPosition();
                var searchTask = this.viewModel.SearchAsync();

                await Task.WhenAll(currentPositionTask, searchTask);

				var currentPosition = await currentPositionTask;

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
        }

        async void Pin_Clicked(object sender, EventArgs e)
        {
            var pin = sender as Pin;
            await this.Navigation.PushAsync(new LocationPage(pin.BindingContext as LocationViewModel));
        }
    }
}

