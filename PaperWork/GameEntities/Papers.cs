using GameCore;
using GameCore.Collision;

namespace PaperWork
{
    public class Papers : Entity
    {
        public Papers()
        {
            Textures.Add(new EntityTexture("papers", 50, 100));
            Colliders.Add(new GameCollider(this, 50, 50) { LocalPosition = new Coordinate2D(0, 50) });
        }
    }
}
