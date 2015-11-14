using System;

namespace JustRunWithIt
{
	public class User
	{
		public string FirstName { get; private set; }
		public string LastName { get; private set; }

		private int id;
		private string firstName;
		private string lastName;
		private string distance;

		private User() {}
		public User (string first, string last)
		{
			this.firstName = first;
			this.lastName = last;
		}

		public static User createFromID(int id){
			// TODO: Hook up to database and retrieve info

			return new User();
		}
	}
}

