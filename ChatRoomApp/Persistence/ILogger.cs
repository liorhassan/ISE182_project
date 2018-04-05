using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingSample
{
    public interface ILogger
    {
        void ProcessLogMessage(string logMessage);
    }
}
