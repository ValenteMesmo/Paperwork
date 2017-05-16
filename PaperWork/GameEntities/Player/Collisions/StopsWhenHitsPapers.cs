using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities;
using System;

namespace PaperWork.PlayerHandlers.Collisions
{
    public class StopsWhenHitsPapers : CollisionHandler
    {
        private Action<bool> SetJumpEnable;
        private Action<float> SetVerticalSpeed;

        public StopsWhenHitsPapers(
            Action<bool> SetJumpEnable,
            Action<float> SetVerticalSpeed) 
        {
            this.SetJumpEnable = SetJumpEnable;
            this.SetVerticalSpeed = SetVerticalSpeed;
        }

        public override void CollisionFromBelow(GameCollider collider, GameCollider papers)
        {
            if (papers.ParentEntity is PapersEntity || papers.ParentEntity is SolidBlock)
            {
                SetVerticalSpeed(0);

                collider.ParentEntity.Position =
                    new Coordinate2D(
                        collider.ParentEntity.Position.X,
                        papers.Position.Y - collider.Height -1);

                SetJumpEnable(true);
            }
        }

        public override void CollisionFromAbove(GameCollider collider, GameCollider papers)
        {
            if (papers.ParentEntity is PapersEntity || papers.ParentEntity is SolidBlock)
            {
                SetVerticalSpeed(0);

                collider.ParentEntity.Position =
                    new Coordinate2D(
                        collider.ParentEntity.Position.X,
                        papers.Position.Y + papers.Height + 1);
            }
        }
    }
}
