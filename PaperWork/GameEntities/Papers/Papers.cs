using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Papers;
using System.Linq;

namespace PaperWork
{
    public class PapersEntity : Entity
    {
        public Property<Entity> Target = new Property<Entity>();

        public PapersEntity(GridPositions Grid)
        {
            Textures.Add(new EntityTexture("papers", Grid.cellSize, Grid.cellSize * 2)
            {
                Bonus = new Coordinate2D(0, -Grid.cellSize)
            });

            var mainCollider = new GameCollider(this, Grid.cellSize, Grid.cellSize);

            var followUpdateHandler = new FollowOtherEntity(
                 this,
                 new Coordinate2D(0, -mainCollider.Height),
                 Target.Get);

            UpdateHandlers.Add(followUpdateHandler);
            UpdateHandlers.Add(new FallDownWhenPossibel(
                this,
                Grid,
                Target.HasValue
            ));

            Colliders.Add(mainCollider);
        }
    }
}
