using GameCore;

namespace PaperWork.GameEntities.Collisions
{
    public class FollowOtherEntity : UpdateHandler
    {
        Entity Target;
        Coordinate2D bonus;
        public FollowOtherEntity(
            Entity ParentEntity,
            Entity Target,
            Coordinate2D bonus) : base(ParentEntity)
        {
            this.Target = Target;
            this.bonus = bonus;
        }

        public override void Update()
        {
            ParentEntity.Position = new Coordinate2D(
                Target.Position.X + bonus.X,
                Target.Position.Y + bonus.Y);
        }
    }
}
