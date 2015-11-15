
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
using Android.Widget;

namespace JustRunWithIt
{
	public class EventEditFragment : Fragment
	{
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			Spinner spinner =  F<Spinner> (Resource.Id.spinner1);

			spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
			var adapter = ArrayAdapter.CreateFromResource (
				this, Resource.Array.categories_array, Android.Resource.Layout.SimpleSpinnerItem);

			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spinner.Adapter = adapter;
			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View verticalLinearLayout = inflater.Inflate(Resource.Layout.mylistrow,  null);
			View horizontalLInearLaoyout = verticalLinearLayout.FindViewById(Resource.Id.questionRow);
			Spinner spinner = (Spinner) horizontalLInearLaoyout.FindViewById(Resource.Id.spinner1);

			var adapter = ArrayAdapter.CreateFromResource (
				this, Resource.Array.categories_array, Android.Resource.Layout.SimpleSpinnerItem);

			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spinner.Adapter = adapter;
			

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

