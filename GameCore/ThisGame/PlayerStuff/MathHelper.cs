using System;

namespace PaperWork
{
    static class MathHelper
    {
        public static int RoundUp(float numToRound, int multiple)
        {
            return (int)Math.Round(numToRound / multiple) * multiple;
        }
    }
}
