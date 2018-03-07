using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MJBScom.Models
{
    public class Player
    {
        private static Random r = new Random();

        private int _id;
        private string _name;
        private int _hpTotal;
        private int _hpRemaining;
        private int _agility;
        private int _intelligence;
        private int _strength;
        private int _luck;
        private bool _allegience;
        private int _xPos = 0;
        private int _yPos = 0;

        public Player(string name, int hpTotal, int hpRemaining, int agility, int intel, int strength, int luck, int id = 0)
        {
          _id = id;
          _name = name;
          _hpTotal = hpTotal;
          _hpRemaining = hpRemaining;
          _agility = agility;
          _intelligence = intel;
          _strength = strength;
          _luck = luck;
          _allegience = false;
        }
        public Player(string name, int hpTotal, int hpRemaining, int id = 0)
        {
          _id = id;
          _name = name;
          _hpTotal = hpTotal;
          _hpRemaining = hpRemaining;
          _allegience = false;
          for (int i = 0; i < 15; i++) {
            int stat = r.Next(4);
            switch(stat)
            {
              case 0:
                _agility++;
                break;
              case 1:
                _intelligence++;
                break;
              case 2:
                _strength++;
                break;
              case 3:
                _luck++;
                break;
            }
          }
        }

        public int GetId() {return _id;}
        public string GetName() {return _name;}
        public int GetHPTotal() {return _hpTotal;}
        public int GetHPRemaining() {return _hpRemaining;}
        public int GetAgility() {return _agility;}
        public int GetIntelligence() {return _intelligence;}
        public int GetStrength() {return _strength;}
        public int GetLuck() {return _luck;}
        public bool GetAllegience() {return _allegience;}
        public int GetX() { return _xPos; }
        public int GetY() { return _yPos; }

        public void SetName(string name) {_name = name;}
        public void SetHPTotal(int hpTotal) {_hpTotal = hpTotal;}
        public void SetHPRemaining(int hpRemaining) {_hpRemaining = hpRemaining;}
        public void SetAgility(int agility) {_agility = agility;}
        public void SetIntelligence(int intelligence) {_intelligence = intelligence;}
        public void SetStrength(int strength) {_strength = strength;}
        public void SetLuck(int luck) {_luck = luck;}
        public void SetAllegience(bool side) {_allegience = side;}
        public void SetX(int x) { _xPos = x; }
        public void SetY(int y) { _yPos = y; }

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
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `players` (`name`, `hp_total`, `hp_remaining`, `agility`, `intelligence`, `strength`, `luck`, `allegience`, `x_pos`, `y_pos`) VALUES (@Name, @HPTotal, @HPRemaining, @Agility, @Intelligence, @Strength, @Luck, @Allegience, @XPos, @YPos);";

            cmd.Parameters.AddWithValue("@Name", this._name);
            cmd.Parameters.AddWithValue("@HPTotal", this._hpTotal);
            cmd.Parameters.AddWithValue("@HPRemaining", this._hpRemaining);
            cmd.Parameters.AddWithValue("@Agility", this._agility);
            cmd.Parameters.AddWithValue("@Intelligence", this._intelligence);
            cmd.Parameters.AddWithValue("@Strength", this._strength);
            cmd.Parameters.AddWithValue("@Luck", this._luck);
            cmd.Parameters.AddWithValue("@Allegience", this._allegience);
            cmd.Parameters.AddWithValue("@XPos", this._xPos);
            cmd.Parameters.AddWithValue("@YPos", this._yPos);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
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
                int playerHPTotal = rdr.GetInt32(2);
                int playerHPRemaining = rdr.GetInt32(3);
                int playerAgility = rdr.GetInt32(5);
                int playerIntelligence = rdr.GetInt32(6);
                int playerStrength = rdr.GetInt32(7);
                int playerLuck = rdr.GetInt32(8);
                bool playerAllegience = rdr.GetBoolean(9);
                int playerXPos = rdr.GetInt32(10);
                int playerYPos = rdr.GetInt32(11);
                Player newPlayer = new Player(playerName, playerHPTotal, playerHPRemaining, playerAgility, playerIntelligence, playerStrength, playerLuck, playerId);
                newPlayer.SetAllegience(playerAllegience);
                newPlayer.SetX(playerXPos);
                newPlayer.SetY(playerYPos);

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
                bool hpTotalEquality = (this.GetHPTotal() == newPlayer.GetHPTotal());
                bool hpRemainingEquality = (this.GetHPRemaining() == newPlayer.GetHPRemaining());
                bool agilityEquality = (this.GetAgility() == newPlayer.GetAgility());
                bool intelligenceEquality = (this.GetIntelligence() == newPlayer.GetIntelligence());
                bool strengthEquality = (this.GetStrength() == newPlayer.GetStrength());
                bool luckEquality = (this.GetLuck() == newPlayer.GetLuck());

                return (idEquality && nameEquality && hpTotalEquality && hpRemainingEquality && agilityEquality && intelligenceEquality && strengthEquality && luckEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `players` WHERE id = @ThisId;";

            cmd.Parameters.AddWithValue("@ThisId", this._id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Update()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `players` SET `name` = @Name, `hp_total` = @HPTotal, `hp_remaining` = @HPRemaining, `agility` = @Agility, `intelligence` = @Intelligence, `strength` = @Strength, `luck` = @Luck, `allegience` = @Allegience, `x_pos` = @XPos, `y_pos` = @Ypos WHERE id = @ThisId;";

            cmd.Parameters.AddWithValue("@ThisId", this._id);
            cmd.Parameters.AddWithValue("@Name", this._name);
            cmd.Parameters.AddWithValue("@HPTotal", this._hpTotal);
            cmd.Parameters.AddWithValue("@HPRemaining", this._hpRemaining);
            cmd.Parameters.AddWithValue("@Agility", this._agility);
            cmd.Parameters.AddWithValue("@Intelligence", this._intelligence);
            cmd.Parameters.AddWithValue("@Strength", this._strength);
            cmd.Parameters.AddWithValue("@Luck", this._luck);
            cmd.Parameters.AddWithValue("@Allegience", this._allegience);
            cmd.Parameters.AddWithValue("@XPos", this._xPos);
            cmd.Parameters.AddWithValue("@YPos", this._yPos);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Player Find(int findId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * from `players` WHERE id = @ThisId;";
            cmd.Parameters.AddWithValue("@ThisId", findId);

            int playerId = 0;
            string playerName = "";
            int playerHPTotal = 0;
            int playerHPRemaining = 0;
            int playerAgility = 0;
            int playerIntelligence = 0;
            int playerStrength = 0;
            int playerLuck = 0;
            bool playerAllegience = false;
            int playerXPos = 0;
            int playerYPos = 0;

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            if (rdr.Read())
            {
                playerId = rdr.GetInt32(0);
                playerName = rdr.GetString(1);
                playerHPTotal = rdr.GetInt32(2);
                playerHPRemaining = rdr.GetInt32(3);
                playerAgility = rdr.GetInt32(5);
                playerIntelligence = rdr.GetInt32(6);
                playerStrength = rdr.GetInt32(7);
                playerLuck = rdr.GetInt32(8);
                playerAllegience = rdr.GetBoolean(9);
                playerXPos = rdr.GetInt32(10);
                playerYPos = rdr.GetInt32(11);
            }
            Player foundPlayer = new Player(playerName, playerHPTotal, playerHPRemaining, playerAgility, playerIntelligence, playerStrength, playerLuck, playerId);
            foundPlayer.SetAllegience(playerAllegience);
            foundPlayer.SetX(playerXPos);
            foundPlayer.SetY(playerYPos);

            conn.Close();
            if (conn != null)
              conn.Dispose();
            return foundPlayer;
        }

        public static List<Player> GetEnemies()
        {
          List<Player> allEnemies = new List<Player>{};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM players WHERE allegience = 0;";
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
              bool playerAllegience = rdr.GetBoolean(9);
              int playerXPos = rdr.GetInt32(10);
              int playerYPos = rdr.GetInt32(11);
              Player newPlayer = new Player(playerName, playerHPTotal, playerHPRemaining, playerAgility, playerIntelligence, playerStrength, playerLuck, playerId);
              newPlayer.SetAllegience(playerAllegience);
              newPlayer.SetX(playerXPos);
              newPlayer.SetY(playerYPos);

              allEnemies.Add(newPlayer);
          }
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return allEnemies;
        }

        public void Move(int dir)
        {
          int x = _xPos;
          int y = _yPos;

          if (dir == 37) { x -= 1; }
          else if (dir == 38) { y -= 1; }
          else if (dir == 39) { x += 1; }
          else if (dir == 40) { y += 1; }

          bool canMove = true;
          foreach (Player other in Player.GetAll())
          {
            if (other._xPos == x && other._yPos == y)
            {
              canMove = false;
            }
          }
          if (canMove)
          {
            _xPos = x;
            _yPos = y;
          }

          if (_xPos < 0) { _xPos = 0; }
          if (_yPos < 0) { _yPos = 0; }
          if (_xPos >= 21) { _xPos = 20; }
          if (_yPos >= 11) { _yPos = 10; }
        }

        public int fightDetect()
        {
          List<Player> allEnemies = Player.GetEnemies();
          foreach (Player enemy in allEnemies)
            {
              if ((Math.Abs(this._xPos - enemy._xPos) < 2) && (Math.Abs(this._yPos - enemy._yPos) < 2))
              {
                return enemy._id;
              }
            }
          return -1;
        }
    }
}
