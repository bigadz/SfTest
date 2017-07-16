using System;
using Xamarin.Forms;

namespace AjentiExplorer.Views.Widgets
{
    public class UsernamePassword: ContentView
    {
        private Entry usernameEntry;
        private Entry passwordEntry;

        public UsernamePassword()
        {
            this.usernameEntry = new Entry
            {
                Placeholder = "Enter Username",
                FontSize = 16,
            };

            this.passwordEntry = new Entry
            {
                Placeholder = "Enter Password",
                FontSize = 16,
                IsPassword = true,
            };

            this.Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    new Label
                    {
                        Text = "Username",
                        FontAttributes = FontAttributes.None,
                        FontSize = 16,
                    },
                    usernameEntry,
                    new Label
                    {
                        Text = "Password",
                        FontAttributes = FontAttributes.None,
                        FontSize = 16,
                        Margin = new Thickness(0, 20, 0, 0),
					},
                    passwordEntry,

				}
            };

        }
    }
}
