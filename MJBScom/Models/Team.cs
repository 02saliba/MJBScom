using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace MJBScom.Models
{
  public class Team
  {
    private int _id;
    private string _name;
    private bool _hometeam;

    public Team(string name, int id = 0, bool hometeam = true)
    {
      _id = id;
      _name = name;
      _hometeam = true;
    }

    public int GetId() { return _id; }
    public string GetName() { return _name; }
    public bool GetHometeam() { return _hometeam; }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM team;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Team> GetAll()
    {
      List<Team> allTeams = new List<Team> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM team;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int teamId = rdr.GetInt32(1);
        string teamName = rdr.GetString(0);
        bool homeTeam = rdr.GetBoolean(2);
        Team newTeam = new Team(teamName, teamId, homeTeam);
        allTeams.Add(newTeam);
      }
        conn.Close();
      if (conn != null)
      {
      conn.Dispose();
      }
      return allTeams;
    }


    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM team WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = _id;
      cmd.Parameters.Add(thisId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
