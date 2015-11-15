using System;
using System.Collections;
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
	public class DashActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Dash);
			Button submit = FindViewById<Button> (Resource.Id.button2);
			EditText eventBox = FindViewById<EditText> (Resource.Id.editText1);
			ListView events = FindViewById<ListView> (Resource.Id.listView1);

			ArrayList eventHolder = new ArrayList();
			submit.Click += (sender, e) =>
			{
				eventHolder.Add(eventBox.Text);
				var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1,eventHolder);
				events.Adapter = adapter;

			};


		}
	}
}

