using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MJBScom.Models;
using System;

namespace MJBScom.Tests
{
    [TestClass]
    public class PlayerTests : IDisposable
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

        [TestMethod]
        public void GetAll_DatabaseIsEmpty_0()
        {
            //arrange
            Player newPlayer = new Player("Cameron", 5, 5, 5, 5);

            //act
            List<Player> result = Player.GetAll();

            //assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfSame_Client()
        {
            //arrange, act
            Player newPlayer1 = new Player("Cameron", 5, 5, 5, 5);
            Player newPlayer2 = new Player("Cameron", 5, 5, 5, 5);

            //assert
            Assert.AreEqual(newPlayer1, newPlayer2);
        }

        [TestMethod]
        public void Save_SavesToDatabase_ClientList()
        {
            //arrange
            Player newPlayer = new Player("Cameron", 5, 5, 5, 5);

            //act
            newPlayer.Save();
            List<Player> result = Player.GetAll();
            List<Player> testList = new List<Player>{newPlayer};

            //assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
            //arrange
            Player newPlayer = new Player("Cameron", 5, 5, 5, 5);

            //act
            newPlayer.Save();
            Player savedPlayer = Player.GetAll()[0];
            int result = savedPlayer.GetId();
            int testId = newPlayer.GetId();

            //assign
            Assert.AreEqual(result, testId);
        }



    }
}
