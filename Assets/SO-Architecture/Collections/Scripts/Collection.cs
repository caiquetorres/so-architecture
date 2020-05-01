using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SOArchitecture
{
    public abstract class Collection<TValue> : ScriptableObject, IList<TValue>
    {
        [SerializeField] protected List<TValue> items;
        
        public TValue this[int index]
        {
            get => items[index];
            set => items[index] = value;
        }
        public int Count => items.Count;
        
        public bool IsReadOnly { get; }

        public void Add(TValue item) => items.Add(item);

        public void Clear() => items.Clear();

        public bool Contains(TValue item) => items.Contains(item);

        public void CopyTo(TValue[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

        public bool Remove(TValue item) => items.Remove(item);

        public int IndexOf(TValue item) => items.IndexOf(item);
        
        public void Insert(int index, TValue item) => items.Insert(index, item);
        
        public void RemoveAt(int index) => items.RemoveAt(index);

        public IEnumerator<TValue> GetEnumerator() => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
    }
}
