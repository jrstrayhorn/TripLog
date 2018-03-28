﻿using System;
using System.Threading.Tasks;
using TripLog.Services;
using UIKit;
using Xamarin.Auth;

namespace TripLog.iOS.Services
{
    public class AuthService : IAuthService
    {
        public async Task SignInAsync(string clientId, Uri authUrl, Uri callbackUrl, Action<string> tokenCallback, Action<string> errorCallback)
        {
            var auth = new OAuth2Authenticator(clientId, string.Empty, authUrl, callbackUrl);


            auth.AllowCancel = true;

            var controller = auth.GetUI();
            await UIApplication.SharedApplication
                               .KeyWindow
                               .RootViewController
                               .PresentViewControllerAsync(controller, true);

            auth.Completed += (s, e) =>
            {
                controller.DismissViewController(true, null);
                if (e.Account != null && e.IsAuthenticated)
                {
                    if (tokenCallback != null)
                        tokenCallback(e.Account.Properties["access_token"]);
                }
                else
                {
                    if (errorCallback != null)
                        errorCallback("Not authenticated");
                }
            };

            auth.Error += (s, e) =>
            {
                controller.DismissViewController(true, null);
                if (errorCallback != null)
                    errorCallback(e.Message);
            };
        }
    }
}
