using GameCore;
using GameCore.Collision;
using System;

namespace PaperWork.PlayerHandlers.Collisions
{
    public class StopsWhenHitsTheFloor : CollisionHandler
    {
        private Action AllowJumping;

        public StopsWhenHitsTheFloor(GameCollider Parent, Action AllowJumping) : base(Parent)
        {
            this.AllowJumping = AllowJumping;
        }

        public override void CollisionFromBelow(GameCollider papers)
        {
            if (papers.ParentEntity is Papers)
            {
                ParentEntity.Speed = new Coordinate2D(
                    ParentEntity.Speed.X,
                    0
                );

                ParentEntity.Position =
                    new Coordinate2D(
                        ParentEntity.Position.X,
                        papers.Position.Y - ParentCollider.Height);

                AllowJumping();
            }
        }
    }
}
