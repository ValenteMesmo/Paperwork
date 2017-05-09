using GameCore;
using GameCore.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperWork.GameEntities
{
    public class SolidBlock : Entity
    {
        public SolidBlock()
        {
            Textures.Add(new EntityTexture("block", 50, 50));
            Colliders.Add(new GameCollider(this, 50, 50));
        }
    }
}
