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
	public class EventManagerFragment : Fragment
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
