using System;
using System.Collections.Generic;
using ProjectMM.Scope.Gameplay.Item;

namespace ProjectMM.Scope.Gameplay.Level
{
    [UnityEngine.Scripting.Preserve]
    public class BoardTracker
    {
        private readonly Dictionary<PrototypeData.ItemType, int> _items = new();

        public event Action<PrototypeData.ItemType, int> OrderCountChanged;
        public event Action OrdersCompleted;

        public Dictionary<PrototypeData.ItemType, int> Orders { get; } = new();

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

            Orders[type] -= count;
            OrderCountChanged?.Invoke(type, Orders[type]);

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