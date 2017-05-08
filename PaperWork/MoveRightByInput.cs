using GameCore;

namespace PaperWork
{
    public class MoveRightByInput : UpdateHandler
    {
        private InputRepository Inputs;

        public MoveRightByInput(Entity ParentEntity, InputRepository Inputs) : base(ParentEntity)
        {
            this.Inputs = Inputs;
        }

        public override void Update()
        {
            if(Inputs.RightPressed)
            {
                ParentEntity.Position = new Coordinate2D(
                    ParentEntity.Position.X + 1,
                    ParentEntity.Position.Y);
            }
            else if (Inputs.LeftPressed)
            {
                ParentEntity.Position = new Coordinate2D(
                    ParentEntity.Position.X - 1,
                    ParentEntity.Position.Y);
            }
        }
    }
}
