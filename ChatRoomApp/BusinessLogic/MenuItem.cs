using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    class MenuItem
    {
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

        public MenuItem(char optionKey,string message,string itemFunction,bool loginRequierd)
        {
            _optionKey = optionKey;
            _message = message;
            _itemFunction = itemFunction;
            _loginRequierd = loginRequierd;
        }

        public string toString()
        {
            return _message+" - "+_optionKey;
        }

    }
}
