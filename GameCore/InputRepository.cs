namespace GameCore
{
    public class InputRepository
    {
        public readonly Property<bool> Left = new Property<bool>();
        public readonly Property<bool> Right = new Property<bool>();
        public readonly Property<bool> Up = new Property<bool>();
        public readonly Property<bool> Down = new Property<bool>();
        public readonly Property<bool> Action1 = new Property<bool>();
    }
}
