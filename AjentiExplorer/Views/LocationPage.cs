using System;
using System.Threading.Tasks;
using Xamarin.Forms;


using AjentiExplorer.ViewModels;
using Plugin.ExternalMaps;

namespace AjentiExplorer.Views
{
    public class LocationPage : BaseContentPage
    {
		private LocationViewModel viewModel;

        private string externalMapsAppName;

        private bool cameraImagesLoaded = false;

		public LocationPage(LocationViewModel viewModel)
        {
            BindingContext = this.viewModel = viewModel;

			SetBinding(TitleProperty, new Binding("Title"));

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
			var section1 = new TableSection() { Title = "Site" };
			var section2 = new TableSection() { Title = "Actions" };

			var nameCell = new TextCell { Detail = "Name", Text = viewModel.Name };
			var addressCell = new TextCell { Detail = "Address", Text = viewModel.Address };
            var siteTypesCell = new TextCell { Detail = "Site Types", Text = viewModel.SiteTypes };
			var idCell = new TextCell { Detail = "ADMS Id", Text = viewModel.Id.ToString() };

			section1.Add(nameCell);
			section1.Add(addressCell);
			section1.Add(siteTypesCell);
			section1.Add(idCell);

			var photosButton = new Button { Text = "Photos" };
			photosButton.Clicked += PhotosButton_Clicked;
			var dashboardsButton = new Button { Text = "Dashboards" };
			dashboardsButton.Clicked += DashboardsButton_Clicked;
			var fieldnoteButton = new Button { Text = $"Field Note" };
			fieldnoteButton.Clicked += FieldNoteButton_Clicked;
			var directionsButton = new Button { Text = $"Show in {this.externalMapsAppName} App..." };
			directionsButton.Clicked += DirectionsButton_Clicked;


			section2.Add(new ViewCell { View = photosButton });
			section2.Add(new ViewCell { View = dashboardsButton });
			section2.Add(new ViewCell { View = fieldnoteButton });
			section2.Add(new ViewCell { View = directionsButton });

			table.Root = root;
			root.Add(section1);
			root.Add(section2);

			Content = table;


            this.Appearing += async (sender, e) => 
            {
                if (!this.cameraImagesLoaded)
                {
                    this.cameraImagesLoaded = true;
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

		async void FieldNoteButton_Clicked(object sender, EventArgs e)
		{
            await this.Navigation.PushAsync(new LocationFieldNotePage(new LocationFieldNoteViewModel(this.viewModel)));
		}

		async void PhotosButton_Clicked(object sender, EventArgs e)
		{
            await this.Navigation.PushAsync(new LocationPhotosPage(new LocationPhotosViewModel(this.viewModel)));
            //await this.Navigation.PushAsync(new TimelapsePlayerPage(new TimelapsePlayerViewModel()));
		}

		async void DashboardsButton_Clicked(object sender, EventArgs e)
		{
            await this.Navigation.PushAsync(new LocationDashboardsPage(new LocationDashboardsViewModel(this.viewModel)));
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