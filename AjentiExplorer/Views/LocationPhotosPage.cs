using System;
using Xamarin.Forms;
using AjentiExplorer.ViewModels;
using Syncfusion.ListView.XForms;

namespace AjentiExplorer.Views
{
    public class LocationPhotosPage : ContentPage
    {
		private LocationPhotosViewModel viewModel;

		public LocationPhotosPage(LocationPhotosViewModel viewModel)
        {
			BindingContext = this.viewModel = viewModel;

			SetBinding(TitleProperty, new Binding("Title"));


            var gridLayout = new GridLayout
            {
                SpanCount = Device.Idiom == TargetIdiom.Phone ? 2 : 4,
            };

            var listView = new SfListView
            {
                LayoutManager = gridLayout,
            };

        }
    }
}

