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
        private readonly string xmlPath = "menu.xml";

        public XDocument load()
        {
            try
            {
                XDocument doc = XDocument.Load(xmlPath);
                return doc;
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                return null;
            }
        }
    }
}
