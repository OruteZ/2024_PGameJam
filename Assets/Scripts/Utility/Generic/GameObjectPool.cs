using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Utility.Generic
{
    public class GameObjectPool<T> : IObjectPool<T> where T : Component
    {
        private readonly Queue<T> _pool;
        private readonly GameObject _prefab;

        public GameObjectPool(GameObject prefab = null, int cnt = 1)
        {
            _pool = new Queue<T>(cnt);

            _prefab = prefab;
            if (_prefab == null)
            {
                _prefab = new GameObject();
                _prefab.AddComponent<T>();
                _prefab.SetActive(false);
            }
            
            CreatePool(_prefab, cnt);
        }

        public int CountInactive => _pool.Count;

        public T Get()
        {
            if (_pool.Count == 0)
            {
                CreatePool(_prefab,1);
            }

            var element = _pool.Dequeue();
            element.gameObject.SetActive(true);
            return element;
        }

        /// <summary>
        /// 미구현
        /// </summary>
        public PooledObject<T> Get(out T v)
        {
            throw new System.NotImplementedException();
        }

        public void Release(T element)
        {
            element.gameObject.SetActive(false);
            _pool.Enqueue(element);
        }

        public void Clear()
        {
            _pool.Clear();
        }
        
        private void CreatePool(GameObject prefab, int cnt)
        {
            for (var i = 0; i < cnt; i++)
            {
                var instance = Object.Instantiate(prefab);
                instance.SetActive(false);
                
                if(instance.TryGetComponent(out T component)) _pool.Enqueue(component);
            }
        }
    }
}