using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoggingSample
{
    class FileLogger: ILogger
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
            mFileName = fileName;
        }
        #endregion

        #region Public methods
        public void Init()
        {
            mLogFile = new StreamWriter(mFileName);
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
