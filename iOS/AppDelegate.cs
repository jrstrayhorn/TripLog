using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using TripLog.iOS.Modules;
using TripLog.iOS.Services;
using UIKit;

namespace TripLog.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();
            global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();
            LoadApplication(new App(new TripLogPlatformModule()));

            return base.FinishedLaunching(app, options);
        }

		public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
		{
            // Convert NSUrl to Uri
            var uri = new Uri(url.AbsoluteString);

            // Load redirectUrl page
            AuthHolder.authenticator.OnPageLoading(uri);

            return true;
		}
	}
}
