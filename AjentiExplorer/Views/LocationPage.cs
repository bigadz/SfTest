using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using AjentiExplorer.ViewModels;

namespace AjentiExplorer.Views
{
    public class LocationPage : ContentPage
    {
		private LocationViewModel viewModel;

		public LocationPage(LocationViewModel viewModel)
        {
            BindingContext = this.viewModel = viewModel;

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello LocationPage" }
                }
            };
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