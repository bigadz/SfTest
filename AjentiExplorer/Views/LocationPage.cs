using System;
using System.Threading.Tasks;
using Xamarin.Forms;


using AjentiExplorer.ViewModels;
using Plugin.ExternalMaps;

namespace AjentiExplorer.Views
{
    public class LocationPage : ContentPage
    {
		private LocationViewModel viewModel;

        private string externalMapsAppName;

		public LocationPage(LocationViewModel viewModel)
        {
            BindingContext = this.viewModel = viewModel;

            Title = "Location";

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    this.externalMapsAppName = "Google Maps";
                    break;

                default:
                    this.externalMapsAppName = "Maps";
                    break;
            }

			var table = new TableView() { Intent = TableIntent.Form };
			var root = new TableRoot();
			var section1 = new TableSection() { Title = "Details" };
			var section2 = new TableSection() { Title = "Actions" };

            var idCell = new TextCell { Detail = "Id", Text = viewModel.Id.ToString() };
			var nameCell = new TextCell { Detail = "Name", Text = viewModel.Name };
			var addressCell = new TextCell { Detail = "Address", Text = viewModel.Address };
            var latitudeCell = new TextCell { Detail = "Latitude", Text = viewModel.Latitude.ToString() };
			var longitudeCell = new TextCell { Detail = "Longitude", Text = viewModel.Longitude.ToString() };
            var siteTypesCell = new TextCell { Detail = "Site Types", Text = viewModel.SiteTypes };

            var directionsButton = new Button { Text = $"Open in {this.externalMapsAppName}..." };
            var directionsCell = new ViewCell { View = directionsButton };

            section1.Add(idCell);
			section1.Add(nameCell);
			section1.Add(addressCell);
			section1.Add(latitudeCell);
			section1.Add(longitudeCell);
			section1.Add(siteTypesCell);

			section2.Add(directionsCell);

			table.Root = root;
			root.Add(section1);
			root.Add(section2);

			Content = table;

            directionsButton.Clicked += DirectionsButton_Clicked;

            this.Appearing += async (sender, e) => 
            {
                await this.viewModel.LoadImagesAsync();
                Console.WriteLine($"image count={this.viewModel.CameraImages.Count}");
				foreach (var cameraImage in this.viewModel.CameraImages)
                {
                    Console.WriteLine($"name={cameraImage.name} urls={cameraImage.urls.Count}");
					foreach (var url in cameraImage.urls)
                    {
						Console.WriteLine($"     url={url}");
					}
                }
            };
        }

        async void DirectionsButton_Clicked(object sender, EventArgs e)
        {
            var success = await CrossExternalMaps.Current.NavigateTo(this.viewModel.Name, viewModel.Latitude, viewModel.Longitude);
            if (!success)
            {
                await DisplayAlert("Open Location Error", $"Unable to open location in {this.externalMapsAppName}", "Ok");
            }
        }   
    }
}

/*
 *      public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public double? budget { get; set; }
        public List<string> siteTypes { get; set; }
        public List<Meter> meters { get; set; }
        public List<string> features { get; set; }
        public List<Dashboard> dashboards { get; set; }
*/