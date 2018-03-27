using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Akavache;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        ObservableCollection<TripLogEntry> _logEntries;
        public ObservableCollection<TripLogEntry> LogEntries
        {
            get { return _logEntries;  }
            set {
                _logEntries = value;
                OnPropertyChanged();
            }
        }

        readonly ITripLogDataService _tripLogService;
        readonly IBlobCache _cache;

        public MainViewModel(INavService navService, 
                             ITripLogDataService tripLogService) : base(navService)
        {
            _tripLogService = tripLogService;

            LogEntries = new ObservableCollection<TripLogEntry>();
        }

		public override async Task Init()
		{
            await LoadEntries();
		}

        async Task LoadEntries()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var entries = await _tripLogService.GetEntriesAsync();

                LogEntries = new ObservableCollection<TripLogEntry>(entries);
            }
            finally {
                IsBusy = false;
            }
        }

        Command<TripLogEntry> _viewCommand;
        public Command<TripLogEntry> ViewCommand {
            get { return _viewCommand ?? (_viewCommand = new Command<TripLogEntry>(async (entry) => await ExecuteViewCommand(entry))); }
        }

        Command _newCommand;
        public Command NewCommand {
            get { return _newCommand ?? (_newCommand = new Command(async () => await ExecuteNewCommand())); }
        }

        async Task ExecuteViewCommand(TripLogEntry entry)
        {
            await NavService.NavigateTo<DetailViewModel, TripLogEntry>(entry);
        }

        async Task ExecuteNewCommand()
        {
            await NavService.NavigateTo<NewEntryViewModel>();
        }

	}
}
