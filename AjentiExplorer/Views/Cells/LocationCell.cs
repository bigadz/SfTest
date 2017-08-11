using System;
using AjentiExplorer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AjentiExplorer.Views.Cells
{
    public class LocationCell: ViewCell
    {
		private Map locationMap;
		private Label locationNameLabel;

		public LocationCell()
        {
            var grid = new Grid 
            {
                RowDefinitions = 
                {
					new RowDefinition { Height = new GridLength(180, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute) },
                },
                BackgroundColor = Settings.Dark6,
                //Margin = new Thickness(5),
            };

            this.locationNameLabel = new Label 
            { 
                FontAttributes = FontAttributes.None, 
                TextColor = Color.White,
                BackgroundColor = Settings.Dark1.MultiplyAlpha(0.5), 
                FontSize = 14,
                //VerticalOptions = LayoutOptions.End,
                //HorizontalOptions = LayoutOptions.CenterAndExpand,
                //Margin = new Thickness(5),
            };

            this.locationNameLabel.BindingContextChanged += (sender, e) => 
            {
				var locationViewModel = this.locationNameLabel.BindingContext as LocationViewModel;
				var location = locationViewModel?.Location;
				this.locationNameLabel.Text = location?.Title;
				if (location?.HasCoordinates == true)
				{
					var position = location.Position;
					var mapSpan = MapSpan.FromCenterAndRadius(
						position,
						Distance.FromKilometers(100));
					this.locationMap.MoveToRegion(mapSpan);

					var pin = new Pin
					{
						Type = PinType.SearchResult,
						Label = location.Name,
						Address = location.Address,
						Position = position,
					};

                    this.locationMap.Pins.Clear();
					this.locationMap.Pins.Add(pin);
				}
            };

			this.locationMap = new Map
            {
				IsShowingUser = false,
				MapType = MapType.Street,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
                HasZoomEnabled = false,
                HasScrollEnabled = false,
                IsEnabled = false
            };
                
            grid.Children.Add(this.locationMap,0,1,0,2);
            grid.Children.Add(this.locationNameLabel,0,1,1,2);

            View = grid;
        }
         
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                var locationViewModel = BindingContext as LocationViewModel;
                var location = locationViewModel?.Location;
                this.locationNameLabel.Text = location?.Title;
                if (location?.HasCoordinates == true)
                {
                    var position = location.Position;
                    var mapSpan = MapSpan.FromCenterAndRadius(
	                    position,
	                    Distance.FromKilometers(100));
					this.locationMap.MoveToRegion(mapSpan);

					var pin = new Pin
					{
						Type = PinType.SearchResult,
                        Label = location.Name,
						Address = location.Address,
						Position = position,
					};

                    this.locationMap.Pins.Add(pin);
				}
            }
        }

    }
}
