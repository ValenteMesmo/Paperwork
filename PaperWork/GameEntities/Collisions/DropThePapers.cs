using System;
using GameCore;

namespace PaperWork.GameEntities.Collisions
{
    public class DropThePapers : UpdateHandler
    {
        private Func<bool> HoldingBox;
        private InputRepository Inputs;
        private Action DropPapers;

        public DropThePapers(
            Entity ParentEntity,
            Func<bool> HoldingBox,
            Action DropPapers,
            InputRepository Inputs) : base(ParentEntity)
        {
            this.HoldingBox = HoldingBox;
            this.Inputs = Inputs;
            this.DropPapers = DropPapers;
        }

        public override void Update()
        {
            if (HoldingBox() 
                && Inputs.Grab.GetStatus() == ButtomStatus.Click)
            {
                DropPapers();
            }
        }
    }
}
