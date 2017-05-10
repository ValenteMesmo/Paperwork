using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using PaperWork.PlayerHandlers.Collisions;
using PaperWork.PlayerHandlers.Updates;

namespace PaperWork
{
    public class Papers : Entity
    {
        public Papers()
        {
            Textures.Add(new EntityTexture("papers", 50, 100)
            {
                Bonus = new Coordinate2D(0, -50)
            });
            UpdateHandlers.Add(new GravityFall(this));
            UpdateHandlers.Add(new UsesSpeedToMove(this));
          
            var mainCollider = new GameCollider(this, 50, 50)
            {
                //LocalPosition = new Coordinate2D(0, 50)
            };

            mainCollider.CollisionHandlers.Add(new StopsWhenHitsPapers(
                mainCollider,
                //TODO: !!!!!!!!!!!!!!!!!!!!!!
                () => { }));
            Colliders.Add(mainCollider);

            UpdateHandlers.Add(
              new FollowOtherEntity(
                  GetPlayerBeenFollowed,
                  this,
                  new Coordinate2D(0, -mainCollider.Height)));
        }
    }
}
