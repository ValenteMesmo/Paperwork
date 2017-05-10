namespace PaperWork
{
    public class Property<T>
    {
        private T Value;

        public T Get()
        {
            return Value;
        }

        public void Set(T value)
        {
            Value = value;
        }
    }
}