namespace GameCore
{
    public static class ObjectExtension
    {
        public static T As<T>(this object obj)
        {
            return (T)obj;
        }
    }
}
