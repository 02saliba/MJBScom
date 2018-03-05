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
            Player newPlayer = new Player("Cameron", 20, 20, 5, 5, 5, 5);
            string testName = "Cameron";
            int testHPTotal = 20;
            int testHPRemaining = 20;
            int testAgility = 5;
            int testIntel = 5;
            int testStrength = 5;
            int testLuck = 5;

            //act
            string resultName = newPlayer.GetName();
            int resultHPTotal = newPlayer.GetHPTotal();
            int resultHPRemaining = newPlayer.GetHPRemaining();
            int resultAgility = newPlayer.GetAgility();
            int resultIntel = newPlayer.GetIntelligence();
            int resultStrength = newPlayer.GetStrength();
            int resultLuck = newPlayer.GetLuck();

            //assert
            Assert.AreEqual(testName, resultName);
            Assert.AreEqual(testHPTotal, resultHPTotal);
            Assert.AreEqual(testHPRemaining, resultHPRemaining);
            Assert.AreEqual(testAgility, resultAgility);
            Assert.AreEqual(testIntel, resultIntel);
            Assert.AreEqual(testStrength, resultStrength);
            Assert.AreEqual(testLuck, resultLuck);
        }

        [TestMethod]
        public void GetAll_DatabaseIsEmpty_0()
        {
            //arrange
            Player newPlayer = new Player("Cameron", 20, 20, 5, 5, 5, 5);

            //act
            List<Player> result = Player.GetAll();

            //assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfSame_Client()
        {
            //arrange, act
            Player newPlayer1 = new Player("Cameron", 20, 20, 5, 5, 5, 5);
            Player newPlayer2 = new Player("Cameron", 20, 20, 5, 5, 5, 5);

            //assert
            Assert.AreEqual(newPlayer1, newPlayer2);
        }

        [TestMethod]
        public void Save_SavesToDatabase_ClientList()
        {
            //arrange
            Player newPlayer = new Player("Cameron", 20, 20, 5, 5, 5, 5);

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
            Player newPlayer = new Player("Cameron", 20, 20, 5, 5, 5, 5);

            //act
            newPlayer.Save();
            Player savedPlayer = Player.GetAll()[0];
            int result = savedPlayer.GetId();
            int testId = newPlayer.GetId();

            //assign
            Assert.AreEqual(result, testId);
        }

        [TestMethod]
        public void Delete_DeletesFromDB_Void()
        {
            //arrange
            Player newPlayer1 = new Player("Cameron", 20, 20, 5, 5, 5, 5);
            Player newPlayer2 = new Player("Cameron", 20, 20, 5, 5, 5, 5);
            newPlayer1.Save();
            List<Player> originalList = Player.GetAll();
            newPlayer2.Save();

            //act
            newPlayer2.Delete();
            List<Player> newList = Player.GetAll();

            //assert
            CollectionAssert.AreEqual(newList, originalList);
        }

        [TestMethod]
        public void Setters_CheckSetOnObjects_SetValue()
        {
            //arrange
            Player newPlayer = new Player("Cameron", 20, 20, 5, 5, 5, 5);
            string newName = "Bill";
            int newHPTotal = 25;
            int newHPRemaining = 24;
            int newAgility = 1;
            int newIntelligence = 3;
            int newStrength = 3;
            int newLuck = 3;

            //act
            newPlayer.SetName(newName);
            newPlayer.SetHPTotal(newHPTotal);
            newPlayer.SetHPRemaining(newHPRemaining);
            newPlayer.SetAgility(newAgility);
            newPlayer.SetIntelligence(newIntelligence);
            newPlayer.SetStrength(newStrength);
            newPlayer.SetLuck(newLuck);

            //assert
            Assert.AreEqual(newName, newPlayer.GetName());
            Assert.AreEqual(newHPTotal, newPlayer.GetHPTotal());
            Assert.AreEqual(newHPRemaining, newPlayer.GetHPRemaining());
            Assert.AreEqual(newAgility, newPlayer.GetAgility());
            Assert.AreEqual(newIntelligence, newPlayer.GetIntelligence());
            Assert.AreEqual(newStrength, newPlayer.GetStrength());
            Assert.AreEqual(newLuck, newPlayer.GetLuck());
        }

        [TestMethod]
        public void Update_UpdatesPlayerinDB_UpdatedValues()
        {
            //arrange
            Player newPlayer = new Player("Cameron", 20, 20, 5, 5, 5, 5);
            newPlayer.Save();
            newPlayer.SetName("Bill");

            //act
            newPlayer.Update();
            Player checkPlayer = Player.Find(newPlayer.GetId());

            //assert
            Assert.AreEqual(newPlayer.GetName(), checkPlayer.GetName());
        }

        [TestMethod]
        public void Find_FindsPlayer_Player()
        {
            //arrange
           Player controlPlayer = new Player("Cameron", 20, 20, 5, 5, 5, 5);
           controlPlayer.Save();

           //act
           Player foundPlayer = Player.Find(controlPlayer.GetId());

           //Assert
           Assert.AreEqual(foundPlayer, controlPlayer);
        }



    }
}
