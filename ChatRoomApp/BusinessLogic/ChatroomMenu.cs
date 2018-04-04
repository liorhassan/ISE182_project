using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using System.Xml.Linq;

namespace BusinessLogic
{
    public class ChatroomMenu
    {
        private List<MenuItem> menuItems;
        private XMLHandler xmlHandler;
        private bool _login;
        public bool Login { set => _login = value; }

        public ChatroomMenu()
        {
            _login = false;
            menuItems = new List<MenuItem>();
            xmlHandler = new XMLHandler("C:/temp/menu.xml");
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

        public String getFunction(char key)
        {
            var items = from item in menuItems where item.LoginRequierd == _login && item.OptionKey == key select item.ItemFunction;
            return items.ToString();
        }

        public String toString()
        {
            var itemList = from item in menuItems
                           where item.LoginRequierd==_login
                           orderby item.Order descending
                           select item;
            String output = "";
            foreach (MenuItem item in itemList)
            {
                output += item.toString() + "\n";
            }
            return output;
        }

        internal ChatroomMenu get()
        {
            throw new NotImplementedException();
        }
    }
}
