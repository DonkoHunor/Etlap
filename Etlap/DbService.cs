using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Etlap
{
	internal class DbService
	{
		MySqlConnection connection;

		public DbService() 
		{
			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
			builder.Port = 3306;
			builder.Server = "localhost";
			builder.UserID = "root";
			builder.Password = "";
			builder.Database = "etlapdb";
			connection = new MySqlConnection(builder.ConnectionString);
		}

		public bool Create(Etel etel)
		{
			OpenConnection();
			string sql = "INSERT INTO etlap (nev, leiras, ar, kategoria) VALUES (@nev, @leiras, @ar, @kategoria)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			cmd.Parameters.AddWithValue("nev",etel.Nev);
			cmd.Parameters.AddWithValue("leiras",etel.Leiras);
			cmd.Parameters.AddWithValue("ar",etel.Ar);
			cmd.Parameters.AddWithValue("kategoria",etel.Kategoria);
			int result = cmd.ExecuteNonQuery();
			CloseConnection();
			if (result > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public List<Etel> GetAll()
		{
			List<Etel> list = new List<Etel>();
			OpenConnection();
			string sql = "SELECT * FROM etlap";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			using (MySqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					int id = reader.GetInt32("id");
					string nev = reader.GetString("nev");
					string leiras = reader.GetString("leiras");
					int ar = reader.GetInt32("ar");
					string kategoria = reader.GetString("kategoria");
					Etel etel = new Etel(id, nev, leiras, kategoria, ar);
					list.Add(etel);
				}
			}
			CloseConnection();
			return list;
			
		}

		public void Update(Etel selected, int ar)
		{
			OpenConnection();
			string sql = "UPDATE etlap SET ar = ar + @ar WHERE id = @id";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			cmd.Parameters.AddWithValue("ar", ar);
			cmd.Parameters.AddWithValue("id", selected.Id);
			cmd.ExecuteNonQuery();
			CloseConnection();
		}

		public void UpdateAll(int ar)
		{
			OpenConnection();
			string sql = "UPDATE etlap SET ar = ar + @ar";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			cmd.Parameters.AddWithValue("ar", ar);
			cmd.ExecuteNonQuery();
			CloseConnection();
		}

		public void UpdatePercent(Etel selected, int ar)
		{
			OpenConnection();
			string sql = "UPDATE etlap SET ar = (ar + (ar / 100 * @ar)) WHERE id = @id";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			cmd.Parameters.AddWithValue("ar", ar);
			cmd.Parameters.AddWithValue("id", selected.Id);
			cmd.ExecuteNonQuery();
			CloseConnection();
		}

		public void UpdatePercentAll(int ar)
		{
			OpenConnection();
			string sql = "UPDATE etlap SET ar = (ar + (ar/100 * @ar))";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			cmd.Parameters.AddWithValue("ar", ar);
			cmd.ExecuteNonQuery();
			CloseConnection();
		}

		public void Delete(Etel selected)
		{
			OpenConnection();
			string sql = "DELETE FROM etlap WHERE id=@selected";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			cmd.Parameters.AddWithValue("selected", selected.Id);
			cmd.ExecuteNonQuery();
			CloseConnection();
		}

		private void CloseConnection()
		{
			if (connection.State != System.Data.ConnectionState.Closed) 
			{
				connection.Close();
			}
		}

		private void OpenConnection()
		{
			if (connection.State != System.Data.ConnectionState.Open)
			{
				connection.Open();
			}
		}
	}
}
