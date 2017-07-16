using System;

using Xamarin.Forms;

namespace AjentiExplorer.Views
{
    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
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
            grid.Children.Add(new Widgets.UsernamePassword(), 1, 1);
        }
    }
}

