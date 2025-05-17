using UnityEngine;
using UnityEngine.Pool;

namespace ProjectMM.Core.ObjectPool
{
    public class GameObjectPool<T> where T : MonoBehaviour
    {
        private readonly IObjectPool<T> _objectPool;
        private readonly Transform _parent;
        private readonly T _prefab;

        public GameObjectPool(T prefab, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;

            _objectPool = new ObjectPool<T>(
                createFunc: CreatePooledItem,
                actionOnGet: OnTakeFromPool,
                actionOnRelease: OnReturnedToPool,
                actionOnDestroy: OnDestroyPoolObject,
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 200);
        }

        public T Get()
        {
            return _objectPool.Get();
        }

        public void Release(T obj)
        {
            _objectPool.Release(obj);
        }

        public void Warmup(int count)
        {
            var temp = new T[count];
            for (var i = 0; i < count; i++)
            {
                temp[i] = _objectPool.Get();
            }

            for (var i = 0; i < count; i++)
            {
                _objectPool.Release(temp[i]);
            }
        }

        private T CreatePooledItem()
        {
            var comp = Object.Instantiate(_prefab, _parent);
            comp.gameObject.SetActive(false);
            return comp;
        }

        private static void OnTakeFromPool(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        private static void OnReturnedToPool(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private static void OnDestroyPoolObject(T obj)
        {
            Object.Destroy(obj.gameObject);;
        }
    }
}
