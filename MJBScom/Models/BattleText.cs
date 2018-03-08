using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace MJBScom.Models
{
  public class BattleText
  {
    private int _id;
    private string _startText;
    private string _midText;
    private string _endText;


    public BattleText(string start, string mid, string end, int id = 0)
    {
      _id = id;
      _startText = start;
      _midText = mid;
      _endText = end;
    }

    public int GetId() {return _id;}
    public string GetStart() {return _startText;}
    public string GetMid() {return _midText;}
    public string GetEnd() {return _endText;}

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM battle_text;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<BattleText> GetAll()
    {
      List<BattleText> allText = new List<BattleText> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM battle_text;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string startText = rdr.GetString(1);
        string midText = rdr.GetString(2);
        string endText = rdr.GetString(3);
        allText.Add(new BattleText(startText, midText, endText, itemId));
      }
        conn.Close();
      if (conn != null)
      {
      conn.Dispose();
      }
      return allText;
    }


    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM battle_text WHERE id = @thisId;";

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

     public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO battle_text (start_battle, mid_battle, end_battle) VALUES (@start, @mid, @end);";

      cmd.Parameters.AddWithValue("@start", _startText);
      cmd.Parameters.AddWithValue("@mid", _midText);
      cmd.Parameters.AddWithValue("@end", _endText);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public static List<int> GetIds()
    {
      List<int> allIds = new List<int> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT id FROM battle_text;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        allIds.Add(rdr.GetInt32(0));
      }
        conn.Close();
      if (conn != null)
      {
      conn.Dispose();
      }
      return allIds;
    }

    public static BattleText Find(int findId)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * from `battle_text` WHERE id = @ThisId;";
        cmd.Parameters.AddWithValue("@ThisId", findId);

        int textId = 0;
        string startText = "";
        string midText = "";
        string endText = "";

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        if (rdr.Read())
        {
            textId = rdr.GetInt32(0);
            startText = rdr.GetString(1);
            midText = rdr.GetString(2);
            endText = rdr.GetString(3);

        }
        BattleText foundText = new BattleText(startText, midText, endText, textId);


        conn.Close();
        if (conn != null)
          conn.Dispose();
        return foundText;
    }
  }
}
