using System;

using Xamarin.Forms;

namespace AjentiExplorer.Views
{
    public class SearchListPage : ContentPage
    {
        public SearchListPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

