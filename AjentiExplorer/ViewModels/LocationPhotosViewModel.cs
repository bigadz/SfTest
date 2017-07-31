using System;
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
        }

        public string Name
        {
            get { return this.locationViewModel.Name; }
        }


		ObservableRangeCollection<ImageSource> photos = new ObservableRangeCollection<ImageSource>();
		public ObservableRangeCollection<ImageSource> Photos
		{
			get { return photos; }
			set { SetProperty(ref photos, value); }
		}
    }
}
