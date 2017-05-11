namespace GameCore
{
    public class Property<T>
    {
        private T Value;

        public T Get()
        {
            return Value;
        }

        public void RemoveValue()
        {
            Value = default(T);
        }

        public bool IsNotNull()
        {
            return Value != null;
        }

        public bool IsNull()
        {
            return Value == null;
        }

        public void Set(T value)
        {
            Value = value;
        }
    }
}