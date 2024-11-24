using JetBrains.Annotations;
using UnityEngine;

namespace Bears.Core
{
    /// <summary>
    /// A MonoBehaviour-based game object pool
    /// </summary>
    /// <typeparam name="T">A MonoBehaviour subclass that implements IPoolable</typeparam>
    [PublicAPI]
    public abstract class GameObjectPool<T> : MonoBehaviour, IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        [SerializeField] private T _template; 
        [SerializeField] private int _initialSize = 8;

        private PoolBase<T> _pool;

        protected virtual void Awake()
        {
            EnsurePool();
        }

        private void EnsurePool()
        {
            if (_pool != null)
            {
                return;
            }

            _pool = new PoolBase<T>(_initialSize, () =>
            {
                T instance = Instantiate(_template, transform);
                instance.gameObject.SetActive(false);
                return instance;
            });
        }
        
        public T Draw()
        {
            EnsurePool();
            
            T item = _pool.Draw();
            item.transform.SetParent(null);
            item.gameObject.SetActive(true);
            return item;
        }
        
        public T Draw(Transform parent, bool worldTransformStays = true)
        {
            EnsurePool();
            
            T item = _pool.Draw();
            item.transform.SetParent(parent, worldTransformStays);
            item.gameObject.SetActive(true);
            return item;
        }
        
        public T Draw(Vector3 position, Quaternion rotation, Transform parent)
        {
            EnsurePool();
            
            T item = _pool.Draw();
            item.transform.SetParent(parent);
            item.transform.SetPositionAndRotation(position, rotation);
            item.gameObject.SetActive(true);
            return item;
        }

        public void Discard(T item)
        {
            EnsurePool();
            
            item.transform.SetParent(transform);
            item.gameObject.SetActive(false);
            _pool.Discard(item);
        }
    }
}