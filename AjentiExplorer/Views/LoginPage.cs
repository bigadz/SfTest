using System;

using Xamarin.Forms;
using AjentiExplorer.ViewModels;

namespace AjentiExplorer.Views
{
    public class LoginPage : ContentPage
    {
        private LoginViewModel viewModel;

		public LoginPage(LoginViewModel viewModel)
        {
            BindingContext = this.viewModel = viewModel;
            this.viewModel.Navigation = this.Navigation;

            // Listen for messages from the modelview
            MessagingCenter.Subscribe<LoginViewModel, MessagingCenterAlert>(this, "alert", async (src, alert) =>
			{
				var _alert = alert as MessagingCenterAlert;
                await DisplayAlert(alert.Title, alert.Message, alert.Cancel);
			});

            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(300, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                }
            };
            Content = grid;

            grid.Children.Add(new Label { Text = "Hello ContentPage" }, 1, 0);
            grid.Children.Add(new Controls.UsernamePassword(viewModel), 1, 1);
            grid.Children.Add(new Controls.BusyIndicator(viewModel), 0, 3, 0, 3);

			this.Appearing += async (sender, e) => 
            {
                if (Settings.StayLoggedIn && Settings.IsLoggedIn)
                    await this.viewModel.ReauthenticateAsync();
            };   
		}
    }
}

