namespace GameCore
{
    public interface ICollider
    {
        float X { get; set; }
        float Y { get; set; }
        float Width { get; set; }
        float Height { get; set; }
        float HorizontalSpeed { get; set; }
        float VerticalSpeed { get; set; }
    }
}
