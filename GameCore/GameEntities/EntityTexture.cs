using GameCore.Collision;

namespace GameCore
{
    public class EntityTexture
    {
        public EntityTexture(string Name, int Width, int Height)
        {
            this.Name = Name;
            this.Width = Width;
            this.Height = Height;
        }

        public Coordinate2D Offset { get; set; }
        public int Width { get; }
        public int Height { get; }
        public string Name { get; }
        public bool Disabled { get; set; }
    }
}