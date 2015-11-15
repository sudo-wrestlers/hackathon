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
		public bool isPublic;

		private int id;
		private string name;
		private string description;
		private List<User> attendees;

		private Group() {
			name = "";
			description = "";
			isPublic = true;
			attendees = new List<User>();
			Tags = new List<String> ();
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


	}
}

