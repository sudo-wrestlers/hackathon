
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace JustRunWithIt
{
	public class EventManagerFragment : Fragment
	{
		private LocationManager _lm;
		private Location _cl;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
			string lp = string.Empty;
			_lm = (LocationManager) GetSystemService(LocationService);
			Criteria cr = new Criteria { Accuracy = Accuracy.Fine };
			IList<string> alp = _lm.GetProviders (cr, true);

			if (alp.Any ()) {
				lp = alp.First ();
				_lm.RequestLocationUpdates (lp, this);
			}
		}

		public void OnLocationChanged(Location location)
		{
			_cl = location;
			if (_cl != null) {
				_lm.RemoveUpdates (this);
				List<Event> localEvents = Event.GrabLocalEvents (_cl.Latitude, _cl.Longitude, 5);
				List<string> eventNames = new List<string> ();
				foreach (Event ev in localEvents) {
					eventNames.Add (ev.Name);
				}
				this.View.FindViewById<ListView> (Resource.Id.listView1).ItemsSource = eventNames;
			} 
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			return base.OnCreateView (inflater, container, savedInstanceState);
		}

		[Java.Interop.Export("createEventClickHandler")]
		public void createEventClickHandler(View v) {
			// Replace with EventEditFragment
			FragmentTransaction ft = this.FragmentManager.BeginTransaction ();
			EventEditFragment eef = new EventEditFragment ();

			ft.Replace (Resource.Id.frameLayout1, eef);

			ft.Commit ();
		}
	}
}

