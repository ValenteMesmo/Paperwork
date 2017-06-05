﻿using GameCore;

namespace PaperWork
{
    public class MoveHorizontallyOnInput : IUpdateHandler
    {
        private const int VELOCITY = 2;
        private const int FRICTION = 1;
        private const int CHANGE_DIRECTION_BONUS = 2;
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

        private int Move(int currentSpeed, int velocity)
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