namespace GameCore
{
    public class Property<T>
    {
        private T Value;

        public T Get()
        {
            return Value;
        }

        public void SetDefaut()
        {
            Value = default(T);
        }

        public bool HasValue()
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

        public bool True()
        {
            if (typeof(T) == typeof(bool))
                return (bool)(object)Value;
            return HasValue();
        }

        public bool False()
        {
            if (typeof(T) == typeof(bool))
                return !(bool)(object)Value;
            return IsNull();
        }
    }
}