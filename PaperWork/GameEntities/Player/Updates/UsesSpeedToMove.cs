using GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperWork.PlayerHandlers.Updates
{
    class UsesSpeedToMove : UpdateHandler
    {
        Func<float> GetHorizontalSpeed;
        Func<float> GetVerticalSpeed;
        public UsesSpeedToMove(
            Entity ParentEntity
            ,Func<float> GetHorizontalSpeed
            ,Func<float> GetVerticalSpeed) : base(ParentEntity)
        {
            this.GetHorizontalSpeed=GetHorizontalSpeed;
            this.GetVerticalSpeed  = GetVerticalSpeed;
        }

        public override void Update()
        {
            ParentEntity.Position = new Coordinate2D(
                ParentEntity.Position.X + GetHorizontalSpeed(),
                ParentEntity.Position.Y + GetVerticalSpeed()
            );
        }
    }
}
