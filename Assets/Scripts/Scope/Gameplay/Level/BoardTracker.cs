using System;
using System.Collections.Generic;
using ProjectMM.Scope.Gameplay.Item;

namespace ProjectMM.Scope.Gameplay.Level
{
    [UnityEngine.Scripting.Preserve]
    public class BoardTracker
    {
        public event Action<PrototypeData.ItemType, int> OrderCountChanged;
        public event Action OrdersCompleted;

        private readonly Dictionary<PrototypeData.ItemType, int> _items = new();

        public Dictionary<PrototypeData.ItemType, int> Orders { get; } = new();

        public List<ItemPrototype> Items { get; } = new();

        public void AddItemCount(PrototypeData.ItemType type, int count)
        {
            _items.TryAdd(type, count);
        }

        public void AddOrder(PrototypeData.ItemType type, int count)
        {
            Orders.TryAdd(type, count);
        }

        public int GetCount(PrototypeData.ItemType type)
        {
            return _items.GetValueOrDefault(type, 0);
        }

        public void RemoveItemCount(PrototypeData.ItemType type, int count)
        {
            _items[type] -= count;

            if (!Orders.ContainsKey(type))
            {
                return;
            }

            var oldCount = Orders[type];
            var newCount = Math.Clamp(oldCount - count, 0, int.MaxValue);
            if (oldCount != newCount)
            {
                Orders[type] = newCount;
                OrderCountChanged?.Invoke(type, Orders[type]);
            }

            var remainingOrders = 0;
            foreach (var order in Orders)
            {
                remainingOrders += order.Value;
            }

            if (remainingOrders == 0)
            {
                OrdersCompleted?.Invoke();
            }
        }
    }
}