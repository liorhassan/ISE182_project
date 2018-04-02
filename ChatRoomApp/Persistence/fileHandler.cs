using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    abstract class fileHandler
    {
        private string binPath;

        public fileHandler(string path)
        {
            binPath = path;
        }
        public void save(object o)
        {

        }
        public object load()
        {
            return null;
        }
    }
}
