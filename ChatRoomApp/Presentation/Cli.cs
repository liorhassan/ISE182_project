using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using System.Threading;
using System.Reflection;

namespace Presentation
{
    //a class for the cli window and all its states
    public class Cli
    {
        //fields for the chatroom and menu and a boolean field represanting wheter the app is running
        private Boolean running;
        private Chatroom myChatRoom;
        private ChatroomMenu menu;

        //class constructor
        public Cli()
        {
            myChatRoom = new Chatroom();
            menu = myChatRoom.ChatroomMenu;
            running = true;
        }

        //a function for showing the menu as long as the prooggram is running
        //reads a key from the user and usess the Chatroom menu to get the function to run
        //usses reflection to run the function given or askes for a diffrent key if an empty function is returnd
        public void showMenu()
        {
            
            while(running)
            {
                Console.Clear();
                Console.WriteLine(menu.ToString());
                char key=GetKey();
                string function= menu.getFunction(key);
                while(function == "")
                {
                    Console.WriteLine("key not supported, try again");
                    key = GetKey();
                    function = menu.getFunction(key);
                }
                Type thisType = this.GetType();
                MethodInfo theMethod = thisType.GetMethod(function);
                theMethod.Invoke(this,null );
            }

        }

        //private method used by the showMenu method for getting a single char from the window
        private char GetKey()
        {
            String input = Console.ReadLine();
            while (input.Length!=1)
            {
                Console.WriteLine("Please enter one key!");
                input = Console.ReadLine();
            }
            return input[0];
        }

        //a function for getting new user credentials and send them to the chat room for registration.
        //notifies if the nickname is taken
        public void registration()
        {
            Console.Clear();
            Console.WriteLine("Enter your nickname");
            String nickname = Console.ReadLine();
            while ((nickname!="")&&(!myChatRoom.Register(nickname)))
            {
                Console.WriteLine("Enter another nickname, or press ENTER to go back to the menu");
                nickname = Console.ReadLine();
            }
            if (nickname != "")
            {
                Console.WriteLine("user "+nickname+" created succesfuly♥");
                Console.WriteLine("Press ENTER to go back to the menu");
                Console.ReadLine();
            }

        }

        //a function for login or logout. tries to log out and if it fails, askes for nickname for login
        //notifies if nickname is non-exist
        public void loginLogout()
        {
            Console.Clear();
            if (myChatRoom.Logout())
            {
                Console.WriteLine("Your user logout");
                Console.WriteLine("Press ENTER to go back to the menu");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Enter your nickname");
                String nickname = Console.ReadLine();
                while ((nickname!="")&&(!myChatRoom.Login(nickname)))
                {
                    Console.WriteLine("User non-exist, try again or press ENTER and go back to the menu");
                    nickname = Console.ReadLine();

                }

                if (nickname != "")
                {
                    Console.WriteLine("Your user login");
                    Console.WriteLine("Press ENTER to go back to the menu");
                    Console.ReadLine();
                }
            }

        }

        //askes the chatroom to retrive 10 messages from the server
        //prints how many new messages were recieved
        public void retrive10Messages()
        {
            Console.Clear();
            int numOfMsg = myChatRoom.Retrieve10Messages();
            Console.WriteLine("Received " + numOfMsg + " messages from the server");
            Console.WriteLine("Press ENTER to go back to the menu");
            Console.ReadLine();
        }

        //gets a list of the 20 newest messages from the chatroom and prints them
        public void display20Messages()
        {
            Console.Clear();
            List<Message> messages = myChatRoom.Retrieve20Messages();
            foreach(Message msg in messages)
            {
                Console.WriteLine(msg.ToString());
            }
            Console.WriteLine("Press ENTER to go back to the menu");
            Console.ReadLine();
        }

        //askes for a nickname and group_id and askes the chatroom for all the messages by this user
        //prints all the messages given, notifies if no such messages were returned
        public void displayAllByUser()
        {
            Console.Clear();
            Console.WriteLine("Enter username");
            string username = Console.ReadLine();
            Console.WriteLine("Enter groupID");
            string groupID = Console.ReadLine();
            List<Message> messages = myChatRoom.RetrieveAllByUser(username, groupID);
            if (messages.Count == 0)
            {
                Console.WriteLine("no messages by this user");
            }
            foreach (Message msg in messages)
            {
                Console.WriteLine(msg.ToString());
            }
            Console.WriteLine("Press ENTER to go back to the menu");
            Console.ReadLine();


        }

        //reads the new message from the user and askes the chatroom to send it
        //notifies if failes
        public void writeMessage()
        {
            Console.Clear();
            Console.WriteLine("Write your message");
            String msg = Console.ReadLine();
            while((msg!="")&& !myChatRoom.WriteMessage(msg))
            {
                Console.WriteLine("Can't send the message, try again or press ENTER to go back to the menu");
                msg = Console.ReadLine();
            }
            if (msg != "")
            {
                Console.WriteLine("Your message was sent succesfuly");
                Console.WriteLine("Press ENTER to go back to the menu");
                Console.ReadLine();
            }

        }

        //closes the chatroom
        public void exit()
        {
            myChatRoom.exit();
            running = false;
        }


    }
}
