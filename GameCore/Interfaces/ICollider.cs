﻿namespace GameCore
{
    public interface ICollider
    {
        int X { get; set; }
        int Y { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        int HorizontalSpeed { get; set; }
        int VerticalSpeed { get; set; }
        bool Disabled { get; set; }

        int DrawableX { get; set; }
        int DrawableY { get; set; }
    }
}
