using UnityEngine;
using System.Collections.Generic;

namespace SOArchitecture
{
    public abstract class GameEventBase : ScriptableObject, IGameEvent
    {
        private readonly List<IGameEventListener> _listeners = new List<IGameEventListener>();
        
        public void Raise()
        {
            _listeners.ForEach(listener =>
            {
                listener.OnEventRaised();
            });
        }

        public void Register(IGameEventListener listener)
        {
            _listeners.Add(listener);
        }

        public void Unregister(IGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
    
    public abstract class GameEventBase<T> : ScriptableObject, IGameEvent<T>
    {
        public T value;
        private readonly List<IGameEventListener<T>> _listeners = new List<IGameEventListener<T>>();
        
        public void Raise(T value)
        {
            this.value = value;
            _listeners.ForEach(listener =>
            {
                listener.OnEventRaised(value);
            });
        }

        public void Register(IGameEventListener<T> listener)
        {
            _listeners.Add(listener);
        }

        public void Unregister(IGameEventListener<T> listener)
        {
            _listeners.Remove(listener);
        }
    }
}
