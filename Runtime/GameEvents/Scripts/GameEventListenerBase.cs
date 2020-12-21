using SOArchitecture.Runtime.GameEvents.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace SOArchitecture.Runtime.GameEvents.Scripts
{
    public abstract class GameEventListenerBase<TGameEvent, TUnityEvent> : MonoBehaviour, IGameEventListener
        where TGameEvent : IGameEvent
        where TUnityEvent : UnityEvent
    {
#pragma warning disable 649
        [SerializeField] private TGameEvent gameEvent;
        [SerializeField] private TUnityEvent response;
#pragma warning disable 649

        public void OnEnable()
        {
            gameEvent.Register(this);
        }

        public void OnDisable()
        {
            gameEvent.Unregister(this);
        }

        public void OnEventRaised()
        {
            response?.Invoke();
        }
    }

    public abstract class GameEventListenerBase<TValue, TGameEvent, TUnityEvent> : MonoBehaviour, IGameEventListener<TValue>
        where TGameEvent : IGameEvent<TValue>
        where TUnityEvent : UnityEvent<TValue>
    {
        [SerializeField] private TGameEvent gameEvent;
        [SerializeField] private TUnityEvent response;
        public void OnEnable()
        {
            gameEvent.Register(this);
        }

        public void OnDisable()
        {
            gameEvent.Unregister(this);
        }

        public void OnEventRaised(TValue value)
        {
            response?.Invoke(value);
        }
    }
}
