using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCore.Util
{
    public interface ILog
    {
        void Log(string message);
        void Log(object obj);
    }

    public class FenixLogger : ILog
    {
        private static Action<string> Action;

        public static void LogOfTheLazyProgrammer(string message)
        {
            if (Action != null)
                Action(message);
        }

        public void Log(string message)
        {
            if (Action != null)
                Action(message);
        }

        public void Log(object obj)
        {
            if (obj == null)
                Log("NULL");
            else
                Log(obj.ToString());
        }

        public static void SetCustomLoggingAction(Action<string> action)
        {
            Action = action;
        }
    }
}
