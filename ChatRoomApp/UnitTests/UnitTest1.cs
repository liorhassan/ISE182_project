using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
namespace UnitTests
{
    [TestClass()]
    public class UnitTest1
    {
        Chatroom chatroom = NewMethod();
        [TestMethod()]
        public void TestRegister()
        {
            chatroom.RestartChatroom();
            User user = new User("user1");
            Boolean firstR = chatroom.Register(user.Nickname);
            Assert.AreEqual(firstR, true);
            Boolean secondR = chatroom.Register(user.Nickname);
            Assert.AreEqual(secondR, false);
            chatroom.exit();
        }

        private static Chatroom NewMethod()
        {
            return new Chatroom();
        }

        [TestMethod()]
        public void TestLogin()
        {
            chatroom.RestartChatroom();
            //chatroomTwo.Start();
            //.RestartChatroom();
            User user = new User("user");
            chatroom.Register(user.Nickname);
            Boolean firstL = chatroom.Login("user");
            Assert.AreEqual(firstL, true);
            Boolean secondL = chatroom.Login("otheruser");
            Assert.AreEqual(secondL, false);
            chatroom.exit();
        }


        //[TestMethod()]
        //public void TestMessage()
        //{
        //    Chatroom chatroom = new Chatroom();
        //    chatroom.RestartChatroom();
        //    User user = new User("user");
        //    chatroom.Register(user.Nickname);
        //    chatroom.Login("user");
        //    int first = chatroom.WriteMessage("message");
        //    Assert.AreEqual(first, 1);
        //    String s = new string('a', 151);
        //    int second = chatroom.WriteMessage(s);
        //    Assert.AreEqual(second, -1);
        //}

        [TestMethod()]
        public void TestLogout()
        {
           // chatroom.RestartChatroom();
            User user = new User("user");
            chatroom.Register(user.Nickname);
            chatroom.Login("user");
            Boolean firstlogout = chatroom.Logout();
            Assert.AreEqual(firstlogout, true);
            Boolean secondlogout = chatroom.Logout();
            Assert.AreEqual(secondlogout, false);
            //chatroomThree.Logout();
            //chatroom.RestartChatroom();
            //chatroom.exit();
        }

        //[TestMethod()]
        //public void TestFilter()
        //{
        //    Chatroom chatroom = new Chatroom();
        //    chatroom.RestartChatroom();
        //    User userOne = new User("userOne");
        //    User userTwo = new User("userTwo");
        //    User userThree = new User("userThree");
        //    chatroom.Register(userOne.Nickname);
        //    chatroom.Register(userTwo.Nickname);
        //    chatroom.Register(userThree.Nickname);
        //    chatroom.Login("user");
        //    int first = chatroom.WriteMessage("message");
        //    Assert.AreEqual(first, 1);
        //    String s = new string('a', 151);
        //    int second = chatroom.WriteMessage(s);
        //    Assert.AreEqual(second, -1);
        //}
    }
}
