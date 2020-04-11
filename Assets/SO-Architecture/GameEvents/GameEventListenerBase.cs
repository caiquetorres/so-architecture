using UnityEngine;
using UnityEngine.Events;

namespace SOArchitecture
{
    public class GameEventListenerBase<TValue, TGameEvent, TUnityEvent> : MonoBehaviour, IGameEventListener<TValue>
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
            response.Invoke(value);
        }
    }
}
