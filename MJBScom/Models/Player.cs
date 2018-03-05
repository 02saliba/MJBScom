using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MJBScom.Models
{
  public class Player
  {
    private int _id;
    private string _name;
    private int _agility;
    private int _intelligence;
    private int _strength;
    private int _luck;

    public Player(string name, int agility, int intel, int strength, int luck, int id = 0)
    {
      _id = id;
      _name = name;
      _agility = agility;
      _intelligence = intel;
      _strength = strength;
      _luck = luck;
    }

    public int GetId() {return _id;}
    public string GetName() {return _name;}
    public int GetAgility() {return _agility;}
    public int GetIntelligence() {return _intelligence;}
    public int GetStrength() {return _strength;}
    public int GetLuck() {return _luck;}

    public static void DeleteAll()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM players; ALTER TABLE players AUTO_INCREMENT = 1;";

        cmd.ExecuteNonQuery();

        conn.Close();

        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public void Save()
    {
        // MySqlConnection conn = DB.Connection();
        // conn.Open();
        // var cmd = conn.CreateCommand() as MySqlCommand;
        // cmd.CommandText = @"INSERT INTO `players` (`id`, `name`, `agility`, `intelligence`, `strength`, `luck`) VALUES (@ThisId, @Name, @Agility, @Intelligence, @Strength, @Luck);";
        //
        // cmd.Parameters.AddWithValue("@ThisId", this._id);
        // cmd.Parameters.AddWithValue("@Name", this._name);
        // cmd.Parameters.AddWithValue("@Agility", this._agility);
        // cmd.Parameters.AddWithValue("@Intelligence", this._intelligence);
        // cmd.Parameters.AddWithValue("@Strength", this._strength);
        // cmd.Parameters.AddWithValue("@Luck", this._luck);
        //
        // cmd.ExecuteNonQuery();
        // _id = (int) cmd.LastInsertedId;
        //
        // conn.Close();
        // if (conn != null)
        // {
        //     conn.Dispose();
        // }
    }

    public static List<Player> GetAll()
    {
        List<Player> allPlayers = new List<Player>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM players;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int playerId = rdr.GetInt32(0);
            string playerName = rdr.GetString(1);
            int playerAgility = rdr.GetInt32(6);
            int playerIntelligence = rdr.GetInt32(7);
            int playerStrength = rdr.GetInt32(8);
            int playerLuck = rdr.GetInt32(9);
            Player newPlayer = new Player(playerName, playerAgility, playerIntelligence, playerStrength, playerLuck, playerId);

            allPlayers.Add(newPlayer);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allPlayers;
    }

    public override bool Equals(System.Object otherPlayer)
    {
        if (!(otherPlayer is Player))
        {
            return false;
        }
        else
        {
            Player newPlayer = (Player) otherPlayer;
            bool idEquality = (this.GetId() == newPlayer.GetId());
            bool nameEquality = (this.GetName() == newPlayer.GetName());
            bool agilityEquality = (this.GetAgility() == newPlayer.GetAgility());
            bool intelligenceEquality = (this.GetIntelligence() == newPlayer.GetIntelligence());
            bool strengthEquality = (this.GetStrength() == newPlayer.GetStrength());
            bool luckEquality = (this.GetLuck() == newPlayer.GetLuck());

            return (idEquality && nameEquality && agilityEquality && intelligenceEquality && strengthEquality && luckEquality);
        }
    }

    public override int GetHashCode()
    {
        return this.GetName().GetHashCode();
    }
  }
}
