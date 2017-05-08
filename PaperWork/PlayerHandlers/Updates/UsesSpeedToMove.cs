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
        public UsesSpeedToMove(Entity ParentEntity) : base(ParentEntity)
        {
        }

        public override void Update()
        {
            ParentEntity.Position = new Coordinate2D(
                ParentEntity.Position.X + ParentEntity.Speed.X,
                ParentEntity.Position.Y + ParentEntity.Speed.Y
            );
        }
    }
}
