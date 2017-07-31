using System;
using Xamarin.Forms;
using Syncfusion.SfBusyIndicator.XForms;
using AjentiExplorer.ViewModels;

namespace AjentiExplorer.Views.LayoutFactories
{
    public static class BusyIndicator
    {
        public static SfBusyIndicator Create(ViewModels.BaseViewModel viewModel)
        {
			var sfBusyIndicator = new SfBusyIndicator
            {
                BindingContext = viewModel,
                AnimationType = AnimationTypes.Ball,
                IsEnabled = true,
                IsBusy = true,
                IsVisible = false, // Bind this to IsBusy
				TextColor = Color.White,
				BackgroundColor = Color.Black.MultiplyAlpha(0.7),
				ViewBoxWidth = 200,
				ViewBoxHeight = 200,
			};
			sfBusyIndicator.SetBinding(SfBusyIndicator.TitleProperty, new Binding("BusyMessage"));
            sfBusyIndicator.SetBinding(VisualElement.IsVisibleProperty, new Binding("IsBusy"));

			return sfBusyIndicator;
        }
    }
		
}
