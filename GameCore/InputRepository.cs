namespace GameCore
{
    public class InputRepository
    {
        public readonly Property<bool> Left  = new Property<bool>();
        public readonly Property<bool> Jump  = new Property<bool>();
        public readonly Property<bool> Right = new Property<bool>();
        public readonly Property<bool> Crouch= new Property<bool>();
        public readonly Property<bool> Grab  = new Property<bool>();
    }
}
