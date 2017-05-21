using GameCore;
using GameCore.Collision;
using System;

namespace PaperWork.GameEntities
{
    public class SolidBlock : Entity
    {
        public SolidBlock()
        {
            Textures.Add(new EntityTexture("block", 50, 50));
            Colliders.Add(new Collider(this, 50, 50));
        }
    }
}
