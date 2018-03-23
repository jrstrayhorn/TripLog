using System;

using Xamarin.Forms;

namespace TripLog
{
    public class NewEntryPage : ContentPage
    {
        public NewEntryPage()
        {
            var save = new ToolbarItem
            {
                Text = "Save"
            };

            ToolbarItems.Add(save);

            Title = "New Entry";

            // form fields
            var title = new EntryCell
            {
                Label = "Title"
            };

            var latitude = new EntryCell
            {
                Label = "Latitude",
                Keyboard = Keyboard.Numeric
            };

            var longitude = new EntryCell
            {
                Label = "Longitude",
                Keyboard = Keyboard.Numeric
            };

            var date = new EntryCell
            {
                Label = "Date"
            };

            var rating = new EntryCell
            {
                Label = "Rating",
                Keyboard = Keyboard.Numeric
            };

            var notes = new EntryCell
            {
                Label = "Notes"
            };

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

