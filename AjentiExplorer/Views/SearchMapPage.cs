using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

using AjentiExplorer.ViewModels;

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
				var position = await this.viewModel.GetCurrentPosition();

                if (position != null)
                {
					var map = new Map(
                            MapSpan.FromCenterAndRadius(
	                            new Position(position.Latitude, position.Longitude), 
	                            Distance.FromKilometers(20)))
					{
						IsShowingUser = true,
						//HeightRequest = 100,
						//WidthRequest = 960,
						VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
					};
					var stack = new StackLayout { Spacing = 0 };
					stack.Children.Add(map);
					Content = stack;
                }
            };
        }
    }
}

