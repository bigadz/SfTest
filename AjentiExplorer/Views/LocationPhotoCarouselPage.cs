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

            var itemWidth = (int)(App.DisplayScreenWidth * 0.95);
            var itemHeight = (int)(App.DisplayScreenHeight * 0.95);

            var imageCollection = new ObservableRangeCollection<SfCarouselItem>();

			//var dataSource = new DataSource
			//{
			//	Source = this.viewModel.LocationPhotos,
			//};
			//dataSource.SortDescriptors.Add(new SortDescriptor("Title"));

			foreach (var photo in this.viewModel.LocationPhotos)
			{
				var grid = new Grid
				{
					BackgroundColor = Settings.Dark6,
					Margin = new Thickness(5),
                    ColumnDefinitions = { new ColumnDefinition { Width = new GridLength(itemWidth, GridUnitType.Absolute) } },
					RowDefinitions = { new RowDefinition { Height = new GridLength(itemHeight, GridUnitType.Absolute)} },
				};
				var photoTitle = new Label
				{
                    Text = photo.Title,
					FontAttributes = FontAttributes.None,
					TextColor = Color.White,
					BackgroundColor = Settings.Dark1.MultiplyAlpha(0.5),
					FontSize = 14,
					VerticalOptions = LayoutOptions.End,
					Margin = new Thickness(10),
				};
				var photoImage = new Image
                {
                    Source = photo.ImageSource,
                    Aspect = Aspect.AspectFit,
				};

				grid.Children.Add(photoImage, 0, 1, 0, 1);
				grid.Children.Add(photoTitle, 0, 1, 0, 1);


				imageCollection.Add(new SfCarouselItem
				{
                    ItemContent = grid,//new Image() { Source = photo.ImageSource, Aspect = Aspect.AspectFit }
				});
			}


			var carousel = new SfCarousel
            {
                BackgroundColor = Settings.Dark6,
                ItemSpacing = 10,
                ViewMode = ViewMode.Linear,
                Offset = 0,
                RotationAngle = 0,
                ItemWidth = itemWidth,
                ItemHeight = itemHeight,
                DataSource = imageCollection,
			};

            Content = carousel;
        }
    }
}

