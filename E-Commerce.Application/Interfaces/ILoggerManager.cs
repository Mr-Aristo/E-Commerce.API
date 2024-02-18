using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Interfaces
{//Nlog.Extentions paketi 
    public interface ILoggerManager
    {
        void LogInfo(string Message);
        void LogWarn(string Message);
        void LogDebug(string Message);
        void LogError(string Message);
    }
}
