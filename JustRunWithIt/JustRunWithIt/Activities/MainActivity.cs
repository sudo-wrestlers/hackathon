using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Android.OS;

namespace JustRunWithIt
{
	[Activity (Label = "JustRunWithIt", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			Button login = FindViewById<Button> (Resource.Id.button1);

			login.Click += (sender, e) =>
			{
				StartActivity(typeof(LoginActivity));
			};

		}

	}
}

