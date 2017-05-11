using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Papers;

namespace PaperWork
{
    public class PapersEntity : Entity
    {
        public Property<Entity> Target = new Property<Entity>();
        private GameCollider mainCollider;
        GridPositions Grid;

        public PapersEntity(GridPositions Grid)
        {
            this.Grid = Grid;
            Textures.Add(new EntityTexture("papers", Grid.cellSize, Grid.cellSize * 2)
            {
                Bonus = new Coordinate2D(0, -Grid.cellSize)
            });

            mainCollider = new GameCollider(this, Grid.cellSize, Grid.cellSize);
            
            UpdateHandlers.Add(new FollowOtherEntity(new Coordinate2D(0, -mainCollider.Height), Target.Get));
            UpdateHandlers.Add(new FallDownWhenPossibel(Grid, Target.IsNotNull));

            Colliders.Add(mainCollider);
        }

        public void GrabbedBy(Entity player)
        {
            mainCollider.Disabled = true;
            Target.Set(player);
        }

        public void DroppedBy(Entity player)
        {
            mainCollider.Disabled = false;
            Target.Set(null);
        }
    }
}
