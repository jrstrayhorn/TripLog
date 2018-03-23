using System;
using TripLog.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TripLog
{
    public class DetailPage : ContentPage
    {
        public DetailPage(TripLogEntry entry)
        {
            Title = "Entry Details";

            var mainLayout = new Grid
            {
                RowDefinitions = {
                    new RowDefinition {
                        Height = new GridLength(4, GridUnitType.Star)
                    },
                    new RowDefinition {
                        Height = GridLength.Auto
                    },
                    new RowDefinition {
                        Height = new GridLength(1, GridUnitType.Star)
                    }
                }
            };

            var map = new Map();

            // center the map around the log entry's location
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(entry.Latitude, entry.Longitude), Distance.FromMiles(.5)));

            // place a pin on the map for the log entry's location
            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = entry.Title,
                Position = new Position(entry.Latitude, entry.Longitude)
            });

            var title = new Label
            {
                HorizontalOptions = LayoutOptions.Center
            };
            title.Text = entry.Title;

            var date = new Label
            {
                HorizontalOptions = LayoutOptions.Center
            };
            date.Text = entry.Date.ToString("M");

            var rating = new Label
            {
                HorizontalOptions = LayoutOptions.Center
            };
            rating.Text = $"{entry.Rating} star rating";

            var notes = new Label
            {
                HorizontalOptions = LayoutOptions.Center
            };
            notes.Text = entry.Notes;

            var details = new StackLayout
            {
                Padding = 10,
                Children = {
                    title, date, rating, notes
                }
            };

            var detailsBg = new BoxView
            {
                BackgroundColor = Color.White,
                Opacity = .8
            };

            mainLayout.Children.Add(map);
            mainLayout.Children.Add(detailsBg, 0, 1);
            mainLayout.Children.Add(details, 0, 1);

            Grid.SetRowSpan(map, 3);

            Content = mainLayout;
        }
    }
}

