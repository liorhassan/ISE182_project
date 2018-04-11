using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    //class for handeling the menu items given from the xml file
    //created and uused by the ChatroomMenu class
    class MenuItem
    {
        //fields representing the xml fields
        private bool _loginRequierd;
        public bool LoginRequierd { get => _loginRequierd; }

        private char _optionKey;
        public char OptionKey { get => _optionKey; }

        private string _message;
        public string Message { get => _message; }

        private string _itemFunction;
        public string ItemFunction { get => _itemFunction; }

        private int _order;
        public int Order { get => _order; }

        //constructor
        public MenuItem(char optionKey,string message,string itemFunction,bool loginRequierd)
        {
            _optionKey = optionKey;
            _message = message;
            _itemFunction = itemFunction;
            _loginRequierd = loginRequierd;
        }

        //ToString Function - returns the value of the item to be printed in the menu
        public string ToString()
        {
            return _message+" - "+_optionKey;
        }

    }
}
