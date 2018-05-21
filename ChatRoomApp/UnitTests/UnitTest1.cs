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
        //Chatroom chatroom = new Chatroom();
        User userOne = new User("userOne", "24");
        User userTwo = new User("userTwo", "5");
        User userThree = new User("userThree", "5");
        User userFour = new User("userFour", "8");
        [TestMethod()]
        public void TestRegister()
        {
            TestChatroom register = new TestChatroom();
            register.RestartChatroom();
            Boolean firstR = register.Register(userOne.Nickname, userOne.GroupID);
            Assert.AreEqual(firstR, true);
            Boolean secondR = register.Register(userOne.Nickname, userOne.GroupID);
            Assert.AreEqual(secondR, false);
        }

        [TestMethod()]
        public void TestLogin()
        {
            TestChatroom login = new TestChatroom();
            login.RestartChatroom();
            Console.WriteLine("after");
            login.Register(userTwo.Nickname, userTwo.GroupID);
            Boolean firstL = login.Login(userTwo.Nickname, userTwo.GroupID);
            Assert.AreEqual(firstL, true);
            Boolean secondL = login.Login("otheruser", "4");
            Assert.AreEqual(secondL, false);
        }

        [TestMethod()]
        public void TestLogout()
        {
            TestChatroom logout = new TestChatroom();
            logout.RestartChatroom();
            logout.Register(userOne.Nickname, userOne.GroupID);
            logout.Login(userOne.Nickname, userOne.GroupID);
            Boolean firstlogout = logout.Logout();
            Assert.AreEqual(firstlogout, true);
            Boolean secondlogout = logout.Logout();
            Assert.AreEqual(secondlogout, false);
        }

        [TestMethod()]
        public void TestMessage()
        {
            TestChatroom mes = new TestChatroom();
            mes.RestartChatroom();
            mes.Register(userOne.Nickname, userOne.GroupID);
            mes.Login(userOne.Nickname, userOne.GroupID);
            int first = mes.WriteMessage("message");
            Assert.AreEqual(first, 1);
            String s = new string('a', 151);
            int second = mes.WriteMessage(s);
            Assert.AreEqual(second, -1);
        }

        [TestMethod()]
        public void TestSortByName()
        {
            TestChatroom sortbyname = new TestChatroom();
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
            sortbyname.RestartChatroom();

            sortbyname.Register(userThree.Nickname, userThree.GroupID);
            sortbyname.Login(userThree.Nickname, userThree.GroupID);
            sortbyname.WriteMessage(third);
            sortbyname.Logout();

            sortbyname.Register(userFour.Nickname, userFour.GroupID);
            sortbyname.Login(userFour.Nickname, userFour.GroupID);
            sortbyname.WriteMessage(fourth);
            sortbyname.Logout();

            sortbyname.Register(userOne.Nickname, userOne.GroupID);
            sortbyname.Login(userOne.Nickname, userOne.GroupID);
            sortbyname.WriteMessage(first);
            sortbyname.Logout();

            sortbyname.Register(userTwo.Nickname, userTwo.GroupID);
            sortbyname.Login(userTwo.Nickname, userTwo.GroupID);
            sortbyname.WriteMessage(second);

            sortbyname.SetFilterAndSort(1, 0, true, "", "");
            List<String> messages = sortbyname.GetAllMessages();
            int i = 0;
            foreach (String mess in messages)
            {
                Assert.AreEqual(mess.Contains(test[i]), true);
                i++;
            }

            sortbyname.SetFilterAndSort(1, 0, false, "", "");
            messages = sortbyname.GetAllMessages();
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
            TestChatroom sortbyall = new TestChatroom();
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
            sortbyall.RestartChatroom();

            sortbyall.Register(userThree.Nickname, userThree.GroupID);
            sortbyall.Login(userThree.Nickname, userThree.GroupID);
            sortbyall.WriteMessage(Three_first);
            sortbyall.Logout();
            System.Threading.Thread.Sleep(2000);

            sortbyall.Register(userFour.Nickname, userFour.GroupID);
            sortbyall.Login(userFour.Nickname, userFour.GroupID);
            sortbyall.WriteMessage(Four_first);
            sortbyall.Logout();
            System.Threading.Thread.Sleep(2000);

            sortbyall.Register(userOne.Nickname, userOne.GroupID);
            sortbyall.Login(userOne.Nickname, userOne.GroupID);
            sortbyall.WriteMessage(One_first);
            sortbyall.Logout();
            System.Threading.Thread.Sleep(2000);

            sortbyall.Register(userTwo.Nickname, userTwo.GroupID);
            sortbyall.Login(userTwo.Nickname, userTwo.GroupID);
            sortbyall.WriteMessage(Two_first);
            sortbyall.Logout();
            System.Threading.Thread.Sleep(2000);

            sortbyall.Register(userThree.Nickname, userThree.GroupID);
            sortbyall.Login(userThree.Nickname, userThree.GroupID);
            sortbyall.WriteMessage(Three_second);
            sortbyall.Logout();
            System.Threading.Thread.Sleep(2000);

            sortbyall.Register(userFour.Nickname, userFour.GroupID);
            sortbyall.Login(userFour.Nickname, userFour.GroupID);
            sortbyall.WriteMessage(Four_second);
            sortbyall.Logout();
            System.Threading.Thread.Sleep(2000);

            sortbyall.Register(userOne.Nickname, userOne.GroupID);
            sortbyall.Login(userOne.Nickname, userOne.GroupID);
            sortbyall.WriteMessage(One_second);
            sortbyall.Logout();
            System.Threading.Thread.Sleep(2000);

            sortbyall.Register(userTwo.Nickname, userTwo.GroupID);
            sortbyall.Login(userTwo.Nickname, userTwo.GroupID);
            sortbyall.WriteMessage(Two_second);

            sortbyall.SetFilterAndSort(2, 0, true, "", "");
            List<String> messagesAsc = sortbyall.GetAllMessages();
            int i = 0;
            foreach (String mess in messagesAsc)
            {
                Assert.AreEqual(mess.Contains(test[i]), true);
                i++;
            }

            sortbyall.SetFilterAndSort(2, 0, false, "", "");
            List<String> messagesDes = sortbyall.GetAllMessages();
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
            TestChatroom filter = new TestChatroom();
            String first = "first message";
            String second = "second message";
            String third = "third message";
            List<String> test = new List<String>(3)
            {
                first,
                second,
                third
            };
            filter.RestartChatroom();
            filter.Register(userOne.Nickname, userOne.GroupID);
            filter.Login(userOne.Nickname, userOne.GroupID);
            filter.WriteMessage("my first message");
            filter.WriteMessage("group 24 is the best");
            filter.WriteMessage("100 final grade");
            filter.Logout();

            filter.Register(userTwo.Nickname, userTwo.GroupID);
            filter.Login(userTwo.Nickname, userTwo.GroupID);
            filter.WriteMessage("second message");
            filter.WriteMessage("my message is not important");
            filter.Logout();

            filter.Register(userThree.Nickname, userThree.GroupID);
            filter.Login(userThree.Nickname, userThree.GroupID);
            filter.WriteMessage("third message");
            
            filter.SetFilterAndSort(0, 2, true, userOne.Nickname, "24");
            List<String> messages = filter.GetAllMessages();
            int i = 0;
            foreach (String mess in messages)
            {
                Assert.AreEqual(mess, test[i]);
                i++;
            }

            filter.SetFilterAndSort(0, 2, true, userOne.Nickname, "24");
            messages = filter.GetAllMessages();
            i = 3;
            foreach (String mess in messages)
            {
                Assert.AreEqual(mess, test[i]);
                i--;
            }
        }
    }
}
