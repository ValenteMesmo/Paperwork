using GameCore;

namespace PaperWork
{
    public class Player :
        ICollider
        , IUpdateHandler
        , ICollisionHandler
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float HorizontalSpeed { get; set; }
        public float VerticalSpeed { get; set; }
        
        private const float VELOCITY = 0.08f;
        private const float MAX_SPEED = 3f;
        private const float FRICTION = 0.05f;
        private const float CHANGE_DIRECTION_BONUS = 2.5f;
        private readonly InputRepository Inputs;
        private ICollisionHandler CollisionHandler;

        public Player(InputRepository Inputs)
        {
            this.Inputs = Inputs;

            Width = 50;
            Height = 100;
            CollisionHandler = new StopsWhenCollidingWith<Block>(this);
        }
        
        public void BotCollision(ICollider collider)
        {
            CollisionHandler.BotCollision(collider);
        }

        public void TopCollision(ICollider collider)
        {
            CollisionHandler.TopCollision(collider);
        }

        public void LeftCollision(ICollider collider)
        {
            CollisionHandler.LeftCollision(collider);
        }

        public void RightCollision(ICollider collider)
        {
            CollisionHandler.RightCollision(collider);
        }

        private float Limit(float value, float add)
        {
            if (value > 0 && add < 0)
                value -= CHANGE_DIRECTION_BONUS;
            else if (value < 0 && add > 0)
                value += CHANGE_DIRECTION_BONUS;

            value += add;


            if (value > MAX_SPEED)
                value = MAX_SPEED;
            if (value < -MAX_SPEED)
                value = -MAX_SPEED;
            return value;
        }

        private float EasyToZero(float value)
        {
            if (value > 0)
            {
                if (value < 0.5f)
                    value = 0;
                else
                    value -= FRICTION;
            }
            else if (value < 0)
            {
                if (value > -0.5f)
                    value = 0;
                else
                    value += FRICTION;
            }
            return value;
        }

        public void Update()
        {
            if (Inputs.Left)
                HorizontalSpeed = Limit(HorizontalSpeed, -VELOCITY);
            else if (Inputs.Right)
                HorizontalSpeed = Limit(HorizontalSpeed, VELOCITY);
            else
                HorizontalSpeed = EasyToZero(HorizontalSpeed);

            if (Inputs.Up)
                VerticalSpeed = Limit(VerticalSpeed, -VELOCITY);
            else if (Inputs.Down)
                VerticalSpeed = Limit(VerticalSpeed, VELOCITY);
            else
                VerticalSpeed = EasyToZero(VerticalSpeed);
        }
    }
}
