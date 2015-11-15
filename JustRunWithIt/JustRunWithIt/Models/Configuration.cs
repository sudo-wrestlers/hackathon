using System;
using System.Xml;
using System.Data.SqlClient;

namespace JustRunWithIt
{
	public struct DatabaseInfo{
		public string Host;
		public string Username;
		public string Password;
		public string Database;
	}

	public class Configuration
	{
		private Configuration (){}
		public static XmlDocument retrieveConfig(){
			XmlDocument config = new XmlDocument ();
			config.PreserveWhitespace = true;
			config.Load ("config.xml");
			return config;
		}

		public static DatabaseInfo retrieveDatabaseInfo(){
			// Retrieve and parse document
			XmlDocument config = Configuration.retrieveConfig();
			XmlNode root = config.FirstChild;
			XmlNode databaseInfo = root.SelectSingleNode ("database");

			// Create data struct and hydrate
			DatabaseInfo db = new DatabaseInfo ();
			db.Host = databaseInfo.SelectSingleNode ("host").Value;
			db.Username = databaseInfo.SelectSingleNode ("username").Value;
			db.Password = databaseInfo.SelectSingleNode ("password").Value;
			db.Database = databaseInfo.SelectSingleNode ("database_name").Value;

			return db;
		}

		public static SqlConnection getConnection(){
			DatabaseInfo config = Configuration.retrieveDatabaseInfo();

			// Connect to DB
			SqlConnection db = new SqlConnection ();
			db.ConnectionString = "Data Source: " + config.Host + ";"
			+ "Initial Catalog: " + config.Database + ";"
			+ "User ID: " + config.Username + ";"
			+ "Password: " + config.Password + ";";

			return db;
		}
	}
}

