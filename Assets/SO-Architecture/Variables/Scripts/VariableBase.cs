using UnityEngine;

namespace SOArchitecture
{
    public abstract class VariableBase<TValue, TGameEvent> : ScriptableObject, IVariable<TValue>
        where TGameEvent : IGameEvent<TValue>
    {
        [SerializeField] protected TGameEvent gameEvent;
        [SerializeField] protected TValue value;

        public TValue Value
        {
            get => value;
            set => SetValue(value);
        }

        public virtual void SetValue(TValue value)
        {
            this.value = value;
            if (gameEvent != null)
                gameEvent.Raise(value);
        }
    }
}
