namespace GameCore
{
    public static class FloatExtensions
    {
        public static int Range(this int number, int min, int max)
        {
            if (number > max)
                number = max;

            if (number < min)
                number = min;

            return number;
        }

        public static int EasyToZero(this int value, int decrement)
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