using System;
using System.Collections.Generic;
using TripLog.Models;
using Xamarin.Forms;

namespace TripLog
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
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

            var items = new List<TripLogEntry>
            {
                new TripLogEntry
                {
                    Title = "Washington Monument",
                    Notes = "Amazing!",
                    Rating = 3,
                    Date = new DateTime(2015, 2, 5),
                    Latitude = 38.8895,
                    Longitude = -77.0352
                },
                new TripLogEntry
                {
                    Title = "Statue of Liberty",
                    Notes = "Inspiring!",
                    Rating = 4,
                    Date = new DateTime(2015, 4, 13),
                    Latitude = 40.6892,
                    Longitude = -74.0444
                },
                new TripLogEntry
                {
                    Title = "Golden Gate Bridge",
                    Notes = "Foggy, but beautiful",
                    Rating = 5,
                    Date = new DateTime(2015, 4, 26),
                    Latitude = 37.8268,
                    Longitude = -122.4798
                }
            };

            var itemTemplate = new DataTemplate(typeof(TextCell));
            itemTemplate.SetBinding(TextCell.TextProperty, "Title");
            itemTemplate.SetBinding(TextCell.DetailProperty, "Notes");

            var entries = new ListView
            {
                ItemsSource = items,
                ItemTemplate = itemTemplate
            };

            entries.ItemTapped += async (sender, e) =>
            {
                var item = (TripLogEntry)e.Item;
                await Navigation.PushAsync(new DetailPage(item));
            };

            Content = entries;
        }
    }
}

