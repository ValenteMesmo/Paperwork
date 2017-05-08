using GameCore;

namespace PaperWork.PlayerHandlers.Updates
{
    public class GravityFall : UpdateHandler
    {
        public const float VELOCITY = .05f;
        public const float MAX_SPEED = 5.0f;

        public GravityFall(Entity player) : base(player)
        {

        }

        public override void Update()
        {
            var verticalSpeed = ParentEntity.Speed.Y + VELOCITY;
            if (verticalSpeed > MAX_SPEED)
                verticalSpeed = MAX_SPEED;

            ParentEntity.Speed = new Coordinate2D(ParentEntity.Speed.X, verticalSpeed);

            ParentEntity.Position = new Coordinate2D(
                ParentEntity.Position.X,
                ParentEntity.Position.Y + ParentEntity.Speed.Y);
        }
    }
}
