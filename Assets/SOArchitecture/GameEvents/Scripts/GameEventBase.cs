using UnityEngine;
using System.Collections.Generic;

namespace SOArchitecture
{
    public abstract class GameEventBase : ScriptableObject, IGameEvent
    {
        private readonly List<IGameEventListener> _listeners = new List<IGameEventListener>();
        
        public void Raise()
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
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
#if UNITY_EDITOR
        public T simulateValue;
#endif
        private readonly List<IGameEventListener<T>> _listeners = new List<IGameEventListener<T>>();
        
        public virtual void Raise(T value)
        {
            this.value = value;
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(value);
            }
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
