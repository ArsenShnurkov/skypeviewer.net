using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace console
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try
			{
				var settings = ConfigurationManager.ConnectionStrings ["main_db"];
				Console.WriteLine ("ProviderName: {0}", settings.ProviderName);
				Console.WriteLine ("Connection string: {0}", settings.ConnectionString);
				var factory = DbProviderFactories.GetFactory (settings.ProviderName);
				using (var connection = factory.CreateConnection ())
				{
					connection.ConnectionString = settings.ConnectionString;
					connection.Open ();
					var command = connection.CreateCommand ();
					command.CommandText = "SELECT * FROM Messages";
					using (var reader = command.ExecuteReader ())
					{
						while (reader.Read ())
						{
							var xml = reader ["body_xml"];
							Console.WriteLine (xml);
						}
					}
				}
			} catch (Exception ex)
			{
				Console.WriteLine (ex.ToString ());
			}
		}
	}
}

