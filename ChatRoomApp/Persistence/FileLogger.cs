using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Persistence
{
    public class FileLogger: ILogger
    {
        #region Data
        private string mFileName;
        private StreamWriter mLogFile;

        public string FileName
        {
            get { return mFileName; }
        }
        #endregion

        #region Constructor
        public FileLogger(string fileName)
        {
            if (!File.Exists(fileName))
            {
                FileStream f = File.Create(fileName);
                f.Close();
            }
            mFileName = fileName;
        }
        #endregion

        #region Public methods
        public void Init()
        {
            try
            {
                mLogFile = new StreamWriter(mFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }

        public void Terminate()
        {
            mLogFile.Close();
        }
        #endregion

        #region ILogger Members

        public void ProcessLogMessage(string logMessage)
        {
            // FileLogger implements the ProcessLogMessage method by writing the incoming
            // message to a file.
            mLogFile.WriteLine(logMessage);
        }
        #endregion
    }
}
