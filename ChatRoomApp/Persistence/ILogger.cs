using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public interface ILogger
    {
        void ProcessLogMessage(string logMessage);
    }
}
