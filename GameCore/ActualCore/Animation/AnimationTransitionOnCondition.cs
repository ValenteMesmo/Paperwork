using System;

namespace GameCore
{
    public class AnimationTransitionOnCondition : AnimationTransition
    {
        public Animation[] Sources { get; }
        public Animation Target { get; }
        public Func<bool> Condition { get; }
        public Action AfterTransition { get; }

        public AnimationTransitionOnCondition(Animation[] Sources, Animation Target, Func<bool> Condition, Action AfterTransition = null)
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
