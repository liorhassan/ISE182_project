using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass()]
    public class UnitTest5
    {
        //Chatroom chatroom = new Chatroom();
        User userOne = new User("userOne", "24");
        User userTwo = new User("userTwo", "5");
        User userThree = new User("userThree", "5");
        User userFour = new User("userFour", "8");
        Chatroom chatroom = new Chatroom();
        [TestMethod()]
        public void TestRegister5()
        {         
            chatroom.RestartChatroom();
            chatroom.SetLog("1");
            //chatroom.CheckLog();
            //register.Start();
            Boolean firstR = chatroom.Register(userOne.Nickname, userOne.GroupID);
            Assert.AreEqual(firstR, true);
            Boolean secondR = chatroom.Register(userOne.Nickname, userOne.GroupID);
            Assert.AreEqual(secondR, false);
            //Console.WriteLine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName));
            //Console.WriteLine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName);
            //chatroom.Dispose();
            //chatroom.DeleteLog2();
            //chatroom.Flush();
            
        }

        [TestMethod()]
        public void TestLogin5()
        {
            chatroom.RestartChatroom();
            chatroom.SetLog("2");
            //chatroom.CheckLog();
            //login.Start();
            //Console.WriteLine("after");
            chatroom.Register(userTwo.Nickname, userTwo.GroupID);
            Boolean firstL = chatroom.Login(userTwo.Nickname, userTwo.GroupID);
            Assert.AreEqual(firstL, true);
            Boolean secondL = chatroom.Login("otheruser", "4");
            Assert.AreEqual(secondL, false);
            //chatroom.Dispose();
            //chatroom.DeleteLog2();
            //chatroom.Flush();
            
        }
    }
}
