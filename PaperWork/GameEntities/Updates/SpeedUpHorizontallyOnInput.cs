using GameCore;

namespace PaperWork
{
    public class SpeedUpHorizontallyOnInput : UpdateHandler
    {
        private InputRepository Inputs;

        public SpeedUpHorizontallyOnInput(Entity ParentEntity, InputRepository Inputs) : base(ParentEntity)
        {
            this.Inputs = Inputs;
        }

        public override void Update()
        {
            if(Inputs.Right.GetStatus() == ButtomStatus.Click
                || Inputs.Right.GetStatus() == ButtomStatus.Hold)
            {
                ParentEntity.Speed = new Coordinate2D(
                    1,
                    ParentEntity.Speed.Y);
            }
            else if (Inputs.Left.GetStatus() == ButtomStatus.Click 
                || Inputs.Left.GetStatus() == ButtomStatus.Hold)
            {
                ParentEntity.Speed = new Coordinate2D(
                    - 1,
                    ParentEntity.Speed.Y);
            }
            else
            {
                ParentEntity.Speed = new Coordinate2D(0, ParentEntity.Speed.Y);
            }
        }
    }
}
