using System;

namespace GameCore
{
    public class AnimationTransition
    {
        public readonly Animation[] Sources;
        public readonly Animation Target;
        public readonly Func<bool> Condition;
        public readonly Action AfterTransition;

        public AnimationTransition(Animation[] Sources, Animation Target, Func<bool> Condition, Action AfterTransition = null)
        {
            this.Sources = Sources;
            this.Target = Target;
            this.Condition = Condition;
            if (AfterTransition != null)
                this.AfterTransition = AfterTransition;
            else
                this.AfterTransition = () => { };
        }
    }
}
