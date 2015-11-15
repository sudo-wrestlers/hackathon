using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace JustRunWithIt
{
	public class Group
	{
		public string Name {get; private set;}
		public string Description { get; private set; }
		public List<String> Tags;
		public List<int> Users { get { return users; } private set { } }
		public bool isPublic;

		private int id;
		private string name;
		private string description;
		private List<int> users;

		private Group() {
			name = "";
			description = "";
			isPublic = true;
			Tags = new List<String> ();
			users = new List<int> ();
		}

		public static Group GetFromEventID(int id){
			Group group = new Group();
			group.id = id;

			// Grab Connection
			SqlConnection db = Configuration.getConnection();

			// Establish query
			SqlCommand query = new SqlCommand ();
			query.CommandText = "SELECT * FROM Groups WHERE GroupID = @GROUPID";
			query.Parameters.Add ("@GROUPID", SqlDbType.Int);
			query.Parameters ["@GROUPID"].Value = group.id;

			try{
				db.Open();
				SqlDataReader data = query.ExecuteReader();
				group.name = data.GetString(data.GetOrdinal("GroupName"));
				group.description = data.GetString(data.GetOrdinal("GroupDescription"));
				group.isPublic = data.GetBoolean(data.GetOrdinal("PublicGroup"));

				query.CommandText = "SELECT UserID FROM Groups_Users WHERE GroupID = @GroupID";

				try {
					db.Open();

					SqlDataReader userList = query.ExecuteReader();
					do{
						group.users.Add(userList.GetInt32(userList.GetOrdinal("UserID")));
					} while (userList.NextResult());
				} catch (Exception userlistErr){
					Console.WriteLine(userlistErr.Message);
				}
			} catch (Exception err) {
				Console.WriteLine (err.Message);
			}

			return group;
		}

		public static Group AddGroup(string name, string description){
			Group group = new Group ();
			group.name = name;
			group.description = description;

			// Get Connection
			SqlConnection db = Configuration.getConnection();

			// Establish query
			SqlCommand query = new SqlCommand();
			query.CommandText = "INSERT Groups (GroupName, GroupDescription, PublicGroup) VALUES (@NAME, @DESCRIPT, @PUBLICITY);" +
								"SELECT last_insert_id();";
			query.Parameters.Add ("@NAME", SqlDbType.Int);
			query.Parameters.Add ("@DESCRIPT", SqlDbType.VarChar);
			query.Parameters.Add ("@PUBLICITY", SqlDbType.Bit);
			query.Parameters ["@NAME"].Value = group.name;
			query.Parameters ["@DESCRIPT"].Value = group.description;
			query.Parameters ["@PUBLICITY"].Value = group.isPublic;

			try{
				db.Open();
				group.id = Convert.ToInt32(query.ExecuteScalar());
			} catch (Exception err) {
				Console.WriteLine (err.Message);
			}

			return group;
		}

		/**
		 * Add user to this group
		 */
		public bool AddUser(int id){
			if (this.Users.Contains (id)) {
				throw new Exception ("This group already contains this user.");
			}

			bool isSuccessful = true;

			SqlConnection db = Configuration.getConnection ();

			SqlCommand query = new SqlCommand ();
			query.CommandText = "INSERT Groups_Events (GroupID, EventID) VALUES (@GROUPID, @EVENTID);";
			query.Parameters.Add ("@GROUPID", SqlDbType.Int);
			query.Parameters.Add ("@EVENTID", SqlDbType.Int);
			query.Parameters ["@GROUPID"].Value = this.id;
			query.Parameters ["@EVENTID"].Value = id;

			try {
				db.Open();
				query.ExecuteNonQuery();
				this.Users.Add(id);
			} catch (Exception err) {
				Console.WriteLine (err.Message);
			}

			return isSuccessful;
		}

		public bool RemoveUser(int id){
			if (!this.Users.Contains (id)) {
				throw new Exception ("This group does not contain.");
			}
			bool isSuccessful = true;

			SqlConnection db = Configuration.getConnection ();

			SqlCommand query = new SqlCommand ();
			query.CommandText = "DELETE Group_Users WHERE GroupID = @GROUPID AND UserID = @USERID";
			query.Parameters.Add ("@GROUPID", SqlDbType.Int);
			query.Parameters.Add ("@USERID", SqlDbType.Int);
			query.Parameters ["@GROUPID"].Value = this.id;
			query.Parameters ["@USERID"].Value = id;

			try {
				db.Open();
				query.ExecuteNonQuery();
			} catch (Exception err) {
				Console.WriteLine(err.Message);
			}

			return isSuccessful;
		}
	}
}

