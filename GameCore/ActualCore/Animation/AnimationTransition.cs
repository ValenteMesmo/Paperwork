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
}
