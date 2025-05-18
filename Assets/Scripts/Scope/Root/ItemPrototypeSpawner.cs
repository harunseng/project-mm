using ProjectMM.Core.ObjectPool;
using ProjectMM.Scope.Gameplay.Item;
using UnityEngine;

namespace ProjectMM.Scope.Root
{
    public class ItemPrototypeSpawner : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private ItemPrototype _Prototype;

        #endregion

        private GameObjectPool<ItemPrototype> _itemPrototypePool;

        private void Start()
        {
            _itemPrototypePool = new GameObjectPool<ItemPrototype>(_Prototype, transform);
            _itemPrototypePool.Warmup(10);
        }

        public ItemPrototype GetItemPrototype()
        {
            return _itemPrototypePool.Get();
        }

        public void ReleaseItemPrototype(ItemPrototype itemPrototype)
        {
            _itemPrototypePool.Release(itemPrototype);
        }

        public void ReleaseAll()
        {
            foreach (var itemPrototype in _itemPrototypePool.Items)
            {
                if (itemPrototype.gameObject.activeInHierarchy)
                {
                    ReleaseItemPrototype(itemPrototype);
                }
            }
        }
    }
}