namespace PaperWork
{
    public class Property<T>
    {
        private T Value;

        public T Get()
        {
            return Value;
        }

        public bool HasValue()
        {
            return Value != null;
        }

        public bool IsEmpty()
        {
            return Value == null;
        }

        public void Set(T value)
        {
            Value = value;
        }
    }
}