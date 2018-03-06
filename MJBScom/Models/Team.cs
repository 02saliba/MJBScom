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
    private int _xPos = 0;
    private int _yPos = 0;

    public Team(string name, int id = 0, bool hometeam = true)
    {
      _id = id;
      _name = name;
      _hometeam = true;
    }

    public int GetId() { return _id; }
    public string GetName() { return _name; }
    public bool GetHometeam() { return _hometeam; }
    public int GetXPos() { return _xPos; }
    public int GetYPos() { return _yPos; }

    public void SetX(int x)
    {
      _xPos = x;
    }
    public void SetY(int y)
    {
      _xPos = y;
    }

    public override bool Equals(System.Object otherTeam)
    {
        if (!(otherTeam is Team))
        {
            return false;
        }
        else
        {
            Team newTeam = (Team) otherTeam;
            bool idEquality = _id == newTeam._id;
            bool nameEquality = _name == newTeam._name;
            bool hometeamEquality = _hometeam == newTeam._hometeam;
            bool xEquality = _xPos == newTeam._xPos;
            bool yEquality = _yPos == newTeam._yPos;

            return (idEquality && nameEquality && hometeamEquality && xEquality && yEquality);
        }
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM teams;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO teams (name, coordinates, allegiance) VALUES (@name, @coords, @homeTeam);";
      string coordString = _xPos.ToString() + ',' + _yPos.ToString();
      cmd.Parameters.AddWithValue("@name", _name);
      cmd.Parameters.AddWithValue("@coords", coordString);
      cmd.Parameters.AddWithValue("@homeTeam", _hometeam);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

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
      cmd.CommandText = @"SELECT * FROM teams;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int teamId = rdr.GetInt32(1);
        string teamName = rdr.GetString(0);
        bool homeTeam = rdr.GetBoolean(3);
        string posString = rdr.GetString(2);

        Team newTeam = new Team(teamName, teamId, homeTeam);
        Console.WriteLine(posString);
        string[] posArray = posString.Split(',');
        newTeam._xPos = int.Parse(posArray[0]);
        newTeam._yPos = int.Parse(posArray[1]);
        allTeams.Add(newTeam);
      }
        conn.Close();
      if (conn != null)
      {
      conn.Dispose();
      }
      return allTeams;
    }

    public static Team Find(int findId)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * from `teams` WHERE id = @ThisId;";

        cmd.Parameters.AddWithValue("@ThisId", findId);

        int teamId = 0;
        string teamName = "";
        bool isHomeTeam = true;
        string posString = "0,0";

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        if (rdr.Read())
        {
            teamId = rdr.GetInt32(1);
            teamName = rdr.GetString(0);
            isHomeTeam = rdr.GetBoolean(3);
            posString = rdr.GetString(2);
        }

        Team foundTeam = new Team(teamName, teamId, isHomeTeam);
        string[] posArray = posString.Split(',');
        foundTeam._xPos = int.Parse(posArray[0]);
        foundTeam._yPos = int.Parse(posArray[1]);

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }

        return foundTeam;

    }

    public void AddPlayer(int playerId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO players_teams (player_id, team_id) VALUES (@playerId, @teamId);";
      cmd.Parameters.AddWithValue("@playerId", playerId);
      cmd.Parameters.AddWithValue("@teamId", _id);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void RemovePlayer(int playerId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM players_teams WHERE player_id = @playerId AND team_id = @teamId);";
      cmd.Parameters.AddWithValue("@playerId", playerId);
      cmd.Parameters.AddWithValue("@teamId", _id);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Player> GetAllPlayers()
    {
        List<Player> allPlayers = new List<Player>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"
          SELECT players.* FROM teams
          JOIN players_teams ON (teams.id = players_teams.team_id)
          JOIN players ON (players_teams.player_id = players.id)
          WHERE teams.id = @teamId
        ;";
        cmd.Parameters.AddWithValue("@teamId", _id);
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int playerId = rdr.GetInt32(0);
            string playerName = rdr.GetString(1);
            int playerHPTotal = rdr.GetInt32(2);
            int playerHPRemaining = rdr.GetInt32(3);
            int playerAgility = rdr.GetInt32(5);
            int playerIntelligence = rdr.GetInt32(6);
            int playerStrength = rdr.GetInt32(7);
            int playerLuck = rdr.GetInt32(8);
            Player newPlayer = new Player(playerName, playerHPTotal, playerHPRemaining, playerAgility, playerIntelligence, playerStrength, playerLuck, playerId);

            allPlayers.Add(newPlayer);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allPlayers;
    }


    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM teams WHERE id = @thisId;";

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
