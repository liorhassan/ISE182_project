using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    //A class responsiable for reading the xml file describing the menu
    //Uses a default constructor
    public class XMLHandler
    {
        //read-only field for the xml relative path
        private readonly string xmlPath = "menu.xml";

        //tries to load the data from the xml and return it
        // throws exception if fails
        public XDocument load()
        {
            try
            {
                XDocument doc = XDocument.Load(xmlPath);
                return doc;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
