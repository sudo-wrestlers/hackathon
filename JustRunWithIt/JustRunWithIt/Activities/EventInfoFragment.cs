
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace JustRunWithIt
{
	public class EventInfoFragment : Fragment
	{
		private User _user;
		private Event _event;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here

			// get userid some-how here!
			int userid = savedInstanceState.GetInt("userid"); // do real stuff with intents here
			this._user = User.createFromID(userid);

			int eventid = savedInstanceState.GetInt("eventid"); // do real stuff with intents here
			this._event = Event.getFromEventID(eventid);

			// if (user == creator)
			if (this._user.ID == this._event.HostID) {
			//  button = "Cancel Event"
				this.View.FindViewById<Button>(Resource.Id.button1).Text = "Cancel Event";

			// else if (user == signed-up)
			} else if (this._event.Attendees.Contains(this._user.ID)) {
			//  button = "Leave Event"
				this.View.FindViewById<Button>(Resource.Id.button1).Text = "Leave Event";

			// else 
			} else {
			//  button = "Join Event"
				this.View.FindViewById<Button>(Resource.Id.button1).Text = "Join Event";
			}

			// display event information
			this.View.FindViewById<TextView>(Resource.Id.textView2).Text = this._event.Name;
			this.View.FindViewById<TextView> (Resource.Id.textView4).Text = this._event.Description;
			this.View.FindViewById<TextView> (Resource.Id.TextView6).Text = this._event.EvtCategory;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			return base.OnCreateView (inflater, container, savedInstanceState);
		}

		[Java.Interop.Export("joinEventClickHandler")]
		public void joinEventClickHandler(View v) {
			bool joined = false;
			// if (user == creator)
			if (this._user.ID == this._event.HostID) {
				//	cancel event
				this._event.RemoveEvent();
				// redirect to view manager fragment
				FragmentTransaction ft = this.FragmentManager.BeginTransaction ();
				EventManagerFragment emf = new EventManagerFragment ();

				ft.Replace (Resource.Id.frameLayout1, emf);

				ft.Commit ();

				return;
			} 
			// else if (user == signed-up)
			else if (this._event.Attendees.Contains (this._user.ID)) {
				// leave event
				this._event.RemoveAttendee(this._user.ID);
			} 
			// else 
			else {
				// join event
				this._event.AddAttendee(this._user.ID);
				joined = true;
			}

			// Update Button text
			if (joined) {
				v.FindViewById<Button> (Resource.Id.button1).Text = "Leave Event";
			} else {
				v.FindViewById<Button> (Resource.Id.button1).Text = "Join Event";
			}
		}
	}
}

