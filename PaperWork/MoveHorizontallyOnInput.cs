using GameCore;

namespace PaperWork
{
    public class MoveHorizontallyOnInput : IUpdateHandler
    {
        private const float VELOCITY = 0.08f;
        private const float FRICTION = 0.05f;
        private const float CHANGE_DIRECTION_BONUS = 2.5f;
        private readonly InputRepository Inputs;
        private readonly ICollider Parent;

        public MoveHorizontallyOnInput(
            ICollider Parent
            , InputRepository Inputs)
        {
            this.Parent = Parent;
            this.Inputs = Inputs;
        }

        public void Update()
        {
            if (Inputs.Left)
                Parent.HorizontalSpeed = Move(Parent.HorizontalSpeed, -VELOCITY);
            else if (Inputs.Right)
                Parent.HorizontalSpeed = Move(Parent.HorizontalSpeed, VELOCITY);
            else
                Parent.HorizontalSpeed = Parent.HorizontalSpeed.EasyToZero(FRICTION);
        }

        private float Move(float currentSpeed, float velocity)
        {
            if (currentSpeed > 0 && velocity < 0)
                currentSpeed -= CHANGE_DIRECTION_BONUS;
            else if (currentSpeed < 0 && velocity > 0)
                currentSpeed += CHANGE_DIRECTION_BONUS;

            currentSpeed += velocity;

            return currentSpeed;
        }
    }
}