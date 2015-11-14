using System;
using System.Collections.Generic;

namespace JustRunWithIt
{
	public class Group
	{
		public string Name {get; private set;}
		public string Description { get; private set; }
		public List<String> Tags;

		private string name;
		private string description;
		private List<User> attendees;

		private Group() {}
		public Group (string name, string description)
		{
			this.Name = name;
			this.Description = description;
			this.attendees = new List<User> ();
		}




	}
}

