using System;
using AjentiExplorer.Models;
using Xamarin.Forms;

namespace AjentiExplorer.ViewModels
{
    public class PhotoViewModel : BaseViewModel
    {
        public PhotoViewModel(Photo photo)
        {
            this.Photo = photo;
            this.Title = photo.Title;
        }

		Photo photo;
		public Photo Photo
		{
			get { return photo; }
			set { SetProperty(ref photo, value); }
		}
    }
}
