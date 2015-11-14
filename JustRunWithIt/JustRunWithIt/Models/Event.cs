using System;
using System.Collections;
using System.Collections.Generic;

namespace JustRunWithIt
{
	public enum Category {Sports, Outdoorsy, Social, Running};

	public class Event
	{
		public string Name { public get; private set;}
		public Category EvtCategory { public get; private set; }
		public Tuple<float, float> Location { public get; private set; }
		public DateTime StartTime { public get; private set; }
		public DateTime EndTime {public get; private set; }

		private int id;
		private int hostid;
		private string name;
		private Category evtCategory;
		private Tuple<float, float> location;
		private DateTime startTime;
		private DateTime endTime;

		public Event (string name, float longitude, float latitude, int hostid)
		{
			//TODO: Add to sql and retrieve sql identifier
		
			this.name = name;
			this.location.Item1 = longitude;
			this.location.Item2 = latitude;
			this.hostid = hostid;
		}

		public getFromEventID (int eventid)
		{
			//TODO: Hydrate model from database
	}
}

