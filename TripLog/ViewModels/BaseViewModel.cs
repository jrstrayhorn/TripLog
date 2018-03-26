using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TripLog.Services;

namespace TripLog.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected INavService NavService { get; private set; }

        protected BaseViewModel(INavService navService)
        {
            NavService = navService;
        }

        public abstract Task Init();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class BaseViewModel<TParameter> : BaseViewModel
    {
        protected BaseViewModel (INavService navService) : base (navService) { }

        public override async Task Init()
        {
            await Init(default(TParameter));
        }

        public abstract Task Init(TParameter parameter);
    }
}
