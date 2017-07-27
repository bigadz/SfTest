using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;


namespace AjentiExplorer.ViewModels
{
    public class InRangeViewModel : BaseViewModel
    {
        public InRangeViewModel()
        {
		}

		double range = 1000000;
		public double Range
		{
			get { return range; }
			set { SetProperty(ref range, value); }
		}

		double latitude;
		public double Latitude
		{
			get { return latitude; }
			set { SetProperty(ref latitude, value); }
		}

		double longitude;
		public double Longitude
		{
			get { return longitude; }
			set { SetProperty(ref longitude, value); }
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


        ObservableRangeCollection<LocationViewModel> locations = new ObservableRangeCollection<LocationViewModel>();
		public ObservableRangeCollection<LocationViewModel> Locations
		{
			get { return locations; }
			set { SetProperty(ref locations, value); }
		}

		public async Task InRangeAsync()
		{
			try
			{
				IsBusy = true;
				BusyMessage = "Loading nearby locations...";

				await TryInRangeAsync();
			}
			finally
			{
				BusyMessage = string.Empty;
				IsBusy = false;
			}
		}


        async Task<bool> TryInRangeAsync()
		{
			this.locations.Clear();
			
            var installationsInRangeRequest = new JsonMsgs.InstallationsInRangeRequest
			{
                distance = this.Range,
                latitude = this.Latitude,
                longitude = this.longitude,
			};
            var response = await this.dataViewApi.InstallationsInRangeAsync(installationsInRangeRequest);

            if (!response.result)
            {
				MessagingCenter.Send(this, "alert", new MessagingCenterAlert
				{
					Title = "In Range Failure",
                    Message = $"Details: {response.message}",
					Cancel = "OK"
				});
            }
            else
            {
                response.results.ForEach(result => this.locations.Add(new LocationViewModel(result)));
                if (this.locations.Count == 0)
                {
					MessagingCenter.Send(this, "alert", new MessagingCenterAlert
					{
						Title = "Nearby Installations",
						Message = "No nearby installations found",
						Cancel = "OK"
					});
                }
            }

            return response.result;
		}

	}
}
