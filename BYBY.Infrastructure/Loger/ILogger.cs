using System;

namespace BYBY.Infrastructure.Loger
{
    public interface ILogger
    {
        void Log(string message);


        void Log(string message, int Level);
        void Log(Exception ex);
    }

}
