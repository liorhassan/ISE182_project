using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class XMLHandler
    {
        private readonly string xmlPath = @"C:\Temp\Chatroom24\menu.xml";

        public XDocument load()
        {
            XDocument doc = XDocument.Load(xmlPath);
            return doc;
        }
    }
}
