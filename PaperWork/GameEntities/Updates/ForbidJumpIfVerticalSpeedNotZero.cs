using GameCore;
using System;

namespace PaperWork.PlayerHandlers.Updates
{
    public class ForbidJumpIfVerticalSpeedNotZero : UpdateHandler
    {
        private Action ForbidJump;
        public ForbidJumpIfVerticalSpeedNotZero(Entity ParentEntity, Action ForbidJump) : base(ParentEntity)
        {
            this.ForbidJump = ForbidJump;
        }

        public override void Update()
        {
            if (ParentEntity.Speed.Y != 0)
                ForbidJump();
        }
    }
}
