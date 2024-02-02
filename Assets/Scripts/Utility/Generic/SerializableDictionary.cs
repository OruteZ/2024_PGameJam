using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utility.Generic
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private TKey defaultKey;
        [SerializeField] private TValue defaultValue;
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();
        
        
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            
            foreach (var pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();

            //add new key-value pair in inspector
            // smaller one will add new default 
            if (keys.Count - values.Count == 1)
            {
                values.Add(defaultValue);
            }
            else if (values.Count - keys.Count == 1)
            {
                keys.Add(defaultKey);
            }
            
            if (keys.Count != values.Count)
            {
                throw new System.Exception($"There are {keys.Count} keys and {values.Count} values after deserialization. " +
                                           $"Make sure that both key and value types are serializable.");
            }
            
            for (var i = 0; i < keys.Count; i++)
            {
                Add(keys[i], values[i]);
            }
        }
    }
}