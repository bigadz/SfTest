using System;

using Xamarin.Forms;
using Syncfusion.SfBusyIndicator.XForms;
using AjentiExplorer.ViewModels;

namespace AjentiExplorer.Views.Controls
{
    public class BusyIndicator : ContentView
    {
		private BaseViewModel viewModel;

        private SfBusyIndicator sfBusyIndicator;

		public BusyIndicator(BaseViewModel viewModel)
		{
			BindingContext = this.viewModel = viewModel;

			this.sfBusyIndicator = new SfBusyIndicator()
			{
				BindingContext = viewModel,
				AnimationType = AnimationTypes.Ball,
				IsEnabled = true,
				IsBusy = true,
				IsVisible = true,
				TextColor = Color.White,
				BackgroundColor = Color.Black.MultiplyAlpha(0.7),
				ViewBoxWidth = 200,
				ViewBoxHeight = 200,
			};
            this.sfBusyIndicator.SetBinding(SfBusyIndicator.TitleProperty, new Binding("BusyMessage"));

            var grid = new Grid();
			grid.Children.Add(this.sfBusyIndicator);
			Content = grid;



		}
    }
}

