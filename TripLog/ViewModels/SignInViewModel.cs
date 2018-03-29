using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TripLog.Services;
using Xamarin.Auth;
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
            // NEW Client ID: 409009394217-p3cu602apd7vkaucmg4p1vijhqm4is6f.apps.googleusercontent.com
            // OLD Client ID: 409009394217-11dcm7db57c38qp5q47jj4p8gfmi88p1.apps.googleusercontent.com
            await _authService.SignInAsync(
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
