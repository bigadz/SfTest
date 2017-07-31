using System;
namespace AjentiExplorer.ViewModels
{
	public class LocationFieldNoteViewModel : BaseViewModel
	{
		private LocationViewModel locationViewModel;

		public LocationFieldNoteViewModel(LocationViewModel locationViewModel)
		{
			this.locationViewModel = locationViewModel;
			this.Title = "Field Note";
		}

		public string Name
		{
			get { return this.locationViewModel.Name; }
		}
    }
}
