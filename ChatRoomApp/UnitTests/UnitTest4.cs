using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass()]
    public class UnitTest4
    {
        //Chatroom chatroom = new Chatroom();
        User userOne = new User("userOne", "24");
        User userTwo = new User("userTwo", "5");
        User userThree = new User("userThree", "5");
        User userFour = new User("userFour", "8");
        [TestMethod()]
        public void TestRegister4()
        {
            Chatroom register = new Chatroom("1");
            register.RestartChatroom();
            //register.SetLog("1");
            //register.CheckLog();
            //register.Start();
            Boolean firstR = register.Register(userOne.Nickname, userOne.GroupID);
            register.CheckLog();
            Assert.AreEqual(firstR, true);
            Boolean secondR = register.Register(userOne.Nickname, userOne.GroupID);
            Assert.AreEqual(secondR, false);
            //Console.WriteLine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName));
            //Console.WriteLine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName);
            register.Dispose();
            register.DeleteLog("1");
        }

        [TestMethod()]
        public void TestLogin4()
        {
            Chatroom login = new Chatroom("2");
            login.RestartChatroom();
            //login.SetLog("2");
            //login.CheckLog();
            //login.Start();
            //Console.WriteLine("after");
            login.Register(userTwo.Nickname, userTwo.GroupID);
            Boolean firstL = login.Login(userTwo.Nickname, userTwo.GroupID);
            Assert.AreEqual(firstL, true);
            Boolean secondL = login.Login("otheruser", "4");
            Assert.AreEqual(secondL, false);
            login.Dispose();
            login.DeleteLog("2");
        }


        [TestMethod()]
        public void TestLogout4()
        {
            Chatroom logout = new Chatroom();
            logout.RestartChatroom();
            logout.Start();
            logout.Register(userOne.Nickname, userOne.GroupID);
            logout.Login(userOne.Nickname, userOne.GroupID);
            Boolean firstlogout = logout.Logout();
            Assert.AreEqual(firstlogout, true);
            Boolean secondlogout = logout.Logout();
            Assert.AreEqual(secondlogout, false);
            //chatroomThree.Logout();
            //chatroom.RestartChatroom();
            logout.exit();
        }
    }
}
