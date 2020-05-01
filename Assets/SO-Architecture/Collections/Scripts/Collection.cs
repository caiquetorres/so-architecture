using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SOArchitecture
{
    public abstract class Collection<TValue> : ScriptableObject, IList<TValue>
    {
        [SerializeField] protected VoidGameEvent onChangedCollection;
        [SerializeField] protected List<TValue> items;
        
        public TValue this[int index]
        {
            get => items[index];
            set => items[index] = value;
        }
        public int Count => items.Count;
        
        public bool IsReadOnly { get; }

        public void Add(TValue item)
        {
            items.Add(item);
            onChangedCollection.Raise();
        }

        public void Clear()
        {
            items.Clear();
            onChangedCollection.Raise();
        }

        public bool Contains(TValue item) => items.Contains(item);

        public void CopyTo(TValue[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

        public bool Remove(TValue item)
        {
            var removed = items.Remove(item);
            onChangedCollection.Raise();
            return removed;
        }

        public int IndexOf(TValue item) => items.IndexOf(item);
        
        public void Insert(int index, TValue item)
        {
            items.Insert(index, item);
            onChangedCollection.Raise();
        }

        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
            onChangedCollection.Raise();
        }

        public IEnumerator<TValue> GetEnumerator() => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
    }
}
