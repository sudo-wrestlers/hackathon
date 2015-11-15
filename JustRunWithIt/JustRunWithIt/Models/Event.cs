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
		public string Description { get { return description; }private set { } }
		public string HostID { get { return HostID; } private set { } }
		public Category EvtCategory;
		public Tuple<float, float> Location;
		public DateTime StartTime;
		public DateTime EndTime;
		public bool isPublic;

		private int id;
		private int hostid;
		private string hosttype;
		private string name;
		private string description;

		private Event() {
			id = -1;
			name = "";
			description = "";
			isPublic = true;
			EvtCategory = Category.NULL;
			Location = new Tuple<float, float> (0,0);
			StartTime = new DateTime ();
			EndTime = new DateTime ();
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
			query.CommandText = "INSERT INTO Events (EventName, EventDescription, PublicEvent, EventStartTime, EventEndTime, HostID, HostType, EventType, Longitude, Latitude)"
							  + "VALUES (@NAME, @DESCRIPT, @PUBLICITY, @START, @END, @HOSTID, @HOSTTYPE, @EVENTTYPE, @LONGIT, @LATIT)";
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
			query.Parameters ["@HOSTTYPE"].Value = this.hosttype;
			query.Parameters ["@EVENTTYPE"].Value = (int)this.EvtCategory;
			query.Parameters ["@LONGIT"].Value = this.Location.Item1;
			query.Parameters ["@LATIT"].Value = this.Location.Item2;

			bool isSuccessful = true;
			try{
				db.Open();
				query.ExecuteNonQuery();
			} catch (Exception err) {
				Console.WriteLine (err.Message);
			}

			return isSuccessful;
		}

		public bool SaveEvent(){
			bool isSuccessful = true;

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
			} catch (Exception err){
				Console.WriteLine(err.Message);
			}

			return new Event();
		}
	}
}

