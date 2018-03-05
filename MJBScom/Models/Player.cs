using System;
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

    }
  }
}
