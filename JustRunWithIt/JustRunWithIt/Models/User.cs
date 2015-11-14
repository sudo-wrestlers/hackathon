using System;
using System.Data.SqlClient;

namespace JustRunWithIt
{
	public class User
	{
		public string FirstName { get; private set; }
		public string LastName { get; private set; }

		private int id;
		private string firstName;
		private string lastName;
		private float distance;

		private User() {}
		public User (string first, string last)
		{
			this.firstName = first;
			this.lastName = last;
		}

		public static User createFromID(int id){
			User user = new User ();

			SqlConnection db = new SqlConnection();


			return user;
		}
	}
}

