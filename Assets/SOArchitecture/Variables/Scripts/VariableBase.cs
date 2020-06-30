using UnityEngine;
using UnityEngine.Serialization;

namespace SOArchitecture
{
    public abstract class VariableBase<TValue, TGameEvent> : ScriptableObject, IVariable<TValue>
        where TGameEvent : IGameEvent<TValue>
    {
        // delete in the next release '[FormerlySerializedAs("readOnly")]'
        [FormerlySerializedAs("readOnly")] [SerializeField] protected bool isReadOnly; 
        [SerializeField] protected TGameEvent gameEvent;
        [SerializeField] protected TValue value;

        public bool IsReadOnly
        {
            get => isReadOnly;
            protected set => isReadOnly = value;
        }

        public TValue Value
        {
            get => value;
            set
            {
                if (IsReadOnly)
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
