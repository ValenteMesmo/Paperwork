using GameCore;
using System.Collections.Generic;

namespace PaperWork
{
    public class Detector<T> :
        ICollisionHandler
        , IUpdateHandler
        , Collider
        , IAfterUpdateHandler where T : Collider
    {
        private readonly HashSet<T> Colliders;
        private readonly int OffSetY;
        private readonly int OffSetX;

        public IEnumerable<T> GetDetectedItems() { return Colliders; }
        public Collider Parent { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }
        public bool Disabled { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        public void Update()
        {
            if (Parent != null)
            {
                X = Parent.X + OffSetX;
                Y = Parent.Y + OffSetY;
            }
        }

        public Detector(int OffSetX, int OffSetY, int Width, int Height)
        {
            Colliders = new HashSet<T>();
            this.Width = Width;
            this.Height = Height;
            X = this.OffSetX = OffSetX;
            Y = this.OffSetY = OffSetY;
        }

        public void AfterUpdate()
        {
            Colliders.Clear();
        }

        public void BotCollision(Collider other)
        {
            if (other is T)
            {
                Colliders.Add(other.As<T>());
            }
        }

        public void LeftCollision(Collider other)
        {
            if (other is T)
            {
                Colliders.Add(other.As<T>());
            }
        }

        public void RightCollision(Collider other)
        {
            if (other is T)
            {
                Colliders.Add(other.As<T>());
            }
        }

        public void TopCollision(Collider other)
        {
            if (other is T)
            {
                Colliders.Add(other.As<T>());
            }
        }
    }
}