using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using PaperWork.PlayerHandlers.Collisions;
using PaperWork.PlayerHandlers.Updates;

namespace PaperWork
{
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

            mainCollider.CollisionHandlers.Add(new StopsWhenHitsPapers(mainCollider, () => canJump = true));
            mainCollider.CollisionHandlers.Add(new MoveOnCollision(mainCollider));

            Colliders.Add(mainCollider);
        }
    }
}
