using System;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
             
namespace AjentiExplorer.ViewModels
{
    public class LocationViewModel : BaseViewModel
    {
        private JsonMsgs.Location location;

		public ICommand LoadImagesCommand { get; }

		public LocationViewModel(JsonMsgs.Location location)
        {
            this.location = location;
			LoadImagesCommand = new Command(async () => await LoadImagesAsync());

		}

		public int Id
		{
            get { return this.location.id; }
		}

		public string Name
		{
            get { return this.location.name; }
		}

		public string Address
		{
            get { return this.location.address; }
		}

        private string siteTypes;
		public string SiteTypes
		{
            get 
            {
                if (this.siteTypes == null)
                {
                    var sb = new StringBuilder();
                    foreach (var siteType in this.location.siteTypes)
                    {
                        if (sb.Length == 0)
                        {
                            sb.Append(siteType);
                        }
                        else
                        {
                            sb.Append(", ");
                            sb.Append(siteType);
                        }
                    }
                    this.siteTypes = sb.ToString();
                }
                return this.siteTypes;
            }
		}

		public bool HasCoordinates
		{
            get { return this.location.latitude.HasValue && this.location.longitude.HasValue; }
		}

		public double Latitude
		{
            get { return this.location.latitude.Value; }
		}

		public double Longitude
		{
            get { return this.location.longitude.Value; }
		}

        public Position Position
        {
            get { return new Position(Latitude, Longitude);  }
        }


        ObservableRangeCollection<JsonMsgs.CameraImage> cameraImages = new ObservableRangeCollection<JsonMsgs.CameraImage>();
        public ObservableRangeCollection<JsonMsgs.CameraImage> CameraImages
        {
            get { return cameraImages; }
            set { SetProperty(ref cameraImages, value); }
        }

		public async Task LoadImagesAsync()
		{
			try
			{
				IsBusy = true;
				BusyMessage = "Loading images...";

				await TryLoadImagesAsync();
			}
			finally
			{
				BusyMessage = string.Empty;
				IsBusy = false;
			}
		}


		async Task<bool> TryLoadImagesAsync()
		{
			this.cameraImages.Clear();

            var getCameraImageLinksRequest = new JsonMsgs.GetCameraImageLinksRequest
			{
                id = this.location.id,
			};
            var response = await this.dataViewApi.GetCameraImageLinksAsync(getCameraImageLinksRequest);

			if (!response.result)
			{
				MessagingCenter.Send(this, "alert", new MessagingCenterAlert
				{
					Title = "Load Images Failure",
					Message = $"Details: {response.message}",
					Cancel = "OK"
				});
			}
			else
			{
                response.images.ForEach(image => this.cameraImages.Add(image));
			}

			return response.result;
		}

	}
}
