﻿
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
	public class EventEditFragment : Fragment
	{
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			return base.OnCreateView (inflater, container, savedInstanceState);
		}

		[Java.Interop.Export("createCreateEventClickHandler")]
		public void createCreateEventClickHandler(View v) {
			// Create New Event
			// 	send to database

			// Send user to EventInfoFragment for new event
			//  preserve event id
			FragmentTransaction ft = this.FragmentManager.BeginTransaction ();
			EventInfoFragment eif = new EventInfoFragment ();

			ft.Replace (Resource.Id.frameLayout1, eif);

			ft.Commit ();
		}

	}
}

