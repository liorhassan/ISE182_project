using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 

namespace Presentation
{
    class Cli
    {
        private Boolean running;
        private ChatRoom myChatRoom;
        private List<MenuItem> menuItems;

        public void showMenu()
        {
            Console.Clear();

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
         
        }

        private void loginLogout()
        {
            Console.Clear();
            if (myChatRoom.Logout())
                Console.WriteLine("Your user logout");
            else
            {
                Console.WriteLine("Enter your nickname");
                String nickname = Console.ReadLine();
                while ((nickname!="")&&(!myChatRoom.Login(nickname)))
                {
                    Console.WriteLine("User non-exist, try egain or press ENTER and go back to the menu");
                    nickname = Console.ReadLine();

                }


            }

        }

        private void retrive10Messages()
        {

        }

        private void display20Messages()
        {

        }

        private void diplayAllByUser()
        {

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
        
        }

        private void exit()
        {

        }


    }
}
