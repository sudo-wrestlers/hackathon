using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace JustRunWithIt
{
	public enum Category {Sports, Outdoorsy, Social, Running, NULL};

	public class Event
	{
		public string Name { get { return name; } private set { }}
		public string Description { get {return description;} private set { } }
		public int HostID { get { return HostID; } private set { } }
		public List<int> Attendees { get { return attendees; } private set { } }
		public string HostType;
		public Category EvtCategory;
		public Tuple<float, float> Location;
		public DateTime StartTime;
		public DateTime EndTime;
		public bool isPublic; 

		private int id;
		private int hostid;
		private string name;
		private string description;
		private List<int> attendees;

		private Event() {
			id = -1;
			hostid = -1;
			HostType = "";
			name = "";
			description = "";
			isPublic = true;
			EvtCategory = Category.NULL;
			Location = new Tuple<float, float> (0,0);
			StartTime = new DateTime ();
			EndTime = new DateTime ();
			attendees = new List<int> ();
		}

		/**
		 * Used for adding the event on the first addition to
		 * the database & never again. Exception is thrown if
		 * an attempt to readd an existing event
		 */
		public bool AddEvent(int OwnerID){
			// Save ownerID
			this.hostid = OwnerID;

			SqlConnection db = Configuration.getConnection ();

			if (this.id == -1) {
				throw new Exception ("Saving existing event.");
			}

			// Set up command
			SqlCommand query = new SqlCommand();
			query.CommandText = "INSERT INTO Events (EventName, EventDescription, PublicEvent, EventStartTime, EventEndTime, HostID, HostType, EventType, Longitude, Latitude)" +
				"VALUES (@NAME, @DESCRIPT, @PUBLICITY, @START, @END, @HOSTID, @HOSTTYPE, @EVENTTYPE, @LONGIT, @LATIT);" +
				"SELECT last_insert_id();";
			query.Parameters.Add ("@NAME", SqlDbType.VarChar);
			query.Parameters.Add ("@DESCRIPT", SqlDbType.VarChar);
			query.Parameters.Add ("@PUBLICITY", SqlDbType.Bit);
			query.Parameters.Add ("@START", SqlDbType.DateTime);
			query.Parameters.Add ("@END", SqlDbType.DateTime);
			query.Parameters.Add ("@HOSTID", SqlDbType.Int);
			query.Parameters.Add ("@HOSTTYPE", SqlDbType.VarChar);
			query.Parameters.Add ("@EVENTTYPE", SqlDbType.Int);
			query.Parameters.Add ("@LONGIT", SqlDbType.Decimal);
			query.Parameters.Add ("@LATIT", SqlDbType.Decimal);
			query.Parameters ["@NAME"].Value = this.name;
			query.Parameters ["@DESCRIPT"].Value = this.description;
			query.Parameters ["@PUBLICITY"].Value = this.isPublic;
			query.Parameters ["@START"].Value = this.StartTime.ToString (CultureInfo.InvariantCulture.DateTimeFormat);
			query.Parameters ["@END"].Value = this.EndTime.ToString (CultureInfo.InvariantCulture.DateTimeFormat);
			query.Parameters ["@HOSTID"].Value = this.hostid;
			query.Parameters ["@HOSTTYPE"].Value = this.HostType;
			query.Parameters ["@EVENTTYPE"].Value = (int)this.EvtCategory;
			query.Parameters ["@LONGIT"].Value = this.Location.Item1;
			query.Parameters ["@LATIT"].Value = this.Location.Item2;

			bool isSuccessful = true;
			try{
				db.Open();

				// Retrieves the ID in one go
				this.id = Convert.ToInt32(query.ExecuteScalar());
			} catch (Exception err) {
				isSuccessful = false;
				Console.WriteLine (err.Message);
			}
			return isSuccessful;
		}

		public bool SaveEvent(){
			bool isSuccessful = true;

			// Grab Connection
			SqlConnection db = Configuration.getConnection();

			// Establish Connection
			SqlCommand query = new SqlCommand();
			query.CommandText = "UPDATE Events" +
				"SET EventName = @NAME," +
				"SET EventDescription = @DESCRIPT," +
				"SET PublicEvent = @PUBLICITY," +
				"SET EventStartTime = @START," +
				"SET EventEndTime = @END," +
				"SET HostID = @HOSTID," +
				"SET HostType = @HOSTTYPE," +
				"SET EventType = @EVENTTYPE," +
				"SET Longitude = @LONGIT," +
				"SET Latitude = @LATIT" +
				"WHERE EventID = @EVENTID";
			query.Parameters.Add ("@EVENTID", SqlDbType.Int);
			query.Parameters.Add ("@NAME", SqlDbType.VarChar);
			query.Parameters.Add ("@DESCRIPT", SqlDbType.VarChar);
			query.Parameters.Add ("@PUBLICITY", SqlDbType.Bit);
			query.Parameters.Add ("@START", SqlDbType.DateTime);
			query.Parameters.Add ("@END", SqlDbType.DateTime);
			query.Parameters.Add ("@HOSTID", SqlDbType.Int);
			query.Parameters.Add ("@HOSTTYPE", SqlDbType.VarChar);
			query.Parameters.Add ("@EVENTTYPE", SqlDbType.Int);
			query.Parameters.Add ("@LONGIT", SqlDbType.Decimal);
			query.Parameters.Add ("@LATIT", SqlDbType.Decimal);
			query.Parameters ["@EVENTID"].Value = this.id;
			query.Parameters ["@NAME"].Value = this.name;
			query.Parameters ["@DESCRIPT"].Value = this.description;
			query.Parameters ["@PUBLICITY"].Value = this.isPublic;
			query.Parameters ["@START"].Value = this.StartTime.ToString (CultureInfo.InvariantCulture.DateTimeFormat);
			query.Parameters ["@END"].Value = this.EndTime.ToString (CultureInfo.InvariantCulture.DateTimeFormat);
			query.Parameters ["@HOSTID"].Value = this.hostid;
			query.Parameters ["@HOSTTYPE"].Value = this.HostType;
			query.Parameters ["@EVENTTYPE"].Value = (int)this.EvtCategory;
			query.Parameters ["@LONGIT"].Value = this.Location.Item1;
			query.Parameters ["@LATIT"].Value = this.Location.Item2;

			try{
				db.Open();
				query.ExecuteNonQuery();
			} catch (Exception err) {
				Console.WriteLine (err.Message);
			}

			return isSuccessful;
		}
			

		public bool RemoveEvent(){
			SqlConnection db = Configuration.getConnection ();

			// Setup Command
			SqlCommand query = new SqlCommand ();
			query.CommandText = "DELETE Events WHERE EventID = @EVENTID";
			query.Parameters.Add ("@EVENTID", SqlDbType.Int);
			query.Parameters ["@EVENTID"].Value = this.id;

			// Execute Command
			bool success = true;
			try{
				query.ExecuteNonQuery();
			} catch (Exception err){
				success = false;
				Console.WriteLine (err.Message);
			}

			// Remove List
			query.CommandText = "DELETE Events_Users WHERE EventID = @EVENTID";
			try{
				query.ExecuteNonQuery();
			} catch (Exception err) {
				success = false;
				Console.WriteLine (err.Message);
			}

			return success;
		}

		public static Event getFromEventID (int eventid)
		{
			Event model = new Event();

			SqlConnection db = Configuration.getConnection();
			SqlCommand query = new SqlCommand ();
			query.CommandText = "SELECT * FROM Events WHERE EventID = @EVENTID";
			query.Parameters.Add ("@EVENTID", SqlDbType.Int);
			query.Parameters ["@EVENTID"].Value = eventid;

			try {
				db.Open();
				SqlDataReader data = query.ExecuteReader();

				model.id = data.GetInt32(data.GetOrdinal("EventID"));
				model.name = data.GetString(data.GetOrdinal("EventName"));
				model.description = data.GetString(data.GetOrdinal("EventDescription"));
				model.hostid = data.GetInt32(data.GetOrdinal("HostID"));
				model.EvtCategory = (Category)data.GetInt32(data.GetOrdinal("EventType"));
				model.Location = new Tuple<float, float>(data.GetFloat(data.GetOrdinal("Latitude")), data.GetFloat(data.GetOrdinal("Longitude")));
				model.StartTime = data.GetDateTime(data.GetOrdinal("EventStartTime"));
				model.EndTime = data.GetDateTime(data.GetOrdinal("EventEndTime"));

				// Retrieve Attendees List
				query.CommandText = "SELECT UserID FROM Events_Users WHERE EventID = @EVENTID";
				query.Parameters.Add ("@EVENTID", SqlDbType.Int);
				query.Parameters ["@EVENTID"].Value = model.id;

				try{
					db.Open();

					SqlDataReader eventList = query.ExecuteReader();

					// Add id to list
					do{
						model.attendees.Add(eventList.GetInt32(eventList.GetOrdinal("UserID")));
					} while (eventList.NextResult());
				} catch (Exception attendErr) {
					Console.WriteLine(attendErr.Message);
				}
			} catch (Exception err){
				Console.WriteLine(err.Message);
			}

			return new Event();
		}

		public static List<Event> GrabLocalEvents(float myLat, float myLong, float radius){
			List<Event> events = new List<Event>();

			// Convert degrees for lat & long
			float degreesDeviance = radius * 1.0f/69; // HaHa 69

			// Establish Connection
			SqlConnection db = Configuration.getConnection();

			SqlCommand query = new SqlCommand ();
			query.CommandText = "SELECT * " +
								"FROM Events" +
								"WHERE Longitude > @MINLONGIT" +
								"AND Longitude < @MAXLONGIT" +
								"AND Latitude > @MINLATIT" +
								"AND Latitude < @MAXLATIT;";
			query.Parameters.Add ("@MINLONGIT", SqlDbType.Decimal);
			query.Parameters.Add ("@MAXLONGIT", SqlDbType.Decimal);
			query.Parameters.Add ("@MINLATIT", SqlDbType.Decimal);
			query.Parameters.Add ("@MAXLATIT", SqlDbType.Decimal);
			// query.Parameters.Add ("@CURDATE", SqlDbType.DateTime);
			// query.Parameters.Add ("@CURDATE", SqlDbType.DateTime);
			query.Parameters ["@MINLONGIT"].Value = myLong - degreesDeviance;
			query.Parameters ["@MAXLONGIT"].Value = myLong + degreesDeviance;
			query.Parameters ["@MINLATIT"].Value = myLat - degreesDeviance;
			query.Parameters ["@MAXLATIT"].Value = myLat + degreesDeviance;

			try {
				db.Open();
				SqlDataReader data = query.ExecuteReader();

				do {
					Event model = new Event();

					model.id = data.GetInt32(data.GetOrdinal("EventID"));
					model.name = data.GetString(data.GetOrdinal("EventName"));
					model.description = data.GetString(data.GetOrdinal("EventDescription"));
					model.hostid = data.GetInt32(data.GetOrdinal("HostID"));
					model.EvtCategory = (Category)data.GetInt32(data.GetOrdinal("EventType"));
					model.Location = new Tuple<float, float>(data.GetFloat(data.GetOrdinal("Latitude")), data.GetFloat(data.GetOrdinal("Longitude")));
					model.StartTime = data.GetDateTime(data.GetOrdinal("EventStartTime"));
					model.EndTime = data.GetDateTime(data.GetOrdinal("EventEndTime"));

					// Retrieve Attendees List
					query.CommandText = "SELECT UserID FROM Events_Users WHERE EventID = @EVENTID";
					query.Parameters.Add ("@EVENTID", SqlDbType.Int);
					query.Parameters ["@EVENTID"].Value = model.id;

					try{
						db.Open();

						SqlDataReader eventList = query.ExecuteReader();

						do{
							model.attendees.Add(eventList.GetInt32(eventList.GetOrdinal("UserID")));
							

						} while (eventList.NextResult());
					} catch (Exception attendErr) {
						Console.WriteLine(attendErr.Message);
					}
					events.Add(model);
				} while (data.NextResult());
			} catch (Exception err){
				Console.WriteLine (err.Message);
			}

			return events;
		}

		public bool AddAttendee(int id){
			bool isSuccess = true;

			// Connection
			SqlConnection db = Configuration.getConnection ();

			SqlCommand query = new SqlCommand ();
			query.CommandText = "INSERT Events_Users(EventID, UserID)" +
			"VALUES (@EVENTID, @USERID);";
			query.Parameters.Add ("@EVENTID", SqlDbType.Int);
			query.Parameters.Add ("@USERID", SqlDbType.Int);
			query.Parameters ["@EVENTID"].Value = this.id;
			query.Parameters ["@USERID"].Value = id;
			try {
				db.Open ();

				query.ExecuteNonQuery ();
			} catch (Exception err) {
				isSuccess = false;
				Console.WriteLine (err.Message);
			}

			return isSuccess;
		}

		public bool RemoveAttendee(int id) {
			if (!this.Attendees.Contains (id)) {
				throw new Exception ("Group does not contain userid.");
			}

			bool isSuccess = true;

			// Connection
			SqlConnection db = Configuration.getConnection();
			SqlCommand query = new SqlCommand ();
			query.CommandText = "DELETE Events_Users WHERE EventID = @EVENTID AND UserID = @USERID";
			query.Parameters.Add ("@EVENTID", SqlDbType.Int);
			query.Parameters.Add ("@USERID", SqlDbType.Int);
			query.Parameters ["@EVENTID"].Value = this.id;
			query.Parameters ["@USERID"].Value = id;

			try {
				db.Open();

				query.ExecuteNonQuery();
			} catch (Exception err) {
				isSuccess = false;
				Console.WriteLine (err.Message);
			}

			return isSuccess;
		}
	}
}

