using System;
namespace AjentiExplorer.ViewModels
{
	public class LocationDashboardsViewModel : BaseViewModel
	{
		private LocationViewModel locationViewModel;

		public LocationDashboardsViewModel(LocationViewModel locationViewModel)
		{
            this.locationViewModel = locationViewModel;
            this.Title = "Dashboards";
		}

		public string Name
		{
			get { return this.locationViewModel.Name; }
		}
    }
}
