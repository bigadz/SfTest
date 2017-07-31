using System;
using Xamarin.Forms;
using AjentiExplorer.ViewModels;


namespace AjentiExplorer.Views
{
    public class LocationFieldNotePage : ContentPage
    {
		private LocationFieldNoteViewModel viewModel;

		public LocationFieldNotePage(LocationFieldNoteViewModel viewModel)
        {
			BindingContext = this.viewModel = viewModel;

			SetBinding(TitleProperty, new Binding("Title"));
        }
    }
}

