using System;
namespace AjentiExplorer.Models
{
    public class LocationCollection: ObservableObject
    {
        public LocationCollection()
        {
        }


		string title = string.Empty;
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}
    }
}
