using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProg
{
    class Program
    {
        static void Main(string[] args)
        {
            Chatroom chatroom = new Chatroom();
            Console.Write(chatroom.login("Ohad"));
           // chatroom.register("Ohad");
        }
    }
}
