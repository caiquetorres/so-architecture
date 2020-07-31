namespace SOArchitecture
{
    public interface IVariable<T>
    {
        bool IsReadOnly { get; }
        T Value { get; set; }
        void SetValue(T value);
    }
}
