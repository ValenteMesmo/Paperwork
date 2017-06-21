using System;

namespace GameCore
{
    public interface AnimationTransition
    {
        Animation[] Sources { get; }
        Animation Target { get; }
        Func<bool> Condition { get; }
        Action AfterTransition { get; }
    }

    public class WhenAnimationEnded : AnimationTransition
    {
        public Animation[] Sources { get; }
        public Animation Target { get; }
        public Func<bool> Condition { get; }
        public Action AfterTransition { get; }

        public WhenAnimationEnded(Animation Source, Animation Target, Func<bool> Condition, Action AfterTransition = null)
        {
            this.Sources = new[] { Source };
            this.Target = Target;
            this.Condition = ()=> Condition() && Source.Ended;

            if (AfterTransition != null)
                this.AfterTransition = AfterTransition ;
            else
                this.AfterTransition = () => { };
        }
    }

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
