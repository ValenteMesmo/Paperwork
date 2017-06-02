using GameCore;

namespace PaperWork
{
    public class Player :
        ICollider
        , IUpdateHandler
        , IRightCollisionHandler
        , ILeftCollisionHandler
        , ITopCollisionHandler
        , IBotCollisionHandler
    {
        private readonly InputRepository Inputs;

        public Player(InputRepository Inputs)
        {
            this.Inputs = Inputs;

            Width = 50;
            Height = 100;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float HorizontalSpeed { get; set; }

        public float VerticalSpeed { get; set; }

        public void BotCollision(ICollider collider)
        {
            if (collider is Block)
            {
                Y = collider.Top()
                    - Height
                    - World.SPACE_BETWEEN_THINGS;
            }
        }

        public void TopCollision(ICollider collider)
        {
            if (collider is Block)
            {
                Y = collider.Bottom()
                    + World.SPACE_BETWEEN_THINGS;
            }
        }

        public void LeftCollision(ICollider collider)
        {
            if (collider is Block)
            {
                X = collider.Right()
                    + World.SPACE_BETWEEN_THINGS;
            }
        }

        public void RightCollision(ICollider collider)
        {
            if (collider is Block)
            {
                X = collider.Left()
                    - collider.Width
                    - World.SPACE_BETWEEN_THINGS;
            }
        }

        public void Update()
        {
            if (Inputs.Left)
                HorizontalSpeed = -1;
            else if (Inputs.Right)
                HorizontalSpeed = 1;
            else
                HorizontalSpeed = 0;

            if (Inputs.Up)
                VerticalSpeed = -1;
            else if (Inputs.Down)
                VerticalSpeed = 1;
            else
                VerticalSpeed = 0;
        }
    }
}
