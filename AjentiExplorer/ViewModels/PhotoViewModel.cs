using System;
using AjentiExplorer.Models;
using Xamarin.Forms;

namespace AjentiExplorer.ViewModels
{
    public class PhotoViewModel : BaseViewModel
    {
        private LocationPhotosViewModel locationPhotosViewModel;

        public PhotoViewModel(Photo photo, LocationPhotosViewModel locationPhotosViewModel)
        {
            this.Photo = photo;
            this.locationPhotosViewModel = locationPhotosViewModel;
            this.Title = photo.Title;
        }

		Photo photo;
		public Photo Photo
		{
			get { return photo; }
			set { SetProperty(ref photo, value); }
		}


        public ObservableRangeCollection<Photo> LocationPhotos
        {
            get { return this.locationPhotosViewModel.Photos; }
        }
    }
}
