namespace SOArchitecture
{
    public interface IVariable<T>
    {
        T Value { get; set; }
        void SetValue(T value);
    }
}
