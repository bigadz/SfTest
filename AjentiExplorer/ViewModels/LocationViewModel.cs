using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
             
namespace AjentiExplorer.ViewModels
{
    public class LocationViewModel : BaseViewModel
    {
        private JsonMsgs.Location location;

        public LocationViewModel(JsonMsgs.Location location)
        {
            this.location = location;
        }

		public string Name
		{
            get { return this.location.name; }
		}

		public string Address
		{
            get { return this.location.address; }
		}

		public bool HasCoordinates
		{
            get { return this.location.latitude.HasValue && this.location.longitude.HasValue; }
		}

		public double Latitude
		{
            get { return this.location.latitude.Value; }
		}

		public double Longitude
		{
            get { return this.location.longitude.Value; }
		}

        public Position Position
        {
            get { return new Position(Latitude, Longitude);  }
        }
	}
}
