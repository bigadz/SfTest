using System;
using Xamarin.Forms;
using AjentiExplorer.ViewModels;


namespace AjentiExplorer.Views
{
    public class LoginPage : BaseContentPage
    {
        private LoginViewModel viewModel;

		public LoginPage(LoginViewModel viewModel)
        {
            BindingContext = this.viewModel = viewModel;
            this.viewModel.Navigation = this.Navigation;

            Title = "Login";

            // Listen for messages from the modelview
            MessagingCenter.Subscribe<LoginViewModel, MessagingCenterAlert>(this, "alert", HandleMessagingCenterAlert);

            var layoutGrid = new Grid
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
            Content = layoutGrid;

			layoutGrid.Children.Add(new Controls.UsernamePassword(viewModel), 1, 1);
            layoutGrid.Children.Add(LayoutFactories.BusyIndicator.Create(viewModel), 0, 3, 0, 3);

			this.Appearing += async (sender, e) => 
            {
                if (Settings.StayLoggedIn && Settings.IsLoggedIn)
                    await this.viewModel.ReauthenticateAsync();
            };   
		}
    }
}

