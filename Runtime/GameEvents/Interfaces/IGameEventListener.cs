namespace SOArchitecture.Runtime.GameEvents.Interfaces
{
    public interface IGameEventListener
    {
        void OnEnable();
        void OnDisable();
        void OnEventRaised();
    }

    public interface IGameEventListener<T>
    {
        void OnEnable();
        void OnDisable();
        void OnEventRaised(T value);
    }
}
