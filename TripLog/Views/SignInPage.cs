using System;

using Xamarin.Forms;

namespace TripLog.Views
{
    public class SignInPage : ContentPage
    {
        public SignInPage()
        {
            Padding = 20;

            var googleButton = new Button
            {
                BackgroundColor = Color.DarkRed,
                TextColor = Color.White,
                Text = "Sign in with Google"
            };
            googleButton.SetBinding(Button.CommandProperty, "SignInCommand");

            var mainLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = { googleButton }
            };

            Content = mainLayout;
        }
    }
}

