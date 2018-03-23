using System;
using TripLog.Models;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class NewEntryViewModel : BaseViewModel
    {
        string _title; public string Title { get { return _title; } set { _title = value; OnPropertyChanged(); SaveCommand.ChangeCanExecute(); } }
        double _latitude; public double Latitude { get { return _latitude; } set { _latitude = value; OnPropertyChanged(); } }
        double _longitude; public double Longitude { get { return _longitude; } set { _longitude = value; OnPropertyChanged(); } }
        DateTime _date; public DateTime Date { get { return _date; } set { _date = value; OnPropertyChanged(); } }
        int _rating; public int Rating { get { return _rating; } set { _rating = value; OnPropertyChanged(); } }
        string _notes; public string Notes { get { return _notes; } set { _notes = value; OnPropertyChanged(); } }

        public NewEntryViewModel()
        {
            Date = DateTime.Today;
            Rating = 1;
        }

        Command _saveCommand;
        public Command SaveCommand
        {
            get {
                return _saveCommand ?? (_saveCommand = new Command(ExecuteSaveCommand, CanSave));
            }
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }

        private void ExecuteSaveCommand()
        {
            var newItem = new TripLogEntry
            {
                Title = this.Title,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                Date = this.Date,
                Rating = this.Rating,
                Notes = this.Notes
            };
            // TODO: Implement logic to persist Entry in a later chapter.
        }
    }
}
