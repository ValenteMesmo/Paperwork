namespace GameCore
{
    public interface Touchable : DimensionalThing
    {
        void TouchEnded();
        void TouchBegin();
        void TouchContinue();
    }
}
