using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass()]
    public class UnitTest3
    {
        
       
        //User userThree = new User("userThree");
        //User userFour = new User("userFour");
        [TestMethod()]
        public void TestRegister2()
        {
            Chatroom register = new Chatroom();
            User userOne = new User("userOne");
            register.RestartChatroom();  
            Boolean firstR = register.Register(userOne.Nickname);
            Assert.AreEqual(firstR, true);
            Boolean secondR = register.Register(userOne.Nickname);
            Assert.AreEqual(secondR, false);
            register.exit();
        }

        [TestMethod()]
        public void TestLogin2()
        {
            Chatroom login = new Chatroom();          
            login.RestartChatroom();
            login.Start();
            User userOne = new User("userOne");
            User userTwo = new User("userTwo");
            Console.WriteLine("after");
            login.Register(userTwo.Nickname);
            Boolean firstL = login.Login(userTwo.Nickname);
            Assert.AreEqual(firstL, true);
            Boolean secondL = login.Login("otheruser");
            Assert.AreEqual(secondL, false);
        }
    }
}
