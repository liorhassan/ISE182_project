﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
            Stream myFileStream = File.OpenRead(binPath);
            BinaryFormatter serializes = new BinaryFormatter();
            serializes.Serialize(myFileStream, o);
            myFileStream.Close();
        }
        public object load()
        {
            Stream myOtherFileStream = File.OpenRead(binPath);
            BinaryFormatter deserializer = new BinaryFormatter();
            object o = deserializer.Deserialize(myOtherFileStream);
            myOtherFileStream.Close();
            return o;
        }
    }
}