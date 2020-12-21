namespace SOArchitecture.Runtime.GameEvents.Interfaces
{
    public interface IGameEvent
    {
        void Raise();
        void Register(IGameEventListener listener);
        void Unregister(IGameEventListener listener);
    }

    public interface IGameEvent<T>
    {
        void Raise(T value);
        void Register(IGameEventListener<T> listener);
        void Unregister(IGameEventListener<T> listener);
    }
}
