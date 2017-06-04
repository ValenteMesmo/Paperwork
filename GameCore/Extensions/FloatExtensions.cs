namespace GameCore
{
    public static class FloatExtensions
    {
        public static float Range(this float number, float min, float max)
        {
            if (number > max)
                number = max;

            if (number < min)
                number = min;

            return number;
        }

        public static float EasyToZero(this float value, float decrement)
        {
            if (value > 0)
            {
                if (value < 0.5f)
                    value = 0;
                else
                    value -= decrement;
            }
            else if (value < 0)
            {
                if (value > -0.5f)
                    value = 0;
                else
                    value += decrement;
            }

            return value;
        }
    }
}