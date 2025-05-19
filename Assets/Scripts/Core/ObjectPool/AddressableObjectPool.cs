using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;

namespace ProjectMM.Core.ObjectPool
{
    public class AddressableObjectPool<T> where T : MonoBehaviour
    {
        private readonly string _address;
        private readonly IObjectPool<T> _objectPool;
        private readonly Transform _parent;

        public AddressableObjectPool(string address, Transform parent = null)
        {
            _address = address;
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

        public async UniTask<T> Get(CancellationToken token)
        {
            try
            {
                var handle = Addressables.InstantiateAsync(_address, _parent);
                var instance = await handle.ToUniTask(cancellationToken: token);

                var component = instance.GetComponent<T>();
                component.gameObject.SetActive(false);
                _objectPool.Release(component);
                return component;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Release(T obj)
        {
            _objectPool.Release(obj);
        }

        private static T CreatePooledItem()
        {
            throw new InvalidOperationException("Use Get() instead Create preload from Addressable");
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
            Addressables.ReleaseInstance(obj.gameObject);
        }
    }
}