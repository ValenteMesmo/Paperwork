using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities;
using System;

namespace PaperWork.PlayerHandlers.Collisions
{
    public class StopsWhenHitsPapers : IHandleCollision
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

        public void CollisionFromBelow(BaseCollider collider, BaseCollider papers)
        {
            if (papers.ParentEntity is PapersEntity
                || papers.ParentEntity is SolidBlock)
            {
                SetVerticalSpeed(0);

                collider.ParentEntity.Position =
                    new Coordinate2D(
                        collider.ParentEntity.Position.X,
                        papers.Position.Y - collider.Height - 1);

                SetJumpEnable(true);
            }
        }

        public void CollisionFromAbove(BaseCollider collider, BaseCollider papers)
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

        public void CollisionFromTheLeft(BaseCollider collider, BaseCollider other)
        {
        }

        public void CollisionFromTheRight(BaseCollider collider, BaseCollider other)
        {
        }
    }
}
