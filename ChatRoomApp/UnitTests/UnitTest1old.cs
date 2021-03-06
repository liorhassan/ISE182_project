using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass()]
    public class UnitTest1
    {
        Chatroom chatroom = new Chatroom();
        User userOne = new User("userOne", "24");
        User userTwo = new User("userTwo", "5");
        User userThree = new User("userThree", "5");
        User userFour = new User("userFour", "8");
        [TestMethod()]
        public void TestRegister()
        {
            chatroom.RestartChatroom();  
            Boolean firstR = chatroom.Register(userOne.Nickname, userOne.GroupID);
            Assert.AreEqual(firstR, true);
            Boolean secondR = chatroom.Register(userOne.Nickname, userOne.GroupID);
            Assert.AreEqual(secondR, false);
           // Environment.Exit(0);
        }

        [TestMethod()]
        public void TestLogin()
        {
            chatroom.RestartChatroom();
            Console.WriteLine("after");
            chatroom.Register(userTwo.Nickname, userTwo.GroupID);
            Boolean firstL = chatroom.Login(userTwo.Nickname, userTwo.GroupID);
            Assert.AreEqual(firstL, true);
            Boolean secondL = chatroom.Login("otheruser", "4");
            Assert.AreEqual(secondL, false);
            chatroom.exit();
        }


        [TestMethod()]
        public void TestMessage()
        {
            chatroom.RestartChatroom();
            chatroom.Register(userOne.Nickname, userOne.GroupID);
            chatroom.Login(userOne.Nickname, userOne.GroupID);
            int first = chatroom.WriteMessage("message");
            Assert.AreEqual(first, 1);
            String s = new string('a', 151);
            int second = chatroom.WriteMessage(s);
            Assert.AreEqual(second, -1);
        }

        [TestMethod()]
        public void TestLogout()
        {
            chatroom.RestartChatroom();
            chatroom.Register(userOne.Nickname, userOne.GroupID);
            chatroom.Login(userOne.Nickname, userOne.GroupID);
            Boolean firstlogout = chatroom.Logout();
            Assert.AreEqual(firstlogout, true);
            Boolean secondlogout = chatroom.Logout();
            Assert.AreEqual(secondlogout, false);
            //chatroomThree.Logout();
            //chatroom.RestartChatroom();
            //chatroom.exit();
        }

        [TestMethod()]
        public void TestSortByName()
        {
            String first = "message of userOne";
            String second = "message of userTwo";
            String third = "message of userThree";
            String fourth = "message of userFour";
            List<String> test = new List<String>(4)
            {
                fourth,
                first,
                third,
                second  
            };
            chatroom.RestartChatroom();

            chatroom.Register(userThree.Nickname, userThree.GroupID);
            chatroom.Login(userThree.Nickname, userThree.GroupID);
            chatroom.WriteMessage(third);
            chatroom.Logout();

            chatroom.Register(userFour.Nickname, userFour.GroupID);
            chatroom.Login(userFour.Nickname, userFour.GroupID);
            chatroom.WriteMessage(fourth);
            chatroom.Logout();

            chatroom.Register(userOne.Nickname, userOne.GroupID);
            chatroom.Login(userOne.Nickname, userOne.GroupID);
            chatroom.WriteMessage(first);           
            chatroom.Logout();

            chatroom.Register(userTwo.Nickname, userTwo.GroupID);
            chatroom.Login(userTwo.Nickname, userTwo.GroupID);
            chatroom.WriteMessage(second);

            chatroom.SetFilterAndSort(1, 0, true, "", "");
            List<String> messages = chatroom.GetAllMessages();
            int i = 0;
            foreach (String mess in messages)
            {
                Assert.AreEqual(mess.Contains(test[i]), true);
                i++;
            }

            chatroom.SetFilterAndSort(1, 0, false, "", "");
            messages = chatroom.GetAllMessages();
            i = 3;
            foreach (String mess in messages)
            {
                Assert.AreEqual(mess.Contains(test[i]), true);
                i--;
            }

        }

        [TestMethod()]
        public void TestSortByAll()
        {
            String One_first = "first message of userOne";
            String One_second = "second message of userOne";
            String Two_first = "first message of userTwo";
            String Two_second = "second message of userTwo";
            String Three_first = "first message of userThree";
            String Three_second = "second message of userThree";
            String Four_first = "first message of userFour";
            String Four_second = "second message of userFour";
            List<String> test = new List<String>(8)
            {
                Three_first, Three_second, Two_first, Two_second,
                Four_first, Four_second, One_first, One_second
                 
            };
            chatroom.RestartChatroom();

            chatroom.Register(userThree.Nickname, userThree.GroupID);
            chatroom.Login(userThree.Nickname, userThree.GroupID);
            chatroom.WriteMessage(Three_first);
            chatroom.Logout();
            System.Threading.Thread.Sleep(2000);

            chatroom.Register(userFour.Nickname, userFour.GroupID);
            chatroom.Login(userFour.Nickname, userFour.GroupID);
            chatroom.WriteMessage(Four_first);
            chatroom.Logout();
            System.Threading.Thread.Sleep(2000);

            chatroom.Register(userOne.Nickname, userOne.GroupID);
            chatroom.Login(userOne.Nickname, userOne.GroupID);
            chatroom.WriteMessage(One_first);
            chatroom.Logout();
            System.Threading.Thread.Sleep(2000);

            chatroom.Register(userTwo.Nickname, userTwo.GroupID);
            chatroom.Login(userTwo.Nickname, userTwo.GroupID);
            chatroom.WriteMessage(Two_first);
            chatroom.Logout();
            System.Threading.Thread.Sleep(2000);

            chatroom.Register(userThree.Nickname, userThree.GroupID);
            chatroom.Login(userThree.Nickname, userThree.GroupID);
            chatroom.WriteMessage(Three_second);
            chatroom.Logout();
            System.Threading.Thread.Sleep(2000);

            chatroom.Register(userFour.Nickname, userFour.GroupID);
            chatroom.Login(userFour.Nickname, userFour.GroupID);
            chatroom.WriteMessage(Four_second);
            chatroom.Logout();
            System.Threading.Thread.Sleep(2000);

            chatroom.Register(userOne.Nickname, userOne.GroupID);
            chatroom.Login(userOne.Nickname, userOne.GroupID);
            chatroom.WriteMessage(One_second);
            chatroom.Logout();
            System.Threading.Thread.Sleep(2000);

            chatroom.Register(userTwo.Nickname, userTwo.GroupID);
            chatroom.Login(userTwo.Nickname, userTwo.GroupID);
            chatroom.WriteMessage(Two_second);

            chatroom.SetFilterAndSort(2, 0, true, "", "");
            List<String> messagesAsc = chatroom.GetAllMessages();
            int i = 0;
            foreach (String mess in messagesAsc)
            {
                Assert.AreEqual(mess.Contains(test[i]), true);
                i++;
            }

            chatroom.SetFilterAndSort(2, 0, false, "", "");
            List<String> messagesDes = chatroom.GetAllMessages();
            i = 7;
            foreach (String mess in messagesDes)
            {
                Assert.AreEqual(mess.Contains(test[i]), true);
                i--;
            }

        }


        [TestMethod()]
        public void TestFilter()
        {
            String first = "first message";
            String second = "second message";
            String third = "third message";
            List<String> test = new List<String>(3)
            {
                first,
                second,
                third
            };
            chatroom.RestartChatroom();
            chatroom.Register(userOne.Nickname, userOne.GroupID);
            chatroom.Login(userOne.Nickname, userOne.GroupID);
            chatroom.WriteMessage("my first message");
            chatroom.WriteMessage("group 24 is the best");
            chatroom.WriteMessage("100 final grade");
            chatroom.Logout();

            chatroom.Register(userTwo.Nickname, userTwo.GroupID);
            chatroom.Login(userTwo.Nickname, userTwo.GroupID);
            chatroom.WriteMessage("second message");
            chatroom.WriteMessage("my message is not important");
            chatroom.Logout();

            chatroom.Register(userThree.Nickname, userThree.GroupID);
            chatroom.Login(userThree.Nickname, userThree.GroupID);
            chatroom.WriteMessage("third message");
            
            chatroom.SetFilterAndSort(0, 2, true, userOne.Nickname, "24");
            List<String> messages = chatroom.GetAllMessages();
            int i = 0;
            foreach (String mess in messages)
            {
                Assert.AreEqual(mess, test[i]);
                i++;
            }

            chatroom.SetFilterAndSort(0, 2, true, userOne.Nickname, "24");
            messages = chatroom.GetAllMessages();
            i = 3;
            foreach (String mess in messages)
            {
                Assert.AreEqual(mess, test[i]);
                i--;
            }
        }
    }
}
