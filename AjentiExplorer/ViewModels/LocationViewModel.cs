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
		public ICommand LoadImagesCommand { get; }

		public LocationViewModel(JsonMsgs.Location location)
        {
            this.Location = new Models.Location(location);
            this.Title = this.Location.Title;
			LoadImagesCommand = new Command(async () => await LoadImagesAsync());
		}

		Models.Location location = null;
		public Models.Location Location
		{
			get { return location; }
			set { SetProperty(ref location, value); }
		}


		public int Id
		{
            get { return this.Location.Id; }
		}

		public string Name
		{
            get { return this.Location.Name; }
		}

		public string Address
		{
            get { return this.Location.Address; }
		}

        private string siteTypes;
		public string SiteTypes
		{
            get 
            {
                if (this.siteTypes == null)
                {
                    var sb = new StringBuilder();
                    foreach (var siteType in this.Location.SiteTypes)
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
            get { return this.Location.HasCoordinates; }
		}

		public double Latitude
		{
            get { return this.Location.Latitude.Value; }
		}

		public double Longitude
		{
            get { return this.location.Longitude.Value; }
		}

        public Position Position
        {
            get { return this.Location.Position;  }
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
                id = this.Location.Id,
                imagesOnDisplay = 1,
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
