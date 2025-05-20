using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectMM.Scope.Gameplay.Item
{
    [UnityEngine.Scripting.Preserve]
    public class ItemSlotsController
    {
        private Transform[] _slots;
        private ItemPrototype[] _items;
        private (int offset, int count) _lastMatchedItems;

        public bool IsSlotsFull => _items != null && _items[_slots.Length - 1]; 

        public void SetSlotTransforms(Transform[] slots)
        {
            _slots = slots;
            _items = new ItemPrototype[_slots.Length];
        }

        public (Vector3, IReadOnlyList<ItemPrototype>) GetAvailableSlot(ItemPrototype itemPrototype)
        {
            var counter = 0;
            _lastMatchedItems = (0, 0);

            for (var i = 0; i < _items.Length; i++)
            {
                if (_items[i])
                {
                    if (_items[i].Type == itemPrototype.Type)
                    {
                        if (counter == 0)
                        {
                            _lastMatchedItems.offset = i;
                        }
                        counter++;
                        continue;
                    }

                    if (counter == 0)
                    {
                        continue;
                    }
                }
                if (counter > 0)
                {
                    for (var j = _items.Length - 1; j > i; j--)
                    {
                        _items[j] = _items[j - 1];
                        if (_items[j])
                        {
                            _items[j].ShiftNextSlot(_slots[j].position);
                        }
                    }
                }

                _items[i] = itemPrototype;
                _lastMatchedItems.count = counter + 1;
                return (_slots[i].position, new ArraySegment<ItemPrototype>(_items, _lastMatchedItems.offset, _lastMatchedItems.count));
            }

            return (Vector3.zero, null);
        }

        public void ReleaseLastMatchedSlots()
        {
            for (var i = _lastMatchedItems.offset; i < _lastMatchedItems.offset + _lastMatchedItems.count; i++)
            {
                _items[i] = null;
            }

            for (var i = _lastMatchedItems.offset + _lastMatchedItems.count; i < _items.Length; i++)
            {
                if (!_items[i])
                {
                    continue;
                }

                var newIndex = i - _lastMatchedItems.count;
                _items[newIndex] = _items[i];
                _items[i].ShiftPreviousSlot(_slots[newIndex].position, _lastMatchedItems.count);
                _items[i] = null;
            }
        }
    }
}