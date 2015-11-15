using System;
using System.Data.SqlClient;
using System.Xml;
using System.Data;

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

		/**
		 * Retrieve a hydrated User object from the database. Receive an empty model
		 * on failure
		 */
		public static User createFromID(int id){
			User user = new User ();
		
			// Get Config File
			DatabaseInfo config = Configuration.retrieveDatabaseInfo();

			// Connect to DB
			SqlConnection db = new SqlConnection();
			db.ConnectionString = "Data Source: " + config.Host + ";"
								+ "Initial Catalog: " + config.Database + ";"
								+ "User ID: " + config.Username + ";"
								+ "Password: " + config.Password + ";";

			// Execute command to retrieve data
			SqlCommand query = new SqlCommand ();
			query.CommandText = "SELECT * FROM Users Where UserID = @USERID";
			query.Parameters.Add ("@USERID", SqlDbType.Int);
			query.Parameters ["@USERID"].Value = id;

			try{
				SqlDataReader data = query.ExecuteReader ();
				user.id = data.GetInt32(data.GetOrdinal("UserID"));
				user.firstName = data.GetString(data.GetOrdinal("FirstName"));
				user.lastName = data.GetString(data.GetOrdinal("LastName"));
				user.distance = data.GetFloat(data.GetOrdinal("Distance"));
			} catch (Exception dbExecute) {
				Console.WriteLine (dbExecute.Message);
			}
			return user;
		}
	}
}

