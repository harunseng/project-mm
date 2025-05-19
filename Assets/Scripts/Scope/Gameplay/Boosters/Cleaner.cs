using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Scope.Gameplay.Item;
using ProjectMM.Scope.Gameplay.Level;
using ProjectMM.Scope.Root;

namespace ProjectMM.Scope.Gameplay.Boosters
{
    public class Cleaner : IBooster
    {
        private const int ItemCount = 3;

        private readonly BoardTracker _boardTracker;
        private readonly ItemPrototypeSpawner _spawner;

        public string Name { get; }

        public Cleaner(BoardTracker boardTracker, ItemPrototypeSpawner spawner)
        {
            Name = "Cleaner";

            _boardTracker = boardTracker;
            _spawner = spawner;
        }

        public UniTask Execute(CancellationToken token)
        {
            foreach (var type in _boardTracker.Orders.Keys.ToList())
            {
                var filteredItems = _boardTracker.Items.Where(item => item.gameObject.activeInHierarchy && item.Type == type && item.IsColliderEnabled).ToList();
                var items = filteredItems.Count >= 3 ? filteredItems.Take(ItemCount) : new List<ItemPrototype>();

                var count = 0;
                foreach (var item in items)
                {
                    _spawner.ReleaseItemPrototype(item);
                    count++;
                }

                if (count > 0)
                {
                    _boardTracker.RemoveItemCount(type, count);
                }
            }
            
            return UniTask.CompletedTask;
        }

        public void Complete()
        {
        }
    }
}
