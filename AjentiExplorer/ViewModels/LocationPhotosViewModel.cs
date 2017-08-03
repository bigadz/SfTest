using System;
using AjentiExplorer.Models;
using Xamarin.Forms;

namespace AjentiExplorer.ViewModels
{
    public class LocationPhotosViewModel : BaseViewModel
    {
        private LocationViewModel locationViewModel;

        public LocationPhotosViewModel(LocationViewModel locationViewModel)
        {
            this.locationViewModel = locationViewModel;
            this.Title = "Photos";

			for (int i = 0; i < 10; i++)
			{
			    this.Photos.Add(new Photo
			    { 
                    Title = $"Image {i}",
                    Url = "https://s-media-cache-ak0.pinimg.com/originals/a8/7f/44/a87f44bd2acf3ecded2e45f6f5295735.jpg",
			    });
			}
		}

        public string Name
        {
            get { return this.locationViewModel.Name; }
        }


		ObservableRangeCollection<Photo> photos = new ObservableRangeCollection<Photo>();
		public ObservableRangeCollection<Photo> Photos
		{
			get { return photos; }
			set { SetProperty(ref photos, value); }
		}

    }
}
