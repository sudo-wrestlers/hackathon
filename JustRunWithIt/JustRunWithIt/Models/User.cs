using System;
using System.Xml;
using System.Data.SqlClient;
using System.Data;

namespace JustRunWithIt
{
	public class User
	{
		public int ID { get { return id; } private set { } }
		public string FirstName { get{ return firstName; } private set{ } }
		public string LastName { get{ return lastName; } private set{ } }
		public string Distance { get{ return Distance; } private set{ } }


		private int id;
		private string firstName;
		private string lastName;
		private float distance;

		private User() {
			id = 0;
			firstName = "";
			lastName = "";
			distance = 0.0f;
		}

		/**
		 * Add user and returns an object of it back upon  completion
		 */
		public static User AddUser(int id, string first, string last)
		{
			// Create Model to return
			User person = new User ();
			person.firstName = first;
			person.lastName = last;
			person.distance = 0;
			person.id = id;

			// Add User to database
			SqlConnection db = Configuration.getConnection();

			SqlCommand query = new SqlCommand ();
			query.CommandText = "INSERT INTO Users (UserID, first, last, distance) VALUES (@USERID, @FIRST, @LAST, @DISTANCE)";
			query.Parameters.Add ("@USERID", SqlDbType.Int);
			query.Parameters.Add ("@FIRST", SqlDbType.VarChar);
			query.Parameters.Add ("@LAST", SqlDbType.VarChar);
			query.Parameters.Add ("@DISTANCE", SqlDbType.Decimal);
			query.Parameters ["@USERID"].Value = person.id;
			query.Parameters ["@FIRST"].Value = person.firstName;
			query.Parameters ["@LAST"].Value = person.lastName;
			query.Parameters ["@DISTANCE"].Value = person.distance;

			try{
				query.ExecuteNonQuery();
			} catch (Exception err){
				Console.WriteLine (err.Message);
			}

			return person;
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
			SqlConnection db = Configuration.getConnection();

			// Execute command to retrieve data
			SqlCommand query = new SqlCommand ();
			query.CommandText = "SELECT * FROM Users Where UserID = @USERID";
			query.Parameters.Add ("@USERID", SqlDbType.Int);
			query.Parameters ["@USERID"].Value = id;

			try{
				db.Open();
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

