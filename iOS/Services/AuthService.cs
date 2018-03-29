using System;
using System.Threading.Tasks;
using TripLog.Services;
using UIKit;
using Xamarin.Auth;

namespace TripLog.iOS.Services
{
    public class AuthService : IAuthService
    {
        public async Task SignInAsync(Action<string> tokenCallback, Action<string> errorCallback)
        {
            var auth = new OAuth2Authenticator(
                clientId: "409009394217-p3cu602apd7vkaucmg4p1vijhqm4is6f.apps.googleusercontent.com",
                clientSecret: null,
                scope: "profile",
                authorizeUrl: new Uri("https://accounts.google.com/o/oauth2/auth"),
                accessTokenUrl: new Uri("https://www.googleapis.com/oauth2/v4/token"),
                redirectUrl: new Uri("com.googleusercontent.apps.409009394217-p3cu602apd7vkaucmg4p1vijhqm4is6f:/oauth2redirect"),
                getUsernameAsync: null,
                isUsingNativeUI: true
            );
            auth.AllowCancel = true;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(auth);

            auth.Completed += (s, e) =>
            {
                //controller.DismissViewController(true, null);

                if (e.Account != null && e.IsAuthenticated)
                {
                    if (tokenCallback != null)
                        tokenCallback(e.Account.Properties["id_token"]);
                }
                else
                {
                    if (errorCallback != null)
                        errorCallback("Not authenticated");
                }
            };

            auth.Error += (s, e) =>
            {
                //controller.DismissViewController(true, null);
                if (errorCallback != null)
                    errorCallback(e.Message);
            };

            AuthHolder.authenticator = auth;
        }
    }
}
