using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using System.Xml.Linq;

namespace BusinessLogic
{
    //class for using the chatroom menu as given from the xml file
    //contains and usses the menu item class
    public class ChatroomMenu
    {
        //list of the menu items it the menu
        private List<MenuItem> menuItems;

        //the xml hadler for the xml menu file
        private XMLHandler xmlHandler;

        //boolean field representing wether a user is logged in or not
        //afects the menu option shown
        private bool _login;
        public bool Login { set => _login = value; }

        //constructor - usess the xml handler and creates the menu items as needed
        public ChatroomMenu()
        {
            _login = false;
            menuItems = new List<MenuItem>();
            xmlHandler = new XMLHandler();
            XDocument doc = xmlHandler.load();
            foreach (var item in doc.Descendants("MenuItem"))
            {
                char tKey = item.Element("key").Value.ToCharArray()[0];
                string tMessage = item.Element("message").Value;
                string tFunction = item.Element("function").Value;
                bool tLogin = item.Element("loginReq").Value == "true";
                menuItems.Add(new MenuItem(tKey, tMessage, tFunction, tLogin));
            }
        }

        //gets a key from the cli and returns the function name to be run
        //returns empty string if no key is supported(either not found or needs login)
        public String getFunction(char key)
        {
            var items = from item in menuItems where (item.LoginRequierd == _login || item.LoginRequierd == false) && item.OptionKey == key select item;
            foreach(MenuItem item in items)
                return item.ItemFunction;
            return "";
        }

        //returns the string of the current menu to be printed by the cli
        public String ToString()
        {
            var itemList = from item in menuItems
                           where item.LoginRequierd == _login || item.LoginRequierd == false
                           orderby item.Order descending
                           select item;
            String output = "";
            foreach (MenuItem item in itemList)
            {
                output += item.ToString() + "\n";
            }
            return output;
        }

        internal ChatroomMenu get()
        {
            throw new NotImplementedException();
        }
    }
}
