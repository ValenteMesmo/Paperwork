using GameCore;
using GameCore.Collision;

namespace PaperWork.GameEntities
{
    public class SolidBlock : Entity
    {
        public SolidBlock() : base(50, 50)
        {
            Textures.Add(new EntityTexture("block", 50, 50));
            Colliders.Add(new BoxCollider(this, 50, 50, 0, 0));
        }
    }
}
