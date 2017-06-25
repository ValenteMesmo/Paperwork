using System.Collections.Generic;
using GameCore;
using Microsoft.Xna.Framework;

namespace PaperWork
{
    public enum Direction
    {
        Center,
        Left,
        Right,
        Top,
        Bot
    }

    public class InvisibleBlock : IPlayerMovementBlocker
    {
        public bool Disabled { get; set; }
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        public InvisibleBlock()
        {
            Width = Height = 1000;
        }
    }

    public class Block :
        Collider
        , Animation
        , IPlayerMovementBlocker
        , IPaperMovementBlocker
    {
        const int SIZE = 1000;
        private Animation Animation;

        public static SimpleAnimation CreateAnimation(Direction Direction)
        {
            var bonus = 20;
            var tx = -World.SPACE_BETWEEN_THINGS * bonus;
            var ty = -World.SPACE_BETWEEN_THINGS * bonus;
            var tz = 0.9f;
            var tw = SIZE + World.SPACE_BETWEEN_THINGS * bonus;
            var th = SIZE + World.SPACE_BETWEEN_THINGS * bonus;

            SimpleAnimation Animation;

            if (Direction == Direction.Center)
                Animation = GeneratedContent.Create_background_wall_center(tx, ty, tz, tw, th);
            else if (Direction == Direction.Left)
                Animation = GeneratedContent.Create_background_wall_left(tx, ty, tz, tw, th);
            else if (Direction == Direction.Right)
                Animation = GeneratedContent.Create_background_wall_right(tx, ty, tz, tw, th);
            else if (Direction == Direction.Top)
                Animation = GeneratedContent.Create_background_wall_top(tx, ty, tz, tw, th);
            else
                Animation = GeneratedContent.Create_background_wall_bot(tx, ty, tz, tw, th);

            Animation.ChangeColor(new Color(158, 165, 178));

            return Animation;
        }

        public Block(Direction Direction)
        {
            Width = SIZE;
            Height = SIZE;

            Animation = CreateAnimation(Direction);
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }
        public bool Disabled { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        //TODO: remove
        public bool Ended => false;

        public IEnumerable<Texture> GetTextures()
        {
            return Animation.GetTextures();
        }

        //todo:  remove update from animation?
        public void Update()
        {
        }
    }
}