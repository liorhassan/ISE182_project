using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    interface ILogger
    {
        void ProcessLogMessage(string logMessage);
    }
}
