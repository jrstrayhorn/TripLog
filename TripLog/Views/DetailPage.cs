using System;
using TripLog.Converters;
using TripLog.Models;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TripLog.Views
{
    public class DetailPage : ContentPage
    {
        private DetailViewModel _vm 
        {
            get 
            { 
                return BindingContext as DetailViewModel; 
            }
        }

        private readonly Map _map;

        public DetailPage()
        {
            BindingContextChanged += (sender, args) =>
            {
                if (_vm == null) return;

                _vm.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "Entry")
                        updateMap();
                };
            };

            //BindingContext = new DetailViewModel(DependencyService.Get<INavService>());

            Title = "Entry Details";

            //var map = new Map();
            _map = new Map();

            var title = new Label
            {
                HorizontalOptions = LayoutOptions.Center
            };
            title.SetBinding(Label.TextProperty, "Entry.Title");

            var date = new Label
            {
                HorizontalOptions = LayoutOptions.Center
            };
            date.SetBinding(Label.TextProperty, "Entry.Date", stringFormat: "{0:M}");

            var rating = new Image
            {
                HorizontalOptions = LayoutOptions.Center
            };
            rating.SetBinding(Image.SourceProperty, "Entry.Rating", converter: new RatingToStarImageNameConverter());

            var notes = new Label
            {
                HorizontalOptions = LayoutOptions.Center
            };
            notes.SetBinding(Label.TextProperty, "Entry.Notes");

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

            mainLayout.Children.Add(_map);
            mainLayout.Children.Add(detailsBg, 0, 1);
            mainLayout.Children.Add(details, 0, 1);

            Grid.SetRowSpan(_map, 3);

            Content = mainLayout;
        }

        private void updateMap() 
        {
            if (_vm.Entry == null)
                return;
            
            // place a pin on the map for the log entry's location
            var position = new Position(_vm.Entry.Latitude, _vm.Entry.Longitude);

            // center the map around the log entry's location
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.5)));

            _map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = _vm.Entry.Title,
                Position = position
            });

        }
    }
}

