using System;

namespace JustRunWithIt
{
	public class User
	{
		public string Name { public get; private set; }

		private int id;
		private string firstName;
		private string lastName;
		private string distance;

		public User (string first, string last)
		{
			this.firstName = first;
			this.lastName = last;
		}

		public static User createFromID(int id){
			// ToDo: Hook up to database and retrieve info
		}
	}
}

