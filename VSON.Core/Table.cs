using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VSON.Core
{
    [Serializable]
    public class Table<TKey, TType, TValue> : ITable<TKey, TType, TValue>
    {
        #region Constructor
        public Table()
        {
            this.ValueDictionary = new Dictionary<TKey, TValue>();
            this.TypeDictionary = new Dictionary<TKey, TType>();
        }
        #endregion Constructor

        #region Properties
        public string Discriminator { get; set; }

        [JsonProperty] internal Dictionary<TKey, TValue> ValueDictionary { get; set; }

        [JsonProperty] internal Dictionary<TKey, TType> TypeDictionary { get; set; }

        public TValue this[TKey key]
        {
            get
            {
                return this.ValueDictionary[key];
            }
            set
            {
                if (this.ContainsKey(key))
                {
                    this.ValueDictionary[key] = value;
                }
                else
                {
                    this.ValueDictionary.Add(key, value);
                }
            }
        }

        [JsonIgnore] public ICollection<TKey> Keys => this.ValueDictionary.Keys;

        [JsonIgnore] public ICollection<TValue> Values => this.ValueDictionary.Values;

        [JsonIgnore] public ICollection<TType> Types => this.TypeDictionary.Values;

        [JsonIgnore] public int Count => this.ValueDictionary.Count;
        #endregion Properties

        #region Methods
        [Obsolete("Use other Add methods.")]
        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException("Use other Add methods.");
        }

        public void Add(TKey key, TType type, TValue val)
        {
            this.TypeDictionary.Add(key, type);
            this.ValueDictionary.Add(key, val);
        }

        public bool ContainsKey(TKey key) => this.ValueDictionary.ContainsKey(key) && this.TypeDictionary.ContainsKey(key);

        public bool Remove(TKey key) => this.ValueDictionary.Remove(key) && this.TypeDictionary.Remove(key);

        public TValue GetValue(TKey key) => this.ValueDictionary[key];

        public TType GetType(TKey key) => this.TypeDictionary[key];

        public bool TryGetValue(TKey key, out TValue value) => this.ValueDictionary.TryGetValue(key, out value);

        public bool TryGetType(TKey key, out TType type) => this.TypeDictionary.TryGetValue(key, out type);

        public void Clear()
        {
            this.ValueDictionary.Clear();
            this.TypeDictionary.Clear();
        }
        #endregion Methods
    }

    public class TableTest
    {
        public void Main()
        {
            Table<string, Type, string> attr = new Table<string, Type, string>();

            attr.Add("Pivot", typeof(System.Drawing.PointF), "100, 100");
            attr.Add("Bounds", typeof(System.Drawing.RectangleF), "0, 0, 100, 200");
            
        }
    }
}
