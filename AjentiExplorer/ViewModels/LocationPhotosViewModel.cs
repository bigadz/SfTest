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

            var rand = new Random((int)DateTime.Now.Ticks);
			for (int i = 0; i < 50; i++)
			{
                var photoIx = rand.Next(0, 5);

			    this.Photos.Add(new Photo
			    { 
                    DateCreated = DateTime.Now.AddHours(rand.Next(0, 24)),
                    Url = photoIx == 0 ? "https://s-media-cache-ak0.pinimg.com/originals/a8/7f/44/a87f44bd2acf3ecded2e45f6f5295735.jpg"
						: photoIx == 1 ? "https://www.watercorporation.com.au/-/media/images/page-images/water-supply-and-services/visiting-our-dams/wungong-dam.jpg"
                        : photoIx == 2 ? "http://www.waternsw.com.au/__data/assets/image/0018/120735/Glenbawn8.jpg"
						: photoIx == 3 ? "http://www.waternsw.com.au/__data/assets/image/0016/120742/Chaffey-Stage-1-completion-ceremony-March-2011-014.jpg"
						: "http://www.waternsw.com.au/__data/assets/image/0012/111153/Cordeaux.jpg",
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
