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

        public bool log;
        
        public bool IsReadOnly
        {
            get => isReadOnly;
            protected set => isReadOnly = value;
        }
#endif

        public TValue Value
        {
            get => value;
            set
            {
#if UNITY_EDITOR
                if (IsReadOnly)
                {
                    if (log)
                        Debug.LogError("You are trying to set a Read-Only variable: " + this);
                    
                    return;
                }
#endif
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
