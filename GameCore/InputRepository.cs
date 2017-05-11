namespace GameCore
{
    public class InputRepository
    {
        public Property<bool> Left  = new Property<bool>();
        public Property<bool> Jump  = new Property<bool>();
        public Property<bool> Right = new Property<bool>();
        public Property<bool> Crouch= new Property<bool>();
        public Property<bool> Grab  = new Property<bool>();
    }
}
