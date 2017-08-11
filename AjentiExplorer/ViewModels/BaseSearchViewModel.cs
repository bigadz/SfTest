using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using AjentiExplorer.Models;

namespace AjentiExplorer.ViewModels
{
    public class BaseSearchViewModel : BaseViewModel
    {
		public ICommand SearchCommand { get; }

        public BaseSearchViewModel()
        {
			SearchCommand = new Command(async () => await SearchAsync());
		}

		string searchString = string.Empty;
		public string SearchString
		{
			get { return searchString; }
			set { SetProperty(ref searchString, value); }
		}


		public async Task<Position> GetLastKnownPosition()
		{
			Position position = null;

			try
			{
				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = 50;

                position = await locator.GetLastKnownLocationAsync();

				if (position == null)
                {
					Console.WriteLine("Position not determined. Using default");
                    position = new Position { Latitude = Settings.Latitude, Longitude = Settings.Longitude, Timestamp = DateTime.Now };
				}

				Console.WriteLine("Position Timestamp: {0}", position.Timestamp);
				Console.WriteLine("Position Latitude: {0}", position.Latitude);
				Console.WriteLine("Position Longitude: {0}", position.Longitude);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
				Console.WriteLine("Position unobtaintable. Using default");
				position = new Position { Latitude = Settings.Latitude, Longitude = Settings.Longitude, Timestamp = DateTime.Now };
			}

			return position;
		}

		
        public async Task<Position> GetCurrentPosition()
        {
            Position position = null;

			try
			{
				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = 50;

				position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
				if (position == null)
					return null;

				Console.WriteLine("Position Timestamp: {0}", position.Timestamp);
				Console.WriteLine("Position Latitude: {0}", position.Latitude);
				Console.WriteLine("Position Longitude: {0}", position.Longitude);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
			}

			return position;
        }


        ObservableRangeCollection<LocationViewModel> locationViewModels = new ObservableRangeCollection<LocationViewModel>();
		public ObservableRangeCollection<LocationViewModel> LocationViewModels
		{
			get { return locationViewModels; }
			set { SetProperty(ref locationViewModels, value); }
		}

        ObservableRangeCollection<Models.Location> locations = new ObservableRangeCollection<Models.Location>();
		public ObservableRangeCollection<Models.Location> Locations
		{
			get { return locations; }
			set { SetProperty(ref locations, value); }
		}

		public async Task SearchAsync()
		{
			try
			{
				IsBusy = true;
				BusyMessage = "Searching locations...";

				await TrySearchAsync();
			}
			finally
			{
				BusyMessage = string.Empty;
				IsBusy = false;
			}
		}


        async Task<bool> TrySearchAsync()
		{
			this.locationViewModels.Clear();
			
            var installationSearchRequest = new JsonMsgs.InstallationSearchRequest
			{
              searchStr = this.SearchString,
			};
		    var response = await this.dataViewApi.InstallationSearchAsync(installationSearchRequest);

            if (!response.result)
            {
				MessagingCenter.Send(this, "alert", new MessagingCenterAlert
				{
					Title = "Search Failure",
                    Message = $"Details: {response.message}",
					Cancel = "OK"
				});
            }
            else
            {
                var newLocationViewModels = new ObservableRangeCollection<LocationViewModel>();
                var newLocations = new ObservableRangeCollection<Models.Location>();
                response.results.ForEach(result =>
                {
                    if (result.latitude.HasValue && result.longitude.HasValue)
                    {
						newLocationViewModels.Add(new LocationViewModel(result));
                        newLocations.Add(new Models.Location(result));
                        Console.WriteLine($"Location {result.name}");
					}
                });
                this.locationViewModels.AddRange(newLocationViewModels);
                this.locations.AddRange(newLocations);
            }

            return response.result;
		}

	}
}
