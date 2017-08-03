using System;
using Xamarin.Forms;

namespace AjentiExplorer.Models
{
    public class Photo: ObservableObject
    {

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string url = string.Empty;
        public string Url
        {
            get { return url; }
            set 
            {
                this.ImageSource = new UriImageSource { Uri = new Uri(value) };
                SetProperty(ref url, value);
            }
        }

		ImageSource imageSource = null;
		public ImageSource ImageSource
		{
			get { return imageSource; }
            set { SetProperty(ref imageSource, value); }
		}   
    }
}
