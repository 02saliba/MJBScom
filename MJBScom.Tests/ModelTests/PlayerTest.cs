using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MJBScom.Models;
using System;

namespace MJBScom.Tests
{
    [TestClass]
    public class PlayerTests
    {
        public void Dispose()
        {
            Player.DeleteAll();
        }

        public PlayerTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mjbs_test";
        }

        [TestMethod]
        public void Getters_GetPlayerInfos_StringsInts()
        {
            //arrange
            Player newPlayer = new Player("Cameron", 5, 5, 5, 5);
            string testName = "Cameron";
            int testAgility = 5;
            int testIntel = 5;
            int testStrength = 5;
            int testLuck = 5;

            //act
            string resultName = newPlayer.GetName();
            int resultAgility = newPlayer.GetAgility();
            int resultIntel = newPlayer.GetIntelligence();
            int resultStrength = newPlayer.GetStrength();
            int resultLuck = newPlayer.GetLuck();

            //assert
            Assert.AreEqual(testName, resultName);
            Assert.AreEqual(testAgility, resultAgility);
            Assert.AreEqual(testIntel, resultIntel);
            Assert.AreEqual(testStrength, resultStrength);
            Assert.AreEqual(testLuck, resultLuck);
        }
    }
}
