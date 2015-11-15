using System;
using System.Collections;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Xamarin.Android;
using Xamarin.Forms;

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
			SearchView search = FindViewById<SearchView> (Resource.Id.searchView1);
			Android.Widget.ListView events = FindViewById<Android.Widget.ListView> (Resource.Id.listView1);
			ArrayList eventHolder = new ArrayList();

			search.QueryTextChange += (sender, e) =>
			{
				updateEvents(search.Query);
			};

			updateEvents("");	

		}
		public static void updateEvents(String query){
			if (query.Equals ("")) {
				//grab everything

				List<Event> events = Event.GrabLocalEvents(0,0,25);
				//display everything
				events.Clear();
				foreach (var i in events) {
					TextCell current = new TextCell ();
					current.Text = i.Name;
					current.TextColor = Xamarin.Forms.Color.Blue;
					current.Detail = i.StartTime + " to " + i.EndTime;
					current.DetailColor = Xamarin.Forms.Color.White;
				}


			} else {
				//grab everything with title containing query
				List<Event> events = Event.GrabLocalEvents(0,0,25);
				//display everything from above
				events.Clear();
				foreach (var i in events) {
					
					if (i.Name.Contains(query)){
						TextCell current = new TextCell ();
						current.Text = i.Name;
						current.TextColor = Xamarin.Forms.Color.Blue;
						current.Detail = i.StartTime + " to " + i.EndTime;
						current.DetailColor = Xamarin.Forms.Color.White;
					}
				}
			}

		}
	}
}

