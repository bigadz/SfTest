using System;
using AjentiExplorer.Models;
using AjentiExplorer.ViewModels;
using Xamarin.Forms;
using Syncfusion.ListView.XForms;
using Syncfusion.DataSource;

namespace AjentiExplorer.Views
{
    public class LocationPhotosPage : ContentPage
    {
		private LocationPhotosViewModel viewModel;

		public LocationPhotosPage(LocationPhotosViewModel viewModel)
        {
			BindingContext = this.viewModel = viewModel;

			SetBinding(TitleProperty, new Binding("Title"));

            var dataSource = new DataSource
            {
                Source = this.viewModel.Photos,
            };
            dataSource.SortDescriptors.Add(new SortDescriptor("Title"));

            var gridLayout = new GridLayout
            {
                SpanCount = Device.Idiom == TargetIdiom.Phone ? 2 : 4, 
            };

            var listView = new SfListView
            {
                LayoutManager = gridLayout,
                ItemsSource = dataSource.DisplayItems,
				ItemTemplate = new DataTemplate(typeof(Cells.PhotoThumbCell)),
                AutoFitMode = AutoFitMode.Height,
			};
            listView.ItemTapped += ListView_ItemTapped;

            Grid grid = new Grid();
            grid.Children.Add(listView);
            Content = grid;
        }

        async void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var photo = e.ItemData as Photo;
            await this.Navigation.PushAsync(new LocationPhotoCarouselPage(new PhotoViewModel(photo, this.viewModel)));
			e.Handled = true;
		}
    }
}

