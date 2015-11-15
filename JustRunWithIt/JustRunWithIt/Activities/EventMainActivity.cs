
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace JustRunWithIt
{
	[Activity (Label = "EventMainActivity")]			
	public class EventMainActivity : Activity
	{
		public User user;
		private int userid;
		private int eventid;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Default to EventManagerFragment
			FragmentTransaction ft = this.FragmentManager.BeginTransaction ();
			// EventManagerFragment emf = new EventManagerFragment ();

			// ft.Add (Resource.Id.frameLayout1, emf);

			ft.Commit ();

			// Create your application here
			// SetContentView(Resource.Layout.event_manager);
		}
	}
}

