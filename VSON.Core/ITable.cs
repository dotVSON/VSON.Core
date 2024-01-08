using System.Collections.Generic;

namespace VSON.Core
{
    public interface ITable<TKey, TType, TValue>
    {
        #region Properties
        TValue this[TKey key] { get; set; }

        ICollection<TKey> Keys { get; }
        
        ICollection<TType> Types { get; }
        
        ICollection<TValue> Values { get; }

        #endregion Properties

        #region Methods
        void Add(TKey key, TValue value);

        void Add(TKey key, TType type, TValue value);

        bool ContainsKey(TKey key);

        bool Remove(TKey key);

        bool TryGetValue(TKey key, out TValue value);

        bool TryGetType(TKey key, out TType type);

        #endregion Methods
    }
}
