namespace ToKO
{
    public class KOValue<T>
    {
        public T Value { get; set; }
        public bool Observable { get; set; }

        public KOValue(T value, bool observable)
        {
            Value = value;
            Observable = observable;
        }
    }
}