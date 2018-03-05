using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MJBScom.Models;
using System;

namespace MJBScom.Tests
{
  [TestClass]
  public class ItemTests : IDisposable
  {
    public void Dispose()
    {
      Item.DeleteAll();
    }

    public ItemTests()
        {
          DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mjbs_test;";
        }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Item.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetDescription_FetchTheDescription_String()
    {
      //arrange
      string controlName = "itemX";
      string controlDesc = "powerful item";
      Item newItem = new Item("itemX", "powerful item");

      //act
      string testName = newItem.GetName();
      string testDesc = newItem.GetDescription();

      //assert
      Assert.AreEqual(controlName, testName);
      Assert.AreEqual(controlDesc, testDesc);
    }
  }
}
