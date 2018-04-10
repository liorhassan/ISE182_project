using Presentation;
using System;
using System.IO;
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
            String path = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(path.Substring(0, path.Length - 31)+ "\\Data");
            Cli Chatroom = new Cli();
            Chatroom.showMenu();
        }
    }
}
