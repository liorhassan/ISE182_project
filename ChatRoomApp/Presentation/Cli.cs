using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using System.Threading;

namespace Presentation
{
    class Cli
    {
        private Boolean running;
        private ChatRoom myChatRoom;
        private ChatroomMenu menu;

        public Cli()
        {
            myChatRoom = new ChatRoom();
            menu = myChatRoom.getMenu();
            running = true;
        }

        public void showMenu()
        {
            Console.Clear();
            while(running)
            {
                Console.WriteLine(menu.toString());
                char key = Console.ReadLine().ToCharArray()[0];
                string function= menu.getFunction(key);
                while(function == null)
                {
                    Console.WriteLine("key not supported, try again");
                    key = Console.ReadLine().ToCharArray()[0];
                    function = menu.getFunction(key);
                }
                this.GetType().GetMethod(function).Invoke(this, null);
            }

        }

        private void registration()
        {
            Console.Clear();
            Console.WriteLine("Enter your nickname");
            String nickname = Console.ReadLine();
            while ((nickname!="")&&(!myChatRoom.register(nickname)))
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

        private void loginLogout()
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

        private void retrive10Messages()
        {
            Console.Clear();
            int numOfMsg = myChatRoom.retrive10Messages();
            Console.WriteLine("Received " + numOfMsg + " messages from the server");
            Console.WriteLine("Press ENTER to go back to the menu");
            Console.ReadLine();
        }

        private void display20Messages()
        {
            Console.Clear();
            List<Message> messages = myChatRoom.display20Messages();
            foreach(Message msg in messages)
            {
                Console.WriteLine(msg.ToString());
            }
            Console.WriteLine("Press ENTER to go back to the menu");
            Console.ReadLine();
        }

        private void diplayAllByUser()
        {
            Console.Clear();
            Console.WriteLine("Enter username");
            string username = Console.ReadLine();
            Console.WriteLine("Enter groupID");
            string groupID = Console.ReadLine();
            List<Message> messages = myChatRoom.diplayAllByUser(username, groupID);
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

        private void writeMessage()
        {
            Console.Clear();
            Console.WriteLine("Write your message");
            String msg = Console.ReadLine();
            while((msg!="")&& !myChatRoom.writeMessage(msg))
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

        private void exit()
        {
            running = false;
        }


    }
}
