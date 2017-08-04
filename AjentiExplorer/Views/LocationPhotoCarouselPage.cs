using System;
using AjentiExplorer.Models;
using AjentiExplorer.ViewModels;
using Xamarin.Forms;

namespace AjentiExplorer.Views
{
    public class LocationPhotoCarouselPage : ContentPage
    {
        private PhotoViewModel viewModel;

        public LocationPhotoCarouselPage(PhotoViewModel viewModel)
        {
			BindingContext = this.viewModel = viewModel;

			SetBinding(TitleProperty, new Binding("Title"));
			
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "LocationPhotoCarouselPage" }
                }
            };
        }
    }
}

