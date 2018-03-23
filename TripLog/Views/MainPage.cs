using System;
using System.Collections.Generic;
using TripLog.Models;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Views
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainViewModel();

            var newButton = new ToolbarItem
            {
                Text = "New"
            };

            newButton.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new NewEntryPage());
            };

            ToolbarItems.Add(newButton);

            Title = "TripLog";

            var itemTemplate = new DataTemplate(typeof(TextCell));
            itemTemplate.SetBinding(TextCell.TextProperty, "Title");
            itemTemplate.SetBinding(TextCell.DetailProperty, "Notes");

            var entries = new ListView
            {
                ItemTemplate = itemTemplate
            };

            entries.SetBinding(ListView.ItemsSourceProperty, "LogEntries");

            entries.ItemTapped += async (sender, e) =>
            {
                var item = (TripLogEntry)e.Item;
                await Navigation.PushAsync(new DetailPage(item));
            };

            Content = entries;
        }
    }
}

