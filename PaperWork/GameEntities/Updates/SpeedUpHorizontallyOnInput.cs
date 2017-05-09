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
            if(Inputs.RightPressed)
            {
                ParentEntity.Speed = new Coordinate2D(
                    1,
                    ParentEntity.Speed.Y);
            }
            else if (Inputs.LeftPressed)
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
