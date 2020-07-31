namespace SOArchitecture
{
    public interface IVariable<T>
    {
#if UNITY_EDITOR
        bool IsReadOnly { get; }
#endif
        T Value { get; set; }
        void SetValue(T value);
    }
}
