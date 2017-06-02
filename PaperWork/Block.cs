using GameCore;

namespace PaperWork
{
    public class Block : ICollider
    {
        public Block()
        {
            Width = 50;
            Height = 50;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float HorizontalSpeed { get; set; }
        public float VerticalSpeed { get; set; }
    }
}
