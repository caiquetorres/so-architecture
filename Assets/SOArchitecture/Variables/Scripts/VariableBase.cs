using UnityEngine;

namespace SOArchitecture
{
    public abstract class VariableBase<TValue, TGameEvent> : ScriptableObject, IVariable<TValue>
        where TGameEvent : IGameEvent<TValue>
    {
        [SerializeField] protected bool isReadOnly; 
        [SerializeField] protected TGameEvent gameEvent;
        [SerializeField] protected TValue value;
        
#if UNITY_EDITOR
        [TextArea(3, 10), SerializeField] private string description;
#endif

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

        public override string ToString() => value.ToString();
        
        public static implicit operator TValue(VariableBase<TValue, TGameEvent> variable) => variable.value;
    }
}
