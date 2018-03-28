using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        readonly IAuthService _authService;
        readonly ITripLogDataService _tripLogService;

        public SignInViewModel(INavService navService,
                              IAuthService authService,
                              ITripLogDataService tripLogService)
            : base(navService)
        {
            _authService = authService;
            _tripLogService = tripLogService;
        }

        ICommand _signInCommand;
        public ICommand SignInCommand {
            get {
                return _signInCommand
                    ?? (_signInCommand = new Command(async () => await ExecuteSignInCommand()));
            }
        }

        async Task ExecuteSignInCommand()
        {
            await _authService.SignInAsync(
                "409009394217-11dcm7db57c38qp5q47jj4p8gfmi88p1.apps.googleusercontent.com",
                new Uri("https://accounts.google.com/o/oauth2/auth"),
                new Uri("https://triplogjr.azurewebsites.net/.auth/login/google/callback"),
                tokenCallback: async t =>
                {

                    // use google token to get Azure auth token
                    var response = await _tripLogService.GetAuthTokenAsync("google", t);

                    // save auth token in local settings
                    Helpers.Settings.TripLogApiAuthToken = response.AuthenticationToken;

                    // Navigate to Main
                    await NavService.NavigateTo<MainViewModel>();
                    await NavService.RemoveLastView();
                },
                errorCallback: e =>
                {
                    // todo: handle invalid auth here
                });
        }

        public async override Task Init()
        {
            await NavService.ClearBackStack();
        }
    }
}
