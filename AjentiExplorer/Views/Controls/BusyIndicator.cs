using System;

using Xamarin.Forms;
using AjentiExplorer.ViewModels;

namespace AjentiExplorer.Views.Controls
{
    public class BusyIndicator : ContentView
    {
		private BaseViewModel viewModel;

        private ActivityIndicator activityIndicator;
		private Label messageLabel;

		public BusyIndicator(BaseViewModel viewModel)
		{
			BindingContext = this.viewModel = viewModel;

			this.activityIndicator = new ActivityIndicator()
            {
				IsRunning = true,
                Color = Color.White,
            	BackgroundColor = Color.Black.MultiplyAlpha(0.7),
            };
			this.activityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, new Binding("IsBusy"));

            this.messageLabel = new Label
            {
				FontAttributes = FontAttributes.None,
				FontSize = 24,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
			};
			this.messageLabel.SetBinding(Label.TextProperty, new Binding("BusyMessage"));

			var grid = new Grid
			{
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
				},
				RowDefinitions =
				{
					new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
				}
			};
			Content = grid;

			grid.Children.Add(this.activityIndicator, 0, 1, 0, 2);
			grid.Children.Add(this.messageLabel, 0, 1, 1, 2);

		}
    }
}

