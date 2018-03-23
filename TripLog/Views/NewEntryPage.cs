using System;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Views
{
    public class NewEntryPage : ContentPage
    {
        public NewEntryPage()
        {
            BindingContext = new NewEntryViewModel();

            var save = new ToolbarItem
            {
                Text = "Save"
            };
            save.SetBinding(ToolbarItem.CommandProperty, "SaveCommand");

            ToolbarItems.Add(save);

            Title = "New Entry";

            // form fields
            var title = new EntryCell
            {
                Label = "Title"
            };
            title.SetBinding(EntryCell.TextProperty, "Title", BindingMode.TwoWay);

            var latitude = new EntryCell
            {
                Label = "Latitude",
                Keyboard = Keyboard.Numeric
            };
            latitude.SetBinding(EntryCell.TextProperty, "Latitude", BindingMode.TwoWay);

            var longitude = new EntryCell
            {
                Label = "Longitude",
                Keyboard = Keyboard.Numeric
            };
            longitude.SetBinding(EntryCell.TextProperty, "Longitude", BindingMode.TwoWay);

            var date = new EntryCell
            {
                Label = "Date"
            };
            date.SetBinding(EntryCell.TextProperty, "Date", BindingMode.TwoWay, stringFormat:"{0:d}");

            var rating = new EntryCell
            {
                Label = "Rating",
                Keyboard = Keyboard.Numeric
            };
            rating.SetBinding(EntryCell.TextProperty, "Rating", BindingMode.TwoWay);

            var notes = new EntryCell
            {
                Label = "Notes"
            };
            notes.SetBinding(EntryCell.TextProperty, "Notes", BindingMode.TwoWay);

            // Form
            var entryForm = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot {
                    new TableSection() {
                        title,
                        latitude,
                        longitude,
                        date,
                        rating,
                        notes
                    }
                }
            };

            Content = entryForm;
        }
    }
}

