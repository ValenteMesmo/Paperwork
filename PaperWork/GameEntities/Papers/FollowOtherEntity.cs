using GameCore;
using System;

namespace PaperWork.GameEntities.Collisions
{
    public class FollowOtherEntity : UpdateHandler
    {
        public Entity Target;
        Coordinate2D bonus;
        public FollowOtherEntity(
            Entity ParentEntity,
            Coordinate2D bonus) : base(ParentEntity)
        {
            this.bonus = bonus;
        }

        public override void Update()
        {
            if (Target == null)
                return;

            ParentEntity.Position = new Coordinate2D(
                Target.Position.X + bonus.X,
                Target.Position.Y + bonus.Y);
        }
    }
}
