using System;
using AjentiExplorer.Models;
using AjentiExplorer.ViewModels;
using Xamarin.Forms;
using Syncfusion.SfCarousel.XForms;
using Syncfusion.DataSource;

namespace AjentiExplorer.Views
{
    public class LocationPhotoCarouselPage : ContentPage
    {
        private PhotoViewModel viewModel;

        public LocationPhotoCarouselPage(PhotoViewModel viewModel)
        {
			BindingContext = this.viewModel = viewModel;

			SetBinding(TitleProperty, new Binding("Title"));

            var imageCollection = new ObservableRangeCollection<SfCarouselItem>();

			//var dataSource = new DataSource
			//{
			//	Source = this.viewModel.LocationPhotos,
			//};
			//dataSource.SortDescriptors.Add(new SortDescriptor("Title"));

			foreach (var photo in this.viewModel.LocationPhotos)
			{
				imageCollection.Add(new SfCarouselItem
				{
                    ItemContent = new Image() { Source = photo.ImageSource, Aspect = Aspect.AspectFit }
				});
			}


			var carousel = new SfCarousel
            {
                BackgroundColor = Settings.Dark6,
                ItemSpacing = 10,
                ViewMode = ViewMode.Linear,
                Offset = 0,
                RotationAngle = 0,
                ItemWidth = (int)(App.DisplayScreenWidth * 0.95),
                ItemHeight = (int)(App.DisplayScreenHeight * 0.95),
                DataSource = imageCollection,
			};

            Content = carousel;
        }
    }
}

