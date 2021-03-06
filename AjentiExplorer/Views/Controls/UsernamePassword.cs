﻿using System;
using Xamarin.Forms;
using AjentiExplorer.ViewModels;

namespace AjentiExplorer.Views.Controls
{
    public class UsernamePassword: ContentView
    {
		private LoginViewModel viewModel;
		
        private Entry usernameEntry;
		private Entry passwordEntry;
		private Button loginButton;
        private Switch stayLoggedInSwitch;

		public UsernamePassword(LoginViewModel viewModel)
        {
            BindingContext = this.viewModel = viewModel;

            this.usernameEntry = new Entry
            {
                Placeholder = "Enter Username",
                FontSize = 16,
            };
            this.usernameEntry.SetBinding(Entry.TextProperty, new Binding("Username"));

			this.passwordEntry = new Entry
            {
                Placeholder = "Enter Password",
                FontSize = 16,
                IsPassword = true,
            };
			this.passwordEntry.SetBinding(Entry.TextProperty, new Binding("Password"));
            this.passwordEntry.Completed += async (sender, e) => await this.viewModel.SignInAsync();

			this.loginButton = new Button
            {
                Command = this.viewModel.SignInCommand,
                Text = "Login",
                HorizontalOptions = LayoutOptions.EndAndExpand,
			};

            this.stayLoggedInSwitch = new Switch
            {
                IsEnabled = Settings.StayLoggedIn,
                VerticalOptions = LayoutOptions.Center
            };
			this.stayLoggedInSwitch.SetBinding(Switch.IsToggledProperty, new Binding("StayLoggedIn"));

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
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
						Margin = new Thickness(0, 20, 0, 0),
						Children =
                        {
                            stayLoggedInSwitch,
                            new Label
                            {
                                Text = "Stay logged in",
                                FontAttributes = FontAttributes.None,
                                FontSize = 16,
                                VerticalOptions = LayoutOptions.Center,
                            },
							loginButton,
                        }
                    },

				}
            };

        }
    }
}
