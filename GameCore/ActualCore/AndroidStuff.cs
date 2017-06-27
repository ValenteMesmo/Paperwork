using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.ActualCore
{
    public static class AndroidStuff
    {
        public static Action<long> Vibrate = f=> { };
        public static bool RunningOnAndroid;
    }
}
