using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;


namespace AjentiExplorer.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
		public ICommand SearchCommand { get; }

        public SearchViewModel()
        {
			SearchCommand = new Command(async () => await SearchAsync());
		}

		string searchString = string.Empty;
		public string SearchString
		{
			get { return searchString; }
			set { SetProperty(ref searchString, value); }
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

				Console.WriteLine("Position Status: {0}", position.Timestamp);
				Console.WriteLine("Position Latitude: {0}", position.Latitude);
				Console.WriteLine("Position Longitude: {0}", position.Longitude);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
			}

			return position;
        }


        ObservableRangeCollection<JsonMsgs.Location> locations = new ObservableRangeCollection<JsonMsgs.Location>();
		public ObservableRangeCollection<JsonMsgs.Location> Locations
		{
			get { return locations; }
			set { SetProperty(ref locations, value); }
		}

		public async Task SearchAsync()
		{
			try
			{
				IsBusy = true;
				BusyMessage = "Searching...";

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
			this.locations.Clear();
			
            var installationSearchRequest = new JsonMsgs.InstallationSearchRequest
			{
			  token = Settings.AuthToken,
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
                response.results.ForEach(result => this.locations.Add((result)));
            }

            return response.result;
		}

	}
}
