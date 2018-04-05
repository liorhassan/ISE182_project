using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingSample
{
    interface ILogger
    {
        void ProcessLogMessage(string logMessage);
    }
}
