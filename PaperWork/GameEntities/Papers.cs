using System;
using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Papers;
using PaperWork.PlayerHandlers.Updates;
using System.Diagnostics;

namespace PaperWork
{
    public class PapersEntity : Entity
    {
        public readonly Property<Entity> Target = new Property<Entity>();
        public readonly Property<float> VerticalSpeed = new Property<float>();
        public readonly Property<float> HorizontalSpeed = new Property<float>();
        private readonly Collider mainCollider;

        public PapersEntity(int cellSize)
        {
            Textures.Add(new EntityTexture("papers", cellSize, cellSize * 2)
            {
                Offset = new Coordinate2D(0, -cellSize)
            });

            mainCollider = new Collider(this, cellSize, cellSize);
            mainCollider.AddHandlers(
                new StopsOnFixedPositionWhenColliding(VerticalSpeed.Set, HorizontalSpeed.Set));
            Colliders.Add(mainCollider);

            AddUpdateHandlers(
                new GravityIncreasesVerticalSpeed(VerticalSpeed.Get, VerticalSpeed.Set)
                , new FrictionSpeedLoss(HorizontalSpeed.Set, HorizontalSpeed.Get)
                , new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get)
                , new FollowOtherEntity(new Coordinate2D(-20, -mainCollider.Height), Target.Get)
            );

            var rightTrigger = new Trigger(this, cellSize - 10, cellSize - 10);
            rightTrigger.Position = new Coordinate2D(cellSize, -10);
            rightTrigger.AddHandlers(new MyClass());

            Colliders.Add(rightTrigger);
        }

        class MyClass : IHandleTriggers
        {
            public void TriggerEnter(BaseCollider triggerCollider, BaseCollider other)
            {
                Debug.WriteLine("Enter " + DateTime.Now.ToString("HH:mm:ss.fff"));
            }

            public void TriggerExit(BaseCollider triggerCollider, BaseCollider other)
            {
                Debug.WriteLine("Exit " + DateTime.Now.ToString("HH:mm:ss.fff"));
            }
        }

        //this should not be here
        public void Grab(Entity grabbedBy)
        {
            mainCollider.Disabled = true;
            Target.Set(grabbedBy);
        }

        //this should not be here
        public void Drop(float speedX, float speedY)
        {
            mainCollider.Disabled = false;
            Target.Set(null);
            VerticalSpeed.Set(speedY);
            HorizontalSpeed.Set(speedX);
            var x = Position.X + 50;
            if (x > 50 * 12)
                x = 50 * 12;
            var y = Position.Y + 25;
            if (y < 50)
                y = 50;

            Position = new Coordinate2D(x, y);
        }
    }
}
