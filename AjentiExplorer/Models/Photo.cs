﻿using System;
using Xamarin.Forms;

namespace AjentiExplorer.Models
{
    public class Photo: ObservableObject
    {

        DateTime dateCreated = DateTime.MinValue;
		public DateTime DateCreated
		{
			get { return dateCreated; }
			set 
            {
                this.Title = $"{value:ddd, MMM d, yyyy h:m:s tt}";
                SetProperty(ref dateCreated, value);
            }
		}

		string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

		string dateString = string.Empty;
		public string DateString
		{
			get { return dateString; }
			set { SetProperty(ref dateString, value); }
		}


		string timeString = string.Empty;
		public string TimeString
		{
			get { return timeString; }
			set { SetProperty(ref timeString, value); }
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
