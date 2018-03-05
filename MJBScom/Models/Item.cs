using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace MJBScom.Models
{
  public class Item
  {
    private int _id;
    private string _name;
    private string _description;


    public Item(string name, string description, int id = 0)
    {
      _id = id;
      _name = name;
      _description = description;
    }

    public int GetId() {return _id;}
    public string GetName() {return _name;}
    public string GetDescription() {return _description;}

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemName = rdr.GetString(1);
        string itemDescription = rdr.GetString(2);
        Item newItem = new Item(itemName, itemDescription, itemId);
        allItems.Add(newItem);
      }
        conn.Close();
      if (conn != null)
      {
      conn.Dispose();
      }
      return allItems;
    }


    public static void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items WHERE id = @thisId;";

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
