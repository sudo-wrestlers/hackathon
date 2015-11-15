using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Android.OS;
using Xamarin.Auth;

namespace JustRunWithIt
{
	[Activity (Label = "JustRunWithIt", MainLauncher = false, Icon = "@drawable/icon")]
	public class LoginActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var auth = new OAuth2Authenticator (
				clientId: "1647251678863645",
				scope: "",
				authorizeUrl: new Uri ("https://m.facebook.com/dialog/oauth/"),
				redirectUrl: new Uri ("http://www.facebook.com/connect/login_success.html"));

			auth.Completed += (sender, eventArgs) => {

				if (eventArgs.IsAuthenticated) {
					// Use eventArgs.Account to do wonderful things
					StartActivity (typeof(DashActivity));

				} else {
					// The user cancelled
					StartActivity (typeof(MainActivity));
				}
			};

			StartActivity (auth.GetUI (this));
		}


	}
}

