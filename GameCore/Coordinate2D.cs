namespace GameCore
{
    public struct Coordinate2D
    {
        public float X;
        public float Y;

        public Coordinate2D(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static Coordinate2D operator *(
            Coordinate2D vector,
            float number)
        {
            vector.X = vector.X * number;
            vector.Y = vector.Y * number;
            return vector;
        }

        public static Coordinate2D operator +(
            Coordinate2D a,
            Coordinate2D b)
        {
            return new Coordinate2D(
                a.X + b.X,
                a.Y + b.Y
            );
        }
    }
}