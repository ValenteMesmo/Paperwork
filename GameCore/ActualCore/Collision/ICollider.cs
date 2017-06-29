namespace GameCore
{
    public interface Something
    {        
    }

    public interface DimensionalThing : Something
    {
        int X { get; set; }
        int Y { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        int DrawableX { get; set; }
        int DrawableY { get; set; }
    }

    public interface Collider : DimensionalThing
    {
        bool Disabled { get; set; }

        int HorizontalSpeed { get; set; }
        int VerticalSpeed { get; set; }
    }
}
