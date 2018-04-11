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
    public class Cli
    {
        private Boolean running;
        private Chatroom myChatRoom;
        private ChatroomMenu menu;

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
                char key = Console.ReadLine().ToCharArray()[0];
                string function= menu.getFunction(key);
                while(function == "")
                {
                    Console.WriteLine("key not supported, try again");
                    key = Console.ReadLine().ToCharArray()[0];
                    function = menu.getFunction(key);
                }
                Type thisType = this.GetType();
                MethodInfo theMethod = thisType.GetMethod(function);
                theMethod.Invoke(this,null );
            }

        }

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

        public void retrive10Messages()
        {
            Console.Clear();
            int numOfMsg = myChatRoom.Retrieve10Messages();
            Console.WriteLine("Received " + numOfMsg + " messages from the server");
            Console.WriteLine("Press ENTER to go back to the menu");
            Console.ReadLine();
        }

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
                Console.WriteLine("Press ENTER to go back to the menu");
                Console.ReadLine();

            }
            foreach (Message msg in messages)
            {
                Console.WriteLine(msg.ToString());
            }
            Console.WriteLine("Press ENTER to go back to the menu");
            Console.ReadLine();


        }

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

        public void exit()
        {
            myChatRoom.exit();
            running = false;
        }


    }
}
