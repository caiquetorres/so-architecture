using UnityEngine;

namespace SOArchitecture
{
    public abstract class VariableBase<TValue, TGameEvent> : ScriptableObject, IVariable<TValue>
        where TGameEvent : IGameEvent<TValue>
    {
        [SerializeField] protected bool readOnly;
        [SerializeField] protected TGameEvent gameEvent;
        [SerializeField] protected TValue value;

        public TValue Value
        {
            get => value;
            set
            {
                if (readOnly)
                {
                    Debug.LogError("You are trying to set a Read-Only variable: " + this);
                    return;
                }
                SetValue(value);
            }
        }

        public virtual void SetValue(TValue value)
        {
            this.value = value;
            if (gameEvent != null)
                gameEvent.Raise(value);
        }

        public static implicit operator TValue(VariableBase<TValue, TGameEvent> variable)
        {
            return variable.value;
        }
    }
}
