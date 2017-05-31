using GameCore;

namespace PaperWork
{
    public static class EntityExtensions
    {
        public static void ZeroHorizontalSpeed(this Entity entity)
        {
             entity.Speed = new Coordinate2D(0, entity.Speed.Y);
        }

        public static void ZeroVerticalSpeed(this Entity entity)
        {
             entity.Speed = new Coordinate2D(entity.Speed.X, 0);
        }

        public static void SetVerticalSpeed(this Entity entity, float f)
        {
             entity.Speed = new Coordinate2D(entity.Speed.X, f);
        }

        public static void SetHorizontalSpeed(this Entity entity, float f)
        {
             entity.Speed = new Coordinate2D(f, entity.Speed.Y);
        }

        public static float GetHorizontalSpeed(this Entity entity)
        {
            return  entity.Speed.X;
        }

        public static float GetVerticalSpeed(this Entity entity)
        {
            return  entity.Speed.Y;
        }
    }
}
