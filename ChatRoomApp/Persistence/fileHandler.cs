﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Persistence
{
    //an abstract class, the inherited by the UsersHandler and MessagesHandler
    public abstract class fileHandler
    {
        //the relative path for the bin file
        private string binPath;

        //the constructor for the abstract class
        public fileHandler(string path)
        {
            String currpath = Directory.GetCurrentDirectory();
            binPath = currpath.Substring(0, currpath.Length - 38) + "\\Data\\"+ path;
            if (!Directory.Exists(currpath.Substring(0, currpath.Length - 38) + "\\Data")) Directory.CreateDirectory(currpath.Substring(0, currpath.Length - 38) + "\\Data");
        }

        //gets an object(needs to be serializable) and saves it to the bin file
        //throw exception if falis
        public void save(object o)
        {
            try
            {
                Stream myFileStream = File.OpenWrite(binPath);
                BinaryFormatter serializes = new BinaryFormatter();
                serializes.Serialize(myFileStream, o);
                myFileStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error!! Failed saving to the bin file: "+binPath);
                Console.ReadLine();
                throw e;
            }
        }

        //tries to load an object from the bin file
        //throws an exception if failes
        public object load()
        {
            if(!File.Exists(binPath))
            {
                FileStream f = File.Create(binPath);
                f.Close();
                return null;
                
            }
            try
            {
                Stream myOtherFileStream = File.OpenRead(binPath);
                BinaryFormatter deserializer = new BinaryFormatter();
                object o = deserializer.Deserialize(myOtherFileStream);
                myOtherFileStream.Close();
                return o;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error!! Failed loading from the bin file: " + binPath);
                Console.ReadLine();
                throw e;
            }

        }
    }
}
