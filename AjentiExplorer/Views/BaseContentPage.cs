using System;
using Xamarin.Forms;
using AjentiExplorer.ViewModels;

namespace AjentiExplorer.Views
{
    public class BaseContentPage: ContentPage
    {
        protected async void HandleMessagingCenterAlert (BaseViewModel viewModel, MessagingCenterAlert alert)
        {
            await DisplayAlert(alert.Title, alert.Message, alert.Cancel);
        }

        public BaseContentPage()
        {
        }
    }
}
