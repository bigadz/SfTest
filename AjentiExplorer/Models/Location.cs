using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace AjentiExplorer.Models
{
    public class Location: ObservableObject
    {
        public Location(JsonMsgs.Location location)
        {
			this.Id = location.id;
			this.Name = location.name;
			this.Address = location.address;
            this.Latitude = location.latitude;
            this.Longitude = location.longitude;
            this.SiteTypes = location.siteTypes;

			this.Title = string.IsNullOrWhiteSpace(location.name) ? (string.IsNullOrWhiteSpace(location.address) ? "<unnamed location>" : location.address) : location.name;
		}

		string title = string.Empty;
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		int id = 0;
		public int Id
		{
			get { return id; }
			set { SetProperty(ref id, value); }
		}

		string name = string.Empty;
		public string Name
		{
			get { return name; }
			set { SetProperty(ref name, value); }
		}


		string address = string.Empty;
		public string Address
		{
			get { return address; }
			set { SetProperty(ref address, value); }
		}


		double? latitude = null;
		public double? Latitude
		{
			get { return latitude; }
			set { SetProperty(ref latitude, value); }
		}

		double? longitude = null;
		public double? Longitude
		{
			get { return longitude; }
			set { SetProperty(ref longitude, value); }
		}

        public bool HasCoordinates
        {
            get
            {
                return this.Longitude.HasValue && this.Latitude.HasValue;
            }
        }

		public Position Position
		{
            get { return new Position(this.Latitude.Value, this.Longitude.Value); }
		}

        List<string> siteTypes = new List<string>();
		public List<string> SiteTypes
		{
			get { return siteTypes; }
			set { SetProperty(ref siteTypes, value); }
		}

	}
}
