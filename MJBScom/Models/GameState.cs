using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace MJBScom.Models
{
  public class GameState
  {
    private static int _playerTeamId;
    private static int _enemyTeamId;
    private static List<Team> _enemyTeams;

    public int GetPlayerTeamId() { return _playerTeamId; }
    public int GetEnemyTeamId() { return _enemyTeamId; }
    public List<Team> GetEnemyTeams() { return _enemyTeams; }
    public void setPlayerTeamId(int id) { _playerTeamId = id; }
    public void setEnemyTeamId(int id) { _enemyTeamId = id; }
    public void setEnemyTeams(List<Team> teams) { _enemyTeams = teams; }
  }
}
