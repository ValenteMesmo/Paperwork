using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class Animator
    {
        private readonly AnimationTransition[] Transitions;
        private Animation CurrentAnimation;

        public Animator(params AnimationTransition[] Transitions)
        {
            this.Transitions = Transitions;
            CurrentAnimation = Transitions[0].Sources[0];
        }

        public void Update()
        {
            foreach (var item in Transitions)
            {
                if (item.Sources.Contains( CurrentAnimation))
                {
                    if (item.Condition())
                    {
                        if (CurrentAnimation is SimpleAnimation)
                            CurrentAnimation.As<SimpleAnimation>().Restart();

                        CurrentAnimation = item.Target;

                        item.AfterTransition();
                    }
                }
            }
            CurrentAnimation.Update();
        }

        public IEnumerable<Texture> GetTextures()
        {
            return CurrentAnimation.GetTextures();
        }
    }
}
