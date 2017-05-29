namespace GameCore
{
    public static class FloatExtensions
    {
        public static bool Between(this float number, float min, float max)
        {
            return number > min && number < max;
        }
    }
}
