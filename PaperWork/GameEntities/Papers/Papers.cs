using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;

namespace PaperWork
{
    public class PapersEntity : Entity
    {
        public readonly Property<Entity> Target = new Property<Entity>();
        private readonly GameCollider mainCollider;
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
            //UpdateHandlers.Add(new FallDownWhenPossibel(Target.IsNotNull, () => Grid.cellSize, Grid.PositionAvailable));

            Colliders.Add(mainCollider);
        }

        public void Grab(Entity grabbedBy)
        {
            mainCollider.Disabled = true;
            Target.Set(grabbedBy);
            Grid.Pop(this.Position);
        }

        public void Drop()
        {
            mainCollider.Disabled = false;
            Target.Set(null);
            Position = new Coordinate2D(Position.X + 50, Position.Y + 50);
            Grid.Push(this);
        }
    }
}
