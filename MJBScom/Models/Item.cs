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
    
    public int GetId() {return _id};
    public string GetName() {return _name};
    public int GetDescription() {return _description};

  }
}
