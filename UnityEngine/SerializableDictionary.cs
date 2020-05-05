using System;
using System.Collections.Generic;

namespace UnityEngine
{
    [Serializable]
    public abstract class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private TKey[] keys = new TKey[0];

        [SerializeField]
        private TValue[] values = new TValue[0];

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Clear();

            for (var i = 0; i < this.keys.Length; i++)
            {
                var value = (i >= this.values.Length) ? default : this.values[i];
                Add(this.keys[i], value);
            }

            this.keys = new TKey[0];
            this.values = new TValue[0];
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (this.Count <= 0)
                return;

            this.keys = new TKey[this.Count];
            this.values = new TValue[this.Count];
            var i = 0;

            foreach (var kv in this)
            {
                this.keys[i] = kv.Key;
                this.values[i] = kv.Value;
                i += 1;
            }
        }
    }
}
