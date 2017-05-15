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
            GameCollider Parent,
            Action<bool> SetJumpEnable,
            Action<float> SetVerticalSpeed) : base(Parent)
        {
            this.SetJumpEnable = SetJumpEnable;
            this.SetVerticalSpeed = SetVerticalSpeed;
        }

        public override void CollisionFromBelow(GameCollider papers)
        {
            if (papers.ParentEntity is PapersEntity || papers.ParentEntity is SolidBlock)
            {
                SetVerticalSpeed(0);

                ParentEntity.Position =
                    new Coordinate2D(
                        ParentEntity.Position.X,
                        papers.Position.Y - ParentCollider.Height -1);

                SetJumpEnable(true);
            }
        }

        public override void CollisionFromAbove(GameCollider papers)
        {
            if (papers.ParentEntity is PapersEntity || papers.ParentEntity is SolidBlock)
            {
                SetVerticalSpeed(0);

                ParentEntity.Position =
                    new Coordinate2D(
                        ParentEntity.Position.X,
                        papers.Position.Y + papers.Height + 1);
            }
        }
    }
}
