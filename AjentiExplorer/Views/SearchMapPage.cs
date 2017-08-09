﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

using AjentiExplorer.ViewModels;
using System.Threading.Tasks;

namespace AjentiExplorer.Views
{
    public class SearchMapPage : BaseContentPage
    {
        private SearchMapViewModel viewModel;

        private Map map;
        private MapSpan lastUsedMapSpan;

		public SearchMapPage(SearchMapViewModel viewModel)
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

			this.map = null;

            this.Appearing += async (sender, e) => 
            {
				NavigationPage.SetHasNavigationBar(this, false);

				// Only create the map on first appearance, otherwise reset region
				if (map != null)
                {
                    try
                    {
                        if (this.lastUsedMapSpan != null)
                        {
                            this.map.MoveToRegion(this.lastUsedMapSpan);
							// No exceptions? Then our job here is done.
							return;
						}
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Can set map's region: {ex.Message}");
                    }
                }

                // This will not return null. It will return the default location (Hobart) if it fails to determine anything else.
                var lastKnownPosition = await this.viewModel.GetLastKnownPosition();

				this.map = new Map(
                        MapSpan.FromCenterAndRadius(
                            new Position(lastKnownPosition.Latitude, lastKnownPosition.Longitude),
                            Distance.FromKilometers(20)))
                {
                    IsShowingUser = false,
                    MapType = MapType.Street,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };
                layoutGrid.Children.Add(this.map, 0, 1, 1, 2);
				layoutGrid.Children.Add(LayoutFactories.BusyIndicator.Create(viewModel), 0, 1, 0, 2);

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
                    this.map.MoveToRegion(MapSpan.FromCenterAndRadius(
                            new Position(currentPosition.Latitude, currentPosition.Longitude),
                            Distance.FromKilometers(20)));
                    this.map.IsShowingUser = true;
                }

                foreach (var location in this.viewModel.Locations)
                {
                    //System.Diagnostics.Debug.WriteLine($"Location {location.Latitude},{location.Longitude} lbl={location.Name} addr={location.Address}");
					var pin = new Pin
					{
                        Type = PinType.SearchResult,
                        BindingContext = location,
                        Label = location.Name, // Note: There is no binding property for Label. (Why not?)
					};
					pin.SetBinding(Pin.PositionProperty, new Binding("Position"));
					pin.SetBinding(Pin.AddressProperty, new Binding("Address"));

					pin.Clicked += Pin_Clicked;
					this.map.Pins.Add(pin);
                }
            };

			this.Disappearing += (sender, e) => NavigationPage.SetHasNavigationBar(this, true);
		}

        async void Pin_Clicked(object sender, EventArgs e)
        {
            var pin = sender as Pin;

            this.lastUsedMapSpan = this.map.VisibleRegion;

            await this.Navigation.PushAsync(new LocationPage(pin.BindingContext as LocationViewModel));
        }
    }
}

