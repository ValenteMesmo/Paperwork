using GameCore;
using GameCore.Collision;
using PaperWork.PlayerHandlers.Collisions;
using PaperWork.PlayerHandlers.Updates;

namespace PaperWork
{
    public class Game1 : BaseGame
    {
        public Game1() : base("char", "papers")
        {
            AddEntity(new Player(PlayerInputs));

            for (int i = 0; i < 10; i++)
            {
                AddEntity(new Papers()
                { Position = new Coordinate2D(i * 50, 150) }
            );
            }

        }
    }

    public class Player : Entity
    {
        public Player(InputRepository PlayerInputs)
        {
            var canJump = true;
            var mainCollider = new GameCollider(this, 50, 100);
            Textures.Add(new EntityTexture("char", 50, 100));
            UpdateHandlers.Add(new SpeedUpHorizontallyOnInput(this, PlayerInputs));
            UpdateHandlers.Add(new JumpOnInput(this, PlayerInputs, () => canJump));
            UpdateHandlers.Add(new GravityFall(this));
            UpdateHandlers.Add(new UsesSpeedToMove(this));
            UpdateHandlers.Add(new ForbidJumpIfVerticalSpeedNotZero(this, () => canJump = false));
            mainCollider.CollisionHandlers.Add(new StopsWhenHitsTheFloor(mainCollider, () => canJump = true));

            Colliders.Add(mainCollider);
        }
    }

    public class Papers : Entity
    {
        public Papers()
        {
            Textures.Add(new EntityTexture("papers", 50, 100));
            Colliders.Add(new GameCollider(this, 50, 50) { Position = new Coordinate2D(0, 50) });
        }
    }
}
