using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MJBScom.Models;
using System;

namespace MJBScom.Tests
{
    [TestClass]
    public class TeamTests : IDisposable
    {
        public void Dispose()
        {
            Player.DeleteAll();
            Team.DeleteAll();
        }

        public TeamTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mjbs_test";
        }

        [TestMethod]
        public void Save_SavesTeamToDatabase_Void()
        {
          Team newTeam = new Team("Cam's Team", 0, true);
          newTeam.Save();

          List<Team> testList = new List<Team>{newTeam};
          List<Team> result = Team.GetAll();

          Assert.AreEqual(testList.Count, result.Count);
          CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_SavesTeamIdToDatabase_Void()
        {
          Team newTeam = new Team("Cam's Team", 0, true);
          newTeam.Save();

          Team result = Team.Find(newTeam.GetId());

          Assert.AreEqual(newTeam, result);
        }
    }
}
