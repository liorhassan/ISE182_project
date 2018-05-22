using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace UnitTests
{
    // RestartChatroom clears the dictionaries so there will be no data inside
    [TestClass()]
    public class UnitTest1
    {
        // create users       
        User userOne = new User("userOne", "24");
        User userTwo = new User("userTwo", "5");
        User userThree = new User("userThree", "5");
        User userFour = new User("userFour", "8");
        [TestMethod()]
        
        public void TestRegister()
        {
            TestChatroom register = new TestChatroom();
            register.RestartChatroom();
            // register the user for the first time
            Boolean firstR = register.Register(userOne.Nickname, userOne.GroupID);
            // return true because user didn't exist in the chatroom before
            Assert.AreEqual(firstR, true);
            // register the same user again
            Boolean secondR = register.Register(userOne.Nickname, userOne.GroupID);
            // returns false because he is already registered 
            Assert.AreEqual(secondR, false);
        }

        [TestMethod()]
        public void TestLogin()
        {
            TestChatroom login = new TestChatroom();
            login.RestartChatroom();
            // register user
            login.Register(userTwo.Nickname, userTwo.GroupID);
            // login an existing user
            Boolean firstL = login.Login(userTwo.Nickname, userTwo.GroupID);
            // returns true because he exists
            Assert.AreEqual(firstL, true);
            // login a nonexistent user
            Boolean secondL = login.Login("otheruser", "4");
            // returns false because he doesn't exist
            Assert.AreEqual(secondL, false);
        }

        [TestMethod()]
        public void TestLogout()
        {
            TestChatroom logout = new TestChatroom();
            logout.RestartChatroom();
            // register user
            logout.Register(userOne.Nickname, userOne.GroupID);
            // login user
            logout.Login(userOne.Nickname, userOne.GroupID);
            // logout user
            Boolean firstlogout = logout.Logout();
            // returns true because a user was loggenin before
            Assert.AreEqual(firstlogout, true);
            // logout again
            Boolean secondlogout = logout.Logout();
            // returns false because no user was loggedin
            Assert.AreEqual(secondlogout, false);
        }

        [TestMethod()]
        public void TestMessage()
        {
            TestChatroom mes = new TestChatroom();
            mes.RestartChatroom();
            // register user
            mes.Register(userOne.Nickname, userOne.GroupID);
            // login user
            mes.Login(userOne.Nickname, userOne.GroupID);
            // write a message
            int first = mes.WriteMessage("message");
            // returns 1 because message is legal
            Assert.AreEqual(first, 1);
            // create illegal message
            String s = new string('a', 151);
            // write illegal message
            int second = mes.WriteMessage(s);
            // retruns 1 because it's illegal and wasn't written
            Assert.AreEqual(second, -1);
        }

        [TestMethod()]
        public void TestSortByName()
        {
            TestChatroom sortbyname = new TestChatroom();
            sortbyname.RestartChatroom();
            // strings of the 4 messages
            String first = "message of userOne";
            String second = "message of userTwo";
            String third = "message of userThree";
            String fourth = "message of userFour";
            // order them in the order they should be, alphabetically by username
            List<String> test = new List<String>(4)
            {
                fourth,
                first,
                third,
                second  
            };
            // register user
            // login user
            // write its' message
            // logout user excect the last one
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

            // set sort ascending
            sortbyname.SetFilterAndSort(1, 0, true, "", "");
            // get the sorted messages ascending
            List<String> messages = sortbyname.GetAllMessages();
            // initialize variable for iteration
            int i = 0;
            // go through the messages and check that each message 
            // contains the body of the message written in the beginning
            // in the correct order
            foreach (String mess in messages)
            {
                Assert.AreEqual(mess.Contains(test[i]), true);
                i++;
            }

            // set sort descending
            sortbyname.SetFilterAndSort(1, 0, false, "", "");
            // get the sorted messages descending
            messages = sortbyname.GetAllMessages();
            // initialize variable for iteration
            i = 3;
            // go through the messages and check that each message 
            // contains the body of the message written in the beginning
            // in the correct order
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
            sortbyall.RestartChatroom();
            // strings of the 8 messages
            String One_first = "first message of userOne";
            String One_second = "second message of userOne";
            String Two_first = "first message of userTwo";
            String Two_second = "second message of userTwo";
            String Three_first = "first message of userThree";
            String Three_second = "second message of userThree";
            String Four_first = "first message of userFour";
            String Four_second = "second message of userFour";
            // order them in the order they should be, 
            // first by group id, then username, then timestamp
            List<String> test = new List<String>(8)
            {
                Three_first, Three_second, Two_first, Two_second,
                Four_first, Four_second, One_first, One_second
                 
            };
            // register user
            // login user
            // write its' message
            // logout user except the last 
            // wait 2 seconds to make sure the time per message is different
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

            // set sort ascending
            sortbyall.SetFilterAndSort(2, 0, true, "", "");
            // get the sorted messages ascending
            List<String> messagesAsc = sortbyall.GetAllMessages();
            // initialize variable for iteration
            int i = 0;
            // go through the messages and check that each message 
            // contains the body of the message written in the beginning
            // in the correct order
            foreach (String mess in messagesAsc)
            {
                Assert.AreEqual(mess.Contains(test[i]), true);
                i++;
            }

            // set sort descending
            sortbyall.SetFilterAndSort(2, 0, false, "", "");
            // get the sorted messages descending
            List<String> messagesDes = sortbyall.GetAllMessages();
            // initialize variable for iteration
            i = 7;
            // go through the messages and check that each message 
            // contains the body of the message written in the beginning
            // in the correct order
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
            filter.RestartChatroom();
            // strings of the 3 messages by userOne
            String first = "my first message";
            String second = "group 24 is the best";
            String third = "100 final grade";
            // order them in the order they sould be, by timestamp 
            List<String> test = new List<String>(3)
            {
                first,
                second,
                third
            };

            // register user
            // login user
            // write a number of messages
            // logout expect the last one
            // wait 2 seconds to make sure the time per message is different
            filter.Register(userOne.Nickname, userOne.GroupID);
            filter.Login(userOne.Nickname, userOne.GroupID);
            filter.WriteMessage(first);
            System.Threading.Thread.Sleep(2000);
            filter.WriteMessage(second);
            System.Threading.Thread.Sleep(2000);
            filter.WriteMessage(third);
            filter.Logout();

            filter.Register(userTwo.Nickname, userTwo.GroupID);
            filter.Login(userTwo.Nickname, userTwo.GroupID);
            filter.WriteMessage("message of second");
            filter.WriteMessage("my message is not important");
            filter.Logout();

            filter.Register(userThree.Nickname, userThree.GroupID);
            filter.Login(userThree.Nickname, userThree.GroupID);
            filter.WriteMessage("message of third");

            // set sort ascending
            filter.SetFilterAndSort(0, 2, true, userOne.GroupID, userOne.Nickname);
            // get the sorted messages ascending
            List<String> messages = filter.GetAllMessages();
            // initialize variable for iteration
            int i = 0;
            // go through the messages and check that each message 
            // contains the body of the message written in the beginning
            // in the correct order
            foreach (String mess in messages)
            {
                Assert.AreEqual(mess.Contains(test[i]), true);
                i++;
            }

            // set sort descending
            filter.SetFilterAndSort(0, 2, false, userOne.GroupID, userOne.Nickname);
            // get the sorted messages descending
            messages = filter.GetAllMessages();
            // initialize variable for iteration
            i = 2;
            // go through the messages and check that each message 
            // contains the body of the message written in the beginning
            // in the correct order
            foreach (String mess in messages)
            {
                Assert.AreEqual(mess.Contains(test[i]), true);             
                i--;
            }
        }
    }
}
