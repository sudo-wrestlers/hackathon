using System;
using System.Collections;
using System.Collections.Generic;

namespace JustRunWithIt
{
	public enum Category {Sports, Outdoorsy, Social, Running};

	public class Event
	{
		public string Name { get; private set;}
		public Category EvtCategory { get; private set; }
		public Tuple<float, float> Location { get; private set; }
		public DateTime StartTime { get; private set; }
		public DateTime EndTime { get; private set; }

		private int id;
		private int hostid;
		private string name;
		private Category evtCategory;
		private Tuple<float, float> location;
		private DateTime startTime;
		private DateTime endTime;

		private Event() {}
		public Event (string name, float longitude, float latitude, int hostid)
		{
			//TODO: Add to sql and retrieve sql identifier
		
			this.name = name;
			this.location = new Tuple<float, float> (latitude, longitude);
			this.hostid = hostid;
		}

		public static Event getFromEventID (int eventid)
		{
			//TODO: Hydrate model from database

			return new Event();
		}
	}
}

