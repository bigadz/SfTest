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

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Determining location" }
                }
            };

            this.Appearing += async (sender, e) => 
            {
                var currentPositionTask = this.viewModel.GetCurrentPosition();
                var searchTask = this.viewModel.SearchAsync();

                await Task.WhenAll(currentPositionTask, searchTask);

				var currentPosition = await currentPositionTask;

                if (currentPosition != null)
                {
					var map = new Map(
                            MapSpan.FromCenterAndRadius(
	                            new Position(currentPosition.Latitude, currentPosition.Longitude), 
	                            Distance.FromKilometers(20)))
					{
						IsShowingUser = true,
                        MapType = MapType.Hybrid,
						VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
					};
					var stack = new StackLayout { Spacing = 0 };
					stack.Children.Add(map);
					Content = stack;

                    foreach (var location in this.viewModel.Locations)
                    {
                        System.Diagnostics.Debug.WriteLine($"Location {location.latitude},{location.longitude} lbl={location.name} addr={location.address}");
						var pin = new Pin
						{
                            Type = PinType.SearchResult,
                            Position = new Position((double)location.latitude, (double)location.longitude),
                            Label = location.name,
                            Address = location.address
						};
						map.Pins.Add(pin);
                    }
                }
            };
        }
	}
}

