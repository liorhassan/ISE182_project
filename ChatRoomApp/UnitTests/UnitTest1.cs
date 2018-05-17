using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRegister()
        {
            Chatroom chatroom = new Chatroom();
            User user = new User("user");
            Boolean firstR = chatroom.Register(user.Nickname);
            Assert.AreEqual(firstR, true);
            //Boolean secondR = chatroom.Register(user.Nickname);
           // Assert.AreEqual(secondR, false);
        }

        [TestMethod]
        public void TestLogin()
        {
            Chatroom chatroom = new Chatroom();
            User user = new User("user");
            chatroom.Register(user.Nickname);
            Boolean firstL = chatroom.Login("user");
            Assert.AreEqual(firstL, true);
            Boolean secondL = chatroom.Login("otheruser");
            Assert.AreEqual(secondL, false);
        }
    }
}
