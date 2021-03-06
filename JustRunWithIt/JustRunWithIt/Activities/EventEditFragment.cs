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

			return base.OnCreateView (inflater, container, savedInstanceState);
		}

		[Java.Interop.Export("createCreateEventClickHandler")]
		public void createCreateEventClickHandler(View v) {

			EditText eventTitle = v.FindViewById<EditText> (Resource.Id.editText3);
			EditText description = v.FindViewById<EditText> (Resource.Id.editText6);
			EditText category = v.FindViewById<EditText> (Resource.Id.editText5);
			// Create New Event
			Event newEvent = new Event(eventTitle.Text, description.Text);




			newEvent.EvtCategory = (Category) Enum.Parse (typeof(Category), category.Text);
			// 	send to database

			newEvent.SaveEvent ();

			//  preserve event id

			FragmentTransaction ft = this.FragmentManager.BeginTransaction ();
			EventInfoFragment eif = new EventInfoFragment ();

			ft.Replace (Resource.Id.frameLayout1, eif);

			ft.Commit ();
		}

	}
}

